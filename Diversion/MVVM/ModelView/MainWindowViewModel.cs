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

namespace Diversion.MVVM.ModelView
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ListCollectionView animalsView { get; set; }

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

        public MainWindowViewModel()
        {
            List<Animal> animals = new List<Animal>
            {
                new Animal("Dog", 5, "Home"),
                new Animal("Squirrel", 1, "Forest"),
                new Animal("Whale", 5, "Water"),
                new Animal("Cat", 2, "Home"),
                new Animal("Dolphin", 6, "Water")
            };
            animalsView = (ListCollectionView)CollectionViewSource.GetDefaultView(animals);
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

            if (type != "none")
            {
                animalsView.CustomSort = new CustomSort(type);
            }
            else
            {
                animalsView.CustomSort = null;
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
