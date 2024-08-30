namespace WinForms_Calculator
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
            this.lblNumOne = new System.Windows.Forms.Label();
            this.txtboxOne = new System.Windows.Forms.TextBox();
            this.txtboxTwo = new System.Windows.Forms.TextBox();
            this.lblNumTwo = new System.Windows.Forms.Label();
            this.txtboxTotal = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSubtract = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btnMultiply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBeRude = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNumOne
            // 
            this.lblNumOne.AutoSize = true;
            this.lblNumOne.Location = new System.Drawing.Point(35, 72);
            this.lblNumOne.Name = "lblNumOne";
            this.lblNumOne.Size = new System.Drawing.Size(66, 13);
            this.lblNumOne.TabIndex = 0;
            this.lblNumOne.Text = "First Number";
            // 
            // txtboxOne
            // 
            this.txtboxOne.Location = new System.Drawing.Point(107, 69);
            this.txtboxOne.Name = "txtboxOne";
            this.txtboxOne.Size = new System.Drawing.Size(100, 20);
            this.txtboxOne.TabIndex = 1;
            this.txtboxOne.TextChanged += new System.EventHandler(this.txtboxOne_TextChanged);
            this.txtboxOne.Leave += new System.EventHandler(this.txtboxOne_Leave);
            this.txtboxOne.Validating += new System.ComponentModel.CancelEventHandler(this.txtboxOne_Validating);
            // 
            // txtboxTwo
            // 
            this.txtboxTwo.Location = new System.Drawing.Point(107, 111);
            this.txtboxTwo.Name = "txtboxTwo";
            this.txtboxTwo.Size = new System.Drawing.Size(100, 20);
            this.txtboxTwo.TabIndex = 3;
            this.txtboxTwo.Leave += new System.EventHandler(this.txtboxTwo_Leave);
            this.txtboxTwo.Validating += new System.ComponentModel.CancelEventHandler(this.txtboxTwo_Validating);
            // 
            // lblNumTwo
            // 
            this.lblNumTwo.AutoSize = true;
            this.lblNumTwo.Location = new System.Drawing.Point(18, 114);
            this.lblNumTwo.Name = "lblNumTwo";
            this.lblNumTwo.Size = new System.Drawing.Size(84, 13);
            this.lblNumTwo.TabIndex = 2;
            this.lblNumTwo.Text = "Second Number";
            // 
            // txtboxTotal
            // 
            this.txtboxTotal.Location = new System.Drawing.Point(107, 156);
            this.txtboxTotal.Name = "txtboxTotal";
            this.txtboxTotal.ReadOnly = true;
            this.txtboxTotal.Size = new System.Drawing.Size(100, 20);
            this.txtboxTotal.TabIndex = 5;
            this.txtboxTotal.TextChanged += new System.EventHandler(this.txtboxTotal_TextChanged);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(63, 159);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(37, 13);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "Result";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(28, 201);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Addition";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSubtract
            // 
            this.btnSubtract.Location = new System.Drawing.Point(121, 201);
            this.btnSubtract.Name = "btnSubtract";
            this.btnSubtract.Size = new System.Drawing.Size(80, 23);
            this.btnSubtract.TabIndex = 7;
            this.btnSubtract.Text = "Subtraction";
            this.btnSubtract.UseVisualStyleBackColor = true;
            this.btnSubtract.Click += new System.EventHandler(this.btnSubtract_Click);
            // 
            // btnDivide
            // 
            this.btnDivide.Location = new System.Drawing.Point(28, 239);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(80, 23);
            this.btnDivide.TabIndex = 8;
            this.btnDivide.Text = "Division";
            this.btnDivide.UseVisualStyleBackColor = true;
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // btnMultiply
            // 
            this.btnMultiply.Location = new System.Drawing.Point(121, 239);
            this.btnMultiply.Name = "btnMultiply";
            this.btnMultiply.Size = new System.Drawing.Size(80, 23);
            this.btnMultiply.TabIndex = 9;
            this.btnMultiply.Text = "Multiplication";
            this.btnMultiply.UseVisualStyleBackColor = true;
            this.btnMultiply.Click += new System.EventHandler(this.bttMultiply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "Rude Calculator 4";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(28, 278);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 23);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBeRude
            // 
            this.btnBeRude.Location = new System.Drawing.Point(121, 278);
            this.btnBeRude.Name = "btnBeRude";
            this.btnBeRude.Size = new System.Drawing.Size(80, 23);
            this.btnBeRude.TabIndex = 12;
            this.btnBeRude.Text = "Be Rude";
            this.btnBeRude.UseVisualStyleBackColor = true;
            this.btnBeRude.Click += new System.EventHandler(this.btnBeRude_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 317);
            this.Controls.Add(this.btnBeRude);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMultiply);
            this.Controls.Add(this.btnDivide);
            this.Controls.Add(this.btnSubtract);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtboxTotal);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtboxTwo);
            this.Controls.Add(this.lblNumTwo);
            this.Controls.Add(this.txtboxOne);
            this.Controls.Add(this.lblNumOne);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Form1";
            this.Text = "Rude Calculator 4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumOne;
        private System.Windows.Forms.TextBox txtboxOne;
        private System.Windows.Forms.TextBox txtboxTwo;
        private System.Windows.Forms.Label lblNumTwo;
        private System.Windows.Forms.TextBox txtboxTotal;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSubtract;
        private System.Windows.Forms.Button btnDivide;
        private System.Windows.Forms.Button btnMultiply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnBeRude;
    }
}

