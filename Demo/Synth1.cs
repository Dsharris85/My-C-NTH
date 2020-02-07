using NAudio.Dsp;
using NAudio.Wave;
using System;

namespace Demo
{
    public class Synth1 : ISampleProvider
    {
        #region Vars

        // For Noise
        private readonly Random random = new Random();

        // Volume [-1.0, 1.0]
        private float volume;

        // Settings to be used
        private ViewSettings settings;

        // Constant
        private const double twoPi = 2 * Math.PI;

        // Keep track of
        private int sampleNumber;
        private bool playing;

        // Standard
        private int sampleRate = 44100;

        // EQ Filters from NAudio
        BiQuadFilter LPF;
        BiQuadFilter HPF;

        // For Envelope calcs
        private ADSREnvelope adsr;

        #endregion

        // Get/set ADSR
        public double Attack
        {
            get => adsr.AttackRate / sampleRate;
            set => adsr.AttackRate = sampleRate * value;
        }
        public double Decay
        {
            get => adsr.DecayRate / sampleRate;
            set => adsr.DecayRate = sampleRate * value;
        }
        public double Sustain
        {
            get => adsr.SustainLevel;
            set => adsr.SustainLevel = value;
        }
        public double Release
        {
            get => adsr.ReleaseRate / sampleRate;
            set => adsr.ReleaseRate = sampleRate * value;
        }
        
        // Am i playing?
        public bool IsPlaying()
        {
            return playing;
        }        

        // Note off when key up, enters release stage
        public void NoteOff() { adsr.Trigger(false); }

        // Keep track of me
        public String SynthID {get; set;}

        // ADSR or not
        public bool UsingEnvelope()
        {
            return settings.UsingEnvelope;
        }
                
        // Samples/Second - need to convert bt actual/represented vals
        public int SampleRate
        {
            get
            {
                switch (sampleRate)
                {
                    case 1:
                        return 2756;
                    case 2:
                        return 5512;
                    case 3:
                        return 11025;
                    case 4:
                        return 22050;
                    case 5:
                        return 44100;
                    default:
                        return 44100;
                }
            }

            set
            {
                if (value.Equals(sampleRate))
                    return;
                switch (value)
                {
                    case 1:
                        sampleRate = 2756;
                        break;
                    case 2:
                        sampleRate = 5512;
                        break;
                    case 3:
                        sampleRate = 11025;
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

        // Plays synth through settings
        public void Play(ViewSettings sets)
        {            
            // Acquire view settings
            settings = sets;
            Init();
        }
        
        // Stop play from var
        public void Stop()
        {
            playing = false;
        }

        // Initialise
        private void Init()
        {
            SampleRate = settings.SampleRate;

            // Init Low/Highpass filters
            LPF = BiQuadFilter.LowPassFilter(sampleRate, settings.LowPassCutoff, settings.LowPassBandwidth);
            HPF = BiQuadFilter.HighPassFilter(sampleRate, settings.HighPassCutoff, settings.HighPassBandwidth);

            // Filter
            adsr = new ADSREnvelope();
            playing = false;
            Attack = settings.EnvelopeAttackTime;
            Decay = settings.EnvelopeDecayTime;
            Sustain = settings.EnvelopeSustainLevel;
            Release = settings.EnvelopeReleaseTime;           

            // Amplitude
            volume = settings.Volume;

            // We're playing, so change stage to atk from off
            playing = true;
            adsr.Trigger(true);
        }

        // Need to implement for Interface
        WaveFormat ISampleProvider.WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);

        // Write method, where the magic happens...
        // Write individual sample buffers
        // Returns # samples which were read, will be read/played by mixer
        // Additive synthesis would be nice, perhaps next version though
        int ISampleProvider.Read(float[] buffer, int offset, int count)
        {           
            // Loop through sample count for this buffer
            for (int sampleCount = 0; sampleCount < count; sampleCount++)
            {
                // Process differently if using filter
                // Signal Chain = TYPE -> DIST -> ADSR -> VOLUME -> BD -> TREMELO -> EQ -> NOCLIP -> SR -> end;
                if (settings.UsingEnvelope)
                {
                    // If we're on
                    if (adsr.Stage != ADSREnvelope.EnvelopeStage.Off)
                    {
                        // For wave samples 
                        double samplePre;
                        double sample = 0f;

                        // Calc wave samples from standard formulas
                        switch (settings.WaveType)
                        {
                            /*
                            * F = (1 / P) HZ
                            * P = (1 / F) Seconds
                            */

                            // Sine wave
                            case WaveTypeSynth1.Sine:
                                sample = Math.Sin(sampleNumber * (twoPi * settings.Frequency / sampleRate));

                                sampleNumber++;
                                break;

                            // Signum of a Sine wave (using +- 0.25 instead of standard +- 1)
                            case WaveTypeSynth1.Square:
                                samplePre = Math.Sin(sampleNumber * (twoPi * settings.Frequency / sampleRate));
                                sample = samplePre > 0 ? 0.25f : -0.25f;

                                sampleNumber++;
                                break;

                            // Non-Standard. Was a gone-wrong Sq wave implementaion
                            // Sounded interesting enough to leave in. Sounds like a robot i guess?
                            case WaveTypeSynth1.Robot:
                                samplePre = Math.Sin(twoPi * settings.Frequency * sampleNumber);
                                sample = samplePre > 0 ? 1 : -1;

                                sampleNumber++;
                                break;

                            // Triangle wave
                            case WaveTypeSynth1.Triangle:
                                samplePre = ((sampleNumber * 2 * settings.Frequency / sampleRate) % 2);
                                sample = 2 * samplePre;
                                
                                // Final write
                                if (sample > 1)
                                    sample = 2 - sample;
                                if (sample < -1)
                                    sample = -2 - sample;                        

                                sampleNumber++;
                                break;

                            // Sawtooth wave, modified triangle really
                            case WaveTypeSynth1.SawTooth:
                                sample = ((sampleNumber * (2 * settings.Frequency / sampleRate)) % 2) - 1;

                                sampleNumber++;
                                break;

                            // Random sample [-1,1]
                            case WaveTypeSynth1.Noise:
                                sample = (2 * random.NextDouble() - 1);
                                sampleNumber++;
                                break;
                        }

                        // Distortion
                        if (settings.DistortionOn)
                        {
                            // METHOD 1 - Clipping at a threshold
                            // Samples cutoff at a threshold value, then brought back up for volume compensation

                            if (sample >= 0)
                            {
                                // Clip at threshold for pos values
                                sample = Math.Min(sample, settings.DistortionThreshold);
                            }
                            else
                            {
                                // Clip at threshold for neg values
                                sample = Math.Min(sample, -settings.DistortionThreshold);
                            }

                            // Compensate volume loss, but not too much
                            //sample /= (settings.DistortionThreshold * 0.5);
                        }   

                        // Multiply sample by our envelope multiplier for current sample
                        sample *= adsr.ProcessSample();

                        // Amplify
                        sample *= (volume * 2);

                        // "Bit Depth" by rounding
                        sample = Math.Round(sample, settings.BitDepth);

                        // Tremelo LFO - Amplitude modulation 
                        if (settings.TremeloOn)
                        {
                            // Switch on wavetype. Using same implementations as before but as LFOs, F = rate
                            // Just multiplying sample by Low Freq Osc ( <20HZ ) 
                            switch (settings.TremeloType)
                            {
                                case (WaveTypeSynth1.Sine):
                                    sample *= Math.Sin(sampleNumber * (twoPi * settings.TremeloRate / sampleRate));
                                    break;
                                case (WaveTypeSynth1.Square):
                                    samplePre = Math.Sin(sampleNumber * (twoPi * settings.TremeloRate / sampleRate));
                                    sample *= samplePre > 0 ? 0.2f : -0.2f;
                                    break;
                                case (WaveTypeSynth1.Triangle):
                                    samplePre = ((sampleNumber * 2 * settings.TremeloRate / sampleRate) % 2);
                                    sample *= 2 * samplePre;

                                    if (sample > 1)
                                        sample *= 2 - sample;
                                    if (sample < -1)
                                        sample *= -2 - sample;
                                    break;
                                case (WaveTypeSynth1.SawTooth):
                                    sample *= ((sampleNumber * (2 * settings.TremeloRate / sampleRate)) % 2) - 1;
                                    break;
                            }

                            // Amplify
                            sample *= (volume * 4);
                        }

                        // Apply any EQ
                        if (settings.LowPassOn)
                        {
                            sample = LPF.Transform((float)sample);
                        }
                        if (settings.HighPassOn)
                        {
                            sample = HPF.Transform((float)sample);
                        }

                        // In case of clipping
                        if (sample > 1.0f)
                            sample = 1.0f;
                        if (sample < -1.0f)
                            sample = -1.0f;

                        // Finally, write sample to buffer
                        // Simulate sample rate degredation for BC (technically always at 44.1khz though...)
                        switch (settings.SampleRate)
                        {
                            case 1:
                                for (int i = 0; i < 16; i++)
                                {
                                    buffer[offset++] = (float)sample;
                                    sampleCount++;
                                }
                                break;

                            case 2:
                                for (int i = 0; i < 8; i++)
                                {
                                    buffer[offset++] = (float)sample;
                                    sampleCount++;
                                }
                                break;

                            case 3:
                                for (int i = 0; i < 4; i++)
                                {
                                    buffer[offset++] = (float)sample;
                                    sampleCount++;
                                }
                                break;

                            case 4:
                                for (int i = 0; i < 2; i++)
                                {
                                    buffer[offset++] = (float)sample;
                                    sampleCount++;
                                }
                                break;

                            case 5:
                                buffer[offset++] = (float)sample;
                                break;

                        }
                    }
                    // On done playing
                    else
                    {
                        if (playing)
                        {
                            playing = false;
                        }

                        // end on 0
                        switch (settings.SampleRate)
                        {
                            case 1:
                                for (int i = 0; i < 16; i++)
                                {
                                    buffer[offset++] = 0;
                                    sampleCount++;
                                }
                                break;

                            case 2:
                                for (int i = 0; i < 8; i++)
                                {
                                    buffer[offset++] = 0;
                                    sampleCount++;
                                }
                                break;

                            case 3:
                                for (int i = 0; i < 4; i++)
                                {
                                    buffer[offset++] = 0;
                                    sampleCount++;
                                }
                                break;

                            case 4:
                                for (int i = 0; i < 2; i++)
                                {
                                    buffer[offset++] = 0;
                                    sampleCount++;
                                }
                                break;

                            case 5:
                                buffer[offset++] = 0;
                                break;

                        }
                        return 0;
                    }
                }
                // If not using ADSR envelope, process differently
                // Signal Chain = TYPE -> DIST -> VOLUME -> BD -> TREMELO -> EQ -> NOCLIP -> SR -> end;
                else
                {
                    // Only read if playing
                    if (!playing)
                        return 0;                        

                    double samplePre;
                    double sample = 0f;

                    // Calc wave samples
                    switch (settings.WaveType)
                    {
                        /*
                        * F = (1 / P) HZ
                        * P = (1 / F) Seconds
                        */

                        case WaveTypeSynth1.Sine:
                            sample = Math.Sin(sampleNumber * (twoPi * settings.Frequency / sampleRate));

                            sampleNumber++;
                            break;

                        case WaveTypeSynth1.Square:
                            samplePre = Math.Sin(sampleNumber * (twoPi * settings.Frequency / sampleRate));
                            sample = samplePre > 0 ? 0.2f : -0.2f;

                            sampleNumber++;
                            break;

                        case WaveTypeSynth1.Robot:
                            samplePre = Math.Sin(twoPi * settings.Frequency * sampleNumber);
                            sample = samplePre > 0 ? 1 : -1;

                            sampleNumber++;
                            break;

                        case WaveTypeSynth1.Triangle:
                            samplePre = ((sampleNumber * 2 * settings.Frequency / sampleRate) % 2);
                            sample = 2 * samplePre;
                            
                            if (sample > 1)
                                sample = 2 - sample;
                            if (sample < -1)
                                sample = -2 - sample;                        

                            sampleNumber++;
                            break;

                        case WaveTypeSynth1.SawTooth:
                            sample = ((sampleNumber * (2 * settings.Frequency / sampleRate)) % 2) - 1;

                            sampleNumber++;
                            break;

                        case WaveTypeSynth1.Noise:
                            sample = (2 * random.NextDouble() - 1);

                            sampleNumber++;
                            break;
                    }

                    // DIST                
                    if (settings.DistortionOn)
                    {
                        if (sample >= 0)
                        {
                            // Clip at threshold for pos values
                            sample = Math.Min(sample, settings.DistortionThreshold);
                        }
                        else
                        {
                            // Clip at threshold for neg values
                            sample = Math.Min(sample, -settings.DistortionThreshold);
                        }

                        // Compensate volume loss
                        sample /= settings.DistortionThreshold;

                    }

                    // Amplify
                    sample *= volume;

                    // Bit Depth 
                    sample = Math.Round(sample, settings.BitDepth);

                    // Tremelo amplitude modulation
                    if (settings.TremeloOn)
                    {
                        switch (settings.TremeloType)
                        {
                            case (WaveTypeSynth1.Sine):
                                sample *= ((Math.Sin(sampleNumber * (twoPi * settings.TremeloRate / sampleRate))) * volume) + volume;
                                break;
                            case (WaveTypeSynth1.Square):
                                samplePre = Math.Sin(sampleNumber * (twoPi * settings.TremeloRate / sampleRate));
                                sample *= samplePre > 0 ? 0.2f : -0.2f;
                                break;
                            case (WaveTypeSynth1.Triangle):
                                samplePre = ((sampleNumber * 2 * settings.TremeloRate / sampleRate) % 2);
                                sample *= 2 * samplePre;

                                if (sample > 1)
                                    sample *= 2 - sample;
                                if (sample < -1)
                                    sample *= -2 - sample;
                                break;
                            case (WaveTypeSynth1.SawTooth):
                                sample *= ((sampleNumber * (2 * settings.TremeloRate / sampleRate)) % 2) - 1;
                                break;
                        }
                    }

                    // Apply any EQ
                    if (settings.LowPassOn)
                    {
                        sample = LPF.Transform((float)sample);
                    }
                    if (settings.HighPassOn)
                    {
                        sample = HPF.Transform((float)sample);
                    }

                    // In case of clipping
                    if (sample > 1.0f)
                        sample = 1.0f;
                    if (sample < -1.0f)
                        sample = -1.0f;             

                    // Simulate sample rate degredation (technically always at 44.1khz)
                    switch (settings.SampleRate)
                    {
                        case 1:
                            for (int i = 0; i < 16; i++)
                            {
                                buffer[offset++] = (float)sample;
                                sampleCount++;
                            }

                            break;
                        case 2:
                            for (int i = 0; i < 8; i++)
                            {
                                buffer[offset++] = (float)sample;
                                sampleCount++;
                            }

                            break;
                          
                        case 3:
                            for (int i = 0; i < 4; i++)
                            {
                                buffer[offset++] = (float)sample;
                                sampleCount++;
                            }                           

                            break;
                        case 4:

                            for(int i = 0; i < 2; i++)
                            {
                                buffer[offset++] = (float)sample;
                                sampleCount++;
                            }

                            break;
                        case 5:
                            buffer[offset++] = (float)sample;
                            break;

                    }                    
                }
            }
            return count;
        }        
    }
}