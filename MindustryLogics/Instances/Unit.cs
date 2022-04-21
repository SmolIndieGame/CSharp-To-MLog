using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    /// <summary>
    /// An instance of a unit.<br/>
    /// This class contains all information about a unit.<br/>
    /// As handling units is different from handling buildings in mindustry,<br/>
    /// Operations using units are in <see cref="Mindustry"/> and <see cref="UnitControl"/>.
    /// </summary>
    public abstract class Unit : Entity
    {
        /// <inheritdoc cref="InfoType.Boosting"/>
        public bool Boosting { get; }
        /// <inheritdoc cref="InfoType.MineX"/>
        public double MineX { get; }
        /// <inheritdoc cref="InfoType.MineY"/>
        public double MineY { get; }
        /// <inheritdoc cref="InfoType.Mining"/>
        public bool Mining { get; }
        /// <inheritdoc cref="InfoType.Type"/>
        public UnitType Type { get; }
        /// <inheritdoc cref="InfoType.Flag"/>
        public double Flag { get; }
        /// <inheritdoc cref="InfoType.Name"/>
        public string Name { get; }
        /// <inheritdoc cref="InfoType.PayloadCount"/>
        public int PayloadCount { get; }
        /// <inheritdoc cref="InfoType.PayloadType"/>
        public object PayloadType { get; }
    }
}
