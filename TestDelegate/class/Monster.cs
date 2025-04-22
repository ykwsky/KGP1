using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDelegate
{
    public class Monster : iMonster
    {
        public int id { get; set; }
        public int type { get; set; }
        public int level { get; set; }
        public int exp { get; set; }
        public int coin { get; set; }
        public int point { get; set; }
        public int hp { get; set; }
        public int sp { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
    }

    public class Slime : Monster
    {

    }
}
