using Diversion.Common;
using Diversion.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Diversion
{
    public class WindowViewModel : ViewModelBase
    {
        public ICollectionView animalsView { get; set; }

        private Animal _CurrentItem;
        public Animal CurrentItem
        {
            get => _CurrentItem;
            set
            {
                _CurrentItem = value;
                OnPropertyChanged();
            }
        }

        public string filter { get; set; }

        public WindowViewModel()
        {
            List<Animal> animals = new List<Animal>();
            animals.Add(new Animal("Dog", 5, "Home"));
            animals.Add(new Animal("Squirrel", 1, "Forest"));
            animals.Add(new Animal("Whale", 5, "Water"));
            animals.Add(new Animal("Cat", 2, "Home"));
            animals.Add(new Animal("Dolphin", 6, "Water"));
            animalsView = CollectionViewSource.GetDefaultView(animals);

            animalsView.CurrentChanged += delegate
            {
                CurrentItem = (Animal)animalsView.CurrentItem;
            };
        }

        ICommand _FilterCommand;
        public ICommand FilterCommand
        {
            get
            {
                if (_FilterCommand == null)
                    _FilterCommand = new RelayCommand(param => Filter());
                return _FilterCommand;
            }
        }
        void Filter()
        {
            animalsView.Filter = delegate (object item)
            {
                Animal a = item as Animal;
                return a.name.ToLower().Contains(filter.ToLower());
            };
        }

        ICommand _SortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                    _SortCommand = new RelayCommand(param => Sort(param.ToString()));
                return _SortCommand;
            }
        }
        void Sort(string type)
        {
            // For 
            if (type != "none")
            {
                animalsView.SortDescriptions.Clear();
                animalsView.SortDescriptions.Add(new SortDescription(type, ListSortDirection.Ascending));
            }
            else
            {
                animalsView.SortDescriptions.Clear();
            }

        }

        ICommand _GroupCommand;
        public ICommand GroupCommand
        {
            get
            {
                if (_GroupCommand == null)
                    _GroupCommand = new RelayCommand(param => Group(param.ToString()));
                return _GroupCommand;
            }
        }
        void Group(string type)
        {
            if (type != "none")
            {
                animalsView.GroupDescriptions.Clear();
                animalsView.GroupDescriptions.Add(new PropertyGroupDescription(type));
            }
            else
            {
                animalsView.GroupDescriptions.Clear();
            }
        }
    }
}
