using System.Windows.Forms;

namespace New_Neural_Net
{
    partial class PrimaryWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.makePopulationButton = new System.Windows.Forms.Button();
            this.makeTrainingSetButton = new System.Windows.Forms.Button();
            this.primaryProgressBar = new System.Windows.Forms.ProgressBar();
            this.FeedForwardButton = new System.Windows.Forms.Button();
            this.ReportResultsButton = new System.Windows.Forms.Button();
            this.BackPropButton = new System.Windows.Forms.Button();
            this.ShowNetworkButton = new System.Windows.Forms.Button();
            this.iterationsInput = new System.Windows.Forms.TextBox();
            this.runIterationsButton = new System.Windows.Forms.Button();
            this.iterationsInputErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.mutateButton = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sortButton = new System.Windows.Forms.Button();
            this.cullButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iterationsInputErrorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // makePopulationButton
            // 
            this.makePopulationButton.Location = new System.Drawing.Point(3, 265);
            this.makePopulationButton.Name = "makePopulationButton";
            this.makePopulationButton.Size = new System.Drawing.Size(139, 23);
            this.makePopulationButton.TabIndex = 1;
            this.makePopulationButton.Text = "Make Population";
            this.makePopulationButton.UseVisualStyleBackColor = true;
            this.makePopulationButton.Click += new System.EventHandler(this.makePopulationButton_Click);
            // 
            // makeTrainingSetButton
            // 
            this.makeTrainingSetButton.Location = new System.Drawing.Point(3, 291);
            this.makeTrainingSetButton.Name = "makeTrainingSetButton";
            this.makeTrainingSetButton.Size = new System.Drawing.Size(139, 23);
            this.makeTrainingSetButton.TabIndex = 2;
            this.makeTrainingSetButton.Text = "makeTrainingSetButton";
            this.makeTrainingSetButton.UseVisualStyleBackColor = true;
            this.makeTrainingSetButton.Click += new System.EventHandler(this.makeTrainingSetButton_Click);
            // 
            // primaryProgressBar
            // 
            this.primaryProgressBar.Location = new System.Drawing.Point(3, 236);
            this.primaryProgressBar.Name = "primaryProgressBar";
            this.primaryProgressBar.Size = new System.Drawing.Size(680, 23);
            this.primaryProgressBar.TabIndex = 3;
            this.primaryProgressBar.Click += new System.EventHandler(this.primaryProgressBar_Click);
            // 
            // FeedForwardButton
            // 
            this.FeedForwardButton.Location = new System.Drawing.Point(3, 320);
            this.FeedForwardButton.Name = "FeedForwardButton";
            this.FeedForwardButton.Size = new System.Drawing.Size(139, 23);
            this.FeedForwardButton.TabIndex = 4;
            this.FeedForwardButton.Text = "FeedForwardButton";
            this.FeedForwardButton.UseVisualStyleBackColor = true;
            this.FeedForwardButton.Click += new System.EventHandler(this.FeedForwardButton_Click);
            // 
            // ReportResultsButton
            // 
            this.ReportResultsButton.Location = new System.Drawing.Point(555, 265);
            this.ReportResultsButton.Name = "ReportResultsButton";
            this.ReportResultsButton.Size = new System.Drawing.Size(128, 23);
            this.ReportResultsButton.TabIndex = 5;
            this.ReportResultsButton.Text = "ReportResultsButton";
            this.ReportResultsButton.UseVisualStyleBackColor = true;
            this.ReportResultsButton.Click += new System.EventHandler(this.ReportResultsButton_Click);
            // 
            // BackPropButton
            // 
            this.BackPropButton.Location = new System.Drawing.Point(555, 294);
            this.BackPropButton.Name = "BackPropButton";
            this.BackPropButton.Size = new System.Drawing.Size(128, 23);
            this.BackPropButton.TabIndex = 6;
            this.BackPropButton.Text = "BackPropButton";
            this.BackPropButton.UseVisualStyleBackColor = true;
            this.BackPropButton.Click += new System.EventHandler(this.BackPropButton_Click);
            // 
            // ShowNetworkButton
            // 
            this.ShowNetworkButton.Location = new System.Drawing.Point(555, 323);
            this.ShowNetworkButton.Name = "ShowNetworkButton";
            this.ShowNetworkButton.Size = new System.Drawing.Size(128, 23);
            this.ShowNetworkButton.TabIndex = 7;
            this.ShowNetworkButton.Text = "ShowNetworkButton";
            this.ShowNetworkButton.UseVisualStyleBackColor = true;
            this.ShowNetworkButton.Click += new System.EventHandler(this.ShowNetworkButton_Click);
            // 
            // iterationsInput
            // 
            this.iterationsInputErrorProvider.SetIconPadding(this.iterationsInput, 2);
            this.iterationsInput.Location = new System.Drawing.Point(314, 265);
            this.iterationsInput.MaxLength = 5;
            this.iterationsInput.Name = "iterationsInput";
            this.iterationsInput.Size = new System.Drawing.Size(100, 20);
            this.iterationsInput.TabIndex = 8;
            this.iterationsInput.Text = "50";
            this.iterationsInput.TextChanged += new System.EventHandler(this.iterationsInput_TextChanged);
            // 
            // runIterationsButton
            // 
            this.runIterationsButton.Location = new System.Drawing.Point(314, 291);
            this.runIterationsButton.Name = "runIterationsButton";
            this.runIterationsButton.Size = new System.Drawing.Size(100, 23);
            this.runIterationsButton.TabIndex = 9;
            this.runIterationsButton.Text = "runIterationsButton";
            this.runIterationsButton.UseVisualStyleBackColor = true;
            this.runIterationsButton.Click += new System.EventHandler(this.runIterationsButton_Click);
            // 
            // iterationsInputErrorProvider
            // 
            this.iterationsInputErrorProvider.ContainerControl = this;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.treeView1.Location = new System.Drawing.Point(692, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(527, 426);
            this.treeView1.TabIndex = 10;
            // 
            // mutateButton
            // 
            this.mutateButton.Location = new System.Drawing.Point(3, 349);
            this.mutateButton.Name = "mutateButton";
            this.mutateButton.Size = new System.Drawing.Size(138, 23);
            this.mutateButton.TabIndex = 11;
            this.mutateButton.Text = "Mutate Population";
            this.mutateButton.UseVisualStyleBackColor = true;
            this.mutateButton.Click += new System.EventHandler(this.mutateButtonClick);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.outputTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(686, 230);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            this.outputTextBox.TextChanged += new System.EventHandler(this.outputTextBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cullButton);
            this.panel1.Controls.Add(this.sortButton);
            this.panel1.Controls.Add(this.mutateButton);
            this.panel1.Controls.Add(this.outputTextBox);
            this.panel1.Controls.Add(this.primaryProgressBar);
            this.panel1.Controls.Add(this.FeedForwardButton);
            this.panel1.Controls.Add(this.runIterationsButton);
            this.panel1.Controls.Add(this.makeTrainingSetButton);
            this.panel1.Controls.Add(this.ReportResultsButton);
            this.panel1.Controls.Add(this.makePopulationButton);
            this.panel1.Controls.Add(this.iterationsInput);
            this.panel1.Controls.Add(this.BackPropButton);
            this.panel1.Controls.Add(this.ShowNetworkButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(686, 426);
            this.panel1.TabIndex = 12;
            // 
            // sortButton
            // 
            this.sortButton.Location = new System.Drawing.Point(555, 353);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(128, 23);
            this.sortButton.TabIndex = 12;
            this.sortButton.Text = "Sort Population";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // cullButton
            // 
            this.cullButton.Location = new System.Drawing.Point(555, 383);
            this.cullButton.Name = "cullButton";
            this.cullButton.Size = new System.Drawing.Size(128, 23);
            this.cullButton.TabIndex = 13;
            this.cullButton.Text = "Cull 50%";
            this.cullButton.UseVisualStyleBackColor = true;
            this.cullButton.Click += new System.EventHandler(this.cullButton_Click);
            // 
            // PrimaryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 426);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Name = "PrimaryWindow";
            this.Text = "PrimaryWindow";
            ((System.ComponentModel.ISupportInitialize)(this.iterationsInputErrorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button makePopulationButton;
        private System.Windows.Forms.Button makeTrainingSetButton;
        private System.Windows.Forms.ProgressBar primaryProgressBar;
        private System.Windows.Forms.Button FeedForwardButton;
        private System.Windows.Forms.Button ReportResultsButton;
        private System.Windows.Forms.Button BackPropButton;
        private System.Windows.Forms.Button ShowNetworkButton;
        private System.Windows.Forms.TextBox iterationsInput;
        private System.Windows.Forms.Button runIterationsButton;
        private System.Windows.Forms.ErrorProvider iterationsInputErrorProvider;
        private TreeView treeView1;
        private Button mutateButton;
        private Panel panel1;
        private RichTextBox outputTextBox;
        private Button sortButton;
        private Button cullButton;
    }
}

