using Sky_Hunt.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Sky_Hunt
{
    public class Projectile
    {
        public PictureBox PictureBox { get; }
        public Projectile(int x, GameForm gameForm)
        {
            PictureBox = new PictureBox()
            {
                Name = "projectile",
                Image = Resources.Bullet,
                Size = new Size(gameForm.Width / 150, gameForm.Height / 40),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(x, gameForm.ClientSize.Height * 159 / 160)
            };
        }

        public void Move(GameForm gameForm)
        {
            PictureBox.Top -= gameForm.Height / 50;
        }
    }
}