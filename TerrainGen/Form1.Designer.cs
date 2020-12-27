
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
            this.filterRegionlbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.mapTab = new System.Windows.Forms.TabControl();
            this.terrainTab = new System.Windows.Forms.TabPage();
            this.mapPage = new System.Windows.Forms.TabPage();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.mapTab.SuspendLayout();
            this.terrainTab.SuspendLayout();
            this.mapPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.canvas.Location = new System.Drawing.Point(6, 6);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(600, 600);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // generateBtn
            // 
            this.generateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.generateBtn.Location = new System.Drawing.Point(686, 208);
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
            this.sizeLabel.Location = new System.Drawing.Point(648, 54);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(48, 20);
            this.sizeLabel.TabIndex = 4;
            this.sizeLabel.Text = "Size :";
            // 
            // seedLabel
            // 
            this.seedLabel.AutoSize = true;
            this.seedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.seedLabel.Location = new System.Drawing.Point(649, 133);
            this.seedLabel.Name = "seedLabel";
            this.seedLabel.Size = new System.Drawing.Size(49, 17);
            this.seedLabel.TabIndex = 11;
            this.seedLabel.Text = "Seed :";
            // 
            // seedBtn
            // 
            this.seedBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.seedBtn.Location = new System.Drawing.Point(790, 107);
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
            this.pictureBox1.Location = new System.Drawing.Point(651, 197);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(198, 5);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox2.Location = new System.Drawing.Point(651, 306);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(198, 5);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // sizeBox
            // 
            this.sizeBox.Location = new System.Drawing.Point(694, 56);
            this.sizeBox.Name = "sizeBox";
            this.sizeBox.Size = new System.Drawing.Size(145, 20);
            this.sizeBox.TabIndex = 15;
            this.sizeBox.Text = "2048";
            this.sizeBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.sizeBox_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label1.Location = new System.Drawing.Point(673, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 29);
            this.label1.TabIndex = 18;
            this.label1.Text = "Terrain Gen";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox3.Location = new System.Drawing.Point(647, 38);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(191, 5);
            this.pictureBox3.TabIndex = 19;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(646, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Noise Settings";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox4.Location = new System.Drawing.Point(650, 86);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(191, 5);
            this.pictureBox4.TabIndex = 25;
            this.pictureBox4.TabStop = false;
            // 
            // seedBox
            // 
            this.seedBox.Location = new System.Drawing.Point(694, 133);
            this.seedBox.Name = "seedBox";
            this.seedBox.Size = new System.Drawing.Size(100, 20);
            this.seedBox.TabIndex = 34;
            this.seedBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.seedBox_KeyUp);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox6.Location = new System.Drawing.Point(652, 159);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(191, 5);
            this.pictureBox6.TabIndex = 35;
            this.pictureBox6.TabStop = false;
            // 
            // filterRegionlbl
            // 
            this.filterRegionlbl.AutoSize = true;
            this.filterRegionlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.filterRegionlbl.Location = new System.Drawing.Point(653, 171);
            this.filterRegionlbl.Name = "filterRegionlbl";
            this.filterRegionlbl.Size = new System.Drawing.Size(99, 20);
            this.filterRegionlbl.TabIndex = 37;
            this.filterRegionlbl.Text = "Filter Region";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.button1.Location = new System.Drawing.Point(686, 261);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 39);
            this.button1.TabIndex = 38;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mapTab
            // 
            this.mapTab.Controls.Add(this.terrainTab);
            this.mapTab.Controls.Add(this.mapPage);
            this.mapTab.Location = new System.Drawing.Point(1, 3);
            this.mapTab.Name = "mapTab";
            this.mapTab.SelectedIndex = 0;
            this.mapTab.Size = new System.Drawing.Size(904, 647);
            this.mapTab.TabIndex = 39;
            // 
            // terrainTab
            // 
            this.terrainTab.Controls.Add(this.canvas);
            this.terrainTab.Controls.Add(this.pictureBox3);
            this.terrainTab.Controls.Add(this.button1);
            this.terrainTab.Controls.Add(this.label1);
            this.terrainTab.Controls.Add(this.filterRegionlbl);
            this.terrainTab.Controls.Add(this.generateBtn);
            this.terrainTab.Controls.Add(this.pictureBox6);
            this.terrainTab.Controls.Add(this.sizeLabel);
            this.terrainTab.Controls.Add(this.seedBox);
            this.terrainTab.Controls.Add(this.seedLabel);
            this.terrainTab.Controls.Add(this.pictureBox4);
            this.terrainTab.Controls.Add(this.seedBtn);
            this.terrainTab.Controls.Add(this.label2);
            this.terrainTab.Controls.Add(this.pictureBox1);
            this.terrainTab.Controls.Add(this.pictureBox2);
            this.terrainTab.Controls.Add(this.sizeBox);
            this.terrainTab.Location = new System.Drawing.Point(4, 22);
            this.terrainTab.Name = "terrainTab";
            this.terrainTab.Padding = new System.Windows.Forms.Padding(3);
            this.terrainTab.Size = new System.Drawing.Size(896, 621);
            this.terrainTab.TabIndex = 0;
            this.terrainTab.Text = "Terrain Gen";
            this.terrainTab.UseVisualStyleBackColor = true;
            // 
            // mapPage
            // 
            this.mapPage.Controls.Add(this.pictureBox5);
            this.mapPage.Location = new System.Drawing.Point(4, 22);
            this.mapPage.Name = "mapPage";
            this.mapPage.Padding = new System.Windows.Forms.Padding(3);
            this.mapPage.Size = new System.Drawing.Size(896, 621);
            this.mapPage.TabIndex = 1;
            this.mapPage.Text = "Map Gen";
            this.mapPage.UseVisualStyleBackColor = true;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Gray;
            this.pictureBox5.Location = new System.Drawing.Point(7, 6);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(600, 600);
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 656);
            this.Controls.Add(this.mapTab);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.mapTab.ResumeLayout(false);
            this.terrainTab.ResumeLayout(false);
            this.terrainTab.PerformLayout();
            this.mapPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label filterRegionlbl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl mapTab;
        private System.Windows.Forms.TabPage terrainTab;
        private System.Windows.Forms.TabPage mapPage;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}

