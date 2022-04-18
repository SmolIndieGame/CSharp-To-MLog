using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    public abstract class Building : Entity
    {
        public double TotalLiquids { get; }
        public double TotalPower { get; }
        public double LiquidCapacity { get; }
        public double PowerCapacity { get; }
        public double PowerNetStored { get; }
        public double PowerNetCapacity { get; }
        public double PowerNetIn { get; }
        public double PowerNetOut { get; }
        public double Heat { get; }
        public double Efficiency { get; }
        /// <summary>
        /// Not available on V6.
        /// </summary>
        public double Progress { get; }
        public double Timescale { get; }
        public BuildingType Type { get; }
        public bool Enabled { get; set; }

        /// <summary>
        /// Setter only works on V7, if you are on V6, use <see cref="Configure"/> instead.
        /// </summary>
        public ItemType Config { get; set; }

        /// <summary>
        /// Same as <see cref="Config"/>, but only works on V6.
        /// </summary>
        public ItemType Configure { set => _ = value; }

        /// <summary>
        /// Order this building to shoot or aim at location (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The x coordinate to shoot or aim at.</param>
        /// <param name="y">The x coordinate to shoot or aim at.</param>
        /// <param name="shootOrAim">Shoot if <see langword="true"/>, only aim at target otherwise.</param>
        public abstract void Shoot(double x, double y, bool shootOrAim);

        /// <summary>
        /// Order this building to shoot or aim at <paramref name="target"/>.<br/>
        /// It will try to predict the <paramref name="target"/> movement.
        /// </summary>
        /// <param name="target">The target entity (building or unit) to shoot or aim at.</param>
        /// <param name="shootOrAim">Shoot if <see langword="true"/>, only aim at target otherwise.</param>
        public abstract void ShootTarget(Entity target, bool shootOrAim);

        /// <summary>
        /// <para>
        /// Locate units around this building.<br/>
        /// Unable to detect units outside the <see cref="Entity.Range"/> of this building.
        /// </para>
        /// <para>
        /// Example:<br/><c>turret1.Radar(Filter.Radar().Enemy, SortMethod.Distance, true)</c><br/>
        /// This will return a enemy that is in range and is closest to turret1.
        /// </para>
        /// </summary>
        /// <param name="target">The target filter.<br/>Use <see cref="Filter.Radar()"/> to create the filter than add constraints into it. (maximum three constraints)<para>Example:<br/>To target enemy ground unit: <c>Filter.Radar().Enemy.Ground</c><br/>To target anything: <c>Filter.Radar()</c> or <c>null</c></para></param>
        /// <param name="sortMethod">How to sort the results if there are multiple units that are in range and pass the filter.<para>Example:<br/>To sort by health: <c>SortMethod.Health</c></para></param>
        /// <param name="firstOrLast">Returns the first unit in the results sorted by <paramref name="sortMethod"/> if <see langword="true"/>, returns the last unit otherwise.</param>
        /// <returns></returns>
        public abstract Unit Radar(RadarFilter target, SortMethod sortMethod, bool firstOrLast);
    }
}
