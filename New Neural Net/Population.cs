using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Neural_Net
{
    class Population
    {
        public Population(int popSize, List<int> topology)
        {
            //cout << "Population Initializer Started" << endl;
            thePopulation = new List<Net>(popSize);
            Random rnd = new Random();
            for (int i = 0; i < popSize; ++i)
            {
                //MessageBox.Show("In Population Constructor: Layer 1, Neuron 1, number of connections = " + newRandGene.getWeights()[0][0].Count);
                Net newNet = new Net(topology, rnd);
                thePopulation.Add(newNet);
                //MessageBox.Show("New Individual: " + i);
                //MessageBox.Show(newIndividual.getDeets());
            }
            //cout << "Population constructor completed" << endl;
        }
        public override string ToString()
        {
            string output = "";
            for (int i=0; i<thePopulation.Count; ++i)
            {
                output += "Individual #" + i + " Fitness = " + thePopulation[i].getFitness() + "\n";
            }
            return output;
        }
        public Net getNet(int index) { return thePopulation[index]; }
        public void addNets(int popSize, List<int> topology)
        {
            Random rnd = new Random();
            for (int i = 0; i < popSize; ++i)
            {
                Net newNet = new Net(topology, rnd);
                thePopulation.Add(newNet);
            }
        }
        public Net getFittest() { return thePopulation[fittestIndex]; }
        public void sortPopulation()
        {
            thePopulation.Sort(CompareNets);
            fittestIndex = 0;
        }
        public int CompareNets(Net x, Net y)
        {
            double tmp = x.getRecentAverageError() - y.getRecentAverageError();
            if (tmp > 0) return 1;
            if (tmp < 0) return -1;
            else return 0;
        }
        public void iterateGeneration(List <double> inputs, List <double> outputs)
        {
            topFitness = 0;
            for (int i=0; i<thePopulation.Count; ++i)
            {
                try
                {
                    thePopulation[i].feedForward(inputs);
                    thePopulation[i].computeError(outputs);
                    thePopulation[i].backProp(outputs);

                    if (thePopulation[i].getFitness() > topFitness)
                    {
                        topFitness = thePopulation[i].getFitness();
                        fittestIndex = i;
                    }
                    else
                    {
                        //thePopulation[i].mutate();
                    }
                    thePopulation[i].feedForward(inputs);
                }
                catch { MessageBox.Show("Iteration Failed"); throw; }
            }
        }
        public void mutateGeneration()
        {
            foreach (Net tmpnet in thePopulation)
            {
                tmpnet.mutateGenes();
            }
        }
        public void cullGeneration(double percentage)
        {
            int max = (int)Math.Floor(Math.Min(percentage, 1.0) * thePopulation.Count);
            thePopulation.RemoveRange(thePopulation.Count - max, max);
        }
        public int getSize() { return thePopulation.Count; }
        public void populateTree (TreeNode basenode)
        {
            
            for (int i=0; i<thePopulation.Count; ++i)
            {
                TreeNode individualnode = new TreeNode("Individual: " + i + " Fitness: " + thePopulation[i].getFitness() + " Recent avg error: " + thePopulation[i].getRecentAverageError());
                thePopulation[i].populateTree(individualnode);
                basenode.Nodes.Add(individualnode);
            }
        }
        private void crossover(Net alice, Net bob) { }
        private List<Net> thePopulation;
        public double topFitness;
        public int fittestIndex;
    };
}
