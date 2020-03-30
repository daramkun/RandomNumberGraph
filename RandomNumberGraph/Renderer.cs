using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumberGraph
{
	public class Renderer
	{
		ConcurrentQueue<Bitmap> renderTargets = new ConcurrentQueue<Bitmap> ();
		ConcurrentQueue<int [,]> depthBuffers = new ConcurrentQueue<int [,]> ();

		BitmapData bitmapData;
		int [,] depthBuffer;
		int width, height;

		public int Width => width;
		public int Height => height;

		public bool DetectSamePoint { get; set; } = true;

		public Renderer (int width, int height)
		{
			ResetBuffer (width, height);
		}

		public void ResetBuffer(int width, int height)
		{
			renderTargets.Enqueue (new Bitmap (width, height));
			depthBuffers.Enqueue (new int [width, height]);
		}

		public void Begin ()
		{
			if (bitmapData != null)
				throw new InvalidOperationException ();
			if (renderTargets.Count > 1)
			{
				if (renderTargets.TryDequeue (out Bitmap temp1))
					temp1.Dispose ();
				depthBuffers.TryDequeue (out int [,] temp2);
			}
			if (!renderTargets.TryPeek (out Bitmap renderTarget))
				return;
			if (!depthBuffers.TryPeek (out depthBuffer))
				return;
			bitmapData = renderTarget.LockBits (new Rectangle (0, 0, width = renderTarget.Width, height = renderTarget.Height),
				ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
		}

		public void End ()
		{
			if (bitmapData == null)
				throw new InvalidOperationException ();
			if (!renderTargets.TryPeek (out Bitmap renderTarget))
				return;

			renderTarget.UnlockBits (bitmapData);
			bitmapData = null;
		}

		public void Clear ()
		{
			if (bitmapData == null)
				throw new InvalidOperationException ();

			var blackArgb = Color.Black.ToArgb ();
			for (int y = 0; y < height; ++y)
			{
				Parallel.For (0, width, (x) =>
				{
					Marshal.WriteInt32 (bitmapData.Scan0 + ((y * width + x) * 4), blackArgb);
					depthBuffer [x, y] = -1;
				});
			}
		}

		public void DrawPoint (int x, int y, Color color, int depthValue)
		{
			if (bitmapData == null)
				throw new InvalidOperationException ();

			if (depthBuffer [x, y] > depthValue)
				return;

			int drawColor = color.ToArgb ();
			if (depthBuffer [x, y] == depthValue && DetectSamePoint)
				drawColor = Color.Red.ToArgb ();

			Marshal.WriteInt32 (bitmapData.Scan0 + ((y * width + x) * 4), drawColor);
			depthBuffer [x, y] = depthValue;
		}

		public void DrawHorizontalLine(int x, int y, int length, Color color, int depthValue)
		{
			int minLength = Math.Min (x + length, width);
			int offset = y * width * 4;
			int drawColor = color.ToArgb ();
			for (int i = x; i < minLength; ++i)
			{
				if (depthBuffer [x + i, y] > depthValue)
					continue;
				Marshal.WriteInt32 (bitmapData.Scan0 + offset + (i * 4), drawColor);
				depthBuffer [x + i, y] = depthValue;
			}
		}

		public void Present (Graphics graphics)
		{
			if (!renderTargets.TryPeek (out Bitmap renderTarget))
				return;
			graphics.DrawImageUnscaled (renderTarget, new Point ());
		}
	}
}
