using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Collections.Generic;

namespace Demo
{
    class Synth2
    {
        // Used for playback
        private WaveOut outDevice;

        // Mixer to bus inputs through
        private MixingSampleProvider mixer;

        // SG from NAudio - acts as a signal generator for various waveforms
        private SignalGenerator osc1;
        private SignalGenerator osc2;
        private SignalGenerator osc3;

        // To hold SGs
        private List<ISampleProvider> samples;

        // Keep track of
        private bool isPlaying;

        // Construct
        public Synth2(ViewSettings settings)
        {
            // Init
            outDevice = new WaveOut();
            
            // Init Oscillators from view settings
            osc1 = new SignalGenerator()
            {
                Gain = settings.Osc1Gain, // volume
                Frequency = settings.Osc1Freq, // note
                Type = (SignalGeneratorType)settings.WaveTypeOsc1, // wavetype
                FrequencyEnd = settings.Osc1FreqEnd, // note to end on (when valid)
                SweepLengthSecs = settings.Osc1SweepLengthSecs // length until note end (when valid)
                
            };
            osc2 = new SignalGenerator()
            {
                Gain = settings.Osc2Gain,
                Frequency = settings.Osc2Freq,
                Type = (SignalGeneratorType)settings.WaveTypeOsc2,
                FrequencyEnd = settings.Osc2FreqEnd,
                SweepLengthSecs = settings.Osc2SweepLengthSecs
            };
            osc3 = new SignalGenerator()
            {
                Gain = settings.Osc3Gain,
                Frequency = settings.Osc3Freq,
                Type = (SignalGeneratorType)settings.WaveTypeOsc3,
                FrequencyEnd = settings.Osc3FreqEnd,                
                SweepLengthSecs = settings.Osc3SweepLengthSecs
            };

            // Add Oscs
            samples = new List<ISampleProvider>();
            samples.Add(osc1);
            samples.Add(osc2);
            samples.Add(osc3);

            // New Mixer, Who Dis?
            mixer = new MixingSampleProvider(samples);

            // Not playing just yet
            isPlaying = false;

            // Default = 300ms, more responsive at performence cost
            outDevice.DesiredLatency = 200;

            // Feed mixer
            outDevice.Init(mixer);
        }

        // Begin Playback
        public void Play()
        {
            isPlaying = true;
            outDevice.Play();
        }

        // Pause Playback
        public void Pause()
        {
            isPlaying = false;
            outDevice.Stop();
        }

        // Cleanup
        public void Dispose()
        {
            try
            {
                outDevice?.Dispose();
            } 
            catch { }
        }
    }
}