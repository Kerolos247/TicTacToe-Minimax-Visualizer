using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp8
{
    internal class TreeDrawer
    {
        int nodeSize = 30;

        public void DrawTree(Graphics g, Node root, int x, int y)
        {
            DrawNode(g, root, x, y);
        }

        private void DrawNode(Graphics g, Node node, int x, int y)
        {
            g.DrawEllipse(Pens.Black, x, y, nodeSize, nodeSize);
            g.DrawString(node.Value.ToString(), new Font("Arial", 10), Brushes.Black, x + 8, y + 8);

            int childX = x - (node.Children.Count * 50);

            foreach (var child in node.Children)
            {
                g.DrawLine(Pens.Black, x + nodeSize / 2, y + nodeSize, childX + nodeSize / 2, y + 80);
                DrawNode(g, child, childX, y + 80);
                childX += 100;
            }
        }
    }
}

