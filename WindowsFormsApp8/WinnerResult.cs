using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp8
{
    public class WinnerResult
    {
        public string Winner { get; set; }
        public List<(int row, int col)> Squares { get; set; }

    }
}
