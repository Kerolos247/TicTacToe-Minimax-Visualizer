using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Runtime.InteropServices;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        string[,] board = new string[3, 3];
        Minimax minimax = new Minimax();
        Node root;
        TreeDrawer drawer = new TreeDrawer();
        SoundPlayer player = new SoundPlayer();
        Timer timer = new Timer();

        GameUIManager ui; 
        bool isStop = false;

        public Form1()
        {
            InitializeComponent();
            ui = new GameUIManager(this);
            this.Load += Form1_Load;
           
            AttachEvents();

            player.SoundLocation = Path.Combine(Application.StartupPath, "gameover.wav");
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();

            ResetBoardData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void AttachEvents()
        {
           
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    ui.Btns[i, j].Click += btn_Click;

            ui.BtnRestart.Click += btn_Restart_GAME;
            ui.BtnShowTree.Click += (s, e) => ui.TreePanel.Refresh();
            ui.TreePanel.Paint += panel1_Paint;
        }

        private void ResetBoardData()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = "";
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (isStop)
                return;

            Button btn = sender as Button;
            int r = int.Parse(btn.Name.Substring(3, 1));
            int c = int.Parse(btn.Name.Substring(4, 1));

            if (board[r, c] != "") return;

            board[r, c] = "O";
            ui.UpdateCell(r, c, "O");

            ComputerPlay();
        }

        void ComputerPlay()
        {
            if (minimax.CheckWinner(board).Winner != "")
                return;

            root = minimax.BuildTree(board, true);
            int bestVal = -1000, bestR = 0, bestC = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == "")
                    {
                        board[i, j] = "X";
                        var val = minimax.BuildTree(board, false).Value;
                        board[i, j] = "";
                        if (val > bestVal) { bestVal = val; bestR = i; bestC = j; }
                    }
                }
            }

            board[bestR, bestC] = "X";
            ui.UpdateCell(bestR, bestC, "X");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckGameOver();
        }
        private void CheckGameOver()
        {
            var result = minimax.CheckWinner(board);
            if (result.Winner != "")
            {
                isStop = true;
                timer.Stop();
                player.Play();
                ui.HighlightWinner(result.Squares);
            }
        }

        private void btn_Restart_GAME(object sender, EventArgs e)
        {
            ResetBoardData();
            ui.ResetUI();
            root = null;
            isStop = false;
            timer.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (root != null)
                drawer.DrawTree(e.Graphics, root, ui.TreePanel.Width / 2, 20);
        }
    }
}