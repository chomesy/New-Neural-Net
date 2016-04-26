using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Neural_Net
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PrimaryWindow());

            //TrainingGenerator trainData = new TrainingGenerator();




           
            //List<double> InputVals, targetVals, resultVals;
            //trainData.makeNewTraining();
            //InputVals = trainData.td_inputs;
            //resultVals = trainData.td_outputs;

            /* for debugging, I killed all my code :(


            Net myNet(topology);
            cout << "Topology: ";
            cout << topology[0] << topology[1] << topology[2] << topology[3] << endl;

            vector<double> inputVals, targetVals, resultVals;
            int trainingPass = 0;

            while (myNet.getRecentAverageError() > 0.05 || trainingPass < 100) {
                ++trainingPass;

                //create next training data example
                trainData.makeNewTraining();

                //debug
                /*cout << "Training Input Size: " << trainData.getTrainingInput().size() << " Training Inputs: ";
                for (unsigned i = 0; i < trainData.getTrainingInput().size(); ++i) {
                    cout << trainData.getTrainingInput()[i] << " ";
                }
                cout << endl;
                cout << "Training Output Size: " << trainData.getTrainingOutput().size() << " Training Outputs: ";
                for (unsigned i = 0; i < trainData.getTrainingOutput().size(); ++i) {
                        cout << trainData.getTrainingOutput()[i] << " ";
                    }
                */
            // Get new input data and feed it forward:
            /*
            if (trainData.getTrainingInput().size() != topology[0]) {
                cout << "Size Mismatch: Break" << endl;
                break;
            }

            myNet.feedForward(trainData.getTrainingInput());

            // Collect the net's actual output results:
            myNet.getResults(resultVals);


            // Train the net what the outputs should have been:
            assert(trainData.getTrainingOutput().size() == topology.back());

            myNet.backProp(trainData.getTrainingOutput());
            // all the output text during execution
            if (remainder(trainingPass, 100000) == 0) {
                cout << endl << "Pass " << trainingPass;
                showVectorVals(": Inputs:", trainData.getTrainingInput());
                showVectorVals("Outputs:", resultVals);
                showVectorVals("Targets:", trainData.getTrainingOutput());
                cout << "Instantaneous Error: " << myNet.getError();
                cout << " Net recent average error: "
                    << myNet.getRecentAverageError() << endl;
                cout << endl;
                //myNet.printNetValues();
            } 

        }

        cout << endl << "Pass: " << trainingPass << " Error: " <<myNet.getRecentAverageError()<< " Done" << endl;
        */
            //cin.get();


        }
    }
}
