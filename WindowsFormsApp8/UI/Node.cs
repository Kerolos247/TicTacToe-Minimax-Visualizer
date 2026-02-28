using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp8
{
    internal class Node
    {
        public string[,] Board = new string[3, 3];
        public int Value;
        public List<Node> Children = new List<Node>();

        public List<(int row, int col)> WinSquares { get; set; } = null;
    }
}
