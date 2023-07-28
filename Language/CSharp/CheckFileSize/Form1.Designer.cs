namespace CheckFileSize
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            RootPathBox = new TextBox();
            label1 = new Label();
            OverSizeFiles = new TextBox();
            RootPathBtn = new Button();
            SizeCapacity = new TextBox();
            label2 = new Label();
            FileSizeBtn = new Button();
            LogBox = new TextBox();
            ClearSizeList = new Button();
            IsGitIgnoreOn = new CheckBox();
            SuspendLayout();
            // 
            // RootPathBox
            // 
            RootPathBox.AcceptsTab = true;
            resources.ApplyResources(RootPathBox, "RootPathBox");
            RootPathBox.Name = "RootPathBox";
            RootPathBox.TextChanged += RootPath_TextChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            label1.Click += label1_Click;
            // 
            // OverSizeFiles
            // 
            resources.ApplyResources(OverSizeFiles, "OverSizeFiles");
            OverSizeFiles.Name = "OverSizeFiles";
            OverSizeFiles.TextChanged += OverSizeFiles_TextChanged;
            // 
            // RootPathBtn
            // 
            resources.ApplyResources(RootPathBtn, "RootPathBtn");
            RootPathBtn.Name = "RootPathBtn";
            RootPathBtn.UseVisualStyleBackColor = true;
            // 
            // SizeCapacity
            // 
            resources.ApplyResources(SizeCapacity, "SizeCapacity");
            SizeCapacity.Name = "SizeCapacity";
            SizeCapacity.TextChanged += SizeCapacity_TextChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // FileSizeBtn
            // 
            resources.ApplyResources(FileSizeBtn, "FileSizeBtn");
            FileSizeBtn.Name = "FileSizeBtn";
            FileSizeBtn.UseVisualStyleBackColor = true;
            FileSizeBtn.Click += FileSizeChangeBtn;
            // 
            // LogBox
            // 
            resources.ApplyResources(LogBox, "LogBox");
            LogBox.Name = "LogBox";
            LogBox.TextChanged += LogBox_TextChanged;
            // 
            // ClearSizeList
            // 
            resources.ApplyResources(ClearSizeList, "ClearSizeList");
            ClearSizeList.Name = "ClearSizeList";
            ClearSizeList.UseVisualStyleBackColor = true;
            ClearSizeList.Click += ClearSizeList_Click;
            // 
            // IsGitIgnoreOn
            // 
            resources.ApplyResources(IsGitIgnoreOn, "IsGitIgnoreOn");
            IsGitIgnoreOn.Checked = true;
            IsGitIgnoreOn.CheckState = CheckState.Checked;
            IsGitIgnoreOn.Name = "IsGitIgnoreOn";
            IsGitIgnoreOn.UseVisualStyleBackColor = true;
            IsGitIgnoreOn.CheckedChanged += IsGitIgnoreOn_CheckedChanged;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(IsGitIgnoreOn);
            Controls.Add(ClearSizeList);
            Controls.Add(LogBox);
            Controls.Add(FileSizeBtn);
            Controls.Add(label2);
            Controls.Add(SizeCapacity);
            Controls.Add(RootPathBtn);
            Controls.Add(OverSizeFiles);
            Controls.Add(label1);
            Controls.Add(RootPathBox);
            Name = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox RootPathBox;
        private Label label1;
        private TextBox OverSizeFiles;
        private Button RootPathBtn;
        private TextBox SizeCapacity;
        private Label label2;
        private Button FileSizeBtn;
        private TextBox LogBox;
        private Button ClearSizeList;
        private CheckBox IsGitIgnoreOn;
    }
}