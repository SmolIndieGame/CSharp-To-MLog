using System;

namespace MindustryLogics
{
    /// <summary>
    /// Use this attribute to remove the credit at the top of the code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class ExcludeCreditAttribute : Attribute
    {
        /// <inheritdoc cref="ExcludeCreditAttribute"/>
        public ExcludeCreditAttribute()
        {
        }
    }
}
