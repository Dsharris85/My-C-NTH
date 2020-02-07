using Demo.View.ViewModels;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Demo.View.Views
{
    /// <summary>
    /// Interaction logic for Synth2View.xaml
    /// </summary>
    public partial class Synth2View : Window
    {
        Synth2ViewModel ViewModel;

        // KB Input
        KBprops KB = new KBprops();

        private List<Key> keysDown = new List<Key>();
        private Key newestKey;

        private Mixer Mixer;

        // Used in keydown method
        private Func<String, ViewSettings> ToSettings;

        private bool mouseDown = false;

        public Synth2View(Synth2ViewModel vm, ref Mixer mixr, Func<String, ViewSettings> toSettings)
        {
            InitializeComponent();
            ViewModel = vm;
            DataContext = vm;

            Mixer = mixr;
            ToSettings = toSettings;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!mouseDown)
            {
                // Play notes
                if (e.Key == Key.Z && KB.ZkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.ZkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.S && KB.SkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.SkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.X && KB.XkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.XkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.D && KB.DkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.DkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.C && KB.CkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.CkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.V && KB.VkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.VkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.G && KB.GkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.GkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.B && KB.BkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.BkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.H && KB.HkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.HkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.N && KB.NkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.NkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.J && KB.JkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.JkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.M && KB.MkeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.MkeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
                if (e.Key == Key.OemComma && KB.CommakeyCheck)
                {
                    newestKey = e.Key;
                    keysDown.Add(newestKey);

                    KB.CommakeyCheck = false;
                    Mixer?.PauseSynth2();
                    Mixer.PlaySynth2(ToSettings(e.Key.ToString()));
                }
            }
            
        }
    
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Pause notes
            if (e.Key == Key.Z && !KB.ZkeyCheck)
            {
                KB.ZkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }

            }
            if (e.Key == Key.S && !KB.SkeyCheck)
            {
                KB.SkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.X && !KB.XkeyCheck)
            {
                KB.XkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.D && !KB.DkeyCheck)
            {
                KB.DkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.C && !KB.CkeyCheck)
            {
                KB.CkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.V && !KB.VkeyCheck)
            {
                KB.VkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.G && !KB.GkeyCheck)
            {
                KB.GkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.B && !KB.BkeyCheck)
            {
                KB.BkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.H && !KB.HkeyCheck)
            {
                KB.HkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.N && !KB.NkeyCheck)
            {
                KB.NkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.J && !KB.JkeyCheck)
            {
                KB.JkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.M && !KB.MkeyCheck)
            {
                KB.MkeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
            if (e.Key == Key.OemComma && !KB.CommakeyCheck)
            {
                KB.CommakeyCheck = true;

                Key tmp = keysDown.Last();

                keysDown.Remove(e.Key);

                //if still held down, play last note down or if still held just keep playing
                if (keysDown.Count > 0)
                {
                    if (e.Key == tmp)
                    {
                        Mixer?.PauseSynth2();
                        Mixer.PlaySynth2(ToSettings(keysDown.Last().ToString()));
                    }
                }
                else
                {
                    Mixer.PauseSynth2();
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseDown = false;
        }
    }
}
