using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    /// <summary>
    /// An instance of a building.<br/>
    /// This class contains all information about a building.<br/>
    /// Do stuff with the building by accessing its member, for example,<br/>
    /// <c>b.Shoot(10, 10, true)</c> will make building b shoot at (10, 10).
    /// </summary>
    public abstract class Building : Entity
    {
        /// <inheritdoc cref="InfoType.TotalLiquids"/>
        public double TotalLiquids { get; }
        /// <inheritdoc cref="InfoType.TotalPower"/>
        public double TotalPower { get; }
        /// <inheritdoc cref="InfoType.LiquidCapacity"/>
        public double LiquidCapacity { get; }
        /// <inheritdoc cref="InfoType.PowerCapacity"/>
        public double PowerCapacity { get; }
        /// <inheritdoc cref="InfoType.PowerNetStored"/>
        public double PowerNetStored { get; }
        /// <inheritdoc cref="InfoType.PowerNetCapacity"/>
        public double PowerNetCapacity { get; }
        /// <inheritdoc cref="InfoType.PowerNetIn"/>
        public double PowerNetIn { get; }
        /// <inheritdoc cref="InfoType.PowerNetOut"/>
        public double PowerNetOut { get; }
        /// <inheritdoc cref="InfoType.Heat"/>
        public double Heat { get; }
        /// <inheritdoc cref="InfoType.Efficiency"/>
        public double Efficiency { get; }
        /// <inheritdoc cref="InfoType.Progress"/>
        public double Progress { get; }
        /// <inheritdoc cref="InfoType.Timescale"/>
        public double Timescale { get; }
        /// <inheritdoc cref="InfoType.Type"/>
        public abstract BuildingType Type { get; }
        /// <inheritdoc cref="InfoType.Enabled"/>
        public bool Enabled { get; set; }

        /// <inheritdoc cref="InfoType.Config"/>
        /// <remarks>Setter is not available on V6, if you are on V6, use <see cref="Configure"/> instead.</remarks>
        public object Config { get; set; }

        /// <summary>
        /// Same as <see cref="Config"/>, but also works on V6.
        /// </summary>
        public object Configure { set => _ = value; }

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
        /// Set the color of the illuminator.<br/>
        /// Only works if the building is a <see cref="BuildingType.Illuminator"/>.
        /// </summary>
        /// <param name="r">The amount of red in the color, 0 is no red, and 255 is full red.</param>
        /// <param name="g">The amount of green in the color, 0 is no green, and 255 is full green.</param>
        /// <param name="b">The amount of blue in the color, 0 is no blue, and 255 is full blue.</param>
        public abstract void SetColor(byte r, byte g, byte b);

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
