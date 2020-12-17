
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
            this.generateBtn = new System.Windows.Forms.Button();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.seedLabel = new System.Windows.Forms.Label();
            this.seedBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.sizeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.seedBox = new System.Windows.Forms.TextBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.regionMap = new System.Windows.Forms.PictureBox();
            this.filterRegionlbl = new System.Windows.Forms.Label();
            this.twoBtn = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionMap)).BeginInit();
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
            // generateBtn
            // 
            this.generateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.generateBtn.Location = new System.Drawing.Point(850, 487);
            this.generateBtn.Name = "generateBtn";
            this.generateBtn.Size = new System.Drawing.Size(129, 39);
            this.generateBtn.TabIndex = 2;
            this.generateBtn.Text = "Generate Terrain";
            this.generateBtn.UseVisualStyleBackColor = true;
            this.generateBtn.Click += new System.EventHandler(this.generateBtn_Click);
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.sizeLabel.Location = new System.Drawing.Point(820, 56);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(48, 20);
            this.sizeLabel.TabIndex = 4;
            this.sizeLabel.Text = "Size :";
            // 
            // seedLabel
            // 
            this.seedLabel.AutoSize = true;
            this.seedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.seedLabel.Location = new System.Drawing.Point(821, 135);
            this.seedLabel.Name = "seedLabel";
            this.seedLabel.Size = new System.Drawing.Size(49, 17);
            this.seedLabel.TabIndex = 11;
            this.seedLabel.Text = "Seed :";
            // 
            // seedBtn
            // 
            this.seedBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.seedBtn.Location = new System.Drawing.Point(972, 131);
            this.seedBtn.Name = "seedBtn";
            this.seedBtn.Size = new System.Drawing.Size(30, 26);
            this.seedBtn.TabIndex = 12;
            this.seedBtn.Text = "R";
            this.seedBtn.UseVisualStyleBackColor = true;
            this.seedBtn.Click += new System.EventHandler(this.seedBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(815, 476);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 5);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox2.Location = new System.Drawing.Point(815, 532);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(198, 5);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // sizeBox
            // 
            this.sizeBox.Location = new System.Drawing.Point(866, 58);
            this.sizeBox.Name = "sizeBox";
            this.sizeBox.Size = new System.Drawing.Size(145, 20);
            this.sizeBox.TabIndex = 15;
            this.sizeBox.Text = "1024";
            this.sizeBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.sizeBox_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.Location = new System.Drawing.Point(845, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 29);
            this.label1.TabIndex = 18;
            this.label1.Text = "Terrain Gen";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox3.Location = new System.Drawing.Point(819, 40);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(191, 5);
            this.pictureBox3.TabIndex = 19;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(818, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Noise Settings";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox4.Location = new System.Drawing.Point(822, 88);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(191, 5);
            this.pictureBox4.TabIndex = 25;
            this.pictureBox4.TabStop = false;
            // 
            // seedBox
            // 
            this.seedBox.Location = new System.Drawing.Point(866, 135);
            this.seedBox.Name = "seedBox";
            this.seedBox.Size = new System.Drawing.Size(100, 20);
            this.seedBox.TabIndex = 34;
            this.seedBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.seedBox_KeyUp);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox6.Location = new System.Drawing.Point(824, 161);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(191, 5);
            this.pictureBox6.TabIndex = 35;
            this.pictureBox6.TabStop = false;
            // 
            // regionMap
            // 
            this.regionMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.regionMap.Location = new System.Drawing.Point(819, 241);
            this.regionMap.Name = "regionMap";
            this.regionMap.Size = new System.Drawing.Size(200, 200);
            this.regionMap.TabIndex = 36;
            this.regionMap.TabStop = false;
            // 
            // filterRegionlbl
            // 
            this.filterRegionlbl.AutoSize = true;
            this.filterRegionlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.filterRegionlbl.Location = new System.Drawing.Point(825, 173);
            this.filterRegionlbl.Name = "filterRegionlbl";
            this.filterRegionlbl.Size = new System.Drawing.Size(99, 20);
            this.filterRegionlbl.TabIndex = 37;
            this.filterRegionlbl.Text = "Filter Region";
            // 
            // twoBtn
            // 
            this.twoBtn.Location = new System.Drawing.Point(819, 212);
            this.twoBtn.Name = "twoBtn";
            this.twoBtn.Size = new System.Drawing.Size(75, 23);
            this.twoBtn.TabIndex = 39;
            this.twoBtn.Text = "addRegion";
            this.twoBtn.UseVisualStyleBackColor = true;
            this.twoBtn.Click += new System.EventHandler(this.twoBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(819, 447);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 23);
            this.applyBtn.TabIndex = 40;
            this.applyBtn.Text = "apply";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 812);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.twoBtn);
            this.Controls.Add(this.filterRegionlbl);
            this.Controls.Add(this.regionMap);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.seedBox);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sizeBox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.seedBtn);
            this.Controls.Add(this.seedLabel);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.generateBtn);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regionMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button generateBtn;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label seedLabel;
        private System.Windows.Forms.Button seedBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox sizeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox seedBox;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox regionMap;
        private System.Windows.Forms.Label filterRegionlbl;
        private System.Windows.Forms.Button twoBtn;
        private System.Windows.Forms.Button applyBtn;
    }
}

