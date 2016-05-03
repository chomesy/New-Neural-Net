using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Neural_Net
{
   /* class Individual
    {
        public Individual(List<int> topology)
        {
            //cout << "Individual constructor called" << endl;
            Gene myGenes = new Gene(topology, new Random());
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
        public double getFitness()
        {
            return (1/Math.Abs(myBrain.getError()));
        }
        public void mutate() { }
        public void feedForward(List<double> inputs)
        {
            myBrain.feedForward(inputs);
        }
        public void backProp(List<double> targetVals)
        {
            myBrain.backProp(targetVals);
        }
        public void feedForward(List<double> inputs, List<double> outputs)
        {
            myBrain.feedForward(inputs);
            myBrain.computeError(outputs);
        }
        public string getDeets()
        {
            return myBrain.printNetValues();
        }
        public void populateTree(TreeNode basenode)
        {
            myBrain.populateTree(basenode);
        }
        public List<double> getOutputs()
        {
            List<double> resultvals = new List<double>();
            myBrain.getResults(resultvals);
            return resultvals;
        }
        public Gene myGenes { set; get; }
        private static readonly double i_mutationrate;
        private Net myBrain;
    };
    */
}
