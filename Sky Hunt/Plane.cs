using Sky_Hunt.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Sky_Hunt
{
    public class Plane
    {
        bool movesLeft;
        public PictureBox PictureBox { get; }
        public void Move(GameForm gameForm)
        {
            if (movesLeft)
                PictureBox.Left -= gameForm.Width / 150;
            else
                PictureBox.Left += gameForm.Width / 150;
        }

        public Plane(PlaneState state, int y, GameForm gameForm)
        {
            PictureBox = new PictureBox()
            {
                Name = "plane",
                Size = new Size(gameForm.Width / 20, gameForm.Height / 20),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            PictureBox.MouseClick += (s, e) =>
            {
                if (gameForm.startGame && e.Button == MouseButtons.Left && !gameForm.reload && !gameForm.isGameOver)
                    gameForm.Shoot();
            };

            if (state == PlaneState.EnemyLeft)
            {
                movesLeft = true;
                PictureBox.Image = Resources.EnemyLeft;
                PictureBox.Location = new Point(gameForm.Width * 99 / 100, y);
            }

            else if (state == PlaneState.EnemyRight)
            {
                movesLeft = false;
                PictureBox.Image = Resources.EnemyRight;
                PictureBox.Location = new Point(gameForm.Width / 100 * (-3), y);
            }

            else if (state == PlaneState.EnemyLightLeft)
            {
                movesLeft = true;
                PictureBox.Image = Resources.EnemyLightLeft;
                PictureBox.Location = new Point(gameForm.Width * 99 / 100, y);
            }

            else if (state == PlaneState.EnemyLightRight)
            {
                movesLeft = false;
                PictureBox.Image = Resources.EnemyLightRight;
                PictureBox.Location = new Point(gameForm.Width / 100 * (-3), y);
            }

            else if (state == PlaneState.EnemyHeavyRight)
            {
                movesLeft = false;
                PictureBox.Image = Resources.EnemyHeavyRight;
                PictureBox.Location = new Point(gameForm.Width / 100 * (-3), y);
            }

            else
            {
                movesLeft = true;
                PictureBox.Image = Resources.EnemyHeavyLeft;
                PictureBox.Location = new Point(gameForm.Width * 99 / 100, y);
            }
        }
    }
}