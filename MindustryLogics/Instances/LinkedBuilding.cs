using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    /// <summary>
    /// An instance of a building that is linked to the processor by name.<br/>
    /// This class contains additional information about a linked building.<br/>
    /// Use <see cref="Mindustry.GetLink(BuildingType, int)"/> to get a instance of a <see cref="LinkedBuilding"/>.
    /// </summary>
    /// <remarks>This class is created to solve the issue of needing to type <see cref="Mindustry.GetLink(BuildingType, int)"/><br/>
    /// everytime you wanted to get a linked building by its name.</remarks>
    public abstract class LinkedBuilding : Building
    {
        /// <summary>
        /// The name of the linked building.
        /// </summary>
        public string Name { get; }
    }
}
