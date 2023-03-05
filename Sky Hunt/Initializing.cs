using Sky_Hunt.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace Sky_Hunt
{
    public static class Initializing
    {
        public static void InitializeSettings(GameForm gameForm)
        {
            gameForm.Text = "Sky Hunt";
            gameForm.Icon = Resources.Logo1;
            gameForm.FormBorderStyle = FormBorderStyle.None;
            gameForm.WindowState = FormWindowState.Maximized;
            gameForm.BackgroundImage = Resources.Sky;
            gameForm.BackgroundImageLayout = ImageLayout.Stretch;
            MusicPlayer.Play(gameForm.random.Next(3));
        }

        public static void InitializeMenuOnLoad(GameForm gameForm)
        {
            gameForm.Load += (s, e) =>
            {
                gameForm.Controls.Add(new Label()
                {
                    Name = "text",
                    Text = "Enter to restart game" + "\n" +
                    "LeftMouseClick to shoot" + "\n" +
                    "Escape to exit" + "\n" +
                    "if (missed==3)" + "\n" +
                    "    GameOver();" + "\n" +
                    "sigma" + "\n" +
                    "VLAD KUDRIN THE GENIUS",
                    Location = new Point(gameForm.Width * 2 / 5, gameForm.Height * 2 / 3),
                    Size = new Size(gameForm.Width / 4, gameForm.Height / 3),
                    BackColor = Color.Transparent,
                    Font = new Font("Arial", 20),
                    ForeColor = Color.White,
                });

                gameForm.Controls.Add(new PictureBox()
                {
                    Name = "logo",
                    Image = Resources.Logo,
                    Size = new Size(gameForm.Width / 3, gameForm.Height / 3),
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(gameForm.Width / 3, gameForm.Height / 5)
                });
            };

            InitializeDifficultiesOnLoad(gameForm);
        }

        private static void InitializeDifficultiesOnLoad(GameForm gameForm)
        {
            gameForm.Load += (s, e) =>
            {
                var hardDifficultyLabel = new Label()
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.Red,
                    Font = new Font("Arial", 30),
                    Size = new Size(gameForm.Width / 12, gameForm.Height / 20),
                    Top = gameForm.Height * 19 / 20,
                    Left = gameForm.Width * 11 / 12,
                    Text = "Hard"
                };

                hardDifficultyLabel.Click += (se, ev) =>
                {
                    gameForm.planeSpawnTimer.Interval = 1250;
                    gameForm.levelDifficulty = LevelDifficulty.Hard;
                    if (gameForm.startGame)
                        gameForm.RestartGame();
                    else
                        gameForm.StartGame();
                };

                var mediumDifficultyLabel = new Label()
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.Yellow,
                    Font = new Font("Arial", 30),
                    Size = new Size(gameForm.Width / 9, gameForm.Height / 20),
                    Top = gameForm.Height * 19 / 20,
                    Left = hardDifficultyLabel.Left - gameForm.Width / 9,
                    Text = "Medium"
                };

                mediumDifficultyLabel.Click += (se, ev) =>
                {
                    gameForm.planeSpawnTimer.Interval = 1750;
                    gameForm.levelDifficulty = LevelDifficulty.Medium;
                    if (gameForm.startGame)
                        gameForm.RestartGame();
                    else
                        gameForm.StartGame();
                };

                var easyDifficultyLabel = new Label()
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.Green,
                    Font = new Font("Arial", 30),
                    Size = new Size(gameForm.Width / 13, gameForm.Height / 20),
                    Top = gameForm.Height * 19 / 20,
                    Left = mediumDifficultyLabel.Left - gameForm.Width / 13,
                    Text = "Easy"
                };

                easyDifficultyLabel.Click += (se, ev) =>
                {
                    gameForm.planeSpawnTimer.Interval = 2500;
                    gameForm.levelDifficulty = LevelDifficulty.Easy;
                    if (gameForm.startGame)
                        gameForm.RestartGame();
                    else
                        gameForm.StartGame();
                };

                gameForm.Controls.Add(easyDifficultyLabel);
                gameForm.Controls.Add(mediumDifficultyLabel);
                gameForm.Controls.Add(hardDifficultyLabel);
            };
        }

        public static void InitializeTimers(GameForm gameForm)
        {
            gameForm.gameTimer = new Timer()
            {
                Interval = 20
            };

            gameForm.reloadTimer = new Timer()
            {
                Interval = 600
            };

            gameForm.planeSpawnTimer = new Timer();
            gameForm.planeSpawnTimer.Tick += (s, e) =>
            {
                var randomState = (PlaneState)gameForm.random.Next(6);
                var randomY = gameForm.random.Next(gameForm.Width * 3 / 200, gameForm.Height / 2);
                var plane = new Plane(randomState, randomY, gameForm);
                gameForm.planes.Add(plane);
                gameForm.Controls.Add(plane.PictureBox);
            };

            InitializeGameEventTimer(gameForm);
            gameForm.reloadTimer.Tick += (s, e) => gameForm.reload = false;
        }

        private static void InitializeGameEventTimer(GameForm gameForm)
        {
            gameForm.gameTimer.Tick += (s, e) =>
            {
                foreach (var plane in gameForm.planes.ToArray())
                {
                    foreach (var projectile in gameForm.projectiles.ToArray())
                        if (plane.PictureBox.Bounds.IntersectsWith(projectile.PictureBox.Bounds))
                        {
                            gameForm.ShowExplosion(plane.PictureBox.Location);
                            gameForm.planes.Remove(plane);
                            gameForm.Controls.Remove(plane.PictureBox);
                            gameForm.projectiles.Remove(projectile);
                            gameForm.Controls.Remove(projectile.PictureBox);
                            gameForm.UpdateScore(true);
                            break;
                        }

                    if (!plane.PictureBox.Bounds.IntersectsWith(gameForm.Bounds))
                    {
                        gameForm.planes.Remove(plane);
                        gameForm.Controls.Remove(plane.PictureBox);
                        gameForm.UpdateMissed(true);
                    }

                    else
                        plane.Move(gameForm);
                }

                foreach (var projectile in gameForm.projectiles.ToArray())
                {
                    if (!projectile.PictureBox.Bounds.IntersectsWith(gameForm.Bounds))
                    {
                        gameForm.projectiles.Remove(projectile);
                        gameForm.Controls.Remove(projectile.PictureBox);
                    }

                    else
                        projectile.Move(gameForm);
                }
            };
        }

        public static void InitializeControls(GameForm gameForm)
        {
            gameForm.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    gameForm.Close();
                else if (gameForm.startGame && e.KeyCode == Keys.Enter)
                    gameForm.RestartGame();
            };

            gameForm.MouseClick += (s, e) =>
            {
                if (gameForm.startGame && e.Button == MouseButtons.Left && !gameForm.reload && !gameForm.isGameOver)
                    gameForm.Shoot();
            };
        }
    }
}