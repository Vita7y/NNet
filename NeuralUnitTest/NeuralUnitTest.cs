using System;
using System.Collections.Generic;
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
            var res = Math.Round(Neuron.ActivationCalc(1), 3);
            Assert.AreEqual(0.731, res);
            res = Math.Round(Neuron.ActivationCalc(1.05), 4);
            Assert.AreEqual(0.7408, res);
        }

        [TestMethod]
        public void Test2NeuronInit()
        {
            var input1 = new Neuron();
            var input2 = new Neuron();

            var neuron = new Neuron();
            input1.Connect(new Dictionary<Neuron, double>() {{neuron, 0.9}});
            input2.Connect(new Dictionary<Neuron, double>() {{neuron, 0.3}});

            input1.Input(1);
            input2.Input(0.5);

            var res = Math.Round(neuron.ActivationValue, 4);
            Assert.AreEqual(0.7408, res);
        }

        [TestMethod]
        public void Test4NeuronInit()
        {
            var input1 = new Neuron();
            var input2 = new Neuron();

            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            input1.Connect(new Dictionary<Neuron, double>() {{neuron1, 0.9}, {neuron2, 0.2}});
            input2.Connect(new Dictionary<Neuron, double>() {{neuron1, 0.3}, {neuron2, 0.8}});
            input1.Input(1);
            input2.Input(0.5);

            var res1 = Math.Round(neuron1.ActivationValue, 4);
            Assert.AreEqual(0.7408, res1);
            var res2 = Math.Round(neuron2.ActivationValue, 4);
            Assert.AreEqual(0.6457, res2);
        }

        [TestMethod]
        public void Test9NeuronInit()
        {
            var input1 = new Neuron();
            var input2 = new Neuron();
            var input3 = new Neuron();

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

            input1.Input(0.9);
            input2.Input(0.1);
            input3.Input(0.8);

            var res1 = Math.Round(neuron31.ActivationValue, 3);
            Assert.AreEqual(0.726, res1);
            var res2 = Math.Round(neuron32.ActivationValue, 4);
            Assert.AreEqual(0.7086, res2);
            var res3 = Math.Round(neuron33.ActivationValue, 3);
            Assert.AreEqual(0.778, res3);
        }

        [TestMethod]
        public void Test6NeuronCorrect()
        {
            var input1 = new Neuron();
            var input2 = new Neuron();
            var hide1 = new Neuron();
            var hide2 = new Neuron();
            var out1 = new Neuron();
            var out2 = new Neuron();

            input1.Connect(new Dictionary<Neuron, double>() { { hide1, 0.3 }, { hide2, 0.1 } });
            input2.Connect(new Dictionary<Neuron, double>() { { hide1, 0.2 }, { hide2, 0.7 } });
            hide1.Connect(new Dictionary<Neuron, double>() { { out1, 0.2 }, { out2, 0.1 } });
            hide2.Connect(new Dictionary<Neuron, double>() { { out1, 0.3 }, { out2, 0.4 } });

            input1.Input(1);
            input2.Input(0.5);

            var res1 = Math.Round(out1.ActivationValue, 1);
            Assert.AreEqual(0.6, res1);
            var res2 = Math.Round(out2.ActivationValue, 1);
            Assert.AreEqual(0.6, res2);

            out1.Correction(res1);
            var res11 = Math.Round(input1.ActivationValue, 3);
            Assert.AreEqual(0.6, res11);
            out2.Correction(res2);
            var res22 = Math.Round(input2.ActivationValue, 3);
            Assert.AreEqual(0.1, res22);
        }
    }
}
