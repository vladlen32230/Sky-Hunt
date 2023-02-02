using Sky_Hunt.Properties;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sky_Hunt
{
    public static class Effects
    {
        public static async void ShowExplosion(this GameForm gameForm, Point location)
        {
            var explosion = new PictureBox()
            {
                Image = Resources.Explosion,
                Size = new Size(gameForm.Width / 20, gameForm.Width * 3 / 50),
                BackColor = Color.Transparent,
                Location = new Point(location.X - gameForm.Width / 80, location.Y - gameForm.Height / 40),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            gameForm.Controls.Add(explosion);
            await Task.Delay(300);
            gameForm.Controls.Remove(explosion);
        }

        public static void Shoot(this GameForm gameForm)
        {
            gameForm.reload = true;
            var projectile = new Projectile(Control.MousePosition.X, gameForm);
            gameForm.projectiles.Add(projectile);
            gameForm.Controls.Add(projectile.PictureBox);
        }
    }
}