using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Demo.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Mixer and VM
        private static readonly Mixer mixer = new Mixer();
        private readonly MainViewModel VM = new MainViewModel(mixer);

        // For level feedback
        private DispatcherTimer timer = new DispatcherTimer();

        // KB Input
        KBprops KB = new KBprops();

        private List<Key> keysDown = new List<Key>();
        //private Key newestKey;

        // Set up binding
        public MainWindow()
        {
            DataContext = VM;
            InitializeComponent();                        
        }        

        // Tick ever 10ms for volume bar updates
        private void Tick(object sender, EventArgs e)
        {
            VM.PlaybackLevel = (int)mixer.PlaybackLevel();         
        }
        
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Octave KB input s1
            if (e.Key == Key.Up && KB.UpkeyCheck)
            {
                if (VM.Octave <= 5)
                {
                    VM.Octave++;
                }
                else
                {
                    VM.Octave = 5;
                }
                KB.UpkeyCheck = false;
            }
            if (e.Key == Key.Down && KB.DownkeyCheck)
            {
                if (VM.Octave >= 1)
                {
                    VM.Octave--;
                }
                else
                {
                    VM.Octave = 1;
                }
                KB.DownkeyCheck = false;
            }
            
            // Play notes
            if (e.Key == Key.Z && KB.ZkeyCheck)
            {
                VM.TestC = new SolidColorBrush(Colors.DarkBlue);
                KB.ZkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.S && KB.SkeyCheck)
            {
                VM.TestCS = new SolidColorBrush(Colors.DarkBlue);
                KB.SkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());                    
            }
            if (e.Key == Key.X && KB.XkeyCheck)
            {
                VM.TestD = new SolidColorBrush(Colors.DarkBlue);
                KB.XkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());                    
            }
            if (e.Key == Key.D && KB.DkeyCheck)
            {
                VM.TestEb = new SolidColorBrush(Colors.DarkBlue);
                KB.DkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());                    
            }
            if (e.Key == Key.C && KB.CkeyCheck)
            {
                VM.TestE = new SolidColorBrush(Colors.DarkBlue);
                KB.CkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.V && KB.VkeyCheck)
            {
                VM.TestF = new SolidColorBrush(Colors.DarkBlue);
                KB.VkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.G && KB.GkeyCheck)
            {
                VM.TestFS = new SolidColorBrush(Colors.DarkBlue);
                KB.GkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.B && KB.BkeyCheck)
            {
                VM.TestG = new SolidColorBrush(Colors.DarkBlue);
                KB.BkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.H && KB.HkeyCheck)
            {
                VM.TestAb = new SolidColorBrush(Colors.DarkBlue);
                KB.HkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.N && KB.NkeyCheck)
            {
                VM.TestA = new SolidColorBrush(Colors.DarkBlue);
                KB.NkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.J && KB.JkeyCheck)
            {
                VM.TestBb = new SolidColorBrush(Colors.DarkBlue);
                KB.JkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.M && KB.MkeyCheck)
            {
                VM.TestB = new SolidColorBrush(Colors.DarkBlue);
                KB.MkeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }
            if (e.Key == Key.OemComma && KB.CommakeyCheck)
            {
                VM.TestC2 = new SolidColorBrush(Colors.DarkBlue);
                KB.CommakeyCheck = false;
                mixer.SampleRate = VM.SampleRate;
                mixer.PlaySynth1(VM.ModelToSettings(e.Key.ToString()), e.Key.ToString());
            }                       
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {            
            // Set  key bool to true
            switch (e.Key)
            {
                case Key.Z:
                    VM.TestC = new SolidColorBrush(Colors.White);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.ZkeyCheck = true;
                    break;
                case Key.S:
                    VM.TestCS = new SolidColorBrush(Colors.Black);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.SkeyCheck = true;
                    break;
                case Key.X:
                    VM.TestD = new SolidColorBrush(Colors.White);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.XkeyCheck = true;
                    break;
                case Key.D:
                    VM.TestEb = new SolidColorBrush(Colors.Black);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.DkeyCheck = true;
                    break;
                case Key.C:
                    VM.TestE = new SolidColorBrush(Colors.White);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.CkeyCheck = true;
                    break;
                case Key.V:
                    VM.TestF = new SolidColorBrush(Colors.White);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.VkeyCheck = true;
                    break;
                case Key.G:
                    VM.TestFS = new SolidColorBrush(Colors.Black);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.GkeyCheck = true;
                    break;
                case Key.B:
                    VM.TestG = new SolidColorBrush(Colors.White);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.BkeyCheck = true;
                    break;
                case Key.H:
                    VM.TestAb = new SolidColorBrush(Colors.Black);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.HkeyCheck = true;
                    break;
                case Key.N:
                    VM.TestA = new SolidColorBrush(Colors.White);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.NkeyCheck = true;
                    break;
                case Key.J:
                    VM.TestBb = new SolidColorBrush(Colors.Black);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.JkeyCheck = true;
                    break;
                case Key.M:
                    VM.TestB = new SolidColorBrush(Colors.White);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.MkeyCheck = true;
                    break;
                case Key.OemComma:
                    VM.TestC2 = new SolidColorBrush(Colors.White);
                    mixer.PauseSynth1(e.Key.ToString());
                    KB.CommakeyCheck = true;
                    break;
                case Key.Up:
                    KB.UpkeyCheck = true;
                    break;
                case Key.Down:
                    KB.DownkeyCheck = true;               
                    break;
                default:
                    break;
            }                  
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Tick;
            timer.Start();
        }

        // Clean on close
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {    
            mixer.Dispose();
            base.OnClosed(e);            
        }

        // unused...
        private void PlaySong(object sender, RoutedEventArgs e)
        {
        
        }
        private void StopSong(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }       
    }
}