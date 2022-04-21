﻿using System;
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
    /// If you don't want credits, attach a <see cref="ExcludeCreditAttribute"/> at the top of the class.</remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class CreditAttribute : Attribute
    {
        /// <param name="credit">The message to display.</param>
        /// <inheritdoc cref="CreditAttribute"/>
        public CreditAttribute(string credit)
        {
            Credit = credit;
        }

        /// <summary>
        /// The message to display.
        /// </summary>
        public string Credit { get; }
    }

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
