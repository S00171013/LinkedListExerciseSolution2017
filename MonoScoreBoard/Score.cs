using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoScoreBoard
{
    public class Score
    {
        public string PlayerName;
        public int score;
        public override string ToString()
        {
            return PlayerName + " " + score.ToString();
        }
    }
}
