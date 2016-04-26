using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Neural_Net
{
    class Net
    {
        public Net(List<int> topology)
        {
            // cout << "Net Initializer Called" << endl;
            m_layers = new List<List<Neuron>>();
            int numLayers = topology.Count;
            for (int layerNum = 0; layerNum < numLayers; ++layerNum)
            {
                m_layers.Add(new List<Neuron>());
                int numOutputs = layerNum == topology.Count - 1 ? 0 : topology[layerNum + 1];

                // We have a new layer, now fill it with neurons, and
                // add a bias neuron in each layer.
                for (int neuronNum = 0; neuronNum <= topology[layerNum]; ++neuronNum)
                {
                    m_layers.Last().Add(new Neuron(numOutputs, neuronNum));
                    // for debugging: cout << "Made a Neuron!" << endl;
                }

                // Force the bias node's output to 1.0 (it was the last neuron pushed in this layer):
                m_layers.Last().Last().m_outputVal = 1.0;

                //reset error to zero

            }
            // cout << "Net Initializer Completed" << endl;
        }


        public Net(List<int> topology, Gene inputGene)
        {
            m_layers = new List<List<Neuron>>();
            //cout << "Started topo + gene Net Constructor" << endl;
            int numLayers = topology.Count;
            //cout << "Number of Layers from topo: " << numLayers << endl;
            //cout << "Number of Layers from gene: " << inputGene.getWeights().size() << endl;
            for (int layerNum = 0; layerNum < numLayers; ++layerNum)
            {
                m_layers.Add(new List<Neuron>());
                //cout << "pushed on layer: " << layerNum << endl;
                //cout << "Number of neurons in layer from topo: " << topology[layerNum] << endl;
                //cout << "Number of neurons in layer from gene: " << inputGene.getWeights()[layerNum].size() << endl;
                int numOutputs = layerNum == topology.Count - 1 ? 0 : topology[layerNum + 1];
                // We have a new layer, now fill it with neurons, and
                // add a bias neuron in each layer.
                //cout << "number of outputs for layer calculated as: " << numOutputs << endl;

                for (int neuronNum = 0; neuronNum < topology[layerNum]; ++neuronNum)
                {
                    //cout << "number of outputs coming from gene: " << inputGene.getWeights()[layerNum][neuronNum].size() << endl;

                    m_layers.Last().Add(new Neuron(numOutputs, neuronNum, inputGene.getWeights()[layerNum][neuronNum]));
                    // for debugging: cout << "Made a Neuron!" << endl;
                }
                // add a bias node outside the loop... fml while debugging
                // shorthand, toplogy[layerNum] gives real value for number of nodes. Array indexes from 0 to topology[layerNum]-1. Therefore topology[layerNum] is the extra bias node.
                m_layers.Last().Add(new Neuron(numOutputs, topology[layerNum]));
                // Force the bias node's output to 1.0 (it was the last neuron pushed in this layer):
                m_layers.Last().Last().m_outputVal = 1.0;
                //reset error to zero
            }
        }


        public void feedForward(List<double> inputVals)
        {

            Debug.Assert(inputVals.Count == m_layers[0].Count - 1);

            // Assign (latch) the input values into the input neurons
            for (int i = 0; i < inputVals.Count; ++i)
            {
                m_layers[0][i].m_outputVal = inputVals[i];
            }

            // forward propagate
            for (int layerNum = 1; layerNum < m_layers.Count; ++layerNum)
            {
                List <Neuron> prevLayer = m_layers[layerNum - 1];
                for (int n = 0; n < m_layers[layerNum].Count - 1; ++n)
                {
                    m_layers[layerNum][n].feedForward(prevLayer);
                }
            }
        }


	    public void backProp(List<double> targetVals)
        {
            // Calculate overall net error (RMS of output neuron errors)

            List<Neuron> outputLayer = m_layers.Last();
            m_error = 0.0;

            for (int n = 0; n < outputLayer.Count - 1; ++n)
            {
                double delta = targetVals[n] - outputLayer[n].m_outputVal;
                m_error += delta * delta;
            }
            m_error /= outputLayer.Count - 1; // get average error squared
            m_error = Math.Sqrt(m_error); // RMS

            // Implement a recent average measurement

            m_recentAverageError =
                (m_recentAverageError * m_recentAverageSmoothingFactor + m_error)
                / (m_recentAverageSmoothingFactor + 1.0);

            // Calculate output layer gradients

            for (int n = 0; n < outputLayer.Count - 1; ++n)
            {
                outputLayer[n].calcOutputGradients(targetVals[n]);
            }

            // Calculate hidden layer gradients

            for (int layerNum = m_layers.Count - 2; layerNum > 0; --layerNum)
            {
                List<Neuron> hiddenLayer = m_layers[layerNum];
                List<Neuron> nextLayer = m_layers[layerNum + 1];

                for (int n = 0; n < hiddenLayer.Count; ++n)
                {
                    hiddenLayer[n].calcHiddenGradients(nextLayer);
                }
            }

            // For all layers from outputs to first hidden layer,
            // update connection weights

            for (int layerNum = m_layers.Count - 1; layerNum > 0; --layerNum)
            {
                List<Neuron> layer = m_layers[layerNum];
                List<Neuron> prevLayer = m_layers[layerNum - 1];

                for (int n = 0; n < layer.Count - 1; ++n)
                {
                    layer[n].updateInputWeights(prevLayer);
                }
            }
        }
	    public void getResults(List<double> resultVals)
        {
            resultVals.Clear();

            for (int n = 0; n < m_layers.Last().Count - 1; ++n)
            {
                resultVals.Add(m_layers.Last()[n].m_outputVal);
            }
        }
        public void printNetValues()
        {
            for (int i = 0; i < m_layers.Count; ++i)
            {
                //cout << "Layer :" << i << endl;
                for (int n = 0; n < m_layers[i].Count; ++n)
                {
                    // cout << "     Neuron: " << n << "Output Value: " << m_layers[i][n].getOutputVal() << endl;
                    for (int nconnect = 0; nconnect < m_layers[i][n].m_outputWeights.Count; ++nconnect)
                    {
                        //cout << "          Weight to Neuron" << nconnect << ": " << m_layers[i][n].getOutputConnections()[nconnect].weight << endl;
                    }
                }
            }
        }
        public double getRecentAverageError() { return m_recentAverageError; }
        public double getError() { return m_error; }
        private List<List<Neuron>> m_layers; // m_layers[layerNum][neuronNum]
        private double m_error;
        private double m_recentAverageError = 0;
        private static double m_recentAverageSmoothingFactor = 100.0;
    };
}
