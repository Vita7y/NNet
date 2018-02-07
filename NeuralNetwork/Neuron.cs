using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class SignalArgs: EventArgs
    {
        public SignalArgs(double value)
        {
            Value = value;
        }

        public double Value { get; }

    }

    [Serializable]
    public class Neuron
    {
        private readonly Dictionary<Neuron, double> _parrents;
        private readonly bool _useActivation;
        private event EventHandler<SignalArgs> GeneratedSignal;

        public Neuron(Dictionary<Neuron, double> parrents, bool useActivation=true)
        {
            _useActivation = useActivation;
            _parrents = parrents ?? new Dictionary<Neuron, double>();

            if (parrents != null)
            {
                foreach (var father in parrents)
                {
                    father.Key.AddChild(this, father.Value);
                    father.Key.GeneratedSignal += OnInputSignal;
                }
            }
        }

        private void AddChild(Neuron neuron, double childWeight)
        {
            _parrents.Add(neuron, childWeight);
        }

        public double Value { get; private set; } 

        public void Activation(double value)
        {
            foreach (var parrent in _parrents)
            {
                var result = _useActivation ? Calc(value, parrent.Value) : (value * parrent.Value);
                Value = result;
                GeneratedSignal?.Invoke(this, new SignalArgs(result));
            }
        }

        public static double Calc(double value, double weight)
        {
            return (1 / (1 + Math.Exp(-value)));
        }

        private void OnInputSignal(object sender, SignalArgs args)
        {
            Activation(_parrents?.Sum(am => am.Key.Value) ?? args.Value);
        }

    }
}
