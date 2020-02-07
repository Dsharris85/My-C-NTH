using System.Collections.Generic;

namespace Demo
{
    // Global WaveTypes
    public enum WaveTypeSynth1
    {   
        Sine,
        Square,
        Triangle,
        SawTooth,
        Noise,
        Robot
    }
    public enum WaveTypeSynth2
    {
        Pink,
        White,
        Sweep,
        Sin,
        Square,
        Triangle,
        SawTooth,
    }
    public enum Songs
    {
        Mary,
        Twinkle,
        CMaj1,
        CMaj2,
        AMin
    }

    public class ViewSettings
    {
        // To display on combobox
        KeyValuePair<string, string> displayWaveType = new KeyValuePair<string, string>("Sine Wave", "./Images/panels/sineIcon.png");

        #region Gets/Sets      

        // Tremelo
        public bool TremeloOn { get; set; }
        public float TremeloRate { get; set; }
        public WaveTypeSynth1 TremeloType { get; set; }

        // For Bitcrusher
        public int SampleRate { get; set; }
        public int BitDepth { get; set; }

        /// <summary>
        /// Polyphonic synth
        /// </summary>
        
        // Wavetype / Frequency / Volume
        public WaveTypeSynth1 WaveType { get; set; }
        public KeyValuePair<string,string> DisplayWaveType { get; set; }
        public float Frequency { get; set; }
        public float Volume { get; set; }

        // ADSR
        public bool UsingEnvelope { get; set; }
        public float EnvelopeAttackTime { get; set; }
        public float EnvelopeDecayTime { get; set; }
        public float EnvelopeSustainLevel { get; set; }
        public float EnvelopeReleaseTime { get; set; }

        // Distortion
        public float DistortionThreshold { get; set; }
        public bool DistortionOn { get; set; }
        
        // EQ SETTINGS
        public float LowPassCutoff { get; set; }
        public float LowPassBandwidth { get; set; }
        public bool LowPassOn { get; set; }
        public float HighPassCutoff { get; set; }
        public float HighPassBandwidth { get; set; }
        public bool HighPassOn { get; set; }

        /// <summary>
        /// Monophonic Synth
        /// </summary>
        
        // Volume
        public float Osc1Gain { get; set; }
        public float Osc2Gain { get; set; } 
        public float Osc3Gain { get; set; }

        public int Osc1Octave { get; set; }
        public int Osc2Octave { get; set; }
        public int Osc3Octave { get; set; }

        // Wavetype
        public WaveTypeSynth2 WaveTypeOsc1 { get; set; }
        public WaveTypeSynth2 WaveTypeOsc2 { get; set; }
        public WaveTypeSynth2 WaveTypeOsc3 { get; set; }

        // Frequency
        public float Osc1Freq { get; set; }
        public float Osc2Freq { get; set; }
        public float Osc3Freq { get; set; }

        // Start Frequency / Note Length
        public float Osc1FreqEnd { get; set; }
        public float Osc1SweepLengthSecs { get; set; }
        public float Osc2FreqEnd { get; set; }
        public float Osc2SweepLengthSecs { get; set; }
        public float Osc3FreqEnd { get; set; }
        public float Osc3SweepLengthSecs { get; set; }

        #endregion

        public ViewSettings( // Default Params

            //Synth1
            int sampleRate = 44100,
            int bitDepth = 7,

            WaveTypeSynth1 waveType = WaveTypeSynth1.Sine,

            float frequency = 0.3f,

            bool usingEnvelope = false,
            float envelopeAttackTime = 2.0f,
            float envelopeDecayTime = 2.0f,
            float envelopeSustainLevel = 0.3f,
            float envelopeReleaseTime = 2.0f,

            float lpCutoff = 25,
            float lpBand = 0.5f,
            bool lpOn = false,

            float hpCutoff = 20,
            float hpBand = 0.5f,
            bool hpOn = false,

            float distThreshold = 0.8f,
            bool distOn = false,

            float volume = 0.1f,

            float tremeloRate = 1f,
            bool tremeloOn = false,
            WaveTypeSynth1 tremeloType = WaveTypeSynth1.Sine,

            // Synth2
            float osc1Gain = 0.1f,
            float osc2Gain = 0.1f,
            float osc3Gain = 0.1f,

            int osc1Octave = 3,
            int osc2Octave = 3,
            int osc3Octave = 3,

            WaveTypeSynth2 osc1WaveType = WaveTypeSynth2.Sin,
            WaveTypeSynth2 osc2WaveType = WaveTypeSynth2.Sin,
            WaveTypeSynth2 osc3WaveType = WaveTypeSynth2.Sin,

            float osc1Freq = 440.0f,
            float osc2Freq = 440.0f,
            float osc3Freq = 440.0f,

            float osc1FreqEnd = 880.0f,
            float osc2FreqEnd = 880.0f,
            float osc3FreqEnd = 880.0f,
            
            float osc1FreqLengh = 3.0f,
            float osc2FreqLengh = 3.0f,
            float osc3FreqLengh = 3.0f)
            
        {
            // Synth1
            SampleRate = sampleRate;
            BitDepth = bitDepth;
            
            WaveType = waveType;
            DisplayWaveType = displayWaveType;
            Frequency = frequency;

            UsingEnvelope = usingEnvelope;
            EnvelopeAttackTime = envelopeAttackTime;
            EnvelopeDecayTime = envelopeDecayTime;
            EnvelopeSustainLevel = envelopeSustainLevel;
            EnvelopeReleaseTime = envelopeReleaseTime;
    
            LowPassBandwidth = lpBand;
            LowPassCutoff = lpCutoff;
            LowPassOn = lpOn;

            HighPassBandwidth = hpBand;
            HighPassCutoff = hpCutoff;
            HighPassOn = hpOn;

            DistortionOn = distOn;
            DistortionThreshold = distThreshold;

            Volume = volume;

            TremeloOn = tremeloOn;
            TremeloRate = tremeloRate;
            TremeloType = tremeloType;

            // Synth2
            Osc1Gain = osc1Gain;
            Osc2Gain = osc2Gain;
            Osc3Gain = osc3Gain;

            Osc1Octave = osc1Octave;
            Osc2Octave = osc2Octave;
            Osc3Octave = osc3Octave;

            WaveTypeOsc1 = osc1WaveType;
            WaveTypeOsc2 = osc2WaveType;
            WaveTypeOsc3 = osc3WaveType;

            Osc1Freq = osc1Freq;
            Osc2Freq = osc2Freq;
            Osc3Freq = osc3Freq;

            Osc1FreqEnd = osc1FreqEnd;
            Osc2FreqEnd = osc2FreqEnd;
            Osc3FreqEnd = osc3FreqEnd;

            Osc1SweepLengthSecs = osc1FreqLengh;
            Osc2SweepLengthSecs = osc2FreqLengh;
            Osc3SweepLengthSecs = osc3FreqLengh;
        }
    }
}