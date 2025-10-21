using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public MainViewModel()
        {
            OptionsVm = new OptionsViewModel();
            OptionsVm.PropertyChanged += Options_PropertyChanged;

            CurrentView = OptionsVm;
        }

        private object _currentView;

        private void Options_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OptionsViewModel.BirdCreated) && OptionsVm.BirdCreated)
            {
                CreateBirdView();
            }

            if (e.PropertyName == nameof(OptionsViewModel.FileLocation))
            {
                // need to also check that this is a valid Bird, maybe we try to parse it here before trying to load it
                LoadBirdView();
            }
        }

        private void CreateBirdView()
        {
            BirdVm = new BirdViewModel(OptionsVm.NewBirdName);
            CurrentView = BirdVm;

            // else:
            // load Bird.xml
            // get Bird.xml.BirdName
            // set BirdVm to a new BirdViewModel with Bird.xml.BirdName
            // Set CurrentView to BirdVm

            // We need to get bird if loading, or make a new one with the input name and defaults if creating one
        }

        private void LoadBirdView()
        {
            // either create a birdvm or load a birdvm (not sure how xml read/write works), set BirdVm to the new birdvm, and set CurrentView to BirdVm
        }

        // need to bind to button events in BirdViewModel and OptionsViewModel to update MainViewModel's CurrentView
    }
}
