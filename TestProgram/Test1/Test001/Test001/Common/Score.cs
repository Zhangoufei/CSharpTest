using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test001.Common
{
    public class Score
    {
        private string scores;
        private string titile;
        private string secondTitile;

        public string Scores
        {
            get
            {
                return scores;
            }

            set
            {
                scores = value;
            }
        }

        public string Titile
        {
            get
            {
                return titile;
            }

            set
            {
                titile = value;
            }
        }

        public string SecondTitile
        {
            get
            {
                return secondTitile;
            }

            set
            {
                secondTitile = value;
            }
        }
    }
}
