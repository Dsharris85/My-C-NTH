using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Demo.View.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        // Sound Player
        private bool isPlayingSong = false;
        private Songs currentSong = Songs.Mary;
        private String displaySong = "Mary Had a Little Lamb";
        private int playerNoteLife = 1;
        private int tempo = 120;
        public SongData songData;
        private bool canChangeSong = true;
        private float opacity = 1;

        public PlayerViewModel()
        {
            SongTitles = new List<String>() { "Mary Had a Little Lamb", "Twinkle Twinkle, Little Star", "Loop in C - 1", "Loop in C - 2", "Loop in Am" };
        }
        
        public List<String> SongTitles { get; }

        private LinearGradientBrush pulse = new LinearGradientBrush(
            Color.FromArgb(255, 150, 31, 195),   
            Color.FromArgb(255, 255, 0, 246),
            new Point(0, 0),
            new Point(0, 1) 
            );

        public event PropertyChangedEventHandler PropertyChanged;

        public SongData Data { get => songData; set { } }

        public LinearGradientBrush Pulse
        {
            get => pulse;
            set
            {
                if (value.Equals(pulse))
                    return;
                pulse = value;
                OnPropertyChanged();
            }            
        }

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

        // Lock options while playing
        public bool CanChangeSong
        {
            get => canChangeSong;
            set
            {
                if (value.Equals(canChangeSong))
                    return;
                canChangeSong = value;
                OnPropertyChanged();
            }
        }

        public int Tempo
        {
            get => tempo;
            set
            {
                if (value.Equals(tempo))
                    return;
                tempo = value;
                OnPropertyChanged();
            }
        }
                
        public Songs CurrentSong
        {
            get => currentSong;

            set
            {
                if (value.Equals(currentSong))
                    return;

                songData = new SongData(PlayerNoteLife, currentSong);                

                currentSong = value;
                OnPropertyChanged();
            }
        }

        public String DisplaySong
        {
            get => displaySong;
            set
            {
                if (value.Equals(displaySong))
                    return;
                displaySong = value;

                switch (displaySong)
                {
                    case "Mary Had a Little Lamb":
                        CurrentSong = Songs.Mary;
                        break;
                    case "Twinkle Twinkle, Little Star":
                        CurrentSong = Songs.Twinkle;
                        break;
                    case "Loop in C - 1":
                        CurrentSong = Songs.CMaj1;
                        break;
                    case "Loop in C - 2":
                        CurrentSong = Songs.CMaj2;
                        break;
                    case "Loop in Am":
                        CurrentSong = Songs.AMin;
                        break;
                }
                OnPropertyChanged();
            }
        }
        

        public bool IsPlayingSong
        {
            get => isPlayingSong;
            set
            {
                if (value.Equals(isPlayingSong))
                    return;
                isPlayingSong = value;

                CanChangeSong = !isPlayingSong;

                OnPropertyChanged();
            }
        }

        public int PlayerNoteLife
        {
            get => playerNoteLife;
            set
            {
                if (value.Equals(playerNoteLife))
                    return;
                playerNoteLife = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
