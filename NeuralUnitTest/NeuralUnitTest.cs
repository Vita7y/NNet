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

            input1.Connect(new Dictionary<Neuron, double>() {{neuron1, 0.9}, {neuron2, 0.2}});
            input2.Connect(new Dictionary<Neuron, double>() {{neuron1, 0.3}, {neuron2, 0.8}});
            input1.Activation(1);
            input2.Activation(0.5);

            var res1 = Math.Round(neuron1.ActivationValue, 4);
            Assert.AreEqual(0.7408, res1);
            var res2 = Math.Round(neuron2.ActivationValue, 4);
            Assert.AreEqual(0.6457, res2);
        }

        [TestMethod]
        public void Test9NeuronInit()
        {
            var input1 = new Neuron(false);
            var input2 = new Neuron(false);
            var input3 = new Neuron(false);

            var neuron21 = new Neuron();
            var neuron22 = new Neuron();
            var neuron23 = new Neuron();

            var neuron31 = new Neuron();
            var neuron32 = new Neuron();
            var neuron33 = new Neuron();

            input1.Connect(new Dictionary<Neuron, double>() { { neuron21, 0.9 }, { neuron22, 0.2 }, { neuron23, 0.1 } });
            input2.Connect(new Dictionary<Neuron, double>() { { neuron21, 0.3 }, { neuron22, 0.8 }, { neuron23, 0.5 } });
            input3.Connect(new Dictionary<Neuron, double>() { { neuron21, 0.4 }, { neuron22, 0.2 }, { neuron23, 0.6 } });

            neuron21.Connect(new Dictionary<Neuron, double>() { { neuron31, 0.3 }, { neuron32, 0.6 }, { neuron33, 0.8 } });
            neuron22.Connect(new Dictionary<Neuron, double>() { { neuron31, 0.7 }, { neuron32, 0.5 }, { neuron33, 0.1 } });
            neuron23.Connect(new Dictionary<Neuron, double>() { { neuron31, 0.5 }, { neuron32, 0.2 }, { neuron33, 0.9 } });

            input1.Activation(0.9);
            input2.Activation(0.1);
            input3.Activation(0.8);

            var res1 = Math.Round(neuron31.ActivationValue, 3);
            Assert.AreEqual(0.726, res1);
            var res2 = Math.Round(neuron32.ActivationValue, 4);
            Assert.AreEqual(0.7086, res2);
            var res3 = Math.Round(neuron33.ActivationValue, 3);
            Assert.AreEqual(0.778, res3);
        }

    }
}
