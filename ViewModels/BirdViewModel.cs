using LearningApp1.Core;

namespace BirdMaker.ViewModels
{
    public class BirdViewModel : ObservableObject
    {
        // Beak types that a bird can have
        public enum beakType
        {
            Normal,
            Hooked,
            Pointy,
            Jagged,
            UpsideDown
        }

        // Color options for birds
        public enum color
        {
            Gray,
            White,
            Black,
            Brown,
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Indigo,
            Violet
        }
        public string Name
        {
            get => (_name);
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public bool CanFly
        {
            get => (_canFly);
            set
            {
                _canFly = value;
                OnPropertyChanged();
            }
        }
        public bool HasTalons
        {
            get => (_hasTalons);
            set
            {
                _hasTalons = value;
                OnPropertyChanged();
            }
        }
        public int NumberOfWings
        {
            get => (_numberOfWings);
            set
            {
                _numberOfWings = value;
                OnPropertyChanged();
            }
        }
        public bool NeedsHelmet
        {
            get => (_needsHelmet);
            set
            {
                _needsHelmet = value;
                OnPropertyChanged();
            }
        }
        public color Color
        {
            get => (_color);
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
        public beakType BeakType
        {
            get => (_beakType);
            set
            {
                _beakType = value;
                OnPropertyChanged();
            }
        }

        public BirdViewModel(string inName, bool inCanFly = true, bool inHasTalons = true, int inNumberOfWings = 2, bool inNeedsHelmet = false, color inColor = color.Gray, beakType inBeakType = beakType.Normal)
        {
            Name = inName;
            CanFly = inCanFly;
            HasTalons = inHasTalons;
            NumberOfWings = inNumberOfWings;
            NeedsHelmet = inNeedsHelmet;
            Color = inColor;
            BeakType = inBeakType;
        }

        private string _name;
        private bool _canFly;
        private bool _hasTalons;
        private int _numberOfWings;
        private bool _needsHelmet;
        private color _color;
        private beakType _beakType;
    }
}
