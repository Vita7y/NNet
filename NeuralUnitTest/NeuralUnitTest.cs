using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralNetwork;

namespace NeuralUnitTest
{
    [TestClass]
    public class NeuralUnitTest
    {
        [TestMethod]
        public void TestOneNeuronCreate()
        {
            var neuron = new Neuron();
        }

        [TestMethod]
        public void TestOneNeuronInit()
        {
            var res = Math.Round(Neuron.Calc(1), 3);
            Assert.AreEqual(0.731, res);
            res = Math.Round(Neuron.Calc(1.05), 4);
            Assert.AreEqual(0.7408, res);
        }

        [TestMethod]
        public void Test2NeuronInit()
        {
            var input1 = new Neuron(false);
            var input2 = new Neuron(false);

            var neuron = new Neuron();
            input1.Connect(new Dictionary<Neuron, double>() {{neuron, 0.9}});
            input2.Connect(new Dictionary<Neuron, double>() {{neuron, 0.3}});

            input1.Activation(1);
            input2.Activation(0.5);

            var res = Math.Round(neuron.ActivationValue, 4);
            Assert.AreEqual(0.7408, res);
        }

        [TestMethod]
        public void Test4NeuronInit()
        {
            var input1 = new Neuron(false);
            var input2 = new Neuron(false);

            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            input1.Connect(new Dictionary<Neuron, double>() {{neuron1, 0.9}, {neuron2, 0.3}});
            input2.Connect(new Dictionary<Neuron, double>() {{neuron1, 0.2}, {neuron2, 0.8}});
            input1.Activation(1);
            input2.Activation(0.5);

            var res1 = Math.Round(neuron1.ActivationValue, 4);
            Assert.AreEqual(0.7408, res1);
            var res2 = Math.Round(neuron2.ActivationValue, 4);
            Assert.AreEqual(0.6457, res2);
        }

    }
}
