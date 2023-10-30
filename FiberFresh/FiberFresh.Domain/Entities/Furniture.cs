using System.ComponentModel;

namespace FiberFresh.Domain.Entities
{
    public enum Furniture
    {
        [Description("Dywan")]
        Carpet,
        [Description("Wykładzina")]
        Facing,
        [Description("Narożnik (Kształt L)")]
        CornerL,
        [Description("Narożnik (Kształt U lub C)")]
        CornerUC,
        [Description("Materac")]
        Mattress,
        [Description("Sofa")]
        Sofa,
        [Description("Kanapa")]
        Couch,
        [Description("Fotel")]
        Armchair,
        [Description("Krzesło")]
        Chair,
        [Description("Pufa (Ottoman)")]
        Pouf,
        [Description("Ława")]
        Bench,
        [Description("Stołek")]
        Stool,
        [Description("Hoker barowy")]
        BarStool,
        [Description("Lóżko")]
        Bed,
        [Description("Szezlong")]
        ChaiseLongue,
        [Description("Kozetka")]
        Settee,
        [Description("Podnóżek")]
        Footrest,
        [Description("Tapicerka samochodowa - pakiet podstawowy")]
        CarUpholsteryBasic,
        [Description("Tapicerka samochodowa - pakiet rozszerzony")]
        CarUpholsteryExtended
    }
}
