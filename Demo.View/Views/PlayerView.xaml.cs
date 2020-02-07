using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Demo.View.ViewModels;

namespace Demo.View.Views
{
    /// <summary>
    /// Interaction logic for SoundPlayerViewModel.xaml
    /// </summary>
    public partial class PlayerView : Window
    {
        // For Playback
        private DispatcherTimer timer2 = new DispatcherTimer();
        private bool timerStop;
        private bool skip = true;
        private bool canPlay = true;
        private float ms = 500;
        private String currentNote = "";
        private List<SongData.MyTuple> notesDown = new List<SongData.MyTuple>();
        private List<SongData.MyTuple> removeTheseNotes = new List<SongData.MyTuple>();

        private PlayerViewModel ViewModel;
        private Mixer Mixer;

        private Func<String, ViewSettings> ToSettings;
        
        // Construct
        public PlayerView(PlayerViewModel vm, ref Mixer mixer, Func<String, ViewSettings> toSettings)
        {
            InitializeComponent();
            ViewModel = vm;
            DataContext = vm;
            Mixer = mixer;
            ToSettings = toSettings;
        }

        private bool doubleTime = true;
        private void Tick2(object sender, EventArgs e)
        {            
            if (ViewModel.IsPlayingSong)
            {
                // BPM to ms quarter notes
                ms = (60000 / ViewModel.Tempo) / 2;
                timer2.Interval = new TimeSpan(0, 0, 0, 0, (int)ms);

                if (doubleTime)
                {
                    // Makes new TS each tick :c
                    foreach (SongData.MyTuple noteDown in notesDown)
                    {
                        removeTheseNotes = new List<SongData.MyTuple>();

                        // if playtime over or about to be played again, need to pause before play again
                        if (noteDown.Life == 0 || noteDown.Note == currentNote) // mayb print 
                        {
                            // Chords
                            if (ViewModel.CurrentSong == Songs.AMin || ViewModel.CurrentSong == Songs.CMaj1 || ViewModel.CurrentSong == Songs.CMaj2)
                            {
                                Mixer.PauseSynth1((noteDown.Note[0]).ToString());

                                try
                                {
                                    Mixer.PauseSynth1(noteDown.Note[1].ToString());
                                    Mixer.PauseSynth1(noteDown.Note[2].ToString());
                                }
                                catch { }

                            }
                            else
                            {
                                Mixer.PauseSynth1((noteDown.Note));
                            }

                            removeTheseNotes.Add(noteDown);
                        }
                        else
                        {
                            noteDown.Life--;
                        }
                    }

                    foreach (SongData.MyTuple step in removeTheseNotes)
                    {
                        notesDown.Remove(step);
                    }

                    if (ViewModel.Data.song.Count > 0)
                    {
                        var step = ViewModel.Data.song.Dequeue();

                        currentNote = step.Note;

                        notesDown.Add(step);
                        if (ViewModel.CurrentSong == Songs.AMin || ViewModel.CurrentSong == Songs.CMaj1 || ViewModel.CurrentSong == Songs.CMaj2)
                        {
                            Mixer.PlaySynth1(ToSettings(step.Note[0].ToString()), step.Note[0].ToString());
                            Mixer.PlaySynth1(ToSettings(step.Note[1].ToString()), step.Note[1].ToString());
                            Mixer.PlaySynth1(ToSettings(step.Note[2].ToString()), step.Note[2].ToString());
                        }
                        else
                        {
                            Mixer.PlaySynth1(ToSettings(step.Note), step.Note);
                        }
                    }
                    else
                    {
                        if (ViewModel.CurrentSong == Songs.AMin || ViewModel.CurrentSong == Songs.CMaj1 || ViewModel.CurrentSong == Songs.CMaj2)
                        {
                            // Loop
                            ViewModel.songData = new SongData(ViewModel.PlayerNoteLife, ViewModel.CurrentSong);
                            ViewModel.IsPlayingSong = true;
                        }
                        else
                        {
                            ViewModel.IsPlayingSong = false;
                        }
                    }

                    ViewModel.songData.counter++;
                    doubleTime = false;
                }
                else doubleTime = true;

                if (skip)
                {
                    ViewModel.Pulse = new LinearGradientBrush(
                                            Color.FromArgb(255, 50, 255, 20),
                                            Color.FromArgb(255, 70, 225, 98),
                                            new Point(0, 0),
                                            new Point(0, 1)
                                            );
                    skip = false;
                }
                else
                {
                    ViewModel.Pulse = new LinearGradientBrush(
                                            Color.FromArgb(255, 150, 31, 195),
                                            Color.FromArgb(255, 255, 0, 246),
                                            new Point(0, 0),
                                            new Point(0, 1)
                                            );
                    skip = true;
                }               
            }
            else
            {
                foreach (SongData.MyTuple noteDown in notesDown)
                {    
                    if (ViewModel.CurrentSong == Songs.AMin || ViewModel.CurrentSong == Songs.CMaj1 || ViewModel.CurrentSong == Songs.CMaj2)
                    {
                        Mixer.PauseSynth1(noteDown.Note[0].ToString());
                        Mixer.PauseSynth1(noteDown.Note[1].ToString());
                        Mixer.PauseSynth1(noteDown.Note[2].ToString());
                    }
                    else
                    {
                       Mixer.PauseSynth1(noteDown.Note);
                    }

                }

                ViewModel.Pulse = new LinearGradientBrush(
                                        Color.FromArgb(255, 150, 31, 195),
                                        Color.FromArgb(255, 255, 0, 246),
                                        new Point(0, 0),
                                        new Point(0, 1)
                                        );
                timer2.Stop();
                timerStop = false;
                canPlay = true;
              
            }
        }        

        private void PlaySong(object sender, RoutedEventArgs e)
        {
            if (canPlay)
            {
                if (timerStop)
                {
                    timer2.Stop();
                }
                else
                {
                    timer2.Start();
                    timerStop = false;
                }

                ViewModel.songData = new SongData(ViewModel.PlayerNoteLife, ViewModel.CurrentSong);
                ViewModel.songData.CurrentSong = ViewModel.CurrentSong;
                ViewModel.IsPlayingSong = true;
                canPlay = false;
            }
        }

        private void StopPlay()
        {
            if (!canPlay)
            {
                ViewModel.IsPlayingSong = false;

                timerStop = true;

                ViewModel.songData = new SongData(ViewModel.PlayerNoteLife, ViewModel.CurrentSong);
            }
            canPlay = true;
        }

        private void StopSong(object sender, RoutedEventArgs e)
        {
            StopPlay();
        }    

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 500 = pulse every beat (quarter notes), at 120 bpm | 1000 = pulse every half beat (half notes), at 120 bpm
            timer2.Interval = TimeSpan.FromMilliseconds(500);
            timer2.Tick += Tick2;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
            StopPlay();
        }
    }
}
