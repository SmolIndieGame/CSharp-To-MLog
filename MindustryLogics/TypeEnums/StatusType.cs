namespace MindustryLogics
{
    /// <summary>
    /// The type of a status type.<br/>
    /// Currently, the only usage of this enum is to pass it into <see cref="Mindustry.GetIcon(StatusType)"/>.
    /// </summary>
    public enum StatusType
    {
        /** */ None = 0,
        /** */ Burning = 0xf7b6,
        /** */ Freezing = 0xf7b5,
        /** */ Unmoving = 0xf7b4,
        /** */ Wet = 0xf7b2,
        /** */ Melting = 0xf7b0,
        /** */ Sapped = 0xf7af,
        /** */ Electrified = 0xf781,
        /** */ Sprore_Slowed = 0xf7ae,
        /** */ Tarred = 0xf7ad,
        /** */ Overdrive = 0xf7ac,
        /** */ Overclock = 0xf7ab,
        /** */ Guardian = 0xf7a9,
        /** */ Shocked = 0xf7a8,
        /** */ Blasted = 0xf7a7
    }
}