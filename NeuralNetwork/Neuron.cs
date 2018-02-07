using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace NeuralNetwork
{
    [Serializable]
    public class Neuron
    {
        public Neuron()
        {
            Children = new Dictionary<Neuron, double>();
            Parents = new List<Neuron>();
            UseActivationCalc = true;
        }

        public Neuron(bool useActivation) : this()
        {
            UseActivationCalc = useActivation;
        }

        [XmlIgnore]
        public double ActivationValue { get; private set; }

        public readonly Dictionary<Neuron, double> Children;
        public readonly List<Neuron> Parents;
        public bool UseActivationCalc { get; set; }

        public void Connect(Dictionary<Neuron, double> children)
        {
            foreach (var child in children)
            {
                Children.Add(child.Key, child.Value);
                child.Key.AddFather(this);
            }
        }

        private void AddFather(Neuron father)
        {
            Parents.Add(father);
        }

        public void Activation(double value)
        {
            if (UseActivationCalc)
                ActivationValue = Calc(Parents.Sum(am => am.ActivationValue * am.Children[this]));
            else
                ActivationValue = value;

            foreach (var child in Children)
            {
                child.Key.Activation(ActivationValue);
            }
        }

        public static double Calc(double value)
        {
            return (1 / (1 + Math.Exp(-value)));
        }
    }
}
