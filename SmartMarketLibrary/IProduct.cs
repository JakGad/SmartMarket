using System.Collections.Generic;

namespace SmartMarketLibrary
{
    interface IProduct
    {
        int Id { get; set; }
        string Name { get; }
        string Manufacturer { get; }
        decimal Price { get; }
        string Code { get; }
        Group ProductGroup { get; }
    }

    interface IManagerProduct : IProduct
    {
        new string Name { get; set; }
        new string Manufacturer { get; set; }
        decimal NetPrice { get; set; }
        int Quantity { get; set; }
        new string Code { get; set; }
        decimal Margin { get; set; }
        bool Availability { get; set; }
        new Group ProductGroup { get; set; }
        IReadOnlyList<ProductChange> Changes { get; }
    }
}
