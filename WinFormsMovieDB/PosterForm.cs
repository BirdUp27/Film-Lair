using System.Drawing;
using System.Windows.Forms;

namespace WinFormsMovieDB
{
    public partial class PosterForm : Form
    {
        public PosterForm(Image posterImage)
        {
            InitializeComponent();
            this.Text = "Poster Viewer";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(600, 900);

            var pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = posterImage;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(pictureBox);
        }
        private void PosterForm_Load(object sender, EventArgs e)
        {
          
        }

    }
}
