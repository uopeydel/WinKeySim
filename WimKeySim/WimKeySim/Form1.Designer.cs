namespace WimKeySim
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
            label1 = new Label();
            label2 = new Label();
            numX = new NumericUpDown();
            numY = new NumericUpDown();
            btnClick = new Button();
            btnType = new Button();
            tbxTextToType = new TextBox();
            tbxReadAllBtn = new TextBox();
            btnReadAllBtn = new Button();
            tbxBtnNameClick = new TextBox();
            btnClickBtnName = new Button();
            ((System.ComponentModel.ISupportInitialize)numX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 117);
            label1.Name = "label1";
            label1.Size = new Size(12, 15);
            label1.TabIndex = 0;
            label1.Text = "x";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(156, 117);
            label2.Name = "label2";
            label2.Size = new Size(13, 15);
            label2.TabIndex = 1;
            label2.Text = "y";
            // 
            // numX
            // 
            numX.Location = new Point(30, 115);
            numX.Maximum = new decimal(new int[] { 800, 0, 0, 0 });
            numX.Name = "numX";
            numX.Size = new Size(120, 23);
            numX.TabIndex = 2;
            numX.Value = new decimal(new int[] { 266, 0, 0, 0 });
            // 
            // numY
            // 
            numY.Location = new Point(175, 115);
            numY.Maximum = new decimal(new int[] { 800, 0, 0, 0 });
            numY.Name = "numY";
            numY.Size = new Size(120, 23);
            numY.TabIndex = 3;
            numY.Value = new decimal(new int[] { 343, 0, 0, 0 });
            // 
            // btnClick
            // 
            btnClick.Location = new Point(314, 115);
            btnClick.Name = "btnClick";
            btnClick.Size = new Size(133, 23);
            btnClick.TabIndex = 4;
            btnClick.Text = "Button Click";
            btnClick.UseVisualStyleBackColor = true;
            btnClick.Click += btnClick_Click;
            // 
            // btnType
            // 
            btnType.Location = new Point(314, 86);
            btnType.Name = "btnType";
            btnType.Size = new Size(133, 23);
            btnType.TabIndex = 5;
            btnType.Text = "Type Text";
            btnType.UseVisualStyleBackColor = true;
            btnType.Click += btnType_Click;
            // 
            // tbxTextToType
            // 
            tbxTextToType.Location = new Point(12, 86);
            tbxTextToType.Name = "tbxTextToType";
            tbxTextToType.Size = new Size(283, 23);
            tbxTextToType.TabIndex = 6;
            tbxTextToType.Text = "test";
            // 
            // tbxReadAllBtn
            // 
            tbxReadAllBtn.Location = new Point(12, 12);
            tbxReadAllBtn.Name = "tbxReadAllBtn";
            tbxReadAllBtn.Size = new Size(283, 23);
            tbxReadAllBtn.TabIndex = 8;
            tbxReadAllBtn.Text = "Rag";
            // 
            // btnReadAllBtn
            // 
            btnReadAllBtn.Location = new Point(314, 12);
            btnReadAllBtn.Name = "btnReadAllBtn";
            btnReadAllBtn.Size = new Size(133, 23);
            btnReadAllBtn.TabIndex = 7;
            btnReadAllBtn.Text = "Read All Button";
            btnReadAllBtn.UseVisualStyleBackColor = true;
            btnReadAllBtn.Click += btnReadAllBtn_Click;
            // 
            // tbxBtnNameClick
            // 
            tbxBtnNameClick.Location = new Point(12, 41);
            tbxBtnNameClick.Name = "tbxBtnNameClick";
            tbxBtnNameClick.Size = new Size(283, 23);
            tbxBtnNameClick.TabIndex = 10;
            // 
            // btnClickBtnName
            // 
            btnClickBtnName.Location = new Point(314, 41);
            btnClickBtnName.Name = "btnClickBtnName";
            btnClickBtnName.Size = new Size(133, 23);
            btnClickBtnName.TabIndex = 9;
            btnClickBtnName.Text = "Click Btn Name";
            btnClickBtnName.UseVisualStyleBackColor = true;
            btnClickBtnName.Click += btnClickBtnName_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(489, 147);
            Controls.Add(tbxBtnNameClick);
            Controls.Add(btnClickBtnName);
            Controls.Add(tbxReadAllBtn);
            Controls.Add(btnReadAllBtn);
            Controls.Add(tbxTextToType);
            Controls.Add(btnType);
            Controls.Add(btnClick);
            Controls.Add(numY);
            Controls.Add(numX);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            StartPosition = FormStartPosition.Manual;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private NumericUpDown numX;
        private NumericUpDown numY;
        private Button btnClick;
        private Button btnType;
        private TextBox tbxTextToType;
        private TextBox tbxReadAllBtn;
        private Button btnReadAllBtn;
        private TextBox tbxBtnNameClick;
        private Button btnClickBtnName;
    }
}