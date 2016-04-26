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


        public PrimaryWindow()
        {
            InitializeComponent();

            bw = new System.ComponentModel.BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

        }

        private void outputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void makePopulationButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            outputTextBox.AppendText("Making the population!");
            bw.RunWorkerAsync();
            outputTextBox.AppendText("\nDonezo");

        }

        private void makeTrainingSetButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();
            outputTextBox.AppendText("Making Training set...");

        }

        private void primaryProgressBar_Click(object sender, EventArgs e)
        {

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            TrainingGenerator trainData = new TrainingGenerator();
            worker.ReportProgress(25);
            //hard code network topology
            List<int> topology = new List<int>();
            topology.Add(trainData.td_inputs.Count);
            topology.Add(16);
            topology.Add(8);
            topology.Add(trainData.td_outputs.Count);
            worker.ReportProgress(40);
            Population myPop = new Population(50, topology);
            worker.ReportProgress(100);
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.outputTextBox.AppendText("\n" + e.ProgressPercentage.ToString() + "%");
            this.primaryProgressBar.Value = e.ProgressPercentage;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.outputTextBox.AppendText("\nCompleted");
        }

    }

}
