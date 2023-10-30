using System.ComponentModel;

namespace FiberFresh.Domain.Entities
{
    public enum Fabric
    {
        [Description("Poliester")]
        Polyester,
        [Description("Nylon")]
        Nylon,
        [Description("Polipropylen (Poliwłókno)")]
        Polypropylene,
        [Description("Akryl")]
        Acrylic,
        [Description("Mikrofibra")]
        Microfiber,
        [Description("Sztuczna skóra (Eko skóra, Skaj)")]
        FauxLeather,
        [Description("Winyl")]
        Vinylon,
        [Description("Triexta")]
        Triexta,
        [Description("Wiskoza")]
        Viscose,
        [Description("Poliamid")]
        Polyamide,
        [Description("Cordura")]
        Cordura,
        [Description("Alcantara")]
        Alcantara,
        [Description("Carabu")]
        Carabu,
        [Description("Penta")]
        Penta,
        [Description("Olefin")]
        Olefin,

        [Description("Bawełna")]
        Cotton,
        [Description("Len")]
        Linen,
        [Description("Jedwab")]
        Silk,
        [Description("Wełna")]
        Wool,
        [Description("Naturalna skóra")]
        GenuineLeather,
        [Description("Kokos")]
        Coconut,
        [Description("Juta")]
        Jute,
        [Description("Sizal")]
        Sisal,
        [Description("Ramia")]
        Ramie,
        [Description("Plusz (Aksamit, Welur, Welwet, Manchester)")]
        Plush,
        [Description("Sztruks")]
        Corduroy,

        [Description("Inne (Opisz materiał w opisie w następnym kroku)")]
        Other
    }
}
