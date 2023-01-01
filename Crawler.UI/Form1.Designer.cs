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
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_deleteall = new System.Windows.Forms.Button();
            this.txt_seed = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblResponseTime = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblQueue = new System.Windows.Forms.Label();
            this.txt_log = new System.Windows.Forms.RichTextBox();
            this.prg_total = new System.Windows.Forms.ProgressBar();
            this.cbx_follow_internal = new System.Windows.Forms.CheckBox();
            this.cbx_follow_sitemap = new System.Windows.Forms.CheckBox();
            this.num_max_internal_depth = new System.Windows.Forms.NumericUpDown();
            this.lbl_max_depth = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_max_internal_depth)).BeginInit();
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
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(12, 12);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 0;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Visible = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_deleteall
            // 
            this.btn_deleteall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_deleteall.Location = new System.Drawing.Point(1063, 12);
            this.btn_deleteall.Name = "btn_deleteall";
            this.btn_deleteall.Size = new System.Drawing.Size(75, 23);
            this.btn_deleteall.TabIndex = 1;
            this.btn_deleteall.Text = "Delete All";
            this.btn_deleteall.UseVisualStyleBackColor = true;
            this.btn_deleteall.Click += new System.EventHandler(this.btn_deleteall_Click);
            // 
            // txt_seed
            // 
            this.txt_seed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_seed.Location = new System.Drawing.Point(12, 41);
            this.txt_seed.Name = "txt_seed";
            this.txt_seed.PlaceholderText = "https://mysite.com";
            this.txt_seed.Size = new System.Drawing.Size(1126, 23);
            this.txt_seed.TabIndex = 2;
            this.txt_seed.TextChanged += new System.EventHandler(this.txt_seed_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblResponseTime);
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.lblQueue);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 77);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crawl stats";
            // 
            // lblResponseTime
            // 
            this.lblResponseTime.AutoSize = true;
            this.lblResponseTime.Location = new System.Drawing.Point(7, 49);
            this.lblResponseTime.Name = "lblResponseTime";
            this.lblResponseTime.Size = new System.Drawing.Size(87, 15);
            this.lblResponseTime.TabIndex = 2;
            this.lblResponseTime.Text = "Response time:";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(6, 34);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(85, 15);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "Pages crawled:";
            // 
            // lblQueue
            // 
            this.lblQueue.AutoSize = true;
            this.lblQueue.Location = new System.Drawing.Point(6, 19);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(86, 15);
            this.lblQueue.TabIndex = 0;
            this.lblQueue.Text = "Links in queue:";
            // 
            // txt_log
            // 
            this.txt_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_log.Location = new System.Drawing.Point(192, 70);
            this.txt_log.Name = "txt_log";
            this.txt_log.Size = new System.Drawing.Size(946, 531);
            this.txt_log.TabIndex = 4;
            this.txt_log.Text = "";
            // 
            // prg_total
            // 
            this.prg_total.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prg_total.Location = new System.Drawing.Point(12, 607);
            this.prg_total.Name = "prg_total";
            this.prg_total.Size = new System.Drawing.Size(1126, 23);
            this.prg_total.TabIndex = 5;
            // 
            // cbx_follow_internal
            // 
            this.cbx_follow_internal.AutoSize = true;
            this.cbx_follow_internal.Location = new System.Drawing.Point(206, 14);
            this.cbx_follow_internal.Name = "cbx_follow_internal";
            this.cbx_follow_internal.Size = new System.Drawing.Size(134, 19);
            this.cbx_follow_internal.TabIndex = 6;
            this.cbx_follow_internal.Text = "Follow Internal Links";
            this.cbx_follow_internal.UseVisualStyleBackColor = true;
            this.cbx_follow_internal.CheckedChanged += new System.EventHandler(this.cbx_follow_internal_CheckedChanged);
            // 
            // cbx_follow_sitemap
            // 
            this.cbx_follow_sitemap.AutoSize = true;
            this.cbx_follow_sitemap.Location = new System.Drawing.Point(93, 14);
            this.cbx_follow_sitemap.Name = "cbx_follow_sitemap";
            this.cbx_follow_sitemap.Size = new System.Drawing.Size(107, 19);
            this.cbx_follow_sitemap.TabIndex = 7;
            this.cbx_follow_sitemap.Text = "Follow Sitemap";
            this.cbx_follow_sitemap.UseVisualStyleBackColor = true;
            this.cbx_follow_sitemap.CheckedChanged += new System.EventHandler(this.cbx_follow_sitemap_CheckedChanged);
            // 
            // num_max_internal_depth
            // 
            this.num_max_internal_depth.CausesValidation = false;
            this.num_max_internal_depth.Location = new System.Drawing.Point(412, 11);
            this.num_max_internal_depth.Name = "num_max_internal_depth";
            this.num_max_internal_depth.Size = new System.Drawing.Size(40, 23);
            this.num_max_internal_depth.TabIndex = 8;
            this.num_max_internal_depth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.num_max_internal_depth.ValueChanged += new System.EventHandler(this.num_max_internal_depth_ValueChanged);
            // 
            // lbl_max_depth
            // 
            this.lbl_max_depth.AutoSize = true;
            this.lbl_max_depth.Location = new System.Drawing.Point(344, 15);
            this.lbl_max_depth.Name = "lbl_max_depth";
            this.lbl_max_depth.Size = new System.Drawing.Size(68, 15);
            this.lbl_max_depth.TabIndex = 9;
            this.lbl_max_depth.Text = "Max Depth:";
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 642);
            this.Controls.Add(this.lbl_max_depth);
            this.Controls.Add(this.num_max_internal_depth);
            this.Controls.Add(this.cbx_follow_sitemap);
            this.Controls.Add(this.cbx_follow_internal);
            this.Controls.Add(this.prg_total);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_seed);
            this.Controls.Add(this.btn_deleteall);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_stop);
            this.Name = "frm_main";
            this.Text = "Respectful Crawler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_main_FormClosing);
            this.Shown += new System.EventHandler(this.frm_main_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_max_internal_depth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btn_start;
        private Button btn_stop;
        private Button btn_deleteall;
        private TextBox txt_seed;
        private GroupBox groupBox1;
        private Label lblResponseTime;
        private Label lblCount;
        private Label lblQueue;
        private RichTextBox txt_log;
        private ProgressBar prg_total;
        private CheckBox cbx_follow_internal;
        private CheckBox cbx_follow_sitemap;
        private NumericUpDown num_max_internal_depth;
        private Label lbl_max_depth;
    }
}