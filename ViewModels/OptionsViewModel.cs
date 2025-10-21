using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningApp1.Core;

namespace BirdMaker.ViewModels
{
    internal class OptionsViewModel : ObservableObject
    {
        public string NewBirdName
        {
            get { return _newBirdName; }
            set
            {
                _newBirdName = value;
                OnPropertyChanged();
            }
        }

        public bool BirdCreated
        {
            get { return _birdCreated; }
            set
            {
                _birdCreated = value;
                OnPropertyChanged();
            }
        }

        public string FileLocation
        {
            get { return _fileLocation; }
            set
            {
                _fileLocation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CreateBirdViewModelCommand { get; set; }
        public RelayCommand LoadBirdViewModelCommand { get; set; }

        private string _newBirdName = "Enter Bird Name";
        private bool _birdCreated = false;
        private string _fileLocation;

        public OptionsViewModel()
        {
            CreateBirdViewModelCommand = new RelayCommand(o =>
            {
                if (NewBirdName != String.Empty && NewBirdName != "Enter Bird Name")
                {
                    BirdCreated = true;
                }
            });

            LoadBirdViewModelCommand = new RelayCommand(o =>
            {

            });
        }
    }
}
