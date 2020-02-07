using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo.View.ViewModels
{
    public class Synth2ViewModel : INotifyPropertyChanged
    {
        private float osc1Gain = 0.05f;
        private float osc2Gain = 0.05f;
        private float osc3Gain = 0.05f;

        private int osc1Octave = 3;
        private int osc2Octave = 3;
        private int osc3Octave = 3;

        private float osc1Freq = 444.0f;
        private float osc2Freq = 444.0f;
        private float osc3Freq = 444.0f;

        private float osc1FreqEnd = 880.0f;
        private float osc2FreqEnd = 880.0f;
        private float osc3FreqEnd = 880.0f;

        private float osc1SweepLengthSecs = 1;
        private float osc2SweepLengthSecs = 1;
        private float osc3SweepLengthSecs = 1;

        private WaveTypeSynth2 osc1Type = WaveTypeSynth2.Sin;
        private WaveTypeSynth2 osc2Type = WaveTypeSynth2.Sin;
        private WaveTypeSynth2 osc3Type = WaveTypeSynth2.Sin;

        private float opacity = 1;

        #region Gets/Sets

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

        public int Osc1Octave
        {
            get => osc1Octave;
            set
            {
                if (value.Equals(osc1Octave))
                    return;
                osc1Octave = value;
                OnPropertyChanged();
            }
        }
        public int Osc2Octave
        {
            get => osc2Octave;
            set
            {
                if (value.Equals(osc2Octave))
                    return;
                osc2Octave = value;
                OnPropertyChanged();
            }
        }
        public int Osc3Octave
        {
            get => osc3Octave;
            set
            {
                if (value.Equals(osc3Octave))
                    return;
                osc3Octave = value;
                OnPropertyChanged();
            }
        }


        public float Osc1Gain
        {
            get => osc1Gain;
            set
            {
                if (value.Equals(osc1Gain))
                    return;
                osc1Gain = value;
                OnPropertyChanged();
            }
        }
        public float Osc2Gain
        {
            get => osc2Gain;
            set
            {
                if (value.Equals(osc2Gain))
                    return;
                osc2Gain = value;
                OnPropertyChanged();
            }
        }
        public float Osc3Gain
        {
            get => osc3Gain;
            set
            {
                if (value.Equals(osc3Gain))
                    return;
                osc3Gain = value;
                OnPropertyChanged();
            }
        }

        public float Osc1Freq
        {
            get => osc1Freq;
            set
            {
                if (value == osc1Freq)
                    return;
                osc1Freq = value;
                OnPropertyChanged();
            }
        }
        public float Osc2Freq
        {
            get => osc2Freq;
            set
            {
                if (value == osc2Freq)
                    return;
                osc2Freq = value;
                OnPropertyChanged();
            }
        }
        public float Osc3Freq
        {
            get => osc3Freq;
            set
            {
                if (value == osc3Freq)
                    return;
                osc3Freq = value;
                OnPropertyChanged();
            }
        }

        public WaveTypeSynth2 Osc1Type
        {
            get => osc1Type;
            set
            {
                if (value == osc1Type)
                    return;
                osc1Type = value;
                OnPropertyChanged();
            }
        }
        public WaveTypeSynth2 Osc2Type
        {
            get => osc2Type;
            set
            {
                if (value == osc2Type)
                    return;
                osc2Type = value;
                OnPropertyChanged();
            }
        }
        public WaveTypeSynth2 Osc3Type
        {
            get => osc3Type;
            set
            {
                if (value == osc3Type)
                    return;
                osc3Type = value;
                OnPropertyChanged();
            }
        }

        public float Osc1FreqEnd
        {
            get => osc1FreqEnd;
            set
            {
                if (value == osc1FreqEnd)
                    return;
                osc1FreqEnd = value;
                OnPropertyChanged();
            }
        }
        public float Osc1SweepLengthSecs
        {
            get => osc1SweepLengthSecs;
            set
            {
                if (value == osc1SweepLengthSecs)
                    return;
                osc1SweepLengthSecs = value;
                OnPropertyChanged();
            }
        }
        public float Osc2FreqEnd
        {
            get => osc2FreqEnd;
            set
            {
                if (value == osc2FreqEnd)
                    return;
                osc2FreqEnd = value;
                OnPropertyChanged();
            }
        }
        public float Osc2SweepLengthSecs
        {
            get => osc2SweepLengthSecs;
            set
            {
                if (value == osc2SweepLengthSecs)
                    return;
                osc2SweepLengthSecs = value;
                OnPropertyChanged();
            }
        }
        public float Osc3FreqEnd
        {
            get => osc3FreqEnd;
            set
            {
                if (value == osc3FreqEnd)
                    return;
                osc3FreqEnd = value;
                OnPropertyChanged();
            }
        }
        public float Osc3SweepLengthSecs
        {
            get => osc3SweepLengthSecs;
            set
            {
                if (value == osc3SweepLengthSecs)
                    return;
                osc3SweepLengthSecs = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
