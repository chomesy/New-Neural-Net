using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Neural_Net
{
    using Layer = List<Neuron>;

    class Connection
    {
        public double weight;
        public double deltaWeight;
    }

    class Neuron
    {
        public Neuron(int numOutputs, int myIndex)
        {
            m_outputWeights = new List<Connection>();
            for (int c = 0; c < numOutputs; ++c)
            {
                m_outputWeights.Add(new Connection());
                m_outputWeights.Last().weight = rnd.NextDouble();
            }

            m_myIndex = myIndex;
        }
        public Neuron(int numOutputs, int myIndex, List<double> outputweights)
        {
            m_outputWeights = new List<Connection>();
            for (int c = 0; c < outputweights.Count && c < numOutputs; ++c)
            {
                m_outputWeights.Add(new Connection());
                m_outputWeights.Last().weight = outputweights[c];
            }
            for (int c = outputweights.Count(); c < numOutputs; ++c)
            {
                m_outputWeights.Add(new Connection());
                m_outputWeights.Last().weight = rnd.NextDouble();
            }

            m_myIndex = myIndex;
        }

        public void feedForward(Layer prevLayer)
        {
            double sum = 0.0;

            // Sum the previous layer's outputs (which are our inputs)
            // Include the bias node from the previous layer.

            for (int n = 0; n < prevLayer.Count; ++n)
            {
                sum += prevLayer[n].m_outputVal *
                    prevLayer[n].m_outputWeights[m_myIndex].weight;
            }

            m_outputVal = transferFunction(sum);
        }

        public void calcOutputGradients(double targetVal)
        {
            double delta = targetVal - m_outputVal;
            m_gradient = delta * transferFunctionDerivative(m_outputVal);
        }

        public void calcHiddenGradients(Layer nextLayer)
        {
            double dow = sumDOW(nextLayer);
            m_gradient = dow * transferFunctionDerivative(m_outputVal);
        }

        public void updateInputWeights(Layer prevLayer)
        {
            // The weights to be updated are in the Connection container
            // in the neurons in the preceding layer

            for (int n = 0; n < prevLayer.Count; ++n)
            {
                Neuron neuron = prevLayer[n];
                double oldDeltaWeight = neuron.m_outputWeights[m_myIndex].deltaWeight;

                double newDeltaWeight =
                    // Individual input, magnified by the gradient and train rate:
                    eta
                    * neuron.m_outputVal
                    * m_gradient
                    // Also add momentum = a fraction of the previous delta weight;
                    + alpha
                    * oldDeltaWeight;

                neuron.m_outputWeights[m_myIndex].deltaWeight = newDeltaWeight;
                neuron.m_outputWeights[m_myIndex].weight += newDeltaWeight;
            }
        }


        private static double eta = 0.15;   // [0.0..1.0] overall net training rate
        private static double alpha = 0.5;// [0.0..n] multiplier of last weight change (momentum)
        private static double transferFunction(double x) { return Math.Tanh(x); /* tanh - output range [-1.0..1.0] */}
        private static double transferFunctionDerivative(double x) { return 1.0 - x * x; /* tanh derivative */ }
        private Random rnd = new Random();
        private double sumDOW(Layer nextLayer)
        { 
            double sum = 0.0;
            // Sum our contributions of the errors at the nodes we feed.

            for (int n = 0; n<nextLayer.Count - 1; ++n) {
                sum += m_outputWeights[n].weight* nextLayer[n].m_gradient;
            }

            return sum;
        }
        public double m_outputVal { get; set; }
        public List<Connection> m_outputWeights { get; set; }
        private int m_myIndex;
        private double m_gradient;
    }
}
