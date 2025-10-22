using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
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
        }

        // Check that the Xml is valid against the BirdSchema.xsd, and if so
        private bool ValidateXml(string xmlPath)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();

            string birdSchema = "BirdMaker.Schemas.BirdSchema.xsd"; // would prefer not to hard code the schema location, but it's fine for this application

            // find and add the schema to schemas if valid
            using (Stream? schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(birdSchema))
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
