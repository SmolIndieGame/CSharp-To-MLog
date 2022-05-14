using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    /// <summary>
    /// <para>Use this attribute to write a message that will not be executed at the top of the code.<br/>
    /// Attach multiple <see cref="CreditAttribute"/> to write multiple messages.</para>
    /// <para>Example:<br/>
    /// <code>
    /// [Credit("Author: &lt;yourname&gt;")]<br/>
    /// class YourClass<br/>
    /// {<br/>
    /// ...
    /// </code></para>
    /// </summary>
    /// <remarks>Deleting the credit of the translated code will break the program!<br/>
    /// If you don't want the credit, attach a <see cref="ExcludeCreditAttribute"/> at the top of the class.</remarks>
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
