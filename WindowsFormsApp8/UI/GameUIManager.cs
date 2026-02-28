using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public class GameUIManager
    {
        private Form _parentForm;
        public Button[,] Btns = new Button[3, 3];
        public Panel TreePanel;
        public Button BtnRestart;
        public Button BtnShowTree;

        public GameUIManager(Form form)
        {
            _parentForm = form;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _parentForm.Width = 1200;
            _parentForm.Height = 700;
            _parentForm.Text = "Tic Tac Toe + Minimax Visualization";

           
            int x = 20, y = 20, size = 80;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button b = new Button
                    {
                        Name = $"btn{i}{j}",
                        Width = size,
                        Height = size,
                        Font = new Font("Arial", 18, FontStyle.Bold),
                        Location = new Point(x + j * size, y + i * size),
                        BackColor = Color.White
                    };
                    Btns[i, j] = b;
                    _parentForm.Controls.Add(b);
                }
            }

           
            BtnShowTree = new Button
            {
                Text = "Show Tree",
                Width = 200,
                Height = 40,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 300)
            };
            _parentForm.Controls.Add(BtnShowTree);

           
            BtnRestart = new Button
            {
                Text = "Restart Game",
                Size = new Size(150, 50),
                Location = new Point(20, 500),
                BackColor = Color.LightSkyBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            _parentForm.Controls.Add(BtnRestart);

          
            TreePanel = new Panel
            {
                Width = 800,
                Height = 600,
                BackColor = Color.White,
                Location = new Point(350, 20),
                BorderStyle = BorderStyle.FixedSingle
            };
            _parentForm.Controls.Add(TreePanel);
        }

        public void UpdateCell(int r, int c, string text)
        {
            Btns[r, c].Text = text;
        }

       
        public void HighlightWinner(System.Collections.Generic.List<(int row, int col)> squares)
        {
            foreach (var sq in squares)
            {
                
                Btns[sq.row, sq.col].BackColor = Color.Yellow;
            }
        }
        public void ResetUI()
        {
            foreach (var btn in Btns)
            {
                btn.Text = "";
                btn.BackColor = Color.White;
                btn.Enabled = true;
            }
            
        }
    }
}