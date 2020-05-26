using SmartMarketLibrary.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartMarketLibrary
{
    partial class ProductDelivery : INotifyPropertyChanged
    {
        public ProductDelivery()
        {
        }
        public string Name
        {
            get => Product1?.Name ?? "";
        }



        public ProductDelivery(ProductDelivery toCopy)
        {
            Product = toCopy.Product;
            Delivery = toCopy.Delivery;
            Price = toCopy.Price;
            Quantity = toCopy.Quantity;
            Product1 = toCopy.Product1;
            Delivery1 = toCopy.Delivery1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is ProductDelivery productDelivery)
            {
                return Product1 == productDelivery.Product1 && Delivery == productDelivery.Delivery &&
                       Price == productDelivery.Price &&
                       Quantity == productDelivery.Quantity;
            }

            return false;
        }

        public void Set(ProductDelivery toCopy)
        {
            Product = toCopy.Product;
            Price = toCopy.Price;
            Quantity = toCopy.Quantity;
            Product1 = toCopy.Product1;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
