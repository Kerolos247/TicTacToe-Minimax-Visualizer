using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        string[,] board = new string[3, 3];
        Minimax minimax = new Minimax();
        Node root;
        TreeDrawer drawer = new TreeDrawer();
        SoundPlayer player = new SoundPlayer();
        

        Button[,] btns = new Button[3, 3];
        Panel panel1;
        Button btnShowTree;
        Button btnRestart_GAME=new Button();

        Timer timer=new Timer();

        bool isStop= false;

        public Form1()
        {
            this.Width = 1200;
            this.Height = 700;
            this.Text = "Tic Tac Toe + Minimax Visualization";
            timer.Tick += Timer_Tick;
            timer.Start();
            player.SoundLocation = @"D:\BackEnd_Clininc\WindowsFormsApp8\WindowsFormsApp8\gameover.wav";
            CreateBoardButtons();
            CreateShowTreeButton();
            CreatePanelForTree();
            btnRestart_GAME.Name = "btnRestart_GAME";
            btnRestart_GAME.Text = "Restart Game";
            btnRestart_GAME.Size = new Size(150, 50);
            btnRestart_GAME.Location = new Point(20,500);  
            btnRestart_GAME.BackColor = Color.LightSkyBlue;
            btnRestart_GAME.ForeColor = Color.White;
            btnRestart_GAME.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnRestart_GAME.FlatStyle = FlatStyle.Flat;
            btnRestart_GAME.FlatAppearance.BorderSize = 0;   
            btnRestart_GAME.Click += btn_Restart_GAME;

       
            this.Controls.Add(btnRestart_GAME);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = "";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (minimax.CheckWinner(board).Winner!="")
            {
                
                isStop = true;
                timer.Stop();
                //player.Play();
                foreach (var c in minimax.CheckWinner(board).Squares)
                {
                    btns[c.Item1, c.Item2].BackColor = Color.Yellow;
                }
              
            }
        }
        private void btn_Restart_GAME(object sender, EventArgs e)
        {
            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = "";

           
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Button btn = this.Controls[$"btn{r}{c}"] as Button;

                    if (btn != null)
                    {
                        btn.Text = "";                        
                        btn.BackColor = Color.White;         
                        btn.Enabled = true;                 
                    }
                }
            }

            
            root = null;
            isStop = false;
            timer.Start();
            panel1.Refresh();
        }


        private void CreateBoardButtons()
        {
            int x = 20, y = 20;
            int size = 80;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button b = new Button();
                    b.Name = $"btn{i}{j}";
                    b.Text = "";
                    b.Width = size;
                    b.Height = size;
                    b.Font = new Font("Arial", 18, FontStyle.Bold);
                    b.Location = new Point(x + j * size, y + i * size);
                    b.Click += btn_Click;

                    btns[i, j] = b;
                    this.Controls.Add(b);
                }
            }
        }

        
        private void CreateShowTreeButton()
        {
            btnShowTree = new Button();
            btnShowTree.Text = "Show Tree";
            btnShowTree.Width = 200;
            btnShowTree.Height = 40;
            btnShowTree.Font = new Font("Arial", 12, FontStyle.Bold);
            btnShowTree.Location = new Point(20, 300);

            btnShowTree.Click += btnShowTree_Click;

            this.Controls.Add(btnShowTree);
        }

        private void CreatePanelForTree()
        {
            panel1 = new Panel();
            panel1.Name = "panel1";
            panel1.Width = 800;
            panel1.Height = 600;
            panel1.BackColor = Color.White;
            panel1.Location = new Point(350, 20);
            panel1.BorderStyle = BorderStyle.FixedSingle;

            panel1.Paint += panel1_Paint;

            this.Controls.Add(panel1);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(isStop)
                return;
            Button btn = sender as Button;
            int r = int.Parse(btn.Name[3].ToString());
            int c = int.Parse(btn.Name[4].ToString());
           
            if (btn.Text != "") 
                return;

            btn.Text = "O";
            board[r, c] = "O";

           
            ComputerPlay();
        }

       
        void ComputerPlay()
        {
            root = minimax.BuildTree(board, true);

            int bestVal = -999999999;
            int bestR = 0, bestC = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == "")
                    {
                        board[i, j] = "X";
                        var val = minimax.BuildTree(board, false).Value;
                        board[i, j] = "";

                        if (val > bestVal)
                        {
                            bestVal = val;
                            bestR = i;
                            bestC = j;
                        }
                    }
                }
            }

            board[bestR, bestC] = "X";
            btns[bestR, bestC].Text = "X";
        }

        private void btnShowTree_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (root != null)
                drawer.DrawTree(e.Graphics, root, panel1.Width / 2, 20);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}

