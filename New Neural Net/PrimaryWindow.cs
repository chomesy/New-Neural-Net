using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Neural_Net
{
    public partial class PrimaryWindow : Form
    {
        private BackgroundWorker bw;
        private TrainingGenerator trainData = new TrainingGenerator();
        private Population myPop;
        private int numiterations;
        private List<int> topology = new List<int>(4){ 2, 8,4, 1 };

        public PrimaryWindow()
        {
            InitializeComponent();

            bw = new System.ComponentModel.BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            //List<int> topology = new List<int>();
            //this.topology.Add(trainData.td_inputs.Count);
            //this.topology.Add(4);
            //topology.Add(3);
            //this.topology.Add(trainData.td_outputs.Count);
        }

        private void outputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void makePopulationButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            //hard code network topology
           
            myPop = new Population(100, this.topology);

        }

        private void makeTrainingSetButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            outputTextBox.AppendText("Making Training set...");
            trainData.makeNewTraining();
            outputTextBox.AppendText("\nTraining Set is: " + trainData.ToString());
        }

        private void primaryProgressBar_Click(object sender, EventArgs e)
        {

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.outputTextBox.AppendText(e.ProgressPercentage.ToString() + "%\n");
            this.primaryProgressBar.Value = e.ProgressPercentage;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.outputTextBox.AppendText("Completed\n");
        }

        private void FeedForwardButton_Click(object sender, EventArgs e)
        {
            for (int i= 0; i<myPop.getSize(); ++i)
            {
                myPop.getNet(i).feedForward(trainData.td_inputs);
                myPop.getNet(i).computeError(trainData.td_outputs);
                this.outputTextBox.Clear();
                outputTextBox.AppendText("Fed\n");
                this.primaryProgressBar.Value = (100 * (i + 1) / myPop.getSize());
            }
        }

        private void ReportResultsButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            outputTextBox.AppendText(myPop.ToString());
        }

        private void BackPropButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < myPop.getSize(); ++i)
            {
                myPop.getNet(i).backProp(trainData.td_outputs);
                this.primaryProgressBar.Value = (100 * (i + 1) / myPop.getSize());
            }
        }

        private void ShowNetworkButton_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Clear();
            outputTextBox.AppendText("Fittest Individual: " + myPop.fittestIndex + "\n");
            outputTextBox.AppendText("Fitness: " + myPop.topFitness + "\n");
            outputTextBox.AppendText("Training Data: " + trainData.ToString() + "\n");
            outputTextBox.AppendText("Actual Output: " + myPop.getFittest().resultVals[0] + "\n");
            TreeNode populationnode = new TreeNode("Population");
            myPop.populateTree(populationnode);
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(populationnode);
            
        }

        private void iterationsInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void runIterationsButton_Click(object sender, EventArgs e)
        {
            this.outputTextBox.Clear();
            if (int.TryParse(iterationsInput.Text, out numiterations))
            {
                iterationsInputErrorProvider.SetError(this.iterationsInput, "");
                numiterations = int.Parse(iterationsInput.Text);
                this.outputTextBox.AppendText("Starting iterations...\n");
                try
                {
                    for (int i = 0; i < numiterations; ++i)
                    {
                        trainData.makeNewTraining();
                        myPop.iterateGeneration(trainData.td_inputs, trainData.td_outputs);
                        primaryProgressBar.Value = (100 * (i + 1) / numiterations);
                        //this.outputTextBox.AppendText("Generation " + i + "\n");
                        //this.outputTextBox.AppendText("Fittest individual is: " + myPop.fittestIndex + " with fitness of: " + myPop.topFitness + "\n");
                    }
                    this.outputTextBox.AppendText("Iterations Complete.");
                }
                catch(NotFiniteNumberException ex)
                {
                    MessageBox.Show("Iterations closed due to Infinity Exception");
                }
            }
            else
            { 
                iterationsInputErrorProvider.SetError(this.iterationsInput, "Not an Integer");
            }

        }

        private void mutateButtonClick(object sender, EventArgs e)
        {
            myPop.mutateGeneration();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            myPop.sortPopulation();
        }

        private void cullButton_Click(object sender, EventArgs e)
        {
            myPop.cullGeneration(.5);
            int addsize = 100 - myPop.getSize();
            //myPop.addNets(25, topology);
            
            for (int i=0; i<addsize; i++)
            {
                //MessageBox.Show("button click loop");
                myPop.addNets(myPop.getNet(i).clone());
            }
        }

        private void procreate_Click(object sender, EventArgs e)
        {
            int currentsize = myPop.getSize();
            for (int i = 0; i < currentsize; ++i)
            {
                myPop.addNets(myPop.getNet(i).procreate());
            }
        }
    }

}
