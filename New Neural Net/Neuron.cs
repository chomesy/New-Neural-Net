using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Neural_Net
{
    using System.Windows.Forms;
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
                Random rnd = new Random(c);
                m_outputWeights.Add(new Connection());
                m_outputWeights.Last().weight = rnd.NextDouble();
            }

            m_myIndex = myIndex;
        }
        public Neuron(int numOutputs, int myIndex, Random rnd)
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
                Random rnd = new Random(c);
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
                try
                {               
                        sum += prevLayer[n].m_outputVal *
                            prevLayer[n].m_outputWeights[m_myIndex].weight;
                    if (double.IsNaN(sum) || double.IsInfinity(sum)) throw new System.NotFiniteNumberException();
                    
                }
                catch
                {
                    MessageBox.Show("Overflow calculating neuron output value"); throw;
                }
               
            }

            m_outputVal = transferFunction(sum);
        }

        public void calcOutputGradients(double targetVal)
        {
            double delta = m_outputVal - targetVal;
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
                try
                {

                        double newDeltaWeight =
                            // Individual input, magnified by the gradient and train rate:
                            -1
                            * eta
                            * neuron.m_outputVal
                            * m_gradient
                            // Also add momentum = a fraction of the previous delta weight;
                            + alpha
                            * oldDeltaWeight;
                        if (double.IsNaN(newDeltaWeight) || double.IsInfinity(newDeltaWeight)) throw new System.NotFiniteNumberException();

                        neuron.m_outputWeights[m_myIndex].deltaWeight = newDeltaWeight;
                        neuron.m_outputWeights[m_myIndex].weight += newDeltaWeight;
                    
                }
                catch { MessageBox.Show("Overflow when computing Output Weights"); break; throw; }

            }
        }


       
        private static double transferFunction(double x)
        {
            //return Math.Log(1+Math.Exp(x)); /* soft plus baby */
            //return Math.Tanh(x);

            return x;
        }
        private static double transferFunctionDerivative(double x)
        {
            //return 1/(1+Math.Exp(-x)); /* softplus derivative */
            return 1;
            //return 1.0 - x * x;
        }
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

        private static double eta = 0.000005;   // [0.0..1.0] overall net training rate
        private static double alpha = .5;// [0.0..n] multiplier of last weight change (momentum)
        private int m_myIndex;
        private double m_gradient;
    }
}
