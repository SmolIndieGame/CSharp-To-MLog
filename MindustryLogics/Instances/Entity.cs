using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    public abstract class Entity
    {
        public int TotalItems { get; }
        public ItemType FirstItem { get; }
        public int ItemCapacity { get; }
        public int Ammo { get; }
        public int AmmoCapacity { get; }
        public double Health { get; }
        public double MaxHealth { get; }

        /// <summary>
        /// The roataion of the entity.<br/>
        /// <b>For buildings that have directions:</b>
        /// <list type="bullet">
        /// <item><term>0</term> <description>faces right</description></item>
        /// <item><term>1</term> <description>faces up</description></item>
        /// <item><term>2</term> <description>faces left</description></item>
        /// <item><term>3</term> <description>faces down</description></item>
        /// </list>
        /// <b>For turrets and units:</b>
        /// <list type="bullet">
        /// <listheader>It is an angle start from 0 to 360.</listheader>
        /// <item><term>0</term> <description>faces right</description></item>
        /// <item><term>90</term> <description>faces up</description></item>
        /// <item><term>180</term> <description>faces left</description></item>
        /// <item><term>270</term> <description>faces down</description></item>
        /// </list>
        /// </summary>
        public double Rotation { get; }
        public double X { get; }
        public double Y { get; }
        public double ShootX { get; }
        public double ShootY { get; }
        public double Size { get; }
        public bool Dead { get; }
        public double Range { get; }
        public bool Shooting { get; }
        public int Team { get; }
        public ControlType Controlled { get; }
        public Entity Controller { get; }

        public abstract int GetQuantityOf(ItemType type);
        public abstract double GetQuantityOf(LiquidType type);
        public abstract object GetInfo(InfoType type);
    }
}
