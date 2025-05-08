namespace WinFormsMovieDB
{
    partial class Form1
    {
  
        private System.ComponentModel.IContainer components = null;

       
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            textBoxSearch = new TextBox();
            buttonSearch = new Button();
            pictureBoxPoster = new PictureBox();
            searchTypeComboBox = new ComboBox();
            searchTypeLabel = new Label();
            buttonWhite = new Button();
            buttonBlack = new Button();
            buttonQuit = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPoster).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1108, 298);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(622, 359);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(111, 23);
            textBoxSearch.TabIndex = 1;
            // 
            // buttonSearch
            // 
            buttonSearch.BackColor = SystemColors.ActiveCaption;
            buttonSearch.Location = new Point(622, 420);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(111, 23);
            buttonSearch.TabIndex = 2;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = false;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // pictureBoxPoster
            // 
            pictureBoxPoster.Location = new Point(1114, -3);
            pictureBoxPoster.Name = "pictureBoxPoster";
            pictureBoxPoster.Size = new Size(218, 301);
            pictureBoxPoster.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPoster.TabIndex = 3;
            pictureBoxPoster.TabStop = false;
            // 
            // searchTypeComboBox
            // 
            searchTypeComboBox.FormattingEnabled = true;
            searchTypeComboBox.Items.AddRange(new object[] { "Title", "Actor" });
            searchTypeComboBox.Location = new Point(622, 508);
            searchTypeComboBox.Name = "searchTypeComboBox";
            searchTypeComboBox.Size = new Size(111, 23);
            searchTypeComboBox.TabIndex = 4;
            // 
            // searchTypeLabel
            // 
            searchTypeLabel.AutoSize = true;
            searchTypeLabel.Location = new Point(622, 479);
            searchTypeLabel.Name = "searchTypeLabel";
            searchTypeLabel.Size = new Size(69, 15);
            searchTypeLabel.TabIndex = 5;
            searchTypeLabel.Text = "Search for...";
            // 
            // buttonWhite
            // 
            buttonWhite.BackColor = SystemColors.ActiveCaption;
            buttonWhite.ForeColor = SystemColors.ControlText;
            buttonWhite.Location = new Point(12, 479);
            buttonWhite.Name = "buttonWhite";
            buttonWhite.Size = new Size(164, 23);
            buttonWhite.TabIndex = 6;
            buttonWhite.Text = "Light Theme";
            buttonWhite.UseVisualStyleBackColor = false;
            buttonWhite.Click += buttonWhite_Click;
            // 
            // buttonBlack
            // 
            buttonBlack.BackColor = SystemColors.ActiveCaption;
            buttonBlack.Location = new Point(12, 518);
            buttonBlack.Name = "buttonBlack";
            buttonBlack.Size = new Size(164, 23);
            buttonBlack.TabIndex = 7;
            buttonBlack.Text = "Dark Theme";
            buttonBlack.UseVisualStyleBackColor = false;
            buttonBlack.Click += buttonBlack_Click;
            // 
            // buttonQuit
            // 
            buttonQuit.BackColor = SystemColors.ActiveCaption;
            buttonQuit.Location = new Point(1150, 567);
            buttonQuit.Name = "buttonQuit";
            buttonQuit.Size = new Size(137, 53);
            buttonQuit.TabIndex = 8;
            buttonQuit.Text = "Quit";
            buttonQuit.UseVisualStyleBackColor = false;
            buttonQuit.Click += buttonQuit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1330, 647);
            Controls.Add(buttonQuit);
            Controls.Add(buttonBlack);
            Controls.Add(buttonWhite);
            Controls.Add(searchTypeLabel);
            Controls.Add(searchTypeComboBox);
            Controls.Add(pictureBoxPoster);
            Controls.Add(buttonSearch);
            Controls.Add(textBoxSearch);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPoster).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private TextBox textBoxSearch;
        private Button buttonSearch;
        private PictureBox pictureBoxPoster;
        private ComboBox searchTypeComboBox;
        private Label searchTypeLabel;
        private Button buttonWhite;
        private Button buttonBlack;
        private Button buttonQuit;
    }
}
