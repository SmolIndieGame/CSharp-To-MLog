using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    /// <summary>
    /// An instance of a building that is linked to the processor by name.<br/>
    /// This class is created to solve the issue of needing to type <see cref="Mindustry.GetLink(BuildingType, int)"/><br/>
    /// everytime you wanted to get a linked building by its name.
    /// <para>Example declaration:<br/>
    /// <c>
    /// [<see cref="LinkedToAttribute"/>(<see cref="BuildingType.Duo"/>, 27)]<br/>
    /// <see langword="private"/> <see cref="LinkedBuilding"/> turret;<br/>
    /// </c>
    /// Everytime the field turret is referenced, it will transpile to duo27.
    /// </para>
    /// </summary>
    /// <remarks>
    /// LinkedBuildings cannot be assigned and cannot be defined as local variables.
    /// </remarks>
    public abstract class LinkedBuilding : Building
    {
        /// <summary>
        /// The name of the linked building.
        /// </summary>
        public string Name { get; }
    }
}
