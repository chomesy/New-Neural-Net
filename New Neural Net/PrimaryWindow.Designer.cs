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
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.makePopulationButton = new System.Windows.Forms.Button();
            this.makeTrainingSetButton = new System.Windows.Forms.Button();
            this.primaryProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(12, 12);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(662, 230);
            this.outputTextBox.TabIndex = 0;
            this.outputTextBox.Text = "";
            this.outputTextBox.TextChanged += new System.EventHandler(this.outputTextBox_TextChanged);
            // 
            // makePopulationButton
            // 
            this.makePopulationButton.Location = new System.Drawing.Point(12, 278);
            this.makePopulationButton.Name = "makePopulationButton";
            this.makePopulationButton.Size = new System.Drawing.Size(139, 23);
            this.makePopulationButton.TabIndex = 1;
            this.makePopulationButton.Text = "Make Population";
            this.makePopulationButton.UseVisualStyleBackColor = true;
            this.makePopulationButton.Click += new System.EventHandler(this.makePopulationButton_Click);
            // 
            // makeTrainingSetButton
            // 
            this.makeTrainingSetButton.Location = new System.Drawing.Point(12, 307);
            this.makeTrainingSetButton.Name = "makeTrainingSetButton";
            this.makeTrainingSetButton.Size = new System.Drawing.Size(139, 23);
            this.makeTrainingSetButton.TabIndex = 2;
            this.makeTrainingSetButton.Text = "makeTrainingSetButton";
            this.makeTrainingSetButton.UseVisualStyleBackColor = true;
            this.makeTrainingSetButton.Click += new System.EventHandler(this.makeTrainingSetButton_Click);
            // 
            // primaryProgressBar
            // 
            this.primaryProgressBar.Location = new System.Drawing.Point(12, 249);
            this.primaryProgressBar.Name = "primaryProgressBar";
            this.primaryProgressBar.Size = new System.Drawing.Size(662, 23);
            this.primaryProgressBar.TabIndex = 3;
            this.primaryProgressBar.Click += new System.EventHandler(this.primaryProgressBar_Click);
            // 
            // PrimaryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 426);
            this.Controls.Add(this.primaryProgressBar);
            this.Controls.Add(this.makeTrainingSetButton);
            this.Controls.Add(this.makePopulationButton);
            this.Controls.Add(this.outputTextBox);
            this.Name = "PrimaryWindow";
            this.Text = "PrimaryWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.Button makePopulationButton;
        private System.Windows.Forms.Button makeTrainingSetButton;
        private System.Windows.Forms.ProgressBar primaryProgressBar;
    }
}

