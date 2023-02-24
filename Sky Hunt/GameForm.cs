using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Sky_Hunt
{
    public partial class GameForm : Form
    {
        public Random random = new Random();
        public List<Projectile> projectiles = new List<Projectile>();
        public List<Plane> planes = new List<Plane>();
        public Timer planeSpawnTimer;
        public Timer gameTimer;
        public Timer reloadTimer;
        public LevelDifficulty levelDifficulty;
        public bool startGame;
        public bool reload;
        public bool isGameOver;
        public int score;
        public int missed;
        public int highScore = int.Parse(File.ReadAllText(Path.Combine(Directory.GetParent(Directory.
        GetCurrentDirectory()).Parent.FullName, "Resources\\HighScore.txt")));

        public GameForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Initializing.InitializeSettings(this);
            Initializing.InitializeMenuOnLoad(this);
            Initializing.InitializeControls(this);
            Initializing.InitializeTimers(this);
        }
    }
}