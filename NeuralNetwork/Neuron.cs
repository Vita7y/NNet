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
        }

        [XmlIgnore] public double ActivationValue { get; private set; }

        public readonly Dictionary<Neuron, double> Children;
        public readonly List<Neuron> Parents;

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

        public void Input(double value)
        {
            ActivationValue = value;
            ChildrenEnumerate(ActivationValue);
        }

        public void Activation(double value)
        {
            ActivationValue = ActivationCalc(Parents.Sum(am => am.ActivationValue * am.Children[this]));
            ChildrenEnumerate(ActivationValue);
        }

        public void Correction(double value)
        {
            var sum = Parents?.Sum(am => am.ActivationValue)??0;
            if (sum == 0)
            {
                ActivationValue -= value;
                return;
            }

            foreach (var parent in Parents)
            {
                var res = CorrectionCalc(sum, parent.ActivationValue, value);
                parent.Correction(res);
            }
        }

        private void ChildrenEnumerate(double activate)
        {
            foreach (var child in Children)
            {
                child.Key.Activation(activate);
            }
        }

        public static double ActivationCalc(double value)
        {
            return (1 / (1 + Math.Exp(-value)));
        }

        public static double CorrectionCalc(double summ, double part, double value)
        {
            return (part / summ) * value;
        }
    }
}
