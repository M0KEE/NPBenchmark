using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankPageGenerator {
    public class Checker {
        public delegate double Check(string solution);

        public static Luck luck = new Luck();

        public class Luck {
            public double check(string solution) { return rand.Next(); }
            Random rand = new Random();
        }
    }
}
