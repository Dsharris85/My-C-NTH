using NAudio.CoreAudioApi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;

namespace Demo
{
    public class Mixer : IDisposable
    {
        // Standards
        private static int sampleRate = 44100;
        private const int channelCount = 1;

        // Using WaveFormat provided by NAudio. Represents a wave file format, and method creates new 32 bit IEEE floating pt wave format
        // Used to provide to MixingSampleProvider so we know our format 
        private static WaveFormat waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount);

        // Mixer from NAudio used to allow inputs to be added/removed
        private MixingSampleProvider mixer = new MixingSampleProvider(waveFormat);

        // IWaveOut From NAudio is an implemtation of an output device, and connects to soundcard. Chose in part b/c WaveOut implements, and uses the legacy waveOut API from microsoft. 
        // WO uses event callbacks to play audio on a background thread
        private WaveOut outDevice = new WaveOut();

        // Synth1 used as main polyphonic synthesizer
        //private Synth1 poly;
        private ObjectPool<Synth1> pool = new ObjectPool<Synth1>();
        private List<Synth1> synths = new List<Synth1>();

        // Synth2 used as secondary monophonic synthesizer
        private Synth2 mono;

        // For volume levels
        public static MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();     

        public MMDeviceCollection devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
        public List<MMDevice> deviceList;

        // Construct
        public Mixer()
        {
            //returns 0 even when not playing for constant playback
            mixer.ReadFully = true;

            // total duration all buffers
            outDevice.DesiredLatency = 100; 
            outDevice.NumberOfBuffers = 2;

            // configure output device to use our mixer
            outDevice.Init(mixer);

            // Begin Playback (no audio playing currently though)
            outDevice.Play();

            var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            deviceList = new List<MMDevice>(devices);

            // Raised when an input has stopped play
            // When stopped, grab synth an put back in pool
            mixer.MixerInputEnded += (sender, args) =>
            {
               if (args.SampleProvider is Synth1 s)
               {
                    pool.PutObject(s);                   
               }
            };
        }

        // Sample rate (samples/second)
        public int SampleRate
        {
            get => waveFormat.SampleRate;
            set
            {
                if (value.Equals(sampleRate))
                    return;
                switch (value)
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
            }
        }               

        // System Volume level
        public float PlaybackLevel()
        {
            // Default device.
            var s = devices[0].AudioMeterInformation.MasterPeakValue * 150;
            if (s > 100)
                s = 100;
            return s;
        }     
        
        // Play note - grab from pool, assign id (id=key pressed to play) to find later, add to mixer in playing state
        public void PlaySynth1(ViewSettings settings, String id)   
        {
            var newSynth = pool.GetObject();
            newSynth.SynthID = id;
            
            synths.Add(newSynth);

            newSynth.Play(settings);
            
            mixer.AddMixerInput(newSynth);          
        }

        // Stop note - find from id, stop play
        public void PauseSynth1(String id)
        {
            try
            {
                var s = synths.Find(x => x.SynthID == id);

                if (s.UsingEnvelope())
                {
                    s.NoteOff();
                }
                else
                {
                    s.Stop();
                }

                synths.Remove(s);
                
            }
            catch (Exception e) { }
        }

        // Plays s2
        public void PlaySynth2(ViewSettings settings)
        {
            // New instance, begin play
            mono?.Dispose();
            mono = new Synth2(settings);
            mono.Play();
        }

        // Stops s2
        public void PauseSynth2()
        {
            // Stop play if mono
            mono?.Pause();
            //mono?.Dispose();
        }

        // Cleanup on close
        public void Dispose()
        {
            outDevice.Stop();
            outDevice.Dispose();
        }
    }
}