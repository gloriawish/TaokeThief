namespace TaokeThief
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ke_url = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cate_url_txt = new System.Windows.Forms.TextBox();
            this.btn_load = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.load_file = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.file_path = new System.Windows.Forms.TextBox();
            this.btn_open = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "课程url：";
            // 
            // ke_url
            // 
            this.ke_url.Location = new System.Drawing.Point(90, 77);
            this.ke_url.Name = "ke_url";
            this.ke_url.Size = new System.Drawing.Size(699, 21);
            this.ke_url.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(90, 120);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(752, 345);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "课程信息：";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(795, 76);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 3;
            this.btn_start.Text = "Run";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "课程类别url：";
            // 
            // cate_url_txt
            // 
            this.cate_url_txt.Location = new System.Drawing.Point(90, 46);
            this.cate_url_txt.Name = "cate_url_txt";
            this.cate_url_txt.Size = new System.Drawing.Size(699, 21);
            this.cate_url_txt.TabIndex = 1;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(795, 44);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 5;
            this.btn_load.Text = "Run";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // load_file
            // 
            this.load_file.Location = new System.Drawing.Point(795, 12);
            this.load_file.Name = "load_file";
            this.load_file.Size = new System.Drawing.Size(75, 23);
            this.load_file.TabIndex = 6;
            this.load_file.Text = "Run";
            this.load_file.UseVisualStyleBackColor = true;
            this.load_file.Click += new System.EventHandler(this.load_file_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "文件路径：";
            // 
            // file_path
            // 
            this.file_path.Location = new System.Drawing.Point(90, 12);
            this.file_path.Name = "file_path";
            this.file_path.Size = new System.Drawing.Size(610, 21);
            this.file_path.TabIndex = 1;
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(714, 12);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 23);
            this.btn_open.TabIndex = 6;
            this.btn_open.Text = "选择文件";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 486);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Tips:输入框三个任选一个 会在D盘生成data文件";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(882, 524);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.load_file);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.file_path);
            this.Controls.Add(this.cate_url_txt);
            this.Controls.Add(this.ke_url);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "91淘课视频小偷";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ke_url;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cate_url_txt;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button load_file;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox file_path;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Label label5;
    }
}

