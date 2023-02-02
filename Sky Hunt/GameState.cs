using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sky_Hunt
{
    public static class GameState
    {
        public static void RestartGame(this GameForm gameForm)
        {
            gameForm.planeSpawnTimer.Stop();
            gameForm.gameTimer.Stop();
            gameForm.reloadTimer.Stop();
            foreach (var c in gameForm.Controls.Find("plane", false))
                gameForm.Controls.Remove(c);
            foreach (var c in gameForm.Controls.Find("projectile", false))
                gameForm.Controls.Remove(c);
            gameForm.Controls.RemoveByKey("gameOver");
            gameForm.isGameOver = false;
            gameForm.planes.Clear();
            gameForm.projectiles.Clear();
            gameForm.score = 0;
            gameForm.UpdateScore(false);
            gameForm.missed = 0;
            gameForm.UpdateMissed(false);
            gameForm.planeSpawnTimer.Start();
            gameForm.gameTimer.Start();
            gameForm.reloadTimer.Start();
        }


        public static void GameOver(this GameForm gameForm)
        {
            gameForm.Controls.Add(new Label()
            {
                Name = "gameOver",
                Text = "Game Over",
                ForeColor = Color.Red,
                Size = new Size(gameForm.Width / 5 * 2, gameForm.Height / 10),
                BackColor = Color.Transparent,
                Location = new Point(gameForm.Width / 10 * 3, gameForm.Height / 8 * 7),
                Font = new Font("Arial", 70)
            });

            gameForm.planeSpawnTimer.Stop();
            gameForm.gameTimer.Stop();
            gameForm.reloadTimer.Stop();
            gameForm.isGameOver = true;
        }

        public static void StartGame(this GameForm gameForm)
        {
            gameForm.Controls.RemoveByKey("logo");
            gameForm.Controls.RemoveByKey("text");
            gameForm.Controls.Add(new Label()
            {
                Name = "labelHighScore",
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Top = gameForm.Height * 24 / 25,
                Size = new Size(gameForm.Width / 7, gameForm.Height / 25),
                Font = new Font("Arial", 20),
                Text = "HighScore:" + gameForm.highScore
            });

            gameForm.Controls.Add(new Label()
            {
                Name = "labelScore",
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Location = new Point(0, 0),
                Size = new Size(gameForm.Width / 8, gameForm.Height / 25),
                Font = new Font("Arial", 20),
                Text = "Score:" + gameForm.score
            });

            gameForm.Controls.Add(new Label()
            {
                Name = "labelMissed",
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Location = new Point(gameForm.Width - gameForm.Width / 10, 0),
                Size = new Size(gameForm.Width * 3 / 25, gameForm.Height / 25),
                Font = new Font("Arial", 20),
                Text = "Missed:" + gameForm.missed
            });

            gameForm.planeSpawnTimer.Start();
            gameForm.gameTimer.Start();
            gameForm.reloadTimer.Start();
            gameForm.startGame = true;
        }


        public static void UpdateScore(this GameForm gameForm, bool incrementScore)
        {
            if (incrementScore)
            {
                if (gameForm.levelDifficulty == LevelDifficulty.Easy)
                    gameForm.score++;
                else if (gameForm.levelDifficulty == LevelDifficulty.Medium)
                    gameForm.score += 2;
                else
                    gameForm.score += 3;
            }

            if (gameForm.score > gameForm.highScore)
            {
                gameForm.highScore = gameForm.score;
                var stringScore = gameForm.score;
                File.WriteAllText(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())
                .Parent.FullName, "Resources\\HighScore.txt"), stringScore.ToString());
                gameForm.Controls["labelHighScore"].Text = "HighScore:" + gameForm.highScore;
            }

            gameForm.Controls["labelScore"].Text = "Score:" + gameForm.score;
        }

        public static void UpdateMissed(this GameForm gameForm, bool incrementMissed)
        {
            if (incrementMissed)
                gameForm.missed++;
            if (gameForm.missed == 3)
                gameForm.GameOver();
            gameForm.Controls["labelMissed"].Text = "Missed:" + gameForm.missed;
        }
    }
}
