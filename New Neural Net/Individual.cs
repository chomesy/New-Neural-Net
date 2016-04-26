using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Neural_Net
{
    class Individual
    {
        public Individual(List<int> topology)
        {
            //cout << "Individual constructor called" << endl;
            Gene myGenes = new Gene(topology);
            myBrain = new Net(topology, myGenes);
            //cout << "Individual Initializer completed" << endl;
        }
        public Individual(List<int> topology, Gene inputGenes)
        {
            myGenes = inputGenes;
            myBrain = new Net(topology, myGenes);
        }
        public Gene getGenome() { return myGenes; }
        public void setGenome(Gene setGenes) { }
        public double getFitness(List<double> inputs, List<double> outputs) { return 0; }
        public void mutate() { }

        public Gene myGenes { set; get; }
        private static readonly double i_mutationrate;
        private Net myBrain;
    };
}
