using BirdMaker.ViewModels;
using System;
using System.IO;
using System.Xml.Serialization;

namespace BirdMaker.Models
{
    public class Bird
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

        public Bird()
        {

        }

        public string Name;
        public bool CanFly;
        public bool HasTalons;
        public bool NeedsHelmet;
        public beakType BeakType;
        public color Color;
        public int NumberOfWings;
    }
}
