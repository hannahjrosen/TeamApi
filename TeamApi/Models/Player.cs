using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamApi
{
    public class Player
    {
        public long Id { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public string position { get; set; }
        public double salary { get; set; }
    }
}
