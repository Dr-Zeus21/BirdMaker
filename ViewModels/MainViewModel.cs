using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningApp1.Core;

namespace BirdMaker.ViewModels
{
    internal class MainViewModel : ObservableObject
    { 
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public OptionsViewModel OptionsVm { get; set; }

        public BirdViewModel BirdVm { get; set; }

        public RelayCommand BirdViewCommand { get; set; }

        public RelayCommand OptionsCommand { get; set; }

        private object _currentView;

        public MainViewModel()
        {
            OptionsVm = new OptionsViewModel();

            CurrentView = OptionsVm;

            BirdViewCommand = new RelayCommand(o =>
            {
                CurrentView = BirdVm;
            });

            OptionsCommand = new RelayCommand(o =>
            {
                CurrentView = OptionsVm;
            });
        }

        public void MakeBirdView()
        {
            // Two options, loaded bird or new bird
            // if new bird:
            // get Options.BirdName
            // set BirdVm to a new BirdViewModel with Options.BirdName
            // Set CurrentView to BirdVm

            // else:
            // load Bird.xml
            // get Bird.xml.BirdName
            // set BirdVm to a new BirdViewModel with Bird.xml.BirdName
            // Set CurrentView to BirdVm
        }
    }
}
