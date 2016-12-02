using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;

using NeuronDotNet.Core;
using NeuronDotNet.Core.Backpropagation;
using NeuronDotNet.Core.Initializers;
using ZedGraph;

using BackPropNnTrainer;

namespace BackPropNnTrainer
{
    public partial class MainForm : Form
    {
        private MyPointList graphErrorPoints = new MyPointList();
        private MyPointList graphErrorPointsCV = null;
        private NnProject currentProject;
        private BackgroundWorker bw = new BackgroundWorker();
        private double previousUpdateCount = 0;
        private const double updateFrequency = 100000.0;

        public MainForm()
        {
            InitializeComponent();
        }

        // http://msdn.microsoft.com/en-us/library/cc221403(VS.95).aspx
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int epochNumber = (int)e.UserState;
            double totalFieldsTrained = (double)epochNumber * (double)currentProject.TrainingSet.TrainingSampleCount * (double)currentProject.TrainingSet.InputVectorLength;
            if ((totalFieldsTrained - previousUpdateCount) > updateFrequency)
            {
                previousUpdateCount = totalFieldsTrained;
                progressBar.Value = e.ProgressPercentage;
                RefreshProgress();
            }
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
            }
            else
            {
                currentProject.Network.EndEpochEvent +=
                delegate (object network, TrainingEpochEventArgs args)
                {
                    graphErrorPoints.Add(new PointPair(args.TrainingIteration, currentProject.Network.MeanSquaredError));
                    worker.ReportProgress((int)(args.TrainingIteration * 100d / currentProject.LearningParameters.MaxTrainingCycles), args.TrainingIteration);

                    if (graphErrorPointsCV != null)
                    {
                        graphErrorPointsCV.Add(new PointPair(args.TrainingIteration, GetCrossValidationError()));
                    }

                    if (worker.CancellationPending)
                    {
                        currentProject.Network.StopLearning();
                        e.Cancel = true;
                    }
                };

                currentProject.Network.Learn(currentProject.TrainingSet, currentProject.LearningParameters.MaxTrainingCycles);
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
            }

            RefreshProgress();
            btnTrain.Text = "Train";
            EnableControls(true, true);
        }
        private void RefreshProgress()
        {
            lblTrainErrorVal.Text = currentProject.Network.MeanSquaredError.ToString("0.000000");
            if (graphErrorPointsCV != null)
            {
                lblSumSqErrorCV.Text = graphErrorPointsCV[graphErrorPointsCV.Count - 1].Y.ToString("0.000000");
                double percErr = Math.Abs(100 * (currentProject.Network.MeanSquaredError - graphErrorPointsCV[graphErrorPointsCV.Count - 1].Y) / currentProject.Network.MeanSquaredError);
                if (!double.IsInfinity(percErr))
                    lblCvPercError.Text = percErr.ToString("0.00") + " %";
                else
                    lblCvPercError.Text = "";
            }

            if (!errorGraph.GraphPane.IsZoomed)
            {
                errorGraph.GraphPane.YAxis.Scale.Max = currentProject.Network.MeanSquaredError * 2;
                errorGraph.GraphPane.AxisChange();
            }
            LineItem errorCurve = new LineItem("Error Dynamics", graphErrorPoints, Color.Tomato, SymbolType.None, 1.5f);
            LineItem errorCurveCV = new LineItem("Error Dynamics (CV)", graphErrorPointsCV, Color.DarkBlue, SymbolType.None, 1.5f);
            errorGraph.GraphPane.CurveList.Add(errorCurve);
            errorGraph.GraphPane.CurveList.Add(errorCurveCV);
            errorGraph.Invalidate();
        }

        private void Train(object sender, EventArgs e)
        {
            if (currentProject == null)
            {
                MessageBox.Show("No network loaded.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                if (btnTrain.Text == "Cancel")
                {
                    if (bw.WorkerSupportsCancellation == true)
                    {
                        DialogResult dr = MessageBox.Show("Terminate network training?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            bw.CancelAsync();
                            btnTrain.Enabled = false;
                        }
                    }
                    return;
                }

                SaveScreenParametersToCurrentNetwork();
                graphErrorPoints.Clear();
                if (currentProject.CrossValidationSet != null)
                    graphErrorPointsCV = new MyPointList();
                else
                    graphErrorPointsCV = null;
                previousUpdateCount = 0;
                InitGraph();
                btnTrain.Text = "Cancel";
                lblTrainErrorVal.Text = "";
                lblSumSqErrorCV.Text = "";
                lblCvPercError.Text = "";
                EnableControls(false, false);
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                EnableControls(true, true);
                MessageBox.Show("Error in program - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableControls(bool enabled, bool all)
        {
            txtCycles.Enabled = enabled;
            txtInitialLearningRate.Enabled = enabled;
            txtFinalLearningRate.Enabled = enabled;
            comboLRFunction.Enabled = enabled;
            textMomentum.Enabled = enabled;
            buttonInitializeNet.Enabled = enabled;
            mainMenu.Enabled = enabled;
            buttonExit.Enabled = enabled;
            textParam1.Enabled = enabled;
            textParam2.Enabled = enabled;
            comboInitializeFunction.Enabled = enabled;
            progressBar.Value = 0;

            if (all) btnTrain.Enabled = enabled;
        }

        private void LoadForm(object sender, EventArgs e)
        {
            // Fill learning function combo
            comboLRFunction.Items.Add("(none)");
            foreach (LearningRateFunction lrf in Enum.GetValues(typeof(LearningRateFunction)))
            {
                comboLRFunction.Items.Add(lrf);
            }

            foreach (InitializerFunction inf in Enum.GetValues(typeof(InitializerFunction)))
            {
                comboInitializeFunction.Items.Add(inf);
            }
            comboInitializeFunction.SelectedIndex = 2;

            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            InitGraph();
        }

        private void InitGraph()
        {
            GraphPane pane = errorGraph.GraphPane;
            pane.Chart.Fill = new Fill(Color.Snow, Color.Lavender, -45F);
            pane.Title.FontSpec.Size = 12;
            pane.Title.Text = "Back Propagation Training - Error Graph";
            pane.XAxis.Title.FontSpec.Size = 10;
            pane.XAxis.Title.Text = "Training Iteration";
            pane.YAxis.Title.FontSpec.Size = 10;
            pane.YAxis.Title.Text = "Sum Squared Error";
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.Color = Color.LightGray;
            pane.XAxis.MajorGrid.Color = Color.LightGray;
            if (currentProject != null)
                pane.XAxis.Scale.Max = currentProject.LearningParameters.MaxTrainingCycles;
            else
                pane.XAxis.Scale.Max = 1000;
            pane.XAxis.Scale.Min = 0;
            pane.YAxis.Scale.Min = 0;
            errorGraph.GraphPane.YAxis.Scale.MajorStepAuto = true;
            errorGraph.GraphPane.YAxis.Scale.MinorStepAuto = true;
            pane.CurveList.Clear();
            pane.Legend.IsVisible = false;
            pane.AxisChange();
            errorGraph.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                formCreateNew createNewForm = new formCreateNew();
                createNewForm.ShowDialog(this);
                if (createNewForm.NewProject != null)
                {
                    currentProject = createNewForm.NewProject;
                    this.Text = currentProject.SaveFolder + @"\" + currentProject.ProjectName;
                    RefreshScreenForCurrentNetWork();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in program - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.ShowDialog();
                if (ofd.FileName.Length > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    EnableControls(false, true);
                    currentProject = NnProject.Load(ofd.FileName);
                    this.Text = currentProject.SaveFolder + @"\" + currentProject.ProjectName;
                    RefreshScreenForCurrentNetWork();
                    this.Cursor = Cursors.Default;
                    EnableControls(true, true);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error loading network - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControls(true, true);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentProject == null)
                {
                    MessageBox.Show("No network loaded.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                EnableControls(false, true);
                SaveScreenParametersToCurrentNetwork();
                NnProject.Save(currentProject, "");
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error saving network - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControls(true, true);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentProject == null)
                {
                    MessageBox.Show("No network loaded.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = false;
                sfd.CheckFileExists = false;
                sfd.CheckPathExists = false;
                sfd.ValidateNames = false;
                //sfd.DefaultExt = "Network files (*.ndn)|*.ndn";
                sfd.CreatePrompt = false;
                sfd.OverwritePrompt = true;
                sfd.SupportMultiDottedExtensions = false;
                sfd.ShowDialog(this);
                if (sfd.FileName.Length > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    EnableControls(false, true);
                    currentProject.ProjectName = System.IO.Path.GetFileName(sfd.FileName);
                    if (currentProject.ProjectName.Contains("."))
                        currentProject.ProjectName = currentProject.ProjectName.Remove(currentProject.ProjectName.IndexOf('.'));
                    NnProject.Save(currentProject, System.IO.Path.GetDirectoryName(sfd.FileName));
                    this.Text = currentProject.SaveFolder + @"\" + currentProject.ProjectName;
                    RefreshScreenForCurrentNetWork();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error saving network - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControls(true, true);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void saveTrainingSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentProject == null)
            {
                MessageBox.Show("No network loaded.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckFileExists = false;
            sfd.ValidateNames = false;
            sfd.DefaultExt = "CSV files (*.csv)|*.csv";
            sfd.CreatePrompt = false;
            sfd.OverwritePrompt = true;
            sfd.SupportMultiDottedExtensions = true;
            sfd.FileName = currentProject.ProjectName + ".csv";
            sfd.ShowDialog(this);
            if (sfd.FileName.Length > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                DataFile.SaveTrainingSet(currentProject.TrainingSet, sfd.FileName, currentProject.Network);
                this.Cursor = Cursors.Default;
            }
        }
        private void saveCVSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentProject == null)
            {
                MessageBox.Show("No network loaded.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (currentProject.CrossValidationSet == null)
            {
                MessageBox.Show("No Cross Validation set loaded.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckFileExists = false;
            sfd.ValidateNames = false;
            sfd.DefaultExt = "CSV files (*.csv)|*.csv";
            sfd.CreatePrompt = false;
            sfd.OverwritePrompt = true;
            sfd.SupportMultiDottedExtensions = true;
            sfd.FileName = currentProject.ProjectName + ".csv";
            sfd.ShowDialog(this);
            if (sfd.FileName.Length > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                DataFile.SaveTrainingSet(currentProject.CrossValidationSet, sfd.FileName, currentProject.Network);
                this.Cursor = Cursors.Default;
            }
        }

        private void buttonInitializeNet_Click(object sender, EventArgs e)
        {
            if (currentProject == null)
            {
                MessageBox.Show("No network loaded.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult dr = MessageBox.Show("All weights will be reset - are you sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr == DialogResult.OK)
            {
                try
                {
                    IInitializer initFunct;
                    double tmp;
                    double? param1 = null;
                    double? param2 = null;
                    if (double.TryParse(textParam1.Text, out tmp)) param1 = tmp;
                    if (double.TryParse(textParam2.Text, out tmp)) param2 = tmp;
                    switch ((InitializerFunction)comboInitializeFunction.SelectedItem)
                    {
                        case InitializerFunction.Constant: initFunct = new ConstantFunction(param1.Value); break;
                        case InitializerFunction.NguyenWidrow: initFunct = new NguyenWidrowFunction(param1.Value); break;
                        case InitializerFunction.NormRand: initFunct = new NormalizedRandomFunction(); break;
                        case InitializerFunction.Random: initFunct = new RandomFunction(param1.Value, param2.Value); break;
                        case InitializerFunction.Zero: initFunct = new ZeroFunction(); break;
                        default: throw new Exception("InitializerFunction undefined.");
                    }
                    // NEED TO SET BACK TO PREVIOUS because Deserialization fails in new state. Don't know why.
                    IInitializer oldInitializer;
                    foreach (ILayer layer in currentProject.Network.Layers)
                    {
                        oldInitializer = layer.Initializer;
                        layer.Initializer = initFunct;
                        layer.Initialize();
                        layer.Initializer = oldInitializer;
                        foreach (IConnector conn in layer.TargetConnectors)
                        {
                            oldInitializer = conn.Initializer;
                            conn.Initializer = initFunct;
                            conn.Initialize();
                            conn.Initializer = oldInitializer;
                        }
                    }
                    //currentProject.Network.Initialize(); Already called individually
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in program - " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (currentProject != null)
            {
                DialogResult dr = MessageBox.Show("Save current network?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                    return;
                if (dr == DialogResult.Yes)
                    NnProject.Save(currentProject, "");
            }
            this.Close();
        }
        private void comboInitializeFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            textParam1.Visible = false; textParam2.Visible = false;
            labelParam1.Visible = false; labelParam2.Visible = false;
            switch ((InitializerFunction)comboInitializeFunction.SelectedItem)
            {
                case InitializerFunction.Constant:
                    textParam1.Visible = true;
                    labelParam1.Visible = true;
                    labelParam1.Text = "Const.";
                    break;
                case InitializerFunction.NguyenWidrow:
                    textParam1.Visible = true;
                    labelParam1.Visible = true;
                    labelParam1.Text = "Range";
                    break;
                case InitializerFunction.NormRand: break;
                case InitializerFunction.Random:
                    textParam1.Visible = true;
                    textParam2.Visible = true;
                    labelParam1.Visible = true;
                    labelParam1.Text = "Lower";
                    labelParam2.Visible = true;
                    labelParam2.Text = "Upper";
                    break;
                case InitializerFunction.Zero: break;
                default: throw new Exception("InitializerFunction undefined.");
            }
        }

        private void RefreshScreenForCurrentNetWork()
        {
            txtCycles.Text = currentProject.LearningParameters.MaxTrainingCycles.ToString();
            txtInitialLearningRate.Text = currentProject.LearningParameters.InitialLearningRate.ToString();
            if (currentProject.LearningParameters.FinalLearningRate.HasValue)
                txtFinalLearningRate.Text = currentProject.LearningParameters.FinalLearningRate.ToString();
            else
                txtFinalLearningRate.Text = "";

            if (currentProject.LearningParameters.LearningRateFunction.HasValue)
            {
                comboLRFunction.SelectedIndex = (int)currentProject.LearningParameters.LearningRateFunction + 1;
            }

            if (currentProject.LearningParameters.Momentum.HasValue)
                textMomentum.Text = currentProject.LearningParameters.Momentum.ToString();
            else
                textMomentum.Text = "";

            string networkDesc = "";
            BackpropagationNetwork bn = currentProject.Network;
            int layerNo = 1;
            foreach (ILayer layer in bn.Layers)
            {
                networkDesc += "Layer " + layerNo.ToString() + ": {" + layer.ToString().Replace("NeuronDotNet.Core.Backpropagation.", "") + "," + layer.NeuronCount.ToString() + "}" + System.Environment.NewLine;
                layerNo++;
            }
            networkDesc += System.Environment.NewLine + "TrainingSampleCount: " + currentProject.TrainingSet.TrainingSampleCount.ToString();
            networkDescription.Text = networkDesc;

            lblTrainErrorVal.Text = bn.MeanSquaredError.ToString("0.000000");
            if (currentProject.CrossValidationSet != null)
            {
                double cvError = GetCrossValidationError();
                lblSumSqErrorCV.Text = cvError.ToString("0.000000");
                double percErr = Math.Abs(100 * (currentProject.Network.MeanSquaredError - cvError) / currentProject.Network.MeanSquaredError);
                if (!double.IsInfinity(percErr))
                    lblCvPercError.Text = percErr.ToString("0.00") + " %";
                else
                    lblCvPercError.Text = "";
                lblSumSqErrorCV.Enabled = true;
                lblSumSqErrorCV.BackColor = lblTrainErrorVal.BackColor;
            }
            else
            {
                lblSumSqErrorCV.Text = ""; lblCvPercError.Text = "";
                lblSumSqErrorCV.Enabled = false;
                lblSumSqErrorCV.BackColor = lblCVErrLabel.BackColor;
            }
        }
        private void SaveScreenParametersToCurrentNetwork()
        {
            // Training cycles
            currentProject.LearningParameters.MaxTrainingCycles = int.Parse(txtCycles.Text);

            // Get learning rates
            currentProject.LearningParameters.InitialLearningRate = double.Parse(txtInitialLearningRate.Text);
            if (txtFinalLearningRate.Text.Length > 0)
                currentProject.LearningParameters.FinalLearningRate = double.Parse(txtFinalLearningRate.Text);
            else
                currentProject.LearningParameters.FinalLearningRate = null;

            // Is a LRF selected in the combo?
            if (comboLRFunction.SelectedIndex > 0)
                currentProject.LearningParameters.LearningRateFunction = (LearningRateFunction)comboLRFunction.SelectedItem;
            else
                currentProject.LearningParameters.LearningRateFunction = null;

            // Can we use the LRF? Require both learning rates
            if (comboLRFunction.SelectedIndex > 0 && currentProject.LearningParameters.FinalLearningRate.HasValue)
            {
                // Set learning rate function
                currentProject.Network.SetLearningRate(
                    LearningRateFactory.GetLearningRateFunction((LearningRateFunction)comboLRFunction.SelectedItem,
                        currentProject.LearningParameters.InitialLearningRate,
                        currentProject.LearningParameters.FinalLearningRate.Value)
                );
            }
            else
            {
                // Set learning rates
                if (currentProject.LearningParameters.FinalLearningRate.HasValue)
                    currentProject.Network.SetLearningRate(currentProject.LearningParameters.InitialLearningRate, currentProject.LearningParameters.FinalLearningRate.Value);
                else
                    currentProject.Network.SetLearningRate(currentProject.LearningParameters.InitialLearningRate);
            }

            // Set momentum in connectors (0 for not defined)
            double momentum = 0;
            if (textMomentum.Text.Length > 0) momentum = double.Parse(textMomentum.Text);
            foreach (ILayer layer in currentProject.Network.Layers)
            {
                foreach (BackpropagationConnector conn in layer.SourceConnectors)
                    conn.Momentum = momentum;
                foreach (BackpropagationConnector conn in layer.TargetConnectors)
                    conn.Momentum = momentum;
            }
        }
        private double GetCrossValidationError()
        {
            double[] output;
            double error;
            double totalSquaredError = 0.0;
            foreach (TrainingSample ts in currentProject.CrossValidationSet.TrainingSamples)
            {
                output = currentProject.Network.Run(ts.InputVector);
                for (int index = 0; index < ts.OutputVector.Length; index++)
                {
                    error = ts.OutputVector[index] - output[index];
                    totalSquaredError += error * error;
                }
            }
            // Return mean of the total squared error
            return totalSquaredError / (double)currentProject.CrossValidationSet.TrainingSampleCount;
        }
    }

    class MyPointList : List<PointPair>, IPointList
    {

        #region ICloneable Members

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
