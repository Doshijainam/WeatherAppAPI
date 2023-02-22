namespace WeatherApplication
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labConditions = new System.Windows.Forms.Label();
            this.labelDetails = new System.Windows.Forms.Label();
            this.labSun = new System.Windows.Forms.Label();
            this.labSunset = new System.Windows.Forms.Label();
            this.labelWindSpeed = new System.Windows.Forms.Label();
            this.labPressure = new System.Windows.Forms.Label();
            this.labelWeather = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "City";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(232, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 26);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(445, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 2;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(105, 101);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(104, 88);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // labConditions
            // 
            this.labConditions.AutoSize = true;
            this.labConditions.Location = new System.Drawing.Point(101, 206);
            this.labConditions.Name = "labConditions";
            this.labConditions.Size = new System.Drawing.Size(76, 20);
            this.labConditions.TabIndex = 4;
            this.labConditions.Text = "Condition";
            // 
            // labelDetails
            // 
            this.labelDetails.AutoSize = true;
            this.labelDetails.Location = new System.Drawing.Point(101, 253);
            this.labelDetails.Name = "labelDetails";
            this.labelDetails.Size = new System.Drawing.Size(58, 20);
            this.labelDetails.TabIndex = 5;
            this.labelDetails.Text = "Details";
            // 
            // labSun
            // 
            this.labSun.AutoSize = true;
            this.labSun.Location = new System.Drawing.Point(101, 297);
            this.labSun.Name = "labSun";
            this.labSun.Size = new System.Drawing.Size(63, 20);
            this.labSun.TabIndex = 6;
            this.labSun.Text = "Sunrise";
            // 
            // labSunset
            // 
            this.labSunset.AutoSize = true;
            this.labSunset.Location = new System.Drawing.Point(101, 343);
            this.labSunset.Name = "labSunset";
            this.labSunset.Size = new System.Drawing.Size(60, 20);
            this.labSunset.TabIndex = 7;
            this.labSunset.Text = "Sunset";
            // 
            // labelWindSpeed
            // 
            this.labelWindSpeed.AutoSize = true;
            this.labelWindSpeed.Location = new System.Drawing.Point(375, 215);
            this.labelWindSpeed.Name = "labelWindSpeed";
            this.labelWindSpeed.Size = new System.Drawing.Size(96, 20);
            this.labelWindSpeed.TabIndex = 8;
            this.labelWindSpeed.Text = "Wind Speed";
            // 
            // labPressure
            // 
            this.labPressure.AutoSize = true;
            this.labPressure.Location = new System.Drawing.Point(375, 263);
            this.labPressure.Name = "labPressure";
            this.labPressure.Size = new System.Drawing.Size(72, 20);
            this.labPressure.TabIndex = 9;
            this.labPressure.Text = "Pressure";
            // 
            // labelWeather
            // 
            this.labelWeather.AutoSize = true;
            this.labelWeather.Location = new System.Drawing.Point(375, 308);
            this.labelWeather.Name = "labelWeather";
            this.labelWeather.Size = new System.Drawing.Size(104, 20);
            this.labelWeather.TabIndex = 10;
            this.labelWeather.Text = "Temperature ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelWeather);
            this.Controls.Add(this.labPressure);
            this.Controls.Add(this.labelWindSpeed);
            this.Controls.Add(this.labSunset);
            this.Controls.Add(this.labSun);
            this.Controls.Add(this.labelDetails);
            this.Controls.Add(this.labConditions);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labConditions;
        private System.Windows.Forms.Label labelDetails;
        private System.Windows.Forms.Label labSun;
        private System.Windows.Forms.Label labSunset;
        private System.Windows.Forms.Label labelWindSpeed;
        private System.Windows.Forms.Label labPressure;
        private System.Windows.Forms.Label labelWeather;
    }
}

