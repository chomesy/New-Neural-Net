﻿using System;
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
        public void addNets (List<Net> net) { thePopulation.AddRange(net); }
        public void addNets (Net net) { thePopulation.Add(net); }

        public void asex()
        {

        }

        public Net getFittest() { return thePopulation[fittestIndex]; }
        public void sortPopulation()
        {
            thePopulation.Sort();
            computeFittest();
        }
        public int CompareNets(Net x, Net y)
        {
            double tmp = x.getRecentAverageError() - y.getRecentAverageError();
            if (tmp > 0) return 1;
            if (tmp < 0) return -1;
            else return 0;
        }
        public void computeFittest()
        {
            topFitness = 0;
            for (int i = 0; i< thePopulation.Count; ++i)
            {
                if (topFitness< thePopulation[i].getFitness())
                    {
                    topFitness = thePopulation[i].getFitness();
                    fittestIndex = i;
                }
            }
        }
        public void iterateGeneration(List <double> inputs, List <double> outputs)
        {
            topFitness = 0;
            Parallel.For(0, thePopulation.Count, (index, loopstate) =>
            {
                try
                {
                    thePopulation[index].feedForward(inputs);
                    thePopulation[index].computeError(outputs);
                    thePopulation[index].backProp(outputs);
                    
                    if (thePopulation[index].getFitness() > topFitness)
                    {
                        topFitness = thePopulation[index].getFitness();
                        fittestIndex = index; 
                    }
                    else
                    {
                        //thePopulation[i].mutate();
                    }
                    thePopulation[index].feedForward(inputs);
                    thePopulation[index].computeError(outputs);
                }
                catch { MessageBox.Show("Iteration Failed");  loopstate.Stop(); }
            });
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
            thePopulation.Sort();
            int max = (int)Math.Floor(Math.Min(percentage, 1.0) * thePopulation.Count);
            thePopulation.RemoveRange(thePopulation.Count - max, max);
            computeFittest();
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
