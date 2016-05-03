using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Neural_Net
{
    class TrainingGenerator
    {
        public TrainingGenerator()
        {
            td_inputs = new List<double>();
            td_outputs = new List<double>();
            this.makeNewTraining();
        }
        public void makeNewTraining()
        {
            td_inputs.Clear();
            td_outputs.Clear();
            Random rnd = new Random();
            
            // create inputs and se binary input vectors
            double n1 = rnd.Next(_inputmin, _inputmax);

            double n2 = rnd.Next(_inputmin, _inputmax);
            double t = td_algorithm(n1, n2);
            /* Noralized version
            td_inputs.Add((n1 - _inputmin) / (_inputmax - _inputmin));
            td_inputs.Add((n2 - _inputmin) / (_inputmax - _inputmin));
            td_outputs.Add((t - _inputmin) / (_inputmax - _inputmin));
            */

            //Non-noralized version
            td_inputs.Add(n1);
            td_inputs.Add(n2);
            td_outputs.Add(t);

        }
        public override string ToString()
        {
            string output = "Inputs: ";
            foreach (double element in td_inputs)
            {
                output += element.ToString() + " ";
            }

            output += "Outputs: ";

            foreach (double element in td_outputs)
            {
                output += element.ToString() + " ";
            }
            return output;
        }

        private const int _inputmin = 0;
        private const int _inputmax = 100;
	    public List<double> td_inputs { get; }
        public List<double> td_outputs { get; }
        private double td_algorithm(double n1, double n2)
        {
            return Math.Abs(n1 - n2);
        }
    }
}
