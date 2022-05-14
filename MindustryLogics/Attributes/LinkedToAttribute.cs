using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    /// <summary>
    /// <para>Use this attribute to let a linked building know what it is linked to.</para>
    /// <para>Example:<br/>
    /// <code>
    /// ...
    /// [<see cref="LinkedToAttribute"/>(<see cref="BuildingType.Duo"/>, 27)]<br/>
    /// <see langword="private"/> <see cref="LinkedBuilding"/> turret;
    /// ...
    /// </code>
    /// Everytime the field turret is referenced, it will transpile to duo27.</para>
    /// </summary>
    /// <inheritdoc cref="LinkedBuilding" path="/remarks"/>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class LinkedToAttribute : Attribute
    {
        /// <param name="buildingType">The type of the building.</param>
        /// <param name="index">The index of the linked building. (starts at 1)</param>
        /// <inheritdoc cref="LinkedToAttribute"/>
        public LinkedToAttribute(BuildingType buildingType, int index)
        {
            BuildingType = buildingType;
            Index = index;
        }

        /// <summary>
        /// The type of the building.
        /// </summary>
        public BuildingType BuildingType { get; }
        /// <summary>
        /// The index of the linked building. (starts at 1)
        /// </summary>
        public int Index { get; }
    }
}
