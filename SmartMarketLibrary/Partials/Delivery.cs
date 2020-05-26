using SmartMarketLibrary.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartMarketLibrary
{
    partial class Delivery : INotifyPropertyChanged
    {

        private ObservableCollection<Supplier> _suppliers;

        public Delivery(Delivery toCopy)
        {
            Id = toCopy.Id;
            Invoice = toCopy.Invoice;
            Date = toCopy.Date;
            Manager_Id = toCopy.Manager_Id;
            DeliveriesChanges = toCopy.DeliveriesChanges;
            Employee = toCopy.Employee;
            ProductDeliveries = toCopy.ProductDeliveries;
            Supplier = toCopy.Supplier;
            SupplierId = toCopy.SupplierId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Delivery delivery)
            {
                return Id == delivery.Id && Invoice == delivery.Invoice && Date == delivery.Date &&
                       Manager_Id == delivery.Manager_Id &&
                       (Employee?.Equals(delivery.Employee) ?? delivery.Employee == null) &&
                       (ProductDeliveries?.Equals(delivery.ProductDeliveries) ?? delivery.ProductDeliveries == null);
            }

            return false;
        }

        public void set(Delivery toCopy)
        {
            Invoice = toCopy.Invoice;
            Date = toCopy.Date;
            ProductDeliveries = toCopy.ProductDeliveries;
            Supplier = toCopy.Supplier;
            SupplierId = toCopy.SupplierId;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
