namespace TaokeDownload
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.l_one_info = new System.Windows.Forms.Label();
            this.l_total_info = new System.Windows.Forms.Label();
            this.progress_one = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据路径：";
            // 
            // txt_path
            // 
            this.txt_path.Location = new System.Drawing.Point(98, 35);
            this.txt_path.Name = "txt_path";
            this.txt_path.Size = new System.Drawing.Size(607, 21);
            this.txt_path.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "下载情况：";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(29, 105);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(838, 397);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(711, 33);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 23);
            this.btn_open.TabIndex = 4;
            this.btn_open.Text = "选择文件";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // btn_download
            // 
            this.btn_download.Location = new System.Drawing.Point(792, 33);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(75, 23);
            this.btn_download.TabIndex = 5;
            this.btn_download.Text = "Run";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // l_one_info
            // 
            this.l_one_info.AutoSize = true;
            this.l_one_info.Location = new System.Drawing.Point(98, 66);
            this.l_one_info.Name = "l_one_info";
            this.l_one_info.Size = new System.Drawing.Size(0, 12);
            this.l_one_info.TabIndex = 6;
            // 
            // l_total_info
            // 
            this.l_total_info.AutoSize = true;
            this.l_total_info.Location = new System.Drawing.Point(394, 87);
            this.l_total_info.Name = "l_total_info";
            this.l_total_info.Size = new System.Drawing.Size(0, 12);
            this.l_total_info.TabIndex = 7;
            // 
            // progress_one
            // 
            this.progress_one.Location = new System.Drawing.Point(100, 87);
            this.progress_one.Name = "progress_one";
            this.progress_one.Size = new System.Drawing.Size(249, 12);
            this.progress_one.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(882, 524);
            this.Controls.Add(this.progress_one);
            this.Controls.Add(this.l_total_info);
            this.Controls.Add(this.l_one_info);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "91淘课下载";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label l_one_info;
        private System.Windows.Forms.Label l_total_info;
        private System.Windows.Forms.ProgressBar progress_one;
    }
}

