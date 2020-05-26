using SmartMarketLibrary.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartMarketLibrary
{
    partial class Product : INotifyPropertyChanged
    {

        public Product(Product toCopy)
        {
            Id = toCopy.Id;
            Name = toCopy.Name;
            Manufacturer = toCopy.Manufacturer;
            NetPrice = toCopy.NetPrice;
            Quantity = toCopy.Quantity;
            Code = toCopy.Code;
            Margin = toCopy.Margin;
            Availability = toCopy.Availability;
            ProductGroup_Id = toCopy.ProductGroup_Id;
            Group = toCopy.Group;
            ProductsChanges = toCopy.ProductsChanges;
        }

        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Product product)
            {
                return Id == product.Id && Name == product.Name && Manufacturer == product.Manufacturer &&
                       NetPrice == product.NetPrice && Quantity == product.Quantity && Code == product.Code &&
                       Margin == product.Margin && Availability == product.Availability &&
                       ProductGroup_Id == product.ProductGroup_Id;
            }

            return false;
        }
        public decimal Price
        {
            get => NetPrice + Margin * NetPrice + (Group?.Vat ?? 0) * NetPrice;
        }

        public void set(Product toCopy)
        {
            Name = toCopy.Name;
            Manufacturer = toCopy.Manufacturer;
            NetPrice = toCopy.NetPrice;
            Quantity = toCopy.Quantity;
            Code = toCopy.Code;
            Margin = toCopy.Margin;
            Availability = toCopy.Availability;
            ProductGroup_Id = toCopy.ProductGroup_Id;
            Group = toCopy.Group;
            ProductsChanges = toCopy.ProductsChanges;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
