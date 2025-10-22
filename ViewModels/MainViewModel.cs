using System;
using System.ComponentModel;
using BirdMaker.Models;
using System.Xml;
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

            if (e.PropertyName == nameof(OptionsViewModel.FilePath) && OptionsVm.FilePath != null) 
            {
                LoadBirdView();
            }
        }

        // Used to clear out existing BirdVm when saving a bird
        private void Bird_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BirdViewModel.BirdSaved) && BirdVm.BirdSaved)
            {
                OptionsVm.NewBirdName = "Enter Bird Name";
                OptionsVm.FilePath = null;
                OptionsVm.BirdCreated = false;
                CurrentView = OptionsVm;

                BirdVm = null;
            }
        }

        private void CreateBirdView()
        {
            BirdVm = new BirdViewModel(OptionsVm.NewBirdName);
            BirdVm.PropertyChanged += Bird_PropertyChanged;

            CurrentView = BirdVm;
        }

        private void LoadBirdView()
        {
            BirdVm = new BirdViewModel(OptionsVm.NewBirdName);
            BirdVm.PropertyChanged += Bird_PropertyChanged;

            using (XmlReader reader = XmlReader.Create(OptionsVm.FilePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "Name": BirdVm.Name = reader.ReadElementContentAsString(); break;
                            case "CanFly": BirdVm.CanFly = reader.ReadElementContentAsBoolean(); break;
                            case "HasTalons": BirdVm.HasTalons = reader.ReadElementContentAsBoolean(); break;
                            case "NeedsHelmet": BirdVm.NeedsHelmet = reader.ReadElementContentAsBoolean(); break;
                            case "BeakType": Enum.TryParse(reader.ReadElementContentAsString(), out Bird.beakType bt); BirdVm.BeakType = bt; break;
                            case "Color": Enum.TryParse(reader.ReadElementContentAsString(), out Bird.color c); BirdVm.Color = c; break;
                            case "NumberOfWings": BirdVm.NumberOfWings = reader.ReadElementContentAsInt(); break;
                        }
                    }
                }
            }

            CurrentView = BirdVm;
        }

        // need to bind to button events in BirdViewModel and OptionsViewModel to update MainViewModel's CurrentView
    }
}
