namespace Crawler.UI
{
    partial class frm_main
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
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_deleteall = new System.Windows.Forms.Button();
            this.txt_seed = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.prg_total = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(12, 12);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_deleteall
            // 
            this.btn_deleteall.Location = new System.Drawing.Point(713, 12);
            this.btn_deleteall.Name = "btn_deleteall";
            this.btn_deleteall.Size = new System.Drawing.Size(75, 23);
            this.btn_deleteall.TabIndex = 1;
            this.btn_deleteall.Text = "Delete All";
            this.btn_deleteall.UseVisualStyleBackColor = true;
            this.btn_deleteall.Click += new System.EventHandler(this.btn_deleteall_Click);
            // 
            // txt_seed
            // 
            this.txt_seed.Location = new System.Drawing.Point(12, 41);
            this.txt_seed.Name = "txt_seed";
            this.txt_seed.PlaceholderText = "seed url";
            this.txt_seed.Size = new System.Drawing.Size(776, 23);
            this.txt_seed.TabIndex = 2;
            this.txt_seed.Text = "https://";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 77);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crawl stats";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Response Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pages crawled:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Links in queue:";
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(192, 70);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.Size = new System.Drawing.Size(596, 339);
            this.txt_log.TabIndex = 4;
            // 
            // prg_total
            // 
            this.prg_total.Location = new System.Drawing.Point(12, 415);
            this.prg_total.Name = "prg_total";
            this.prg_total.Size = new System.Drawing.Size(776, 23);
            this.prg_total.TabIndex = 5;
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.prg_total);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_seed);
            this.Controls.Add(this.btn_deleteall);
            this.Controls.Add(this.btn_start);
            this.Name = "frm_main";
            this.Text = "Respectful Craweler";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_start;
        private Button btn_deleteall;
        private TextBox txt_seed;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txt_log;
        private ProgressBar prg_total;
    }
}