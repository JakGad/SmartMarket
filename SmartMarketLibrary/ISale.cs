using System;
using System.Collections.Generic;

namespace SmartMarketLibrary
{
    interface ISale
    {
        int Id { get; set; }
        Employee Seller { get; }
        IReadOnlyList<Tuple<Product, int, decimal>> Sold { get; }
        DateTime Date { get; }
    }

    interface ICashierSale : ISale
    {
        new Employee Seller { get; set; }
        new IReadOnlyList<Tuple<Product, int, decimal>> Sold { get; set; }
        new DateTime Date { get; set; }
    }
}
