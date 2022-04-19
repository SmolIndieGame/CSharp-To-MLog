using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    public abstract class Unit : Entity
    {
        public bool Boosting { get; }
        public double MineX { get; }
        public double MineY { get; }
        public bool Mining { get; }
        public UnitType Type { get; }
        public double Flag { get; }
        public string Name { get; }
        public int PayloadCount { get; }
        public object PayloadType { get; }
    }
}
