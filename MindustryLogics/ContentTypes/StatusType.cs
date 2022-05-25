namespace MindustryLogics
{
    /// <summary>
    /// The type of a status.<br/>
    /// There is currently zero usage of this class and its members.
    /// </summary>
    public abstract class StatusType : KebabCaseType
    {
        /** */ public const StatusType None = default;
        /** */ public static StatusType Burning => default;
        /** */ public static StatusType Freezing => default;
        /** */ public static StatusType Unmoving => default;
        /** */ public static StatusType Wet => default;
        /** */ public static StatusType Melting => default;
        /** */ public static StatusType Sapped => default;
        /** */ public static StatusType Electrified => default;
        /** */ public static StatusType Sprore_Slowed => default;
        /** */ public static StatusType Tarred => default;
        /** */ public static StatusType Overdrive => default;
        /** */ public static StatusType Overclock => default;
        /** */ public static StatusType Guardian => default;
        /** */ public static StatusType Shocked => default;
        /** */ public static StatusType Blasted => default;
    }
}