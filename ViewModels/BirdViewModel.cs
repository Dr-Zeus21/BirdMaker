using System;
using System.IO;
using System.Xml;
using BirdMaker.Models;
using LearningApp1.Core;

namespace BirdMaker.ViewModels
{
    internal class BirdViewModel : ObservableObject
    {
        public string Name
        {
            get => (_bird.Name);
            set
            {
                _bird.Name = value;
                OnPropertyChanged();
            }
        }

        public bool CanFly
        {
            get => (_bird.CanFly);
            set
            {
                _bird.CanFly = value;
                OnPropertyChanged();
            }
        }

        public bool HasTalons
        {
            get => (_bird.HasTalons);
            set
            {
                _bird.HasTalons = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfWings
        {
            get => (_bird.NumberOfWings);
            set
            {
                _bird.NumberOfWings = value;
                OnPropertyChanged();
            }
        }

        public bool NeedsHelmet
        {
            get => (_bird.NeedsHelmet);
            set
            {
                _bird.NeedsHelmet = value;
                OnPropertyChanged();
            }
        }

        public Bird.color Color
        {
            get => (_bird.Color);
            set
            {
                _bird.Color = value;
                OnPropertyChanged();
            }
        }

        public Bird.beakType BeakType
        {
            get => (_bird.BeakType);
            set
            {
                _bird.BeakType = value;
                OnPropertyChanged();
            }
        }

        public bool BirdSaved
        {
            get => (_birdSaved);
            set
            {
                _birdSaved = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveBirdViewModelCommand { get; set; }

        public BirdViewModel(string inName, bool inCanFly = true, bool inHasTalons = true, int inNumberOfWings = 2, bool inNeedsHelmet = false, Bird.color inColor = Bird.color.Gray, Bird.beakType inBeakType = Bird.beakType.Normal)
        {
            _bird = new Bird();

            Name = inName;
            CanFly = inCanFly;
            HasTalons = inHasTalons;
            NumberOfWings = inNumberOfWings;
            NeedsHelmet = inNeedsHelmet;
            Color = inColor;
            BeakType = inBeakType;

            SaveBirdViewModelCommand = new RelayCommand(o =>
            {
                if (CheckParameters())
                {
                    SaveBirdToXml();
                    BirdSaved = true;
                }
            });
        }

        private Bird _bird;

        private bool _birdSaved = false;
        private bool CheckParameters()
        {
            if (NumberOfWings < 0)
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(Bird.color), Color))
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(Bird.beakType), BeakType))
            {
                return false;
            }
            return true;
        }

        // Wites the bird using Name.xml to the debug folder
        private void SaveBirdToXml()
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineOnAttributes = false
            };

            // look into XDocument, make these Attributes instead of Elements
            using (XmlWriter writer = XmlWriter.Create(Name + ".xml", settings))
            {
                writer.WriteStartDocument();
                    writer.WriteStartElement("Bird");

                        writer.WriteElementString("Name", Name);
                        writer.WriteElementString("CanFly", CanFly.ToString().ToLower());
                        writer.WriteElementString("HasTalons", HasTalons.ToString().ToLower());
                        writer.WriteElementString("NeedsHelmet", NeedsHelmet.ToString().ToLower());
                        writer.WriteElementString("BeakType", BeakType.ToString());
                        writer.WriteElementString("Color", Color.ToString());
                        writer.WriteElementString("NumberOfWings", NumberOfWings.ToString());

                    writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
