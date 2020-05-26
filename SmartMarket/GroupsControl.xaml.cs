using SmartMarket.Annotations;
using SmartMarketLibrary;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SmartMarket
{
    /// <summary>
    /// Interaction logic for GroupsControl.xaml
    /// </summary>
    public partial class GroupsControl : UserControl, INotifyPropertyChanged
    {
        public GroupsControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private DatabaseServices _database;
        internal void Initialize(DatabaseServices database)
        {
            _database = database;
            try
            {
                Groups = new ObservableCollection<Group>(_database.GetGroups());
            }
            catch (Exception)
            {
                ErrorConnectingDialog.IsOpen = true;
            }
            SelectedGroup = null;
        }

        public Group SelectedGroup { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            var group = new Group();
            Groups.Add(group);
            SelectedGroup = group;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                var groups = _database.GetGroups();
                foreach (var group in Groups)
                {
                    if (group.Id == 0)
                    {
                        _database.AddGroup(group);
                    }
                    else
                    {
                        var temp = groups.FirstOrDefault(x => x.Id == group.Id);
                        if (temp != null && !group.Equals(temp))
                        {
                            _database.UpdateGroup(group);
                        }
                    }
                }
            }
            catch (Exception)
            {
                ErrorConnectingDialog.IsOpen = true;
            }
        }
    }
}
