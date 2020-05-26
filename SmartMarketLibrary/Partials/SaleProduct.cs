namespace SmartMarketLibrary
{
    partial class SaleProduct
    {
        public SaleProduct()
        {
        }
        public string Name
        {
            get => Product1?.Name ?? "";
        }
        public decimal DefaultPrice
        {
            get => Product1?.Price ?? 0;
        }
        public SaleProduct(SaleProduct toCopy)
        {
            Product = toCopy.Product;
            Sale = toCopy.Sale;
            Price = toCopy.Price;
            Quantity = toCopy.Quantity;
            // Product1 = toCopy.Product1;
            // Sale1 = toCopy.Sale1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is SaleProduct saleproduct)
            {
                return Product1 == saleproduct.Product1 && Sale == saleproduct.Sale && Price == saleproduct.Price &&
                       Quantity == saleproduct.Quantity;
            }

            return false;
        }
    }
}
