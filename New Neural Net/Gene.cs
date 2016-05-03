using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Neural_Net
{
    /*
      class Gene
    {

        //Constructors
        public Gene(List<List<List<double>>> weights, List<int> topology)
        {
            g_weights = weights;
            g_topology = topology;
            g_numweights = 0;
            for (int i = 0; i < topology.Count; ++i)
            {
                g_numweights += topology[i];
            }
        }
        public Gene(List<int> topology, Random rnd)
        {
            //MessageBox.Show("Gene being Made");
            g_weights = new List<List<List<double>>>();
            g_topology = topology;
            g_numweights = 0;

            for (int layer = 0; layer < g_topology.Count; ++layer)
            {
                //MessageBox.Show("First loop");
                List<List<double>> tmplayer = new List<List<double>>();
                for (int neuron = 0; neuron < g_topology[layer]; ++neuron)
                {
                    //MessageBox.Show("Second Loop");
                    List<double> tmpneuron = new List<double>();
                    if (layer != g_topology.Count - 1)
                    {
                        //MessageBox.Show("Inside the if");
                        for (int connection = 0; connection < g_topology[layer + 1]; ++connection)
                        {
                            tmpneuron.Add(rnd.NextDouble());
                            g_numweights++;
                            //MessageBox.Show("Adding actual neuron connection: " + debugrand);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Else Condition");
                        tmpneuron.Add(0);
                    }
                    //MessageBox.Show("Neuron " + neuron + " Added to Layer: " + layer + " with " + tmpneuron.Count + " connections");
                    tmplayer.Add(tmpneuron);
                }
                //MessageBox.Show("Layer version: Layer: " + layer + " added to Gene with " + tmplayer.Count + " neurons. Zeroth neuron has " + tmplayer[0].Count + "connections");
                g_weights.Add(tmplayer);
                //MessageBox.Show("Gene Version: Layer: " + (g_weights.Count-1) + " added to Gene with " + g_weights.Last().Count + " neurons. Zeroth neuron has " + g_weights.Last()[0].Count + "connections");
                //MessageBox.Show("Matrix Version: Zeroth neuron has " + g_weights[layer][0].Count);


            }
            //MessageBox.Show("at end of Gene Constructor: Layer 1, Neuron 0 count: " + g_weights[1][0].Count);
        }
        public Gene()
        {
            //uninitialized Gene assumes 1 1 1 toplogy
            g_topology.Add(1);
            g_topology.Add(1);
            g_topology.Add(1);

            //We'll see if this one works...... ick
            //Two weights for 1 to 1 to 1
            List<double> tmpneuron = new List<double>();
            List<List<double>> tmplayer = new List<List<double>>();
            tmpneuron.Add(1);
            tmplayer.Add(tmpneuron);
            g_weights.Add(tmplayer);
            g_weights.Add(tmplayer);

            g_numweights = 2;
        }
        //member function to mutate the genes
        public void mutateGene()
        {
            Random rnd = new Random();
            if (rnd.NextDouble() < g_mutationmix)
            {
                //gene is selected first by random layer, then by random weight
                // hashtag mad randos
                int randomLayer = rnd.Next() % g_weights.Count;
                int randomNeuron = rnd.Next() % g_weights[randomLayer].Count;
                g_weights[randomLayer][randomNeuron][rnd.Next() % g_weights[randomLayer][randomNeuron].Count] = rnd.NextDouble();
            }
            else
            {
                int randomLayer = rnd.Next() % g_weights.Count;
                if (rnd.NextDouble() > 0.5)
                {
                    ++g_topology[randomLayer];
                    // COMEBACK FOR THIS CODE

                }
                else
                {
                    --g_topology[randomLayer];
                    // COME BACK FOR THIS CODE
                }
            }
        }
        //get and set functions
        public double getGene(int parameter, int layerNum, int neuronNum) { return 0; }
        public void setGene(int parameter, int layerNum, int neuronNum, double geneVal) { }
        public List<List<List<double>>> getWeights() { return g_weights; }

        //parameter [0,1] describing mix of weight mutations (0) to topology mutations (1)
        public static readonly double g_mutationmix = 0;

        //Genes are really just the weights and topology encoded
        private List<List<List<double>>> g_weights;
        private List<int> g_topology;
        private int g_numweights;
    };
*/
}
