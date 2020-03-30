using RandomNumberGraph.Randoms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomNumberGraph
{
	public partial class MainWindow : Form
	{
		const int RANDOM_NUMBER_GENERATE_COUNT = 100000;
		const int RANDOM_NUMBER_RANGE = short.MaxValue;//1000;

		Tuple<Random, Color> [] randoms = new Tuple<Random, Color> []
		{
			new Tuple<Random, Color> (new Random (), Color.SpringGreen),
			new Tuple<Random, Color> (new RNGCryptoRandom (), Color.DarkRed),
			new Tuple<Random, Color> (new Well512Random (), Color.CornflowerBlue),
			new Tuple<Random, Color> (new CppNativeRandom (), Color.Purple),
			new Tuple<Random, Color> (new HardwareRandom (), Color.LightPink),
			new Tuple<Random, Color> (new MersenneTwitsterRandom (), Color.BlanchedAlmond),
			new Tuple<Random, Color> (new AdditiveLaggedFibonacciRandom (), Color.Aquamarine),
			new Tuple<Random, Color> (new NR3Random (), Color.LightSteelBlue),
		};
		Dictionary<Color, List<uint>> randomNumbers = new Dictionary<Color, List<uint>> ();
		Dictionary<Color, Brush> brushCache = new Dictionary<Color, Brush> ();
		Dictionary<Color, ListViewItem> lviCache = new Dictionary<Color, ListViewItem> ();

		Renderer renderer;

		public MainWindow ()
		{
			InitializeComponent ();
			ClientSize = new Size (840, 640);
			DrawModeComboBox.SelectedIndex = 1;

			renderer = new Renderer (GraphPictureBox.Width, GraphPictureBox.Height);

			foreach (var random in randoms)
			{
				brushCache.Add (random.Item2, new SolidBrush (random.Item2));

				var listViewItem = RandomListView.Items.Add (random.Item1.GetType ().Name);
				listViewItem.Checked = true;
				listViewItem.ForeColor = random.Item2;
				lviCache.Add (random.Item2, listViewItem);
			}
		}

		private async void MainWindow_Shown (object sender, EventArgs e)
		{
			foreach (var random in randoms)
			{
				LogLabel.Text = $"{random.Item1.GetType ().Name} 임의숫자 산출 중";
				await Task.Run (() =>
				{
					randomNumbers.Add (random.Item2, new List<uint> (RANDOM_NUMBER_GENERATE_COUNT));
					for (int i = 0; i < RANDOM_NUMBER_GENERATE_COUNT; ++i)
					{
						uint r = (uint) random.Item1.Next () % RANDOM_NUMBER_RANGE;
						randomNumbers [random.Item2].Add (r);

						if(i % 1000 == 0)
						{
							try { Invoke (new Action (() => GraphPictureBox.Refresh ())); }
							catch { }
						}
					}
				});
				LogLabel.Text = $"{random.Item1.GetType ().Name} 임의숫자 산출 종료";
				GraphPictureBox.Refresh ();
			}
			LogLabel.Text = $"지정된 모든 임의숫자 알고리즘 산출 종료";
		}

		private void GraphPictureBox_Paint (object sender, PaintEventArgs e)
		{
			if (renderer == null)
				return;

			renderer.Begin ();
			renderer.Clear ();

			int width = renderer.Width, height = renderer.Height;

			if (DrawModeComboBox.SelectedIndex == 0)
			{
				float xUnit = width / (float) RANDOM_NUMBER_GENERATE_COUNT,
					yUnit = height / (float) RANDOM_NUMBER_RANGE;
				for (int colorIndex = randoms.Length - 1; colorIndex >= 0; --colorIndex)
				{
					Color currentColor = randoms [colorIndex].Item2;
					if (!lviCache [currentColor].Checked)
						continue;
					if (!randomNumbers.ContainsKey (currentColor) || randomNumbers [currentColor].Count == 0)
						continue;

					var list = randomNumbers [currentColor];
					Parallel.For (0, list.Count, (i) =>
					{
						uint rn = list [i];
						int x = (int) Math.Floor (i * xUnit), y = (int) Math.Floor (rn * yUnit);
						renderer.DrawPoint (x, y, currentColor, colorIndex);
					});
				}
			}
			else if (DrawModeComboBox.SelectedIndex == 1)
			{
				float xUnit = width / (int) height;
				int [] count = new int [height];
				for (int colorIndex = randoms.Length - 1; colorIndex >= 0; --colorIndex)
				{
					Color currentColor = randoms [colorIndex].Item2;
					if (!lviCache [currentColor].Checked)
						continue;
					if (!randomNumbers.ContainsKey (currentColor) || randomNumbers [currentColor].Count == 0)
						continue;

					Array.Clear (count, 0, count.Length);
					var list = randomNumbers [currentColor];
					Parallel.ForEach (list, (i) => Interlocked.Increment (ref count [i % height]));
					Parallel.For (0, height, (y) => renderer.DrawHorizontalLine (0, y, count [y], currentColor, colorIndex));
				}
			}

			renderer.End ();
			renderer.Present (e.Graphics);
		}

		private void RandomListView_ItemChecked (object sender, ItemCheckedEventArgs e)
		{
			GraphPictureBox.Refresh ();
		}

		private void MainWindow_Resize (object sender, EventArgs e)
		{
			renderer?.ResetBuffer (GraphPictureBox.Width, GraphPictureBox.Height);
			GraphPictureBox?.Refresh ();
		}

		private void DetectSamePointCheckBox_CheckedChanged (object sender, EventArgs e)
		{
			renderer.DetectSamePoint = DetectSamePointCheckBox.Checked;
			GraphPictureBox.Refresh ();
		}

		private void DrawModeComboBox_SelectedValueChanged (object sender, EventArgs e)
		{
			GraphPictureBox.Refresh ();
		}
	}
}
