// Ignore Spelling: CBM

namespace FiberFresh.Domain.Entities
{
    public class Service
    {
        private static readonly Dictionary<Furniture, int> _itemParams = new()
        {
            { Furniture.Carpet, 15 },
            { Furniture.Facing, 15 },
            { Furniture.CornerL, 250 },
            { Furniture.CornerUC, 300 },
            { Furniture.Mattress, 80 },
            { Furniture.Sofa, 200 },
            { Furniture.Couch, 200 },
            { Furniture.Armchair, 60 },
            { Furniture.Chair, 15 },
            { Furniture.Pouf, 20 },
            { Furniture.Bench, 40 },
            { Furniture.Stool, 15 },
            { Furniture.BarStool, 20 },
            { Furniture.Bed, 100 },
            { Furniture.ChaiseLongue, 80 },
            { Furniture.Settee, 50 },
            { Furniture.Footrest, 20 },
            { Furniture.CarUpholsteryBasic, 250 },
            { Furniture.CarUpholsteryExtended, 400 }
        };

        private static readonly Dictionary<Fabric, float> _fabricParams = new()
        {
            { Fabric.Polyester, 1.0f },
            { Fabric.Nylon, 1.0f },
            { Fabric.Polypropylene, 1.0f },
            { Fabric.Acrylic, 1.0f },
            { Fabric.Microfiber, 1.1f },
            { Fabric.FauxLeather, 1.0f },
            { Fabric.Vinylon, 1.0f },
            { Fabric.Triexta, 1.1f },
            { Fabric.Viscose, 1.0f },
            { Fabric.Polyamide, 1.0f },
            { Fabric.Cordura, 1.1f },
            { Fabric.Alcantara, 1.1f },
            { Fabric.Carabu, 1.2f },
            { Fabric.Penta, 1.2f },
            { Fabric.Olefin, 1.0f },
            { Fabric.Cotton, 1.0f },
            { Fabric.Linen, 1.1f },
            { Fabric.Silk, 1.0f },
            { Fabric.Wool, 1.0f },
            { Fabric.GenuineLeather, 1.2f },
            { Fabric.Coconut, 1.1f },
            { Fabric.Jute, 1.2f },
            { Fabric.Sisal, 1.2f },
            { Fabric.Ramie, 1.1f },
            { Fabric.Plush, 1.2f },
            { Fabric.Corduroy, 1.1f },
            { Fabric.Other, 1.0f }
        };

        private static readonly Dictionary<Size, float> _sizeParams = new()
        {
            { Size.Small, 1.0f },
            { Size.Medium, 1.2f },
            { Size.Large, 1.4f }
        };

        public Guid Id { get; set; }
        public Furniture Furniture { get; set; }
        public Fabric Fabric { get; set; }
        public float CBM { get; set; }
        public Size Size { get; set; }
        public PriceRange PriceRange { get; set; }

        public Service(Furniture furniture, Fabric fabric, float cbm)
        {
            Furniture = furniture;
            Fabric = fabric;
            CBM = cbm;

            PriceRange = new PriceRange
            (
                (int)(_itemParams[furniture] * _fabricParams[fabric] * cbm),
                (int)(_itemParams[furniture] * _fabricParams[fabric] * cbm * 1.5f)
            );
        }

        public Service(Furniture furniture, Fabric fabric, Size size)
        {
            Furniture = furniture;
            Fabric = fabric;
            Size = size;

            PriceRange = new PriceRange
            (
                (int)(_itemParams[furniture] * _fabricParams[fabric] * _sizeParams[size]),
                (int)(_itemParams[furniture] * _fabricParams[fabric] * _sizeParams[size] * 1.2f)
            );
        }
    }
}
