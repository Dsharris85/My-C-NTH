using Demo.View.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Demo.View.Commands;
using Demo.View.Views;
using System.Xml;
using Microsoft.Win32;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Demo.View
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(Mixer mixer)
        {
            // For display on wavetype combobox
            DisplayWaveTypeDict = new Dictionary<String, String>(){};
            DisplayWaveTypeDict.Add("Sine Wave", "./Views/Images/panels/sineIcon.png");
            DisplayWaveTypeDict.Add("Square Wave", "./Views/Images/panels/squareIcon.png");
            DisplayWaveTypeDict.Add("Triangle Wave", "./Views/Images/panels/triangleIcon.png");
            DisplayWaveTypeDict.Add("SawTooth Wave", "./Views/Images/panels/sawtoothIcon.png");
            DisplayWaveTypeDict.Add("Noise", "./Views/Images/panels/noiseIcon.png");
            DisplayWaveTypeDict.Add("Robot", "./Views/Images/panels/robotIcon.png");
            
            // New Windows
            SettingsCommand = new SettingsWindowCommand(this);
            Synth2Command = new Synth2WindowCommand(this);
            PlayerCommand = new PlayerWindowCommand(this);

            // Save/load
            SavePresetCommand = new SaveCommand(this);
            LoadPresetCommand = new LoadCommand(this);
            DefaultPresetCommand = new PresetCommand(this);

            // Quit
            QuitAppCommand = new QuitCommand(this);

            // New VMs
            PlayerVM = new PlayerViewModel();
            Synth2VM = new Synth2ViewModel();
            SettingsVM = new SettingsViewModel(this);

            Mixer = mixer;
        }

        // Note Brushes for KB Visual
        private SolidColorBrush testA = new SolidColorBrush(Colors.White);
        public SolidColorBrush TestA
        {
            get => testA;
            set
            {
                if (value.Equals(testA))
                    return;
                testA = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testB = new SolidColorBrush(Colors.White);
        public SolidColorBrush TestB
        {
            get => testB;
            set
            {
                if (value.Equals(testB))
                    return;
                testB = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testC = new SolidColorBrush(Colors.White);
        public SolidColorBrush TestC
        {
            get => testC;
            set
            {
                if (value.Equals(testC))
                    return;
                testC = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testC2 = new SolidColorBrush(Colors.White);
        public SolidColorBrush TestC2
        {
            get => testC2;
            set
            {
                if (value.Equals(testC2))
                    return;
                testC2 = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testD = new SolidColorBrush(Colors.White);
        public SolidColorBrush TestD
        {
            get => testD;
            set
            {
                if (value.Equals(testD))
                    return;
                testD = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testE = new SolidColorBrush(Colors.White);
        public SolidColorBrush TestE
        {
            get => testE;
            set
            {
                if (value.Equals(testE))
                    return;
                testE = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testF = new SolidColorBrush(Colors.White);
        public SolidColorBrush TestF
        {
            get => testF;
            set
            {
                if (value.Equals(testF))
                    return;
                testF = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testG = new SolidColorBrush(Colors.White);
        public SolidColorBrush TestG
        {
            get => testG;
            set
            {
                if (value.Equals(testG))
                    return;
                testG = value;

                OnPropertyChanged();
            }
        }

        private SolidColorBrush testCS = new SolidColorBrush(Colors.Black);
        public SolidColorBrush TestCS
        {
            get => testCS;
            set
            {
                if (value.Equals(testCS))
                    return;
                testCS = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testEb = new SolidColorBrush(Colors.Black);
        public SolidColorBrush TestEb
        {
            get => testEb;
            set
            {
                if (value.Equals(testEb))
                    return;
                testEb = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testFS = new SolidColorBrush(Colors.Black);
        public SolidColorBrush TestFS
        {
            get => testFS;
            set
            {
                if (value.Equals(testFS))
                    return;
                testFS = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testAb = new SolidColorBrush(Colors.Black);
        public SolidColorBrush TestAb
        {
            get => testAb;
            set
            {
                if (value.Equals(testAb))
                    return;
                testAb = value;

                OnPropertyChanged();
            }
        }
        private SolidColorBrush testBb = new SolidColorBrush(Colors.Black);
        public SolidColorBrush TestBb
        {
            get => testBb;
            set
            {
                if (value.Equals(testBb))
                    return;
                testBb = value;

                OnPropertyChanged();
            }
        }

        public Dictionary<String, String> DisplayWaveTypeDict { get; }

        private KeyValuePair<string, string> displayWaveType = new KeyValuePair<string, string>("Sine Wave", "./Views/Images/panels/sineIcon.png");

        // Sure theres a better way :/
        public KeyValuePair<String, String> DisplayWaveType 
        {
            get
            {
                switch (WaveType)
                {

                    case WaveTypeSynth1.Sine:
                        return new KeyValuePair<string, string>("Sine Wave", "./Views/Images/panels/sineIcon.png");
                    case WaveTypeSynth1.Square:
                        return new KeyValuePair<string, string>("Square Wave", "./Views/Images/panels/squareIcon.png");
                    case WaveTypeSynth1.Triangle:
                        return new KeyValuePair<string, string>("Triangle Wave", "./Views/Images/panels/triangleIcon.png");
                    case WaveTypeSynth1.SawTooth:
                        return new KeyValuePair<string, string>("SawTooth Wave", "./Views/Images/panels/sawtoothIcon.png");
                    case WaveTypeSynth1.Noise:
                        return new KeyValuePair<string, string>("Noise Wave", "./Views/Images/panels/noiseIcon.png");
                    case WaveTypeSynth1.Robot:
                        return new KeyValuePair<string, string>("Robot Wave", "./Views/Images/panels/robotIcon.png");
                    default:
                        return new KeyValuePair<string, string>("Sine Wave", "./Views/Images/panels/sineIcon.png");
                }
            }
            set
            {

                if (value.Equals(displayWaveType))
                    return;

                displayWaveType = value;
                
                switch (displayWaveType.Key)
                {
                    case "Sine Wave":
                        WaveType = WaveTypeSynth1.Sine;
                        break;
                    case "Square Wave":
                        WaveType = WaveTypeSynth1.Square;
                        break;
                    case "Triangle Wave":
                        WaveType = WaveTypeSynth1.Triangle;
                        break;
                    case "SawTooth Wave":
                        WaveType = WaveTypeSynth1.SawTooth;
                        break;
                    case "Noise":
                        WaveType = WaveTypeSynth1.Noise;
                        break;
                    case "Robot":
                        WaveType = WaveTypeSynth1.Robot;
                        break;
                }

                OnPropertyChanged();
            }
        }    
        public ObservableCollection<Tuple<String,String>> DisplayWaveItems = new ObservableCollection<Tuple<String, String>>();

        // Close app
        public void Exit()
        {
            App.Current.Shutdown();
        }

        public void NewSettingsWindow()
        {
            // Player window
            SettingsView settingsView = new SettingsView(SettingsVM);
            settingsView.Closing += CloseSettings;
            
            CanOpenSettings = false;
            settingsView.Show();
        }

        public void NewPlayerWindow()
        {
            // Player window
            PlayerView playerView = new PlayerView(PlayerVM, ref Mixer, ModelToSettings);
            playerView.Closed += ClosePlayer;
            
            CanOpenPlayer = false;
            playerView.Show();            
        }

        public void NewSynth2Window()
        {
            // Synth2 Window
            Synth2View synth2View = new Synth2View(Synth2VM, ref Mixer, ModelToSettings);
            synth2View.Closed += CloseSynth2;            

            CanOpenSynth2 = false;
            synth2View.Show();
        }

        public void ResetPreset()
        {
            SettingsToModel(new ViewSettings());
            DisplayWaveType = new KeyValuePair<string, string>("Sine Wave", "./Views/Images/panels/sineIcon.png");
            SampleRate = 5;
        }

        // Open file and set accordingly -- could've implemented nicer, sorry
        public void OpenFile()
        {
            // Get file
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();            
            String file = openFileDialog1.FileName.ToString();

            // Schema
            String schema = @"XML\schema.xsd"; 

            // Validate
            XmlReader reader = null;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add("http://myproject343.edu", schema);
            
            try
            {
                reader = XmlReader.Create(file, settings);
                while (reader.Read()) { }
            }
            catch (Exception guy)
            {
                //Console.WriteLine($"Validation Failed D: \n {guy.Message}");
            }
            finally
            {
                reader?.Close();
            }

            //Console.WriteLine($"Validation Successfull :D");

            // Read from file
            if (!String.IsNullOrEmpty(openFileDialog1.FileName.ToString()))
            {                
                XmlDocument xml = new XmlDocument();
                xml.Load(openFileDialog1.FileName);

                // Synth 1
                foreach (XmlNode xmlNode in xml.GetElementsByTagName("waveType"))
                {
                    switch (xmlNode.InnerText)
                    {
                        case "Sine":
                            WaveType = WaveTypeSynth1.Sine;
                            DisplayWaveType = new KeyValuePair<string, string>("Sine Wave", "./Views/Images/panels/sineIcon.png");
                            break;
                        case "Square":
                            WaveType = WaveTypeSynth1.Square;
                            DisplayWaveType = new KeyValuePair<string, string>("Square Wave", "./Views/Images/panels/squareIcon.png");
                            break;
                        case "Triangle":
                            WaveType = WaveTypeSynth1.Triangle;
                            DisplayWaveType = new KeyValuePair<string, string>("Triangle Wave", "./Views/Images/panels/triangleIcon.png");
                            break;
                        case "SawTooth":
                            WaveType = WaveTypeSynth1.SawTooth;
                            DisplayWaveType = new KeyValuePair<string, string>("SawTooth Wave", "./Views/Images/panels/sawtoothIcon.png");
                            break;
                        case "Noise":
                            WaveType = WaveTypeSynth1.Noise;
                            DisplayWaveType = new KeyValuePair<string, string>("Noise Wave", "./Views/Images/panels/noiseIcon.png");
                            break;
                        case "Robot":
                            WaveType = WaveTypeSynth1.Robot;
                            DisplayWaveType = new KeyValuePair<string, string>("Robot Wave", "./Views/Images/panels/robotIcon.png");
                            break;
                        default:
                            WaveType = WaveTypeSynth1.Sine;
                            DisplayWaveType = new KeyValuePair<string, string>("Sine Wave", "./Views/Images/panels/sineIcon.png");
                            break;
                    }
                }               

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("octave"))
                {                    
                    XmlNode vol = xmlNode.NextSibling;
                    
                    Octave = int.Parse(xmlNode.InnerText);
                    Volume = float.Parse(vol.InnerText);
                }

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("sampleRate"))
                {
                    XmlNode depth = xmlNode.NextSibling;

                    SampleRate = int.Parse(xmlNode.InnerText);
                    BitDepth = int.Parse(depth.InnerText);
                }              

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("frequency"))
                {
                    XmlNode vol = xmlNode.NextSibling;                    
                    Volume = int.Parse(vol.InnerText);
                }

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("usingEnvelope"))
                {                   
                    XmlNode atk = xmlNode.NextSibling;
                    XmlNode dky = atk.NextSibling;
                    XmlNode sus = dky.NextSibling;
                    XmlNode rel = sus.NextSibling;

                    UsingEnvelope = bool.Parse(xmlNode.InnerText);
                    EnvelopeAttackTime = float.Parse(atk.InnerText);
                    EnvelopeDecayTime = float.Parse(dky.InnerText);
                    EnvelopeSustainLevel = float.Parse(sus.InnerText);
                    EnvelopeReleaseTime = float.Parse(rel.InnerText);
                }

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("lpOn"))
                {
                    XmlNode hpOn = xmlNode.NextSibling;
                    XmlNode lpCutoff = hpOn.NextSibling;
                    XmlNode lpBand = lpCutoff.NextSibling;
                    XmlNode hpCutoff = lpBand.NextSibling;
                    XmlNode hpBand = hpCutoff.NextSibling;

                    LowPassOn = bool.Parse(xmlNode.InnerText);
                    HighPassOn = bool.Parse(hpOn.InnerText);

                    LowPassCutoff = float.Parse(lpCutoff.InnerText);
                    HighPassCutoff = float.Parse(hpCutoff.InnerText);

                    LowPassBandwidth = float.Parse(lpBand.InnerText);                    
                    HighPassBandwidth = float.Parse(hpBand.InnerText);
                }

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("distThreshold"))
                {
                    XmlNode distOn = xmlNode.NextSibling;

                    DistortionThreshold = float.Parse(xmlNode.InnerText);
                    DistortionOn = bool.Parse(distOn.InnerText);
                }

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("tremeloRate"))
                {
                    XmlNode tremOn = xmlNode.NextSibling;
                    XmlNode type = tremOn.NextSibling;

                    TremeloRate = int.Parse(xmlNode.InnerText);
                    TremeloOn = bool.Parse(tremOn.InnerText);

                    switch (type.InnerText)
                    {
                        case "Sine":
                            TremeloType = WaveTypeSynth1.Sine;
                            break;
                        case "Square":
                            TremeloType = WaveTypeSynth1.Square;
                            break;
                        case "Triangle":
                            TremeloType = WaveTypeSynth1.Triangle;
                            break;
                        case "SawTooth":
                            TremeloType = WaveTypeSynth1.SawTooth;
                            break;
                        case "Noise":
                            TremeloType = WaveTypeSynth1.Noise;
                            break;
                        case "Robot":
                            TremeloType = WaveTypeSynth1.Robot;
                            break;
                        default:
                            TremeloType = WaveTypeSynth1.Sine;
                            break;
                    }
                }

                // Synth 2
                foreach (XmlNode xmlNode in xml.GetElementsByTagName("osc1Gain"))
                {
                    XmlNode osc2 = xmlNode.NextSibling;
                    XmlNode osc3 = osc2.NextSibling;

                    Synth2VM.Osc1Gain = float.Parse(xmlNode.InnerText);
                    Synth2VM.Osc2Gain = float.Parse(osc2.InnerText);
                    Synth2VM.Osc3Gain = float.Parse(osc3.InnerText);
                }

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("Osc1Octave"))
                {
                    XmlNode osc2 = xmlNode.NextSibling;
                    XmlNode osc3 = osc2.NextSibling;

                    Synth2VM.Osc1Octave = int.Parse(xmlNode.InnerText);
                    Synth2VM.Osc2Octave = int.Parse(osc2.InnerText);
                    Synth2VM.Osc3Octave = int.Parse(osc3.InnerText);
                }

                switch(xml.GetElementsByTagName("Osc1Type")[0].InnerText)
                {
                    case "Sin":
                        Synth2VM.Osc1Type = WaveTypeSynth2.Sin;
                        break;
                    case "Square":
                        Synth2VM.Osc1Type = WaveTypeSynth2.Square;
                        break;
                    case "Triangle":
                        Synth2VM.Osc1Type = WaveTypeSynth2.Triangle;
                        break;
                    case "SawTooth":
                        Synth2VM.Osc1Type = WaveTypeSynth2.SawTooth;
                        break;
                    case "White":
                        Synth2VM.Osc1Type = WaveTypeSynth2.White;
                        break;
                    case "Pink":
                        Synth2VM.Osc1Type = WaveTypeSynth2.Pink;
                        break;
                    case "Sweep":
                        Synth2VM.Osc1Type = WaveTypeSynth2.Sweep;
                        break;
                    default:
                        Synth2VM.Osc1Type = WaveTypeSynth2.Sin;
                        break;
                }

                switch (xml.GetElementsByTagName("Osc2Type")[0].InnerText)
                {
                    case "Sin":
                        Synth2VM.Osc2Type = WaveTypeSynth2.Sin;
                        break;
                    case "Square":
                        Synth2VM.Osc2Type = WaveTypeSynth2.Square;
                        break;
                    case "Triangle":
                        Synth2VM.Osc2Type = WaveTypeSynth2.Triangle;
                        break;
                    case "SawTooth":
                        Synth2VM.Osc2Type = WaveTypeSynth2.SawTooth;
                        break;
                    case "White":
                        Synth2VM.Osc2Type = WaveTypeSynth2.White;
                        break;
                    case "Pink":
                        Synth2VM.Osc2Type = WaveTypeSynth2.Pink;
                        break;
                    case "Sweep":
                        Synth2VM.Osc2Type = WaveTypeSynth2.Sweep;
                        break;
                    default:
                        Synth2VM.Osc2Type = WaveTypeSynth2.Sin;
                        break;
                }

                switch (xml.GetElementsByTagName("Osc3Type")[0].InnerText)
                {
                    case "Sin":
                        Synth2VM.Osc3Type = WaveTypeSynth2.Sin;
                        break;
                    case "Square":
                        Synth2VM.Osc3Type = WaveTypeSynth2.Square;
                        break;
                    case "Triangle":
                        Synth2VM.Osc3Type = WaveTypeSynth2.Triangle;
                        break;
                    case "SawTooth":
                        Synth2VM.Osc3Type = WaveTypeSynth2.SawTooth;
                        break;
                    case "White":
                        Synth2VM.Osc3Type = WaveTypeSynth2.White;
                        break;
                    case "Pink":
                        Synth2VM.Osc3Type = WaveTypeSynth2.Pink;
                        break;
                    case "Sweep":
                        Synth2VM.Osc3Type = WaveTypeSynth2.Sweep;
                        break;
                    default:
                        Synth2VM.Osc3Type = WaveTypeSynth2.Sin;
                        break;
                }
               
                foreach (XmlNode xmlNode in xml.GetElementsByTagName("osc1SweepLengthSecs"))
                {
                    XmlNode osc2 = xmlNode.NextSibling;
                    XmlNode osc3 = osc2.NextSibling;

                    Synth2VM.Osc1SweepLengthSecs = int.Parse(xmlNode.InnerText);
                    Synth2VM.Osc2SweepLengthSecs = int.Parse(osc2.InnerText);
                    Synth2VM.Osc3SweepLengthSecs = int.Parse(osc3.InnerText);
                }

                foreach (XmlNode xmlNode in xml.GetElementsByTagName("Osc1FreqEnd"))
                {
                    XmlNode osc2 = xmlNode.NextSibling;
                    XmlNode osc3 = osc2.NextSibling;

                    Synth2VM.Osc1FreqEnd = int.Parse(xmlNode.InnerText);
                    Synth2VM.Osc2FreqEnd = int.Parse(osc2.InnerText);
                    Synth2VM.Osc3FreqEnd = int.Parse(osc3.InnerText);
                }

            }


        }

        // Save current settings to xml
        public void SaveAs()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            saveFileDialog1.ShowDialog();

            if (!String.IsNullOrEmpty(saveFileDialog1.FileName.ToString()))
            {
                // File name
                String fileName = saveFileDialog1.FileName.ToString();

                // Get current settings
                ViewSettings currSettings = ModelToSettings();

                // File to write
                XmlDocument doc = new XmlDocument();

                // Write
                XmlElement root0 = doc.CreateElement("Synths");
                doc.AppendChild(root0);

                // Synth1
                XmlElement root = doc.CreateElement("Synth1");
                root0.AppendChild(root);

                // Type
                XmlElement Wave = doc.CreateElement("Wave");

                XmlElement type = doc.CreateElement("waveType");
                type.InnerText = "" + currSettings.WaveType;
                Wave.AppendChild(type);
                
                root.AppendChild(Wave);

                // OV
                XmlElement OV = doc.CreateElement("OV");

                XmlElement oct = doc.CreateElement("octave");
                oct.InnerText = "" + octave;
                OV.AppendChild(oct);

                XmlElement vol = doc.CreateElement("volume");
                vol.InnerText = "" + Volume;
                OV.AppendChild(vol);

                root.AppendChild(OV);

                // Bit Crusher
                XmlElement BC = doc.CreateElement("BC");

                XmlElement rate = doc.CreateElement("sampleRate");
                rate.InnerText = "" + currSettings.SampleRate;
                BC.AppendChild(rate);

                XmlElement depth = doc.CreateElement("bitDepth");
                depth.InnerText = "" + currSettings.BitDepth;
                BC.AppendChild(depth);

                root.AppendChild(BC);

                // Envelope
                XmlElement ENV = doc.CreateElement("Envelope");

                XmlElement usingEnv = doc.CreateElement("usingEnvelope");
                usingEnv.InnerText = "" + currSettings.UsingEnvelope;
                ENV.AppendChild(usingEnv);

                XmlElement atk = doc.CreateElement("envelopeAttackTime");
                atk.InnerText = "" + currSettings.EnvelopeAttackTime;
                ENV.AppendChild(atk);

                XmlElement dky = doc.CreateElement("envelopeDecayTime");
                dky.InnerText = "" + currSettings.EnvelopeDecayTime;
                ENV.AppendChild(dky);

                XmlElement sus = doc.CreateElement("envelopeSustainLevel");
                sus.InnerText = "" + currSettings.EnvelopeSustainLevel;
                ENV.AppendChild(sus);

                XmlElement rel = doc.CreateElement("envelopeReleaseTime");
                rel.InnerText = "" + currSettings.EnvelopeReleaseTime;
                ENV.AppendChild(rel);

                root.AppendChild(ENV);

                // EQ
                XmlElement EQ = doc.CreateElement("EQ");

                XmlElement LPOn = doc.CreateElement("lpOn");
                LPOn.InnerText = "" + currSettings.LowPassOn;
                EQ.AppendChild(LPOn);

                XmlElement HPOff = doc.CreateElement("hpOn");
                HPOff.InnerText = "" + currSettings.HighPassOn;
                EQ.AppendChild(HPOff);

                XmlElement LCutoff = doc.CreateElement("lpCutoff");
                LCutoff.InnerText = "" + currSettings.LowPassCutoff;
                EQ.AppendChild(LCutoff);

                XmlElement HCutoff = doc.CreateElement("hpCutoff");
                HCutoff.InnerText = "" + currSettings.HighPassCutoff;
                EQ.AppendChild(HCutoff);

                XmlElement LBand = doc.CreateElement("lpBand");
                LBand.InnerText = "" + currSettings.LowPassBandwidth;
                EQ.AppendChild(LBand);

                XmlElement HBand = doc.CreateElement("hpBand");
                HBand.InnerText = "" + currSettings.HighPassBandwidth;
                EQ.AppendChild(HBand);

                root.AppendChild(EQ);

                // Distortion
                XmlElement DIST = doc.CreateElement("Distortion");

                XmlElement dThresh = doc.CreateElement("distThreshold");
                dThresh.InnerText = "" + currSettings.DistortionThreshold;
                DIST.AppendChild(dThresh);

                XmlElement dOn = doc.CreateElement("distOn");
                dOn.InnerText = "" + currSettings.DistortionOn;
                DIST.AppendChild(dOn);
                
                root.AppendChild(DIST);

                // Tremelo

                XmlElement TM = doc.CreateElement("Tremelo");

                XmlElement tRate = doc.CreateElement("tremeloRate");
                tRate.InnerText = "" + currSettings.TremeloRate;
                TM.AppendChild(tRate);

                XmlElement tOn = doc.CreateElement("tremeloOn");
                tOn.InnerText = "" + currSettings.TremeloOn;
                TM.AppendChild(tOn);
                               
                XmlElement tType = doc.CreateElement("tremeloType");
                tType.InnerText = "" + currSettings.TremeloType;
                TM.AppendChild(tType);
           
                root.AppendChild(TM);

                // Synth2
                XmlElement root2 = doc.CreateElement("Synth2");
                root0.AppendChild(root2);

                // Gain
                XmlElement Gain = doc.CreateElement("Gain");

                XmlElement osc1G = doc.CreateElement("osc1Gain");
                osc1G.InnerText = "" + currSettings.Osc1Gain;
                Gain.AppendChild(osc1G);

                XmlElement osc2G = doc.CreateElement("osc2Gain");
                osc2G.InnerText = "" + currSettings.Osc2Gain;
                Gain.AppendChild(osc2G);

                XmlElement osc3G = doc.CreateElement("osc3Gain");
                osc3G.InnerText = "" + currSettings.Osc3Gain;
                Gain.AppendChild(osc3G);

                root2.AppendChild(Gain);                

                // Octaves
                XmlElement Octave = doc.CreateElement("Octaves");

                XmlElement osc1O = doc.CreateElement("Osc1Octave");
                osc1O.InnerText = "" + currSettings.Osc1Octave;
                Octave.AppendChild(osc1O);

                XmlElement osc2O = doc.CreateElement("Osc2Octave");
                osc2O.InnerText = "" + currSettings.Osc2Octave;
                Octave.AppendChild(osc2O);

                XmlElement osc3O = doc.CreateElement("Osc3Octave");
                osc3O.InnerText = "" + currSettings.Osc3Octave;
                Octave.AppendChild(osc3O);

                root2.AppendChild(Octave);

                // Types
                XmlElement OscTypes = doc.CreateElement("OscTypes");

                XmlElement osc1Type = doc.CreateElement("Osc1Type");
                XmlElement osc2Type = doc.CreateElement("Osc2Type");
                XmlElement osc3Type = doc.CreateElement("Osc3Type");

                osc1Type.InnerText = "" + currSettings.WaveTypeOsc1;
                osc2Type.InnerText = "" + currSettings.WaveTypeOsc2;
                osc3Type.InnerText = "" + currSettings.WaveTypeOsc3;

                OscTypes.AppendChild(osc1Type);
                OscTypes.AppendChild(osc2Type);
                OscTypes.AppendChild(osc3Type);

                root2.AppendChild(OscTypes);

                // Frequency Time
                XmlElement SweepLengthSecs = doc.CreateElement("SlideTime");

                XmlElement osc1SL = doc.CreateElement("osc1SweepLengthSecs");
                XmlElement osc2SL = doc.CreateElement("osc2SweepLengthSecs");
                XmlElement osc3SL = doc.CreateElement("osc3SweepLengthSecs");

                osc1SL.InnerText = "" + currSettings.Osc1SweepLengthSecs; 
                osc2SL.InnerText = "" + currSettings.Osc2SweepLengthSecs; 
                osc3SL.InnerText = "" + currSettings.Osc3SweepLengthSecs;

                SweepLengthSecs.AppendChild(osc1SL);
                SweepLengthSecs.AppendChild(osc2SL);
                SweepLengthSecs.AppendChild(osc3SL);

                root2.AppendChild(SweepLengthSecs);

                // Frequency End
                XmlElement OscFreqEnds = doc.CreateElement("OscFreqEnds");

                XmlElement osc1FE = doc.CreateElement("Osc1FreqEnd");
                XmlElement osc2FE = doc.CreateElement("Osc2FreqEnd");
                XmlElement osc3FE = doc.CreateElement("Osc3FreqEnd");

                osc1FE.InnerText = "" + currSettings.Osc1FreqEnd;
                osc2FE.InnerText = "" + currSettings.Osc2FreqEnd;
                osc3FE.InnerText = "" + currSettings.Osc3FreqEnd;

                OscFreqEnds.AppendChild(osc1FE);
                OscFreqEnds.AppendChild(osc2FE);
                OscFreqEnds.AppendChild(osc3FE);

                root2.AppendChild(OscFreqEnds);


                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                XmlWriter writer = XmlWriter.Create(fileName, settings);

                doc.WriteTo(writer);

                writer.Flush();
                writer.Close();
            }
            

        }

        // Bound to buttons to open windows
        public ICommand PlayerCommand
        {
            get;
            private set;
        }
        public ICommand Synth2Command
        {
            get;
            private set;
        }
        public ICommand SettingsCommand
        {
            get;
            private set;
        }

        // For saving/loading presets
        public ICommand SavePresetCommand
        {
            get;
            private set;
        }
        public ICommand LoadPresetCommand
        {
            get;
            private set;
        }
        public ICommand DefaultPresetCommand
        {
            get;
            private set;
        }

        // Quit
        public ICommand QuitAppCommand
        {
            get;
            private set;
        }

        public Mixer Mixer;

        // Window View Models
        public SettingsViewModel SettingsVM;
        public PlayerViewModel PlayerVM;
        public Synth2ViewModel Synth2VM;
        
        // Which Synth is being used?
        private bool usingSynth1 = true;
        private bool usingSynth2 = false;

        // Settings
        private int playbackLevel = 0;
        private int sampleRate = 44100;
        private int bitDepth = 7;
              
        private float opacity = 1;

        // Can we open these windows?
        public bool canOpenSettings = true;
        public bool canOpenPlayer = true;
        public bool canOpenSynth2 = true;

        // Save/Load
        public bool canSave = true;
        public bool canOpen = true;

        // For panels
        public float Opacity
        {
            get => opacity;
            set
            {
                if (value.Equals(opacity))
                    return;
                opacity = value;
                OnPropertyChanged();
            }
        }
                
        // On close
        public void ClosePlayer(object sender, EventArgs e)
        {
            CanOpenPlayer = true;
        }
        public void CloseSynth2(object sender, EventArgs e)
        {
            CanOpenSynth2 = true;
        }
        public void CloseSettings(object sender, EventArgs e)
        {
            CanOpenSettings = true;

            Opacity = SettingsVM.Opacity;
            Synth2VM.Opacity = SettingsVM.Opacity;
            PlayerVM.Opacity = SettingsVM.Opacity;
        }

        // For drawing
        private int volumeToAttack;
        private int attackToLevel;
        private int decayToSustain;
        private int decayToLevel;
        private int sustainToLevel;

        // Synth1
        private WaveTypeSynth1 waveType = WaveTypeSynth1.Sine;
        private float frequency = 0.3f;

        private bool usingEnvelope = false;
        private float envelopeAttackTime = 2.0f;
        private float envelopeDecayTime = 2.0f; 
        private float envelopeSustainLevel = 0.3f;
        private float envelopeReleaseTime = 2.0f;

        private float lpCutoff = 20.0f;
        private float lpBand = 0.5f;
        private bool lpOn = false;
        
        private float hpCutoff = 20.0f;
        private float hpBand = 0.5f;
        private bool hpOn = false;

        private float octave = 3;

        private float distThreshold = 0.8f;
        private bool distOn = false;

        private float volume = 0.1f;

        private int tremeloRate = 1;
        private bool tremeloOn = false;
        WaveTypeSynth1 tremeloType = WaveTypeSynth1.Sine;
        
        public event PropertyChangedEventHandler PropertyChanged;

        #region Gets/Sets

        // Conversions to draw
        public int VolumeToAttack
        {           
            get => (int)(160 - (volume * 800));
            set
            {
                if (value.Equals(volumeToAttack))
                    return;
                volumeToAttack = value;

                DecayToLevel = DecayToLevel;

                OnPropertyChanged();
            }
        }
        public int AttackToLevel
        {
            get => (int)Map(EnvelopeAttackTime,0,5,0,90);
            set
            {
                if (value.Equals(attackToLevel))
                    return;
                attackToLevel = value;

                //to update with this
                DecayToSustain = DecayToSustain;

                OnPropertyChanged();
            }
        }
        public int DecayToSustain
        {
            get => (int)Map(EnvelopeDecayTime, 0, 12, Math.Min(AttackToLevel, 90), 180);
            set
            {
                if (value.Equals(decayToSustain))
                    return;
                decayToSustain = value;

                OnPropertyChanged();
            }
        }
        public int DecayToLevel
        {
            get => (int)(160 - Map(EnvelopeSustainLevel,0,1,0, (int)(160-VolumeToAttack)));
            set
            {
                if (value.Equals(decayToLevel))
                    return;
                decayToLevel = value;

                OnPropertyChanged();
            }
        }
        public int SustainToLevel
        {
            get => (int)(Map(EnvelopeReleaseTime, 0, 12, 320, 475));
            set
            {
                if (value.Equals(sustainToLevel))
                    return;
                sustainToLevel = value;

                OnPropertyChanged();
            }
        }

        // Save/Open
        public bool CanSave
        {
            get => canSave;
            set
            {
                if (value.Equals(canSave))
                    return;
                canSave = value;

                OnPropertyChanged();
            }
        }
        public bool CanOpen
        {
            get => canOpen;
            set
            {
                if (value.Equals(canOpen))
                    return;
                canOpen = value;

                OnPropertyChanged();
            }
        }

        // Can we open windows?
        public bool CanOpenPlayer
        {
            get => canOpenPlayer;
            set
            {
                if (value.Equals(canOpenPlayer))
                    return;
                canOpenPlayer = value;
               
                OnPropertyChanged();
            }
        }
        public bool CanOpenSynth2
        {
            get => canOpenSynth2;
            set
            {
                if (value.Equals(canOpenSynth2))
                    return;
                canOpenSynth2 = value;
              
                OnPropertyChanged();
            }
        }
        public bool CanOpenSettings
        {
            get => canOpenSettings;
            set
            {
                if (value.Equals(canOpenSettings))
                    return;
                canOpenSettings = value;

                OnPropertyChanged();
            }
        }

        public int SampleRate
        {            
            get
            {
                switch (sampleRate)
                {
                    case 6000:
                        return 1;                       
                    case 12000:
                        return 2;
                    case 16000:
                        return 3;
                    case 22050:
                        return 4;
                    case 44100:
                        return 5;
                    default:
                        return 5;
                }
            }
            
            set
            {
                if (value.Equals(sampleRate))
                    return;
                switch(value)
                {
                    case 1:
                        sampleRate = 6000;
                        break;
                    case 2:
                        sampleRate = 12000;
                        break;
                    case 3:
                        sampleRate = 16000;
                        break;
                    case 4:
                        sampleRate = 22050;
                        break;
                    case 5:
                        sampleRate = 44100;
                        break;
                }                
                OnPropertyChanged();                
            }
        }
        public int BitDepth
        {
            get => bitDepth;
            set
            {
                if (value.Equals(bitDepth))
                    return;
                bitDepth = value;
                OnPropertyChanged();
            }
        }
        public bool UsingSynth1
        {
            get => usingSynth1;
            set
            {
                if (value.Equals(usingSynth1))
                    return;
                usingSynth1 = value;
                OnPropertyChanged();

                UsingSynth2 = !usingSynth1;                
            }
        }
        public bool UsingSynth2
        {
            get => usingSynth2;
            set
            {
                if (value.Equals(usingSynth2))
                    return;
                usingSynth2 = value;
                OnPropertyChanged();

                UsingSynth1 = !usingSynth2;
            }
        }

        public int PlaybackLevel
        {
            get => playbackLevel;
            set
            {
                if (value.Equals(playbackLevel))
                    return;
                playbackLevel = value;                

                OnPropertyChanged();
            }
        }        

        #region Synth1

        public float Frequency
        {
            get => frequency;
            set
            {
                if (value.Equals(frequency))
                    return;
                frequency = value;
                OnPropertyChanged();
            }
        }
        public float Volume
        {
            get => volume;
            set
            {
                if (value.Equals(volume))
                    return;
                volume = value;

                VolumeToAttack = (int)(160 - (volume * 800));

                OnPropertyChanged();
            }
        }
        public WaveTypeSynth1 WaveType
        {
            get => waveType;
            set
            {
                if (value.Equals(waveType))
                    return;
                waveType = value;

                OnPropertyChanged();
            }
        }
        public bool UsingEnvelope
        {
            get => usingEnvelope;
            set
            {
                if (value.Equals(usingEnvelope))
                    return;
                usingEnvelope = value;

                if (usingEnvelope)
                    ADSRVisibility = Visibility.Visible;                
                else
                    ADSRVisibility = Visibility.Hidden;
                OnPropertyChanged();
            }
        }

        private Visibility adsrVisibility = Visibility.Hidden;
        public Visibility ADSRVisibility
        {
            get => adsrVisibility;
            set
            {
                if (value.Equals(adsrVisibility))
                    return;
                adsrVisibility = value;
                OnPropertyChanged();
            }
        }

        // ADSR
        public float EnvelopeAttackTime
        {
            get => envelopeAttackTime;
            set
            {
                if (value.Equals(envelopeAttackTime))
                    return;
                envelopeAttackTime = value;

                AttackToLevel = (int)Map(envelopeAttackTime,0,5,0,90);

                OnPropertyChanged();
            }
        }        
        public float EnvelopeDecayTime
        {
            get => envelopeDecayTime;
            set
            {
                if (value.Equals(envelopeDecayTime))
                    return;
                envelopeDecayTime = value;

                DecayToSustain = (int)Map(EnvelopeDecayTime, 0, 2, AttackToLevel, 180);

                OnPropertyChanged();
            }
        }
        public float EnvelopeSustainLevel
        {
            get => envelopeSustainLevel;
            set
            {
                if (value.Equals(envelopeSustainLevel))
                    return;
                envelopeSustainLevel = value;

                DecayToLevel = (int)(160 - Map(EnvelopeSustainLevel, 0, 1, 0, 160));

                OnPropertyChanged();
            }
        }
        public float EnvelopeReleaseTime
        {
            get => envelopeReleaseTime;
            set
            {
                if (value.Equals(envelopeReleaseTime))
                    return;
                envelopeReleaseTime = value;

                SustainToLevel = (int)(160 - Map(EnvelopeReleaseTime, 0, 12, 260, 350)); 

                OnPropertyChanged();
            }
        }

        // EQ
        public float LowPassCutoff
        {
            get => lpCutoff;
            set
            {
                if (value.Equals(lpCutoff))
                    return;
                lpCutoff = value;
                OnPropertyChanged();
            }
        }
        public float LowPassBandwidth
        {
            get => lpBand;
            set
            {
                if (value.Equals(lpBand))
                    return;
                lpBand = value;
                OnPropertyChanged();
            }
        }
        public float HighPassCutoff
        {
            get => hpCutoff;
            set
            {
                if (value.Equals(hpCutoff))
                    return;
                hpCutoff = value;
                OnPropertyChanged();
            }
        }
        public float HighPassBandwidth
        {
            get => hpBand;
            set
            {
                if (value.Equals(hpBand))
                    return;
                hpBand = value;
                OnPropertyChanged();
            }
        }        
        public bool LowPassOn
        {
            get => lpOn;
            set
            {
                if (value.Equals(lpOn))
                    return;
                lpOn = value;
                OnPropertyChanged();
            }
        }
        public bool HighPassOn
        {
            get => hpOn;
            set
            {
                if (value.Equals(hpOn))
                    return;
                hpOn = value;
                OnPropertyChanged();
            }
        }
        public float Octave
        {
            get => octave;
            set
            {
                if (value.Equals(octave))
                    return;
                octave = value;
                OnPropertyChanged();
            }
        }

        // Clip Distortion
        public bool DistortionOn
        {
            get => distOn;
            set
            {
                if (value.Equals(distOn))
                    return;
                distOn = value;
                OnPropertyChanged();
            }
        }
        public float DistortionThreshold
        {
            get => distThreshold;
            set
            {
                if (value.Equals(distThreshold))
                    return;
                distThreshold = value;
                OnPropertyChanged();
            }
        }

        // Tremelo
        public bool TremeloOn
        {
            get => tremeloOn;
            set
            {
                if (value.Equals(tremeloOn))
                    return;
                tremeloOn = value;
                OnPropertyChanged();
            }
        }
        public int TremeloRate
        {
            get => tremeloRate;
            set
            {
                if (value.Equals(tremeloRate))
                    return;
                tremeloRate = value;
                OnPropertyChanged();
            }
        }
        public WaveTypeSynth1 TremeloType
        {
            get => tremeloType;
            set
            {
                if (value.Equals(tremeloType))
                    return;
                tremeloType = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        // For drawing
        public float Map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }

        /***************
         ~    Idea    ~
         **************/
        //
        //    
        //                      |------> Play through Synth |
        //    VM -> SETTINGS -> |  
        //     ^                |---------------------> XML | -> SETTINGS -> -----|
        //     |                                                                  |
        //     |-------------------------------------------------------------------                                                            

        // Takes current settings from VM and returns as a ViewSettings to be used by Synths, or to save current settings
        // model -> settings
        public ViewSettings ModelToSettings(String key = "Z")
        {
            float freq = 0;

            float multiply = 1;
            float multiply1 = 1;
            float multiply2 = 1;
            float multiply3 = 1;

            // octave multipliers
            switch (Octave)
            {
                case 1: // 2 Down
                    multiply = 0.25f;
                    break;
                case 2: // 1 Down
                    multiply = 0.5f;
                    break;
                case 3: // Neutral
                    multiply = 1.0f;
                    break;
                case 4: // 1 Up
                    multiply = 2.0f;
                    break;
                case 5: // 2 Up
                    multiply = 4.0f;
                    break;
            }
            switch (Synth2VM.Osc1Octave)
            {
                case 1: // 2 Down
                    multiply1 = 0.25f;
                    break;
                case 2: // 1 Down
                    multiply1 = 0.5f;
                    break;
                case 3: // Neutral
                    multiply1 = 1.0f;
                    break;
                case 4: // 1 Up
                    multiply1 = 2.0f;
                    break;
                case 5: // 2 Up
                    multiply1 = 4.0f;
                    break;
            }
            switch (Synth2VM.Osc2Octave)
            {
                case 1: // 2 Down
                    multiply2 = 0.25f;
                    break;
                case 2: // 1 Down
                    multiply2 = 0.5f;
                    break;
                case 3: // Neutral
                    multiply2 = 1.0f;
                    break;
                case 4: // 1 Up
                    multiply2 = 2.0f;
                    break;
                case 5: // 2 Up
                    multiply2 = 4.0f;
                    break;
            }
            switch (Synth2VM.Osc3Octave)
            {
                case 1: // 2 Down
                    multiply3 = 0.25f;
                    break;
                case 2: // 1 Down
                    multiply3 = 0.5f;
                    break;
                case 3: // Neutral
                    multiply3 = 1.0f;
                    break;
                case 4: // 1 Up
                    multiply3 = 2.0f;
                    break;
                case 5: // 2 Up
                    multiply3 = 4.0f;
                    break;
            }

            // note based on key
            switch (key)
            {
                case "Z": // C4
                    freq = 261.63f;
                    break;
                case "S": // C#4
                    freq = 277.18f;
                    break;
                case "X": // D4
                    freq = 293.66f;
                    break;
                case "D": // Eb4
                    freq = 311.13f;
                    break;
                case "C": // E4
                    freq = 329.63f;
                    break;
                case "V": // F4
                    freq = 349.23f;
                    break;
                case "G": // F#4
                    freq = 369.99f;
                    break;
                case "B": // G4
                    freq = 392.00f;
                    break;
                case "H": // Ab4
                    freq = 415.30f;
                    break;
                case "N": // A4
                    freq = 440.00f;
                    break;
                case "J": // Bb4
                    freq = 466.16f;
                    break;
                case "M": // B4
                    freq = 493.88f;
                    break;
                case "OemComma": // C5
                    freq = 523.25f;
                    break;
                case "": // No note (for playback)
                    freq = 0.0f;
                    break;
                default: // A4
                    freq = 440.00f;
                    break;

            }

            // settings with set settings
            return new ViewSettings
            {                
                SampleRate = SampleRate,
                BitDepth = BitDepth,
                
                // Synth1 settings
                WaveType = WaveType,
                DisplayWaveType = DisplayWaveType,
                Frequency = (freq * multiply),
                
                UsingEnvelope = UsingEnvelope,
                EnvelopeAttackTime = EnvelopeAttackTime,
                EnvelopeDecayTime = EnvelopeDecayTime,
                EnvelopeSustainLevel = EnvelopeSustainLevel,
                EnvelopeReleaseTime = EnvelopeReleaseTime,

                LowPassCutoff = LowPassCutoff,
                LowPassBandwidth = LowPassBandwidth,
                LowPassOn = LowPassOn,

                HighPassCutoff = HighPassCutoff,
                HighPassBandwidth = HighPassBandwidth,
                HighPassOn = HighPassOn,
                
                DistortionThreshold = DistortionThreshold,
                DistortionOn = DistortionOn,

                Volume = Volume,

                TremeloOn = TremeloOn,
                TremeloRate = TremeloRate,
                TremeloType = TremeloType,

                // Synth2 settings
                Osc1Gain = Synth2VM.Osc1Gain,
                Osc2Gain = Synth2VM.Osc2Gain,
                Osc3Gain = Synth2VM.Osc3Gain,

                Osc1Octave = Synth2VM.Osc1Octave,
                Osc2Octave = Synth2VM.Osc2Octave,
                Osc3Octave = Synth2VM.Osc3Octave,

                Osc1Freq = (freq * multiply1),
                Osc2Freq = (freq * multiply2),
                Osc3Freq = (freq * multiply3),

                WaveTypeOsc1 = Synth2VM.Osc1Type,
                WaveTypeOsc2 = Synth2VM.Osc2Type,
                WaveTypeOsc3 = Synth2VM.Osc3Type,

                Osc1FreqEnd = Synth2VM.Osc1FreqEnd,
                Osc2FreqEnd = Synth2VM.Osc2FreqEnd,
                Osc3FreqEnd = Synth2VM.Osc3FreqEnd,

                Osc1SweepLengthSecs = Synth2VM.Osc1SweepLengthSecs,
                Osc2SweepLengthSecs = Synth2VM.Osc2SweepLengthSecs,
                Osc3SweepLengthSecs = Synth2VM.Osc3SweepLengthSecs,
            };
        }

        // Given a ViewSettings, will set VM based on
        // settings -> model
        public void SettingsToModel(ViewSettings settings)
        {
            // poly
            UsingEnvelope = settings.UsingEnvelope;
            EnvelopeAttackTime = settings.EnvelopeAttackTime;
            EnvelopeDecayTime = settings.EnvelopeDecayTime;
            EnvelopeSustainLevel = settings.EnvelopeSustainLevel;
            EnvelopeReleaseTime = settings.EnvelopeReleaseTime;

            WaveType = settings.WaveType;
            DisplayWaveType = settings.DisplayWaveType;
            Frequency = settings.Frequency;

            LowPassCutoff = settings.LowPassCutoff;
            LowPassBandwidth = settings.LowPassBandwidth;
            LowPassOn = settings.LowPassOn;

            HighPassCutoff = settings.HighPassCutoff;
            HighPassBandwidth = settings.HighPassBandwidth;
            HighPassOn = settings.HighPassOn;

            DistortionThreshold = settings.DistortionThreshold;
            DistortionOn = settings.DistortionOn;

            Volume = settings.Volume;

            TremeloType = settings.TremeloType;
            TremeloRate = (int)settings.TremeloRate;
            TremeloOn = settings.TremeloOn;

            BitDepth = settings.BitDepth;

            Octave = settings.Osc1Octave;

            // mono
            Synth2VM.Osc1Freq = settings.Osc1Freq;
            Synth2VM.Osc1FreqEnd = settings.Osc1FreqEnd;
            Synth2VM.Osc1Gain = settings.Osc1Gain;
            Synth2VM.Osc1Octave = settings.Osc1Octave;
            Synth2VM.Osc1SweepLengthSecs = settings.Osc1SweepLengthSecs;
            Synth2VM.Osc1Type = settings.WaveTypeOsc1;

            Synth2VM.Osc2Freq = settings.Osc2Freq;
            Synth2VM.Osc2FreqEnd = settings.Osc2FreqEnd;
            Synth2VM.Osc2Gain = settings.Osc2Gain;
            Synth2VM.Osc2Octave = settings.Osc2Octave;
            Synth2VM.Osc2SweepLengthSecs = settings.Osc2SweepLengthSecs;
            Synth2VM.Osc2Type = settings.WaveTypeOsc2;

            Synth2VM.Osc3Freq = settings.Osc3Freq;
            Synth2VM.Osc3FreqEnd = settings.Osc3FreqEnd;
            Synth2VM.Osc3Gain = settings.Osc3Gain;
            Synth2VM.Osc3Octave = settings.Osc3Octave;
            Synth2VM.Osc3SweepLengthSecs = settings.Osc3SweepLengthSecs;
            Synth2VM.Osc3Type = settings.WaveTypeOsc3;
        }
        
        // Notify when we have changed a property
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}