namespace MindustryLogics
{
    /// <summary>
    /// The type of a unit.
    /// </summary>
    public enum UnitType : uint
    {
        /// <summary>
        /// The default value.
        /// </summary>
        None = 0,
        /// <summary>
        /// Tier-1 offensive ground unit.
        /// </summary>
        Dagger = 0xf800,
        /// <summary>
        /// Tier-2 offensive ground unit.
        /// </summary>
        Mace = 0xf7ff,
        /// <summary>
        /// Tier-3 offensive ground unit.
        /// </summary>
        Fortress = 0xf7fe,
        /// <summary>
        /// Tier-4 offensive ground unit.
        /// </summary>
        Scepter = 0xf7db,
        /// <summary>
        /// Tier-5 offensive ground unit.
        /// </summary>
        Reign = 0xf7da,
        /// <summary>
        /// Tier-1 support ground unit.
        /// </summary>
        Nova = 0xf7fd,
        /// <summary>
        /// Tier-2 support ground unit.
        /// </summary>
        Pulsar = 0xf7fc,
        /// <summary>
        /// Tier-3 support ground unit.
        /// </summary>
        Quasar = 0xf7fb,
        /// <summary>
        /// Tier-4 support ground unit.
        /// </summary>
        Vela = 0xf7c1,
        /// <summary>
        /// Tier-5 support ground unit.
        /// </summary>
        Corvus = 0xf7c0,
        /// <summary>
        /// Tier-1 insectoid ground unit.
        /// </summary>
        Crawler = 0xf7fa,
        /// <summary>
        /// Tier-2 insectoid ground unit.
        /// </summary>
        Atrax = 0xf7f9,
        /// <summary>
        /// Tier-3 insectoid ground unit.
        /// </summary>
        Spiroct = 0xf7f8,
        /// <summary>
        /// Tier-4 insectoid ground unit.
        /// </summary>
        Arkyid = 0xf7f7,
        /// <summary>
        /// Tier-5 insectoid ground unit.
        /// </summary>
        Toxopid = 0xf7de,
        /// <summary>
        /// Tier-1 offensive air unit.
        /// </summary>
        Flare = 0xf7f6,
        /// <summary>
        /// Tier-2 offensive air unit.
        /// </summary>
        Horizon = 0xf7f5,
        /// <summary>
        /// Tier-3 offensive air unit.
        /// </summary>
        Zenith = 0xf7f4,
        /// <summary>
        /// Tier-4 offensive air unit.
        /// </summary>
        Antumbra = 0xf7f3,
        /// <summary>
        /// Tier-5 offensive air unit.
        /// </summary>
        Eclipse = 0xf7f2,
        /// <summary>
        /// Tier-1 support air unit.
        /// </summary>
        Mono = 0xf7f1,
        /// <summary>
        /// Tier-2 support air unit.
        /// </summary>
        Poly = 0xf7f0,
        /// <summary>
        /// Tier-3 support air unit.
        /// </summary>
        Mega = 0xf7ef,
        /// <summary>
        /// Tier-4 support air unit.
        /// </summary>
        Quad = 0xf7c3,
        /// <summary>
        /// Tier-5 support air unit.
        /// </summary>
        Oct = 0xf7c2,
        /// <summary>
        /// Tier-1 offensive naval unit.
        /// </summary>
        Risso = 0xf7e7,
        /// <summary>
        /// Tier-2 offensive naval unit.
        /// </summary>
        Minke = 0xf7ed,
        /// <summary>
        /// Tier-3 offensive naval unit.
        /// </summary>
        Bryde = 0xf7ec,
        /// <summary>
        /// Tier-4 offensive naval unit.
        /// </summary>
        Sei = 0xf7c4,
        /// <summary>
        /// Tier-5 offensive naval unit.
        /// </summary>
        Omura = 0xf7c6,
        /// <summary>
        /// Tier-1 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        Retusa = 0xf788,
        /// <summary>
        /// Tier-2 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        Oxynoe = 0xf784,
        /// <summary>
        /// Tier-3 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        Cyerce = 0xf783,
        /// <summary>
        /// Tier-4 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        Aegires = 0xf782,
        /// <summary>
        /// Tier-5 support naval unit.<br/>
        /// Not available on V6.
        /// </summary>
        Navanax = 0xf780,
        /// <summary>
        /// Tier-1 player unit.
        /// </summary>
        Alpha = 0xf7eb,
        /// <summary>
        /// Tier-2 player unit.
        /// </summary>
        Beta = 0xf7ea,
        /// <summary>
        /// Tier-3 player unit.
        /// </summary>
        Gamma = 0xf7e9
    }
}