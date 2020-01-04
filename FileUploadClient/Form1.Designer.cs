namespace FileUploadClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblUrl = new System.Windows.Forms.Label();
            this.tbxUploadUrl = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.tbxDownloadUrl = new System.Windows.Forms.TextBox();
            this.lblSetUrl = new System.Windows.Forms.Label();
            this.btnOpenUploadPanel = new System.Windows.Forms.Button();
            this.btnOpenDownloadPanel = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblChooseAnOption = new System.Windows.Forms.Label();
            this.lblWhatWouldYouLikeToDo = new System.Windows.Forms.Label();
            this.panelUpload = new System.Windows.Forms.Panel();
            this.btnBackUpload = new System.Windows.Forms.Button();
            this.lblSelected = new System.Windows.Forms.Label();
            this.panelDownload = new System.Windows.Forms.Panel();
            this.lblDownloadStatus = new System.Windows.Forms.Label();
            this.btnBackDownload = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelUpload.SuspendLayout();
            this.panelDownload.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(21, 198);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "lblStatus";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.progressBar1.Location = new System.Drawing.Point(19, 231);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(253, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // btnUpload
            // 
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.Location = new System.Drawing.Point(19, 142);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(253, 36);
            this.btnUpload.TabIndex = 14;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(19, 61);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(253, 36);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Select File";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(74, 115);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(35, 13);
            this.lblFileName.TabIndex = 18;
            this.lblFileName.Text = "label2";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(16, 280);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(29, 13);
            this.lblUrl.TabIndex = 19;
            this.lblUrl.Text = "URI:";
            // 
            // tbxUploadUrl
            // 
            this.tbxUploadUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxUploadUrl.Location = new System.Drawing.Point(68, 278);
            this.tbxUploadUrl.Name = "tbxUploadUrl";
            this.tbxUploadUrl.Size = new System.Drawing.Size(204, 20);
            this.tbxUploadUrl.TabIndex = 20;
            // 
            // btnDownload
            // 
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Location = new System.Drawing.Point(17, 154);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(253, 36);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // tbxDownloadUrl
            // 
            this.tbxDownloadUrl.Location = new System.Drawing.Point(17, 112);
            this.tbxDownloadUrl.Name = "tbxDownloadUrl";
            this.tbxDownloadUrl.Size = new System.Drawing.Size(253, 20);
            this.tbxDownloadUrl.TabIndex = 1;
            // 
            // lblSetUrl
            // 
            this.lblSetUrl.AutoSize = true;
            this.lblSetUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetUrl.Location = new System.Drawing.Point(13, 77);
            this.lblSetUrl.Name = "lblSetUrl";
            this.lblSetUrl.Size = new System.Drawing.Size(83, 20);
            this.lblSetUrl.TabIndex = 0;
            this.lblSetUrl.Text = "Enter url:";
            // 
            // btnOpenUploadPanel
            // 
            this.btnOpenUploadPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenUploadPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenUploadPanel.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenUploadPanel.Image")));
            this.btnOpenUploadPanel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenUploadPanel.Location = new System.Drawing.Point(16, 97);
            this.btnOpenUploadPanel.Name = "btnOpenUploadPanel";
            this.btnOpenUploadPanel.Size = new System.Drawing.Size(258, 90);
            this.btnOpenUploadPanel.TabIndex = 26;
            this.btnOpenUploadPanel.Text = "Upload File";
            this.btnOpenUploadPanel.UseVisualStyleBackColor = true;
            this.btnOpenUploadPanel.Click += new System.EventHandler(this.btnOpenUploadPanel_Click);
            // 
            // btnOpenDownloadPanel
            // 
            this.btnOpenDownloadPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenDownloadPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenDownloadPanel.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDownloadPanel.Image")));
            this.btnOpenDownloadPanel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenDownloadPanel.Location = new System.Drawing.Point(16, 215);
            this.btnOpenDownloadPanel.Name = "btnOpenDownloadPanel";
            this.btnOpenDownloadPanel.Size = new System.Drawing.Size(258, 90);
            this.btnOpenDownloadPanel.TabIndex = 27;
            this.btnOpenDownloadPanel.Text = "   Download File";
            this.btnOpenDownloadPanel.UseVisualStyleBackColor = true;
            this.btnOpenDownloadPanel.Click += new System.EventHandler(this.btnOpenDownloadPanel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblChooseAnOption);
            this.panelMain.Controls.Add(this.lblWhatWouldYouLikeToDo);
            this.panelMain.Controls.Add(this.btnOpenUploadPanel);
            this.panelMain.Controls.Add(this.btnOpenDownloadPanel);
            this.panelMain.Location = new System.Drawing.Point(3, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(290, 322);
            this.panelMain.TabIndex = 28;
            // 
            // lblChooseAnOption
            // 
            this.lblChooseAnOption.AutoSize = true;
            this.lblChooseAnOption.Location = new System.Drawing.Point(99, 44);
            this.lblChooseAnOption.Name = "lblChooseAnOption";
            this.lblChooseAnOption.Size = new System.Drawing.Size(90, 13);
            this.lblChooseAnOption.TabIndex = 29;
            this.lblChooseAnOption.Text = "Choose an option";
            // 
            // lblWhatWouldYouLikeToDo
            // 
            this.lblWhatWouldYouLikeToDo.AutoSize = true;
            this.lblWhatWouldYouLikeToDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhatWouldYouLikeToDo.Location = new System.Drawing.Point(32, 12);
            this.lblWhatWouldYouLikeToDo.Name = "lblWhatWouldYouLikeToDo";
            this.lblWhatWouldYouLikeToDo.Size = new System.Drawing.Size(223, 20);
            this.lblWhatWouldYouLikeToDo.TabIndex = 28;
            this.lblWhatWouldYouLikeToDo.Text = "What would you like to do?";
            // 
            // panelUpload
            // 
            this.panelUpload.Controls.Add(this.btnBackUpload);
            this.panelUpload.Controls.Add(this.lblStatus);
            this.panelUpload.Controls.Add(this.tbxUploadUrl);
            this.panelUpload.Controls.Add(this.lblUrl);
            this.panelUpload.Controls.Add(this.lblSelected);
            this.panelUpload.Controls.Add(this.btnBrowse);
            this.panelUpload.Controls.Add(this.lblFileName);
            this.panelUpload.Controls.Add(this.btnUpload);
            this.panelUpload.Controls.Add(this.progressBar1);
            this.panelUpload.Location = new System.Drawing.Point(3, 12);
            this.panelUpload.Name = "panelUpload";
            this.panelUpload.Size = new System.Drawing.Size(290, 322);
            this.panelUpload.TabIndex = 29;
            // 
            // btnBackUpload
            // 
            this.btnBackUpload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBackUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackUpload.Image = ((System.Drawing.Image)(resources.GetObject("btnBackUpload.Image")));
            this.btnBackUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackUpload.Location = new System.Drawing.Point(3, 3);
            this.btnBackUpload.Name = "btnBackUpload";
            this.btnBackUpload.Size = new System.Drawing.Size(42, 37);
            this.btnBackUpload.TabIndex = 21;
            this.btnBackUpload.UseVisualStyleBackColor = true;
            this.btnBackUpload.Click += new System.EventHandler(this.btnBackUpload_Click);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(16, 115);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(52, 13);
            this.lblSelected.TabIndex = 19;
            this.lblSelected.Text = "Selected:";
            // 
            // panelDownload
            // 
            this.panelDownload.Controls.Add(this.lblDownloadStatus);
            this.panelDownload.Controls.Add(this.tbxDownloadUrl);
            this.panelDownload.Controls.Add(this.btnDownload);
            this.panelDownload.Controls.Add(this.lblSetUrl);
            this.panelDownload.Controls.Add(this.btnBackDownload);
            this.panelDownload.Location = new System.Drawing.Point(3, 12);
            this.panelDownload.Name = "panelDownload";
            this.panelDownload.Size = new System.Drawing.Size(290, 322);
            this.panelDownload.TabIndex = 30;
            // 
            // lblDownloadStatus
            // 
            this.lblDownloadStatus.AutoSize = true;
            this.lblDownloadStatus.Location = new System.Drawing.Point(14, 241);
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            this.lblDownloadStatus.Size = new System.Drawing.Size(95, 13);
            this.lblDownloadStatus.TabIndex = 22;
            this.lblDownloadStatus.Text = "lblDownloadStatus";
            // 
            // btnBackDownload
            // 
            this.btnBackDownload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBackDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnBackDownload.Image")));
            this.btnBackDownload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackDownload.Location = new System.Drawing.Point(3, 3);
            this.btnBackDownload.Name = "btnBackDownload";
            this.btnBackDownload.Size = new System.Drawing.Size(42, 37);
            this.btnBackDownload.TabIndex = 21;
            this.btnBackDownload.UseVisualStyleBackColor = true;
            this.btnBackDownload.Click += new System.EventHandler(this.btnBackDownload_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 339);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelUpload);
            this.Controls.Add(this.panelDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelUpload.ResumeLayout(false);
            this.panelUpload.PerformLayout();
            this.panelDownload.ResumeLayout(false);
            this.panelDownload.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox tbxUploadUrl;
        private System.Windows.Forms.TextBox tbxDownloadUrl;
        private System.Windows.Forms.Label lblSetUrl;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnOpenUploadPanel;
        private System.Windows.Forms.Button btnOpenDownloadPanel;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblChooseAnOption;
        private System.Windows.Forms.Label lblWhatWouldYouLikeToDo;
        private System.Windows.Forms.Panel panelUpload;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Button btnBackUpload;
        private System.Windows.Forms.Panel panelDownload;
        private System.Windows.Forms.Button btnBackDownload;
        private System.Windows.Forms.Label lblDownloadStatus;
    }
}

