using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
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

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CreateBirdViewModelCommand { get; set; }
        public RelayCommand LoadBirdViewModelCommand { get; set; }
        public RelayCommand CountWingsCommand { get; set; }

        private string _newBirdName = "Enter Bird Name";
        private bool _birdCreated = false;
        private string _filePath;

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
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "XML Files (.xml)|*.xml"
                };

                // Show open file dialog box
                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    if (ValidateXml(dialog.FileName))
                    {
                        FilePath = dialog.FileName;
                    }
                }
            });

            CountWingsCommand = new RelayCommand(o =>
            {
                var dialog = new Microsoft.Win32.OpenFileDialog
                {
                    Multiselect = true,
                    Filter = "XML Files (.xml)|*.xml"
                };

                List<string> birdFiles = new List<string>();

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    foreach (string fileName in dialog.FileNames)
                    {
                        if (ValidateXml(fileName))
                        {
                            birdFiles.Add(fileName);
                        }
                    }
                }

                // LINQ queries can go multiple levels deep, here I'm selecting the file using the string, then selecting the "NumberOfWings" field
                int totalWings =
                    birdFiles
                        .Select(fileName => XDocument.Load(fileName)) // Load each XML
                        .Select(file => (int?)file.Root?.Element("NumberOfWings")) // Try to get <NumberOfWings>
                        .Where(numWings => numWings.HasValue) // Filter out nulls
                        .Sum(numWings => numWings.Value); // Sum the values

                MessageBox.Show($"Total wings found: {totalWings}");
            });
        }

        public void Reinitialize()
        {
            NewBirdName = "Enter Bird Name";
            FilePath = null;
            BirdCreated = false;
        }

        // Check that the Xml is valid against the BirdSchema.xsd, and if so
        private bool ValidateXml(string xmlPath)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();

            string birdSchema = "BirdMaker.Schemas.BirdSchema.xsd"; // would prefer not to hard code the schema location, but it's fine for this application

            // find and add the schema to schemas if valid
            using (var schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(birdSchema))
            {
                if (schemaStream == null)
                {
                    MessageBox.Show($"Could not find embedded schema: {birdSchema}");
                    return false;
                }

                schemas.Add(null, XmlReader.Create(schemaStream));
            }

            XmlReaderSettings settings = new XmlReaderSettings
            {
                Schemas = schemas,
                ValidationType = ValidationType.Schema
            };

            settings.ValidationEventHandler += (sender, e) =>
            {
                MessageBox.Show($"[{e.Severity}] {e.Message}", "XML Validation");
            };

            using (XmlReader reader = XmlReader.Create(xmlPath, settings))
            {
                try
                {
                    while (reader.Read()) { }
                    return true;
                }

                catch (XmlException ex)
                {
                    MessageBox.Show($"XML Exception: {ex.Message}", "Validation Error");
                    return false;
                }
            }
        }
    }
}
