namespace ClientForm
{
    partial class Form1
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
            this.lblWorkerState = new System.Windows.Forms.Label();
            this.lblTaskType = new System.Windows.Forms.Label();
            this.lblTaskData = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWorkerState
            // 
            this.lblWorkerState.AutoSize = true;
            this.lblWorkerState.Location = new System.Drawing.Point(27, 30);
            this.lblWorkerState.Name = "lblWorkerState";
            this.lblWorkerState.Size = new System.Drawing.Size(71, 13);
            this.lblWorkerState.TabIndex = 0;
            this.lblWorkerState.Text = "Worker state:";
            // 
            // lblTaskType
            // 
            this.lblTaskType.AutoSize = true;
            this.lblTaskType.Location = new System.Drawing.Point(27, 65);
            this.lblTaskType.Name = "lblTaskType";
            this.lblTaskType.Size = new System.Drawing.Size(57, 13);
            this.lblTaskType.TabIndex = 2;
            this.lblTaskType.Text = "Task type:";
            // 
            // lblTaskData
            // 
            this.lblTaskData.AutoSize = true;
            this.lblTaskData.Location = new System.Drawing.Point(27, 102);
            this.lblTaskData.Name = "lblTaskData";
            this.lblTaskData.Size = new System.Drawing.Size(58, 13);
            this.lblTaskData.TabIndex = 3;
            this.lblTaskData.Text = "Task data:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 148);
            this.Controls.Add(this.lblTaskData);
            this.Controls.Add(this.lblTaskType);
            this.Controls.Add(this.lblWorkerState);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWorkerState;
        private System.Windows.Forms.Label lblTaskType;
        private System.Windows.Forms.Label lblTaskData;
    }
}

