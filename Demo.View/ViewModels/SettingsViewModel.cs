using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Demo.View.ViewModels
{    
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public SettingsViewModel(MainViewModel VM)
        {
            viewModel = VM;
        }

        // Just info text
        private MainViewModel viewModel;

        private String version = "Version : 1.0.0               ";
        private String author = "Author : Daniel Harris";
        private String date = "Date : 12/11/19          ";
        private String name = "My C#NTH";

        private float opacity = 1;

        private String explainText = "";
        private String newText = $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"INSTRUCTIONS - {Environment.NewLine}" +
            $"* Play notes with keys (explained on page){Environment.NewLine}" +
            $"* drag window by menu bar.{Environment.NewLine}" +
            $"* To open monophonic synthesizer, click 'Mono Synth'.{Environment.NewLine}" +
            $"* To open music player, click 'Music Player'.{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"PRESETS - " +
            $"Load pre-made or saved preset files to play through both synthesizers. " +
            $"Each preset remembers all of the possible settings that make up your sound. " +
            $"'Reset to Defaults' Resets each parameter to it's original state upon loading.{Environment.NewLine}{Environment.NewLine}" +
            $"FILE - " +
            $"From here, you may exit the program or open up this settings pages.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"POLY SYNTH (main window) {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"Polyphonic synthesizer. Provides the user the ability to play multiple notes simultaneously. This is the main synthesizer, and plays a chosen waveform with other various parameters to be tuned, resulting in a variety of possible sounds.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-MASTER VOLUME - Controls the overall volume of the monophonic synthesizer. Recommended to keep low when experimenting with new sounds to prevent unintended sudden volume changes.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-WAVETYPE - Selects the type of audio wave to be synthesized. Can choose from a variety of predefined waveforms. All are standards, not including 'Robot'. Select it and find out what this is! {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-OCTAVE MULTIPLIER - This controls the overall pitch of the note we play. Think of it as a frequency multiplier. 110HZ, 220HZ, 440HZ, etc. are all the note 'A'! We are just moving it higher or lower. {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-ADSR ENVELOPE - Attack, Decay, Sustain, Release Envelope. These are 4 parameters which modulate our volume over time. When not using the envelope, our note will only have a constant level while the key is held and lifted.{Environment.NewLine}" +
            $"{Environment.NewLine}" +   
            $" * The ATTACK stage is a function over time which describes how long it takes our note being played to go from a volume of 0.0 to our desired volume level provided by master volume. {Environment.NewLine}" +   
            $" * The DECAY stage is a function over time which describes how long it takes our note being played to go from the level reached when we end our attack stage, down to the level defined by our SUSTAIN stage.{Environment.NewLine}" +            
            $" * The SUSTAIN stage is a level-defined function, which will sustain our note at the constant level desired until the key is released.{Environment.NewLine}" +            
            $" * Once we release the key, we enter the final stage, RELEASE. This is a function over time describing the time it takes to go from our sustain level, to 0.0.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-HIGH PASS FILTER - Human ears on average will only perceive frequencies from 20-20,000HZ. A high pass filter will cut off lower frequencies described by a cutoff value. If we set our cutoff at 3,000HZ, we will be hearing only frequencies 3,000HZ and above. We also have a bandwidth setting, describing the slope or steepness of our cutoff.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-LOW PASS FILTER - Human ears on average will only perceive frequencies from 20-20,000HZ. A low pass filter will cut off higher frequencies described by a cutoff value. If we set our cutoff at 3,000HZ, we will be hearing only frequencies 3,000HZ and below. We also have a bandwidth setting, describing the slope or steepness of our cutoff. {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-OD - Overdrive Effect, \"Harshen\" up our tone, by taking a threshold volume level, and hard-clipping our signal at this level, meaning force samples louder than this level to sit at this level.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-TREMELO - This effect provides a \"wobbly\" effect by taking our signal, and letting it's volume be controled by an LFO. An LFO is a Low Frequency Oscillator, usually <20HZ. In our case 0-10HZ. By multiplying our signal by this LFO, this results in volume modulation.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-BIT CRUSHER - A two-part effect, resulting in a broken, downsampled, and/or phasing sound. The first parameter consists of degrading our sample rate, and second our bit depth. The sample rate of audio refers to how many samples are used to represent our signal in HZ. Modern music production requires a minimum sample rate of 44.1kHZ, and by downsampling this parameter, we get a harsher and more broken sound. Bit-Depth, refers to the precision used to represent each sample. In this implementation, We can represent a sample as \"0.9999999\" or \"0.9\" and tweaking this value will provide various sounds.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"MONO SYNTH - Monophonic synthesizer. Unlike the polyphonic synthesizer, we can only play a single note at a time here. We also have 3 oscillators here as opposed to a single oscillator in the polyphonic synthesizer. This provides us with the ability to add and subtract different wavetypes from each other. {Environment.NewLine}" +
            $"{Environment.NewLine}" +    
            $"-OSCILLATOR WAVE TYPE - Similarly to the polyphonic synth, this sets the desired wavetype. All standard again, this time not including \"Sweep\". This represents a sine wavetype with a start/end frequency, and will slide to the end note over a set time period in seconds.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-GAIN - This controls the overall volume of the individual oscillator. Bring to 0.0 to \"turn off\" oscillator. Experiment with different ratios between oscillators!{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-SWEEP END FREQUENCY - This is only enabled while using the \"Sweep\" wavetype. Sets the end frequency.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-SWEEP LENGTH - Time in seconds it will take for note to go from starting frequency to ending frequency.{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"MUSIC PLAYER - Play pre-made songs or loops here, using the current settings dialed in for the polyphonic synthesizer. Here, you are free to play along using the monophonic synthesizer :){Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"TIPS - {Environment.NewLine} " +
            $"* Stick to the bottom row (Z,X,C,V,B,N,M,COMMA) to play in the key of C Major. Hard to sound bad here!{Environment.NewLine}" +
            $"* Experiment with different parameter combinations for different sounds!{Environment.NewLine}" +
            $"* You can play one of the loops in the Music Player, and play over it using the monophonic synth! Loops are best to be played with at under 100bpm! {Environment.NewLine}" +
            $"* Have fun!";

        private String familiarText = $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"INSTRUCTIONS - {Environment.NewLine}" +
            $"* Play notes with keys (explained on page){Environment.NewLine}" +
            $"* drag window by menu bar.{Environment.NewLine}" +
            $"* To open monophonic synthesizer, click 'Mono Synth'.{Environment.NewLine}" +
            $"* To open music player, click 'Music Player'.{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"PRESETS - " +
            $"Load preset files to play through both synthesizers." +
            $"Each preset represents current patch. " +
            $"'Reset to Defaults' Resets each parameter to it's original state upon loading.{Environment.NewLine}{Environment.NewLine}" +
            $"FILE - {Environment.NewLine}" +
            $"From here, you may exit the program or open up this settings pages.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"POLY SYNTH (main window) {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"Polyphonic synthesizer. This is the main synthesizer.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-MASTER VOLUME - Adjusts volume of synthesizer.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-WAVETYPE - Selects wave type.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-OCTAVE MULTIPLIER - Selects note octave.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-ADSR ENVELOPE - Attack, Decay, Sustain, Release volume envelope. {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $" * The ATTACK stage is the fade in over time. {Environment.NewLine}" +
            $" * The DECAY stage fades down from the ATTACK stage to the level defined by our SUSTAIN stage.{Environment.NewLine}" +
            $" * The SUSTAIN stage keeps a constant volume level until the key is released.{Environment.NewLine}" +
            $" * The RELEASE stage fades our note out over time on key release.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-HIGH PASS FILTER -  High Pass Filter. Can adjust frequency cutoff and bandwidth{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-LOW PASS FILTER - Low Pass Filter. Can adjust frequency cutoff and bandwidth{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-OD - Overdrive Effect. Hard-clip threshold.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-TREMELO - Tremelo effect (in HZ). {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-BIT CRUSHER - Bit-Crusher implementation.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"MONO SYNTH - {Environment.NewLine}" +
            $"Monophonic synthesizer. We also have 3 oscillators here as opposed to a single oscillator in the polyphonic synthesizer.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-OSCILLATOR WAVE TYPE - Sets the desired wavetype. \"Sweep\" represents a sine wavetype with a start/end frequency, and will slide to the end note over a set time period in seconds.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-GAIN - This controls the overall volume of the individual oscillator. {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-SWEEP END FREQUENCY - This is only enabled while using the \"Sweep\" wavetype. Sets the end frequency.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-SWEEP LENGTH - Time in seconds it will take for note to go from starting frequency to ending frequency.{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"MUSIC PLAYER - Play pre-made songs or loops here, using the current settings dialed in for the polyphonic synthesizer. Here, you are free to play along using the monophonic synthesizer :){Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"TIPS - {Environment.NewLine}" +
            $"* Try playing legato runs using the monophonic synth!{Environment.NewLine}" +
            $"* You can use the UP and DOWN arrow keys to adjust the octave while you are playing :){Environment.NewLine}" +
            $"* Try to see if you can emulate real instruments through parameter/effect adjustments!{Environment.NewLine}";

        private String experiencedText = $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"INSTRUCTIONS - {Environment.NewLine}" +
            $"* Play notes with keys (explained on page){Environment.NewLine}" +
            $"* drag window by menu bar.{Environment.NewLine}" +
            $"* To open monophonic synthesizer, click 'Mono Synth'.{Environment.NewLine}" +
            $"* To open music player, click 'Music Player'.{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"PRESETS - " +
            $"Load Presets. " +
            $"'Reset to Defaults' Resets each parameter to it's original state upon loading.{Environment.NewLine}{Environment.NewLine}" +
            $"FILE - {Environment.NewLine}" +
            $"From here, you may exit the program or open up this settings pages.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"POLY SYNTH (main window) {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"Polyphonic synthesizer. This is the main synthesizer, and plays a chosen waveform with other various parameters to be tuned, resulting in a variety of possible sounds.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-MASTER VOLUME - Controls the overall volume of the monophonic synthesizer.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-WAVETYPE - Selects the type of audio wave to be synthesized.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-OCTAVE MULTIPLIER - This controls the overall pitch of the note we play. Think of it as a frequency multiplier.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-ADSR ENVELOPE - Attack, Decay, Sustain, Release volume envelope. {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-HIGH PASS FILTER -  Filter only lets the high frequencies pass through. Can adjust frequency cutoff and bandwidth{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-LOW PASS FILTER - Filter only lets the low frequencies pass through. Can adjust frequency cutoff and bandwidth{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-OD - Overdrive Effect, Hard clip our signal at this level, meaning force samples louder than this level to sit at this level.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-TREMELO - This effect provides a \"wobbly\" effect by taking our signal, and letting it's volume be controled by an LFO. {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-BIT CRUSHER - A two-part effect, resulting in a broken, downsampled, and/or phasing sound. The first parameter consists of degrading our sample rate, and second our bit depth.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"MONO SYNTH - {Environment.NewLine}" +
            $"3x Osc monophonic synthesizer.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-OSCILLATOR WAVE TYPE - Sets the desired wavetype. \"Sweep\" represents a sine wavetype with a start/end frequency, and will slide to the end note over a set time period in seconds.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-GAIN - Oscillator gain. {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-SWEEP END FREQUENCY - This is only enabled while using the \"Sweep\" wavetype. Sets the end frequency.{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"-SWEEP LENGTH - Time in seconds it will take for note to go from starting frequency to ending frequency.{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"MUSIC PLAYER - Play pre-made songs or loops here, using the current settings dialed in for the polyphonic synthesizer. Here, you are free to play along using the monophonic synthesizer :){Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"---------------------------------------------------------------------{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"TIPS - {Environment.NewLine}" +
            $"* Try holding chords and switching settings to play a bass line or melody over your chord!{Environment.NewLine}" +
            $"* Use the \"Robot\" wave type in a musical fashion...{Environment.NewLine}" +
            $"* Improv solos over the built-in loop progressions.{Environment.NewLine}" +
            $"* Write a song using only the tools provided here. ( Record elsewhere or 3rd party until V2 :( )";

        private bool newIsChecked = true;
        private bool familiarIsChecked = false;
        private bool experiencedIsChecked = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public String Version { get => version; }
        public String Author { get => author; }
        public String Date { get => date; }
        public String Name { get => name; }

        public String KeyMap
        {
            get => $" KEYBOARD NOTES:{Environment.NewLine}" +
                $"Ab = H {Environment.NewLine}" +
                $"A = N {Environment.NewLine}" +
                $"Bb = J {Environment.NewLine}" +
                $"B = M {Environment.NewLine}" +
                $"C = Z or Comma {Environment.NewLine}" +
                $"C# = S {Environment.NewLine}" +
                $"D = X {Environment.NewLine}" +
                $"Eb = D {Environment.NewLine}" +
                $"E = C {Environment.NewLine}" +
                $"F = V {Environment.NewLine}" +
                $"F# = G {Environment.NewLine}" +
                $"G = B {Environment.NewLine}" +
                $" --- {Environment.NewLine}" +
                $"Octave Up = Arrow Up {Environment.NewLine}" +
                $"Octave Down = Arrow Down{Environment.NewLine}" +
                $" --- {Environment.NewLine}" +
                $"Save Preset - Ctrl + S{Environment.NewLine}" +
                $"Load Preset - Ctrl + O{Environment.NewLine}" +
                $"Open Info Page - Ctrl + I{Environment.NewLine}" +
                $"Reset Synth Settings - Ctrl + R{Environment.NewLine}" +
                $"Quit - Ctrl + Q";
            set { }
        }

        public bool NewIsChecked
        {
            get => newIsChecked;

            set
            {
                if (value.Equals(newIsChecked))
                    return;
                
                newIsChecked = value;
                if (value)
                {
                    FamiliarIsChecked = false;
                    ExperiencedIsChecked = false;
                    ExplainText = newText;
                }
                OnPropertyChanged();
            }
        }
        public bool FamiliarIsChecked
        {
            get => familiarIsChecked;

            set
            {
                if (value.Equals(familiarIsChecked))
                    return;
            
                familiarIsChecked = value;
                if (value)
                {
                    NewIsChecked = false;
                    ExperiencedIsChecked = false;
                    ExplainText = familiarText;
                }
                OnPropertyChanged();
            }
        }
        public bool ExperiencedIsChecked
        {
            get => experiencedIsChecked;

            set
            {
                if (value.Equals(experiencedIsChecked))
                    return;
               
                experiencedIsChecked = value;
                if (value)
                {
                    NewIsChecked = false;
                    FamiliarIsChecked = false;
                    ExplainText = experiencedText;
                }
                OnPropertyChanged();
            }
        }

        public String About
        {
            get => Name + Environment.NewLine + Author + Environment.NewLine + Version + Environment.NewLine + Date;
            set { }
        }

        public String ExplainText
        {
            get
            { 
                if (String.IsNullOrEmpty(explainText))
                {                    
                    return newText;
                }
                else
                {                  
                    return explainText;
                }
            }

            set
            {
                if (value.Equals(explainText))
                    return;
                if (NewIsChecked)
                {
                    explainText = newText;
                }
                    
                else if (FamiliarIsChecked)
                {
                    explainText = familiarText;
                }
                else if (ExperiencedIsChecked)
                {
                    explainText = experiencedText;
                }
                else
                {
                    explainText = newText;
                }
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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
