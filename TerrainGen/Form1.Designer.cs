
namespace TerrainGen
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.scaleLbl = new System.Windows.Forms.Label();
            this.generateBtn = new System.Windows.Forms.Button();
            this.scaleBar = new System.Windows.Forms.TrackBar();
            this.widthLbl = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.heightLbl = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleBar)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.canvas.Location = new System.Drawing.Point(3, 7);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(800, 800);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // scaleLbl
            // 
            this.scaleLbl.AutoSize = true;
            this.scaleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.scaleLbl.Location = new System.Drawing.Point(818, 9);
            this.scaleLbl.Name = "scaleLbl";
            this.scaleLbl.Size = new System.Drawing.Size(57, 20);
            this.scaleLbl.TabIndex = 1;
            this.scaleLbl.Text = "Scale :";
            // 
            // generateBtn
            // 
            this.generateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.generateBtn.Location = new System.Drawing.Point(818, 239);
            this.generateBtn.Name = "generateBtn";
            this.generateBtn.Size = new System.Drawing.Size(177, 39);
            this.generateBtn.TabIndex = 2;
            this.generateBtn.Text = "Generate Terrain";
            this.generateBtn.UseVisualStyleBackColor = true;
            this.generateBtn.Click += new System.EventHandler(this.generateBtn_Click);
            // 
            // scaleBar
            // 
            this.scaleBar.Location = new System.Drawing.Point(822, 41);
            this.scaleBar.Maximum = 100;
            this.scaleBar.Name = "scaleBar";
            this.scaleBar.Size = new System.Drawing.Size(177, 45);
            this.scaleBar.TabIndex = 3;
            this.scaleBar.Value = 100;
            this.scaleBar.Scroll += new System.EventHandler(this.scaleBar_Scroll);
            // 
            // widthLbl
            // 
            this.widthLbl.AutoSize = true;
            this.widthLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.widthLbl.Location = new System.Drawing.Point(822, 89);
            this.widthLbl.Name = "widthLbl";
            this.widthLbl.Size = new System.Drawing.Size(58, 20);
            this.widthLbl.TabIndex = 4;
            this.widthLbl.Text = "Width :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(822, 121);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // heightLbl
            // 
            this.heightLbl.AutoSize = true;
            this.heightLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.heightLbl.Location = new System.Drawing.Point(818, 162);
            this.heightLbl.Name = "heightLbl";
            this.heightLbl.Size = new System.Drawing.Size(64, 20);
            this.heightLbl.TabIndex = 6;
            this.heightLbl.Text = "Height :";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(822, 194);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(173, 20);
            this.textBox2.TabIndex = 7;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 812);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.heightLbl);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.widthLbl);
            this.Controls.Add(this.scaleBar);
            this.Controls.Add(this.generateBtn);
            this.Controls.Add(this.scaleLbl);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label scaleLbl;
        private System.Windows.Forms.Button generateBtn;
        private System.Windows.Forms.TrackBar scaleBar;
        private System.Windows.Forms.Label widthLbl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label heightLbl;
        private System.Windows.Forms.TextBox textBox2;
    }
}

