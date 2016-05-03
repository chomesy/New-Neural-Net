using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Neural_Net
{
    using Layer = List<Neuron>;
    class Net
    {
        public Net(List<int> topology)
        {
            // cout << "Net Initializer Called" << endl;
            m_layers = new List<Layer>();
            int numLayers = topology.Count;
            for (int layerNum = 0; layerNum < numLayers; ++layerNum)
            {
                m_layers.Add(new Layer());
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


        /*
        public Net(List<int> topology, Gene inputGene)
        {
            m_layers = new List<Layer>();
            //cout << "Started topo + gene Net Constructor" << endl;
            int numLayers = topology.Count;
            //cout << "Number of Layers from topo: " << numLayers << endl;
            //cout << "Number of Layers from gene: " << inputGene.getWeights().size() << endl;
            for (int layerNum = 0; layerNum < numLayers; ++layerNum)
            {
                m_layers.Add(new Layer());
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
        */
        public Net(List<int> topology, Random rnd)
        {
            m_layers = new List<Layer>(topology.Count);
            for (int layerNum = 0; layerNum <= topology.Count - 1; ++layerNum)
            {
                m_layers.Add(new Layer());
                int numOutputs = layerNum == topology.Count - 1 ? 0 : topology[layerNum + 1];

                // We have a new layer, now fill it with neurons, and
                // add a bias neuron in each layer.
                for (int neuronNum = 0; neuronNum <= topology[layerNum]; ++neuronNum)
                {
                    m_layers.Last().Add(new Neuron(numOutputs, neuronNum, rnd));
                    // for debugging: cout << "Made a Neuron!" << endl;
                }

                // Force the bias node's output to 1.0 (it was the last neuron pushed in this layer):
                m_layers.Last().Last().m_outputVal = 1.0;
            }
        }

        public void feedForward(List<double> inputVals)
        {
            
            Debug.Assert(inputVals.Count == m_layers[0].Count - 1);
            try
            {
                // Assign (latch) the input values into the input neurons
                for (int i = 0; i < inputVals.Count; ++i)
                {
                    m_layers[0][i].m_outputVal = inputVals[i];
                }

                // forward propagate
                for (int layerNum = 1; layerNum < m_layers.Count; ++layerNum)
                {
                    Layer prevLayer = m_layers[layerNum - 1];
                    for (int n = 0; n < m_layers[layerNum].Count - 1; ++n)
                    {
                        m_layers[layerNum][n].feedForward(prevLayer);
                    }
                }
            }
            catch { MessageBox.Show("Feed Forward Failed"); throw; }
        }

	    public void backProp(List<double> targetVals)
        {
            // Calculate overall net error (RMS of output neuron errors)

            Layer outputLayer = m_layers.Last();
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
                Layer hiddenLayer = m_layers[layerNum];
                Layer nextLayer = m_layers[layerNum + 1];

                for (int n = 0; n < hiddenLayer.Count; ++n)
                {
                    hiddenLayer[n].calcHiddenGradients(nextLayer);
                }
            }

            // For all layers from outputs to first hidden layer,
            // update connection weights

            for (int layerNum = m_layers.Count - 1; layerNum > 0; --layerNum)
            {
                Layer layer = m_layers[layerNum];
                Layer prevLayer = m_layers[layerNum - 1];

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
        public List<double> getResults()
        {
            List<double> resultVals = new List<double>();

            for (int n = 0; n < m_layers.Last().Count - 1; ++n)
            {
                resultVals.Add(m_layers.Last()[n].m_outputVal);
            }
            return resultVals;
        }
        public string printNetValues()
        {
            string output = "";
            for (int i = 0; i < m_layers.Count; ++i)
            {
                 output += "Layer :" + i + "\n";
                for (int n = 0; n < m_layers[i].Count; ++n)
                {
                    output += "     Neuron: " + n + "Output Value: " + m_layers[i][n].m_outputVal + "\n";
                    for (int nconnect = 0; nconnect < m_layers[i][n].m_outputWeights.Count; ++nconnect)
                    {
                        output += "          Weight to Neuron" + nconnect + ": " + m_layers[i][n].m_outputWeights[nconnect].weight + "\n";
                    }
                }
            }
            return output;
        }
        public void populateTree(TreeNode basenode)
        {
            for (int i = 0; i < m_layers.Count; ++i)
            {
                TreeNode layernode = new TreeNode("Layer : " + i);
                for (int n = 0; n < m_layers[i].Count; ++n)
                {
                    TreeNode neuronnode = new TreeNode("Neuron: " + n + " Output Value: " + m_layers[i][n].m_outputVal);
                    for (int nconnect = 0; nconnect < m_layers[i][n].m_outputWeights.Count; ++nconnect)
                    {
                        TreeNode weightnode = new TreeNode("Weight to Neuron " + nconnect + ": " + m_layers[i][n].m_outputWeights[nconnect].weight + " Delta : " + m_layers[i][n].m_outputWeights[nconnect].deltaWeight);
                        neuronnode.Nodes.Add(weightnode);
                    }
                    layernode.Nodes.Add(neuronnode);
                }
                basenode.Nodes.Add(layernode);
            }
        }
        public void mutateGenes()
        {
            Random rnd = new Random();
            if (rnd.NextDouble() < _mutationmix)
            {
                //gene is selected first by random layer, then by random weight
                // hashtag mad randos

                for (int layer=0; layer<m_layers.Count; ++layer)
                {
                    for (int neuron=0; neuron<m_layers[layer].Count; ++neuron)
                    {
                        for (int weight=0; weight<m_layers[layer][neuron].m_outputWeights.Count - 1; ++weight)
                        {
                            m_layers[layer][neuron].m_outputWeights[weight].weight+= (rnd.NextDouble() - 0.5)*m_mutationstrength;
                        }
                    }
                }
            }
            else
            {
                //int randomLayer = rnd.Next() % g_weights.Count;
                if (rnd.NextDouble() > 0.5)
                {
                 //   ++g_topology[randomLayer];
                    // COMEBACK FOR THIS CODE

                }
                else
                {
                 //   --g_topology[randomLayer];
                    // COME BACK FOR THIS CODE
                }
            }
        }
        public double getRecentAverageError() { return m_recentAverageError; }
        public double getError() { return m_error; }
        public void computeError(List<double> targetVals)
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
        }
        public double getFitness() { return Math.Abs(1 / m_error); }

        private List<Layer> m_layers; // m_layers[layerNum][neuronNum]
        private double m_error = 0;
        private double m_recentAverageError = 0;
        private readonly double m_recentAverageSmoothingFactor = 100.0;
        private readonly double _mutationmix = 1; //the mix of weight changes to full neuron additions
        private readonly double m_mutationstrength = .25; //scale factor for perturbation amounts. Value of 1 corresponds to weight changes of [-0.5,0.5]
    };

}
