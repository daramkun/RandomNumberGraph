namespace RandomNumberGraph
{
	partial class MainWindow
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent ()
		{
			this.LogLabel = new System.Windows.Forms.Label();
			this.GraphPictureBox = new System.Windows.Forms.PictureBox();
			this.RandomListView = new System.Windows.Forms.ListView();
			this.RandomColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.DetectSamePointCheckBox = new System.Windows.Forms.CheckBox();
			this.DrawModeComboBox = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.GraphPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// LogLabel
			// 
			this.LogLabel.AutoSize = true;
			this.LogLabel.Location = new System.Drawing.Point(12, 9);
			this.LogLabel.Name = "LogLabel";
			this.LogLabel.Size = new System.Drawing.Size(45, 12);
			this.LogLabel.TabIndex = 0;
			this.LogLabel.Text = "준비 중";
			// 
			// GraphPictureBox
			// 
			this.GraphPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GraphPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.GraphPictureBox.Location = new System.Drawing.Point(14, 33);
			this.GraphPictureBox.Name = "GraphPictureBox";
			this.GraphPictureBox.Size = new System.Drawing.Size(603, 576);
			this.GraphPictureBox.TabIndex = 1;
			this.GraphPictureBox.TabStop = false;
			this.GraphPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphPictureBox_Paint);
			// 
			// RandomListView
			// 
			this.RandomListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RandomListView.CheckBoxes = true;
			this.RandomListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RandomColumnHeader});
			this.RandomListView.HideSelection = false;
			this.RandomListView.Location = new System.Drawing.Point(623, 33);
			this.RandomListView.Name = "RandomListView";
			this.RandomListView.Size = new System.Drawing.Size(200, 576);
			this.RandomListView.TabIndex = 2;
			this.RandomListView.UseCompatibleStateImageBehavior = false;
			this.RandomListView.View = System.Windows.Forms.View.Details;
			this.RandomListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.RandomListView_ItemChecked);
			// 
			// RandomColumnHeader
			// 
			this.RandomColumnHeader.Text = "난수 생성기";
			this.RandomColumnHeader.Width = 175;
			// 
			// DetectSamePointCheckBox
			// 
			this.DetectSamePointCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DetectSamePointCheckBox.AutoSize = true;
			this.DetectSamePointCheckBox.Checked = true;
			this.DetectSamePointCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.DetectSamePointCheckBox.Location = new System.Drawing.Point(580, 8);
			this.DetectSamePointCheckBox.Name = "DetectSamePointCheckBox";
			this.DetectSamePointCheckBox.Size = new System.Drawing.Size(240, 16);
			this.DetectSamePointCheckBox.TabIndex = 3;
			this.DetectSamePointCheckBox.Text = "같은 지점에 점을 그리는 경우 붉게 표시";
			this.DetectSamePointCheckBox.UseVisualStyleBackColor = true;
			this.DetectSamePointCheckBox.CheckedChanged += new System.EventHandler(this.DetectSamePointCheckBox_CheckedChanged);
			// 
			// DrawModeComboBox
			// 
			this.DrawModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DrawModeComboBox.FormattingEnabled = true;
			this.DrawModeComboBox.Items.AddRange(new object[] {
            "회차당값분포",
            "값빈도분포"});
			this.DrawModeComboBox.Location = new System.Drawing.Point(437, 6);
			this.DrawModeComboBox.Name = "DrawModeComboBox";
			this.DrawModeComboBox.Size = new System.Drawing.Size(137, 20);
			this.DrawModeComboBox.TabIndex = 4;
			this.DrawModeComboBox.SelectedValueChanged += new System.EventHandler(this.DrawModeComboBox_SelectedValueChanged);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(835, 621);
			this.Controls.Add(this.DrawModeComboBox);
			this.Controls.Add(this.DetectSamePointCheckBox);
			this.Controls.Add(this.RandomListView);
			this.Controls.Add(this.GraphPictureBox);
			this.Controls.Add(this.LogLabel);
			this.DoubleBuffered = true;
			this.Name = "MainWindow";
			this.Text = "임의숫자 그래프";
			this.Shown += new System.EventHandler(this.MainWindow_Shown);
			this.Resize += new System.EventHandler(this.MainWindow_Resize);
			((System.ComponentModel.ISupportInitialize)(this.GraphPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LogLabel;
		private System.Windows.Forms.PictureBox GraphPictureBox;
		private System.Windows.Forms.ListView RandomListView;
		private System.Windows.Forms.ColumnHeader RandomColumnHeader;
		private System.Windows.Forms.CheckBox DetectSamePointCheckBox;
		private System.Windows.Forms.ComboBox DrawModeComboBox;
	}
}

