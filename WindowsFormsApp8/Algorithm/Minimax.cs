using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp8
{
    internal class Minimax
    {
        public Node BuildTree(string[,] board, bool isMax)
        {
            Node node = new Node();
            node.Board = (string[,])board.Clone();

           
            var result = CheckWinner(board);

           
            if (result.Winner == "X")
            {
                node.Value = 1;
                node.WinSquares = result.Squares; 
                return node;
            }
            if (result.Winner == "O")
            {
                node.Value = -1;
                node.WinSquares = result.Squares;
                return node;
            }

           
            if (IsFull(board))
            {
                node.Value = 0;
                return node;
            }

           

            if (isMax)
            {
                int best = -999999999;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == "")
                        {
                            board[i, j] = "X";

                            var child = BuildTree(board, false);
                            node.Children.Add(child);

                            best = Math.Max(best, child.Value);

                            board[i, j] = "";
                        }
                    }
                }
                node.Value = best;
            }
            else
            {
                int best = 999999999;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == "")
                        {
                            board[i, j] = "O";

                            var child = BuildTree(board, true);
                            node.Children.Add(child);

                            best = Math.Min(best, child.Value);

                            board[i, j] = "";
                        }
                    }
                }
                node.Value = best;
            }

            return node;
        }


        private bool IsFull(string[,] b)
        {
            foreach (var c in b)
                if (c == "") return false;
            return true;
        }

        public WinnerResult CheckWinner(string[,] b)
        {
            string[] p = { "X", "O" };

            foreach (string s in p)
            {
               
                for (int i = 0; i < 3; i++)
                {
                    if (b[i, 0] == s && b[i, 1] == s && b[i, 2] == s)
                    {
                        return new WinnerResult
                        {
                            Winner = s,
                            Squares = new List<(int, int)>
                            {
                                (i,0), (i,1), (i,2)
                            }
                        };
                    }

                    
                    if (b[0, i] == s && b[1, i] == s && b[2, i] == s)
                    {
                        return new WinnerResult
                        {
                            Winner = s,
                            Squares = new List<(int, int)>
                            {
                                (0,i), (1,i), (2,i)
                            }
                        };
                    }
                }

               
                if (b[0, 0] == s && b[1, 1] == s && b[2, 2] == s)
                {
                    return new WinnerResult
                    {
                        Winner = s,
                        Squares = new List<(int, int)>
                        {
                            (0,0), (1,1), (2,2)
                        }
                    };
                }

               
                if (b[0, 2] == s && b[1, 1] == s && b[2, 0] == s)
                {
                    return new WinnerResult
                    {
                        Winner = s,
                        Squares = new List<(int, int)>
                        {
                            (0,2), (1,1), (2,0)
                        }
                    };
                }
            }

            return new WinnerResult { Winner = "", Squares = null };
        }

    }
}
