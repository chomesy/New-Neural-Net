﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Neural_Net
{
    using Layer = List<Neuron>;
    class Net : IComparable
    {
        public Net(List<int> topology)
        {
            // cout << "Net Initializer Called" << endl;
            _layers = new List<Layer>();
            int numLayers = topology.Count;
            for (int layerNum = 0; layerNum < numLayers; ++layerNum)
            {
                _layers.Add(new Layer());
                int numOutputs = layerNum == topology.Count - 1 ? 0 : topology[layerNum + 1];

                // We have a new layer, now fill it with neurons, and
                // add a bias neuron in each layer.
                for (int neuronNum = 0; neuronNum <= topology[layerNum]; ++neuronNum)
                {
                    _layers.Last().Add(new Neuron(numOutputs, neuronNum));
                    // for debugging: cout << "Made a Neuron!" << endl;
                }

                // Force the bias node's output to 1.0 (it was the last neuron pushed in this layer):
                _layers.Last().Last().m_outputVal = 1.0;

                //reset error to zero

            }
            // cout << "Net Initializer Completed" << endl;
        }
        public Net(List<int> topology, Random rnd)
        {
            _layers = new List<Layer>(topology.Count);
            for (int layerNum = 0; layerNum <= topology.Count - 1; ++layerNum)
            {
                _layers.Add(new Layer());
                int numOutputs = layerNum == topology.Count - 1 ? 0 : topology[layerNum + 1];

                // We have a new layer, now fill it with neurons, and
                // add a bias neuron in each layer.
                for (int neuronNum = 0; neuronNum <= topology[layerNum]; ++neuronNum)
                {
                    _layers.Last().Add(new Neuron(numOutputs, neuronNum, rnd));
                    // for debugging: cout << "Made a Neuron!" << endl;
                }

                // Force the bias node's output to 1.0 (it was the last neuron pushed in this layer):
                _layers.Last().Last().m_outputVal = 1.0;
            }
        }
        public Net(List<Layer> inputNet)
        {
            _layers = inputNet;
        }

        public Net clone()
        {
           // MessageBox.Show("clone started");
            List<Layer> tmpNet = new List<Layer>();
            foreach(Layer inputLayer in _layers)
            {
                //MessageBox.Show("clone layer loop started");
                Layer tmpLayer = new Layer();
                foreach(Neuron tmpNeuron in inputLayer)
                {
                   // MessageBox.Show("clone neuron loop started");
                    tmpLayer.Add(tmpNeuron.clone());
                }
                tmpNet.Add(tmpLayer);
                tmpNet.Last().Last().m_outputVal = 1.0;
            }
            return new Net(tmpNet);
        }
        public List<Net> procreate()
        {
            List<Net> resultNets = new List<Net>();
            resultNets.Add(this.clone());
            //try killing a neuron
            double lowOutput = double.PositiveInfinity;
            int tmplay = -1;
            int tmpneu = -1;

            // loops to find lowest ouput value
            // layers start at 1 (can't kill input neurons)
            for (int l=1; l<_layers.Count; ++l)
            {
                // stop before bias neuron (count - 1)
                for (int n=0; n <_layers[l].Count-1; ++n)
                {
                    
                    double tmp = Math.Abs(_layers[l][n].m_outputVal);
                    if (tmp < lowOutput)
                    {
                        lowOutput = tmp;
                        tmplay = l;
                        tmpneu = n;
                    }
                }
            }
            resultNets[0].destroy(tmplay, tmpneu);

            // mutate based on current error size
            resultNets.Add(this.clone());
            resultNets.Last().mutateGenes(_recentAverageError);

            // thats all she wrote
            return resultNets;
        }
        private void destroy (int layer, int neuron)
        {
            if (layer < 0 || neuron < 0) return;
            else
            {
                //destroy the neuron
                this._layers[layer].RemoveAt(neuron);
                //reindex the holding layer
                for (int i = neuron; i<_layers[layer].Count; ++i)
                {
                    _layers[layer][i].m_myIndex = i;

                }
                //repoint previous neurons
                //First check if it's a bias neuron (otherwise there are no weights pointing there already)
                if (neuron != _layers[layer].Count)
                {
                    for (int i = 0; i < _layers[layer - 1].Count; ++i)
                    {
                        _layers[layer - 1][i].m_outputWeights.RemoveAt(neuron);
                    }
                }
            }
        }

        public void feedForward(List<double> inputVals)
        {
            
            Debug.Assert(inputVals.Count == _layers[0].Count - 1);
            try
            {
                // Assign (latch) the input values into the input neurons
                for (int i = 0; i < inputVals.Count; ++i)
                {
                    _layers[0][i].m_outputVal = inputVals[i];
                }

                // forward propagate
                for (int layerNum = 1; layerNum < _layers.Count; ++layerNum)
                {
                    Layer prevLayer = _layers[layerNum - 1];
                    for (int n = 0; n < _layers[layerNum].Count - 1; ++n)
                    {
                        _layers[layerNum][n].feedForward(prevLayer);
                    }
                }
                computeResults();
            }
            catch { MessageBox.Show("Feed Forward Failed"); throw; }
        }
	    public void backProp(List<double> targetVals)
        {
            // Calculate overall net error (RMS of output neuron errors)

            Layer outputLayer = _layers.Last();
            _error = 0.0;

            for (int n = 0; n < outputLayer.Count - 1; ++n)
            {
                double delta = targetVals[n] - outputLayer[n].m_outputVal;
                _error += delta * delta;
            }
            _error /= outputLayer.Count - 1; // get average error squared
            _error = Math.Sqrt(_error); // RMS

            // Implement a recent average measurement

            _recentAverageError =
                (_recentAverageError * _recentAverageSmoothingFactor + _error)
                / (_recentAverageSmoothingFactor + 1.0);

            // Calculate output layer gradients

            for (int n = 0; n < outputLayer.Count - 1; ++n)
            {
                outputLayer[n].calcOutputGradients(targetVals[n]);
            }

            // Calculate hidden layer gradients

            for (int layerNum = _layers.Count - 2; layerNum > 0; --layerNum)
            {
                Layer hiddenLayer = _layers[layerNum];
                Layer nextLayer = _layers[layerNum + 1];

                for (int n = 0; n < hiddenLayer.Count; ++n)
                {
                    hiddenLayer[n].calcHiddenGradients(nextLayer);
                }
            }

            // For all layers from outputs to first hidden layer,
            // update connection weights

            for (int layerNum = _layers.Count - 1; layerNum > 0; --layerNum)
            {
                Layer layer = _layers[layerNum];
                Layer prevLayer = _layers[layerNum - 1];

                for (int n = 0; n < layer.Count - 1; ++n)
                {
                    layer[n].updateInputWeights(prevLayer);
                }
            }
        }
	    /*
        public void getResults(List<double> resultVals)
        {
            resultVals.Clear();

            for (int n = 0; n < _layers.Last().Count - 1; ++n)
            {
                resultVals.Add(_layers.Last()[n].m_outputVal);
            }
        }
        */
        private void computeResults()
        {
            resultVals = new List<double>();

            for (int n = 0; n < _layers.Last().Count - 1; ++n)
            {
                resultVals.Add(_layers.Last()[n].m_outputVal);
            }
        }
        public string printNetValues()
        {
            string output = "";
            for (int i = 0; i < _layers.Count; ++i)
            {
                 output += "Layer :" + i + "\n";
                for (int n = 0; n < _layers[i].Count; ++n)
                {
                    output += "     Neuron: " + n + "Output Value: " + _layers[i][n].m_outputVal + "\n";
                    for (int nconnect = 0; nconnect < _layers[i][n].m_outputWeights.Count; ++nconnect)
                    {
                        output += "          Weight to Neuron" + nconnect + ": " + _layers[i][n].m_outputWeights[nconnect].weight + "\n";
                    }
                }
            }
            return output;
        }
        public void populateTree(TreeNode basenode)
        {
            for (int i = 0; i < _layers.Count; ++i)
            {
                TreeNode layernode = new TreeNode("Layer : " + i);
                for (int n = 0; n < _layers[i].Count; ++n)
                {
                    TreeNode neuronnode = new TreeNode("Neuron: " + n + " Output Value: " + _layers[i][n].m_outputVal);
                    for (int nconnect = 0; nconnect < _layers[i][n].m_outputWeights.Count; ++nconnect)
                    {
                        TreeNode weightnode = new TreeNode("Weight to Neuron " + nconnect + ": " + _layers[i][n].m_outputWeights[nconnect].weight + " Delta : " + _layers[i][n].m_outputWeights[nconnect].deltaWeight);
                        neuronnode.Nodes.Add(weightnode);
                    }
                    layernode.Nodes.Add(neuronnode);
                }
                basenode.Nodes.Add(layernode);
            }
        }
        public void mutateGenes()
        {
            mutateGenes(1);
        }
        public void mutateGenes(double mutationparameter)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() < _mutationmix)
            {
                //gene is selected first by random layer, then by random weight
                // hashtag mad randos

                for (int layer=0; layer<_layers.Count; ++layer)
                {
                    for (int neuron=0; neuron<_layers[layer].Count; ++neuron)
                    {
                        for (int weight=0; weight<_layers[layer][neuron].m_outputWeights.Count - 1; ++weight)
                        {
                            _layers[layer][neuron].m_outputWeights[weight].weight+= (rnd.NextDouble() - 0.5)*_mutationStrength*mutationparameter;
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
        public double getRecentAverageError() { return _recentAverageError; }
        public double getError() { return _error; }
        public void computeError(List<double> targetVals)
        {
            // Calculate overall net error (RMS of output neuron errors)

            List<Neuron> outputLayer = _layers.Last();
            _error = 0.0;

            for (int n = 0; n < outputLayer.Count - 1; ++n)
            {
                double delta = targetVals[n] - outputLayer[n].m_outputVal;
                _error += delta * delta;
            }
            _error = Math.Sqrt(_error); // RMS
        }
        public double getFitness() { return Math.Abs(1 / _error); }

        public int CompareTo(object obj)
        {
            Net comparison = obj as Net;
            double tmp = this.getRecentAverageError() - comparison.getRecentAverageError();
            if (tmp > 0) return 1;
            if (tmp < 0) return -1;
            else return 0;
        }

        public List <double> resultVals { get; private set; }
        private List<Layer> _layers; // m_layers[layerNum][neuronNum]
        private double _error = 0;
        private double _recentAverageError = 0;
        private readonly double _recentAverageSmoothingFactor = 100.0;
        private readonly double _mutationmix = 1; //the mix of weight changes to full neuron additions
        private readonly double _mutationStrength = 1; //scale factor for perturbation amounts. Value of 1 corresponds to weight changes of [-0.5,0.5]
    };

}
