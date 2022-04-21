using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindustryLogics
{
    /// <summary>
    /// An instance of an entity (building or unit).<br/>
    /// This class contains information about a entity.
    /// </summary>
    public abstract class Entity
    {
        /// <inheritdoc cref="InfoType.TotalItems"/>
        public int TotalItems { get; }
        /// <inheritdoc cref="InfoType.FirstItem"/>
        public ItemType FirstItem { get; }
        /// <inheritdoc cref="InfoType.ItemCapacity"/>
        public int ItemCapacity { get; }
        /// <inheritdoc cref="InfoType.Ammo"/>
        public int Ammo { get; }
        /// <inheritdoc cref="InfoType.AmmoCapacity"/>
        public int AmmoCapacity { get; }
        /// <inheritdoc cref="InfoType.Health"/>
        public double Health { get; }

        /// <inheritdoc cref="InfoType.MaxHealth"/>
        public double MaxHealth { get; }

        /// <inheritdoc cref="InfoType.Rotation"/>
        public double Rotation { get; }
        /// <inheritdoc cref="InfoType.X"/>
        public double X { get; }
        /// <inheritdoc cref="InfoType.Y"/>
        public double Y { get; }
        /// <inheritdoc cref="InfoType.ShootX"/>
        public double ShootX { get; }
        /// <inheritdoc cref="InfoType.ShootY"/>
        public double ShootY { get; }
        /// <inheritdoc cref="InfoType.Size"/>
        public double Size { get; }
        /// <inheritdoc cref="InfoType.Dead"/>
        public bool Dead { get; }
        /// <inheritdoc cref="InfoType.Range"/>
        public double Range { get; }
        /// <inheritdoc cref="InfoType.Shooting"/>
        public bool Shooting { get; }
        /// <inheritdoc cref="InfoType.Team"/>
        public int Team { get; }
        /// <inheritdoc cref="InfoType.Controlled"/>
        public ControlType Controlled { get; }
        /// <inheritdoc cref="InfoType.Controller"/>
        public Entity Controller { get; }

        /// <summary>
        /// Get the number of items of type <paramref name="type"/> in an <see cref="Entity"/>.
        /// </summary>
        /// <param name="type">The type of the item.</param>
        /// <returns></returns>
        public abstract int GetQuantityOf(ItemType type);

        /// <summary>
        /// Get the amount of liquid of type <paramref name="type"/> in an <see cref="Entity"/>.
        /// </summary>
        /// <param name="type">The type of the liquid.</param>
        /// <returns></returns>
        public abstract double GetQuantityOf(LiquidType type);

        /// <summary>
        /// Get the information of type <paramref name="type"/> about this entity.
        /// </summary>
        /// <param name="type">The type of information to get.</param>
        /// <returns></returns>
        /// <remarks>Since the return type of this method is an object, it is strongly recommended to access the properties of the entity instead.</remarks>
        public abstract object GetInfo(InfoType type);
    }
}
