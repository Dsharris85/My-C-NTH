using System;

namespace Demo
{   
    class ADSREnvelope
    {
        // Stages for envelope
        public enum EnvelopeStage
        {
            Off,
            Attack,
            Decay,
            Sustain,
            Release
        };

        // Which stage are we in
        private EnvelopeStage stage;

        // Multiplied against sample
        private double multiplier;

        // Attack
        private double attackRate;
        private double attackCoef;
        private double attackStart;
        // Decay
        private double decayRate;
        private double decayCoef;
        private double decayStart;
        // Sustain
        private double sustainLevel;
        // Release
        private double releaseRate;
        private double releaseCoef;
        private double releaseStart;

        // Coefs
        private double targetA;
        private double targetDR;
        private double exp;

        // Get/Sets - calculating line based on each of the 4 sliders posistions
        public double AttackRate
        {
            get => attackRate;
            set
            {
                attackRate = value;
                attackCoef = CalcCoef(attackRate, targetA);
                attackStart = (1.0 + targetA) * (1.0 - attackCoef);
            }
        }
        public double DecayRate
        {
            get => decayRate;
            set
            {
                decayRate = value;
                decayCoef = CalcCoef(decayRate, targetDR);
                decayStart = (sustainLevel - targetDR) * (1.0 - decayCoef);
            }
        }
        public double SustainLevel
        {
            get => sustainLevel;
            set
            {
                sustainLevel = value;
                decayStart = (sustainLevel - targetDR) * (1.0 - decayCoef);
            }
        }
        public double ReleaseRate
        {
            get => releaseRate;
            set
            {
                releaseRate = value;
                releaseCoef = CalcCoef(releaseRate, targetDR);
                releaseStart = -targetDR * (1.0 - releaseCoef);
            }
        }
        public double TargetA
        {            
            set
            {
                // Should be enough
                if (targetA < 0.000000001)
                    targetA = 0.000000001;
                if (value.Equals(targetA))
                    return;
                attackStart = (1.0 + targetA) * (1.0 - attackCoef);
            }
        }
        public double TargetDR
        {
            set
            {
                if (targetDR < 0.000000001)
                    targetDR = 0.000000001;
                if (value.Equals(targetDR))
                    return;
                decayStart = (sustainLevel - targetDR) * (1.0 - decayCoef);
                releaseStart = targetDR * (1.0 - releaseCoef);
            }
        }
        public EnvelopeStage Stage
        {
            get
            {
                return stage;
            }
        }

        // Construct
        public ADSREnvelope()
        {
            stage = EnvelopeStage.Off;
            multiplier = 0.0;
            AttackRate = 0.0;
            DecayRate = 0.0;
            ReleaseRate = 0.0;
            SustainLevel = 1.0;
            TargetA = 0.3;
            TargetDR = 0.0001;
        }

        // Calculate 'curve' between rate & targetRatio
        private double CalcCoef(double time, double target)
        {
            // time = samples for move
            // target = our "overshoot" value
            exp = -Math.Log( (1.0 + target) / target) / time;
            return Math.Exp(exp);
        }

        // Return a multiple to process individual samples by
        public double ProcessSample()
        {
            switch (stage)
            {
                // Do nothing if off
                case EnvelopeStage.Off:
                    break;
                // Calc from start to decay
                case EnvelopeStage.Attack:                    
                    multiplier = attackStart + multiplier * attackCoef;

                    // When mult reaches limit, switch stages
                    if (multiplier >= 1.0f)
                    {
                        // Don't clip!
                        multiplier = 1.0f;
                        stage = EnvelopeStage.Decay;
                    }
                    break;
                // Calc from decay to sustain
                case EnvelopeStage.Decay:
                    multiplier = decayStart + multiplier * decayCoef;
                    
                    // Going from top down, so if < 
                    if (multiplier <= sustainLevel)
                    {
                        // Constant (level !time based)
                        multiplier = sustainLevel;
                        stage = EnvelopeStage.Sustain;
                    }
                    break;
                // No calcs needed, we stay at constant level
                case EnvelopeStage.Sustain:                    
                    break;
                // Calc from release to end
                case EnvelopeStage.Release:
                    multiplier = releaseStart + multiplier * releaseCoef;

                    // When curve ends, turn off
                    if (multiplier <= 0.0)
                    {
                        multiplier = 0.0f;
                        stage = EnvelopeStage.Off;
                    }
                    break;
            }
            return multiplier;
        }

        // Switch b/t Attack and Release stages (used to start/stop)
        public void Trigger(bool gate)
        {
            if (gate)
                stage = EnvelopeStage.Attack;
            else if (stage != EnvelopeStage.Off)
                stage = EnvelopeStage.Release;
        }       
    }
}
