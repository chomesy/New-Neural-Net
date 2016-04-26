using System;
using System.Collections.Generic;
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
            p_popsize = popSize;
            thePopulation = new List<Individual>(popSize);
            for (int i = 0; i < p_popsize; ++i)
            {
                Gene newRandGene = new Gene(topology);
                //MessageBox.Show("In Population Constructor: Layer 1, Neuron 1, number of connections = " + newRandGene.getWeights()[0][0].Count);
                Individual newIndividual = new Individual(topology, newRandGene);
                thePopulation.Add(newIndividual);
                //MessageBox.Show("New Individual: " + i);
            }
            //cout << "Population constructor completed" << endl;
        }
        public Individual getIndividual(int index) { return thePopulation[index]; }
        public Individual getFittest() { return thePopulation[0]; }
        public void iterateGeneration() { }

        private void crossover(Individual alice, Individual bob) { }
        private List<Individual> thePopulation;
        private int p_popsize;
    };
}
