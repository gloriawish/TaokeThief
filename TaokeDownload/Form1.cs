using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TaokeDownload
{
    public partial class Form1 : Form
    {
        private WebClient downWebClient = new WebClient();
        private string tempPath;//临时地址
        //下载地址
        private List<KeItem> downloadList;
        private int index;//下载文件的索引
        private long recvSize;//已经接收文件大小
        private long fileSize;//当前下载文件的大小
        public Form1()
        {
            InitializeComponent();
            InitListView(this.listView1, new string[] { "课程名称:180", "视频名称:180", "文件名:160", "地址:400", "文件大小:80" });
            tempPath = "d:\\taoke\\";
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
        }
        //这个函数是初始化列表的
        public void InitListView(ListView listview, string[] col)
        {
            //设置控件的显示模式
            listview.View = View.Details;
            //按照定义的数据来显示界面
            foreach (String item in col)
            {
                string[] tmp = item.Split(':');
                ColumnHeader header = new ColumnHeader();
                header.Text = tmp[0];
                header.Width = int.Parse(tmp[1]);
                listview.Columns.Add(header);
            }
        }

        private delegate void ListViewDelegate(ListView list, Dictionary<string, string> data);
        //添加单行数据
        public void AddListViewData(ListView list, Dictionary<string, string> data)
        {
            if (list.InvokeRequired)//不能访问就创建委托
            {
                ListViewDelegate d = new ListViewDelegate(AddListViewData);
                list.Invoke(d, new object[] { list, data });
            }
            else
            {
                list.BeginUpdate();
                ListViewItem list_item = new ListViewItem();
                bool need_init = true;
                foreach (KeyValuePair<string, string> cell in data)
                {
                    ListViewItem.ListViewSubItem list_sub_item = new ListViewItem.ListViewSubItem();
                    if (need_init)
                    {
                        list_item.Text = cell.Value;
                        need_init = false;
                    }
                    else
                    {
                        list_sub_item.Text = cell.Value;
                        list_item.SubItems.Add(list_sub_item);
                    }
                }
                list.Items.Add(list_item);
                list.EnsureVisible(list.Items.Count - 1);
                list.EndUpdate();

            }
        }

        //向列表里面，单独的添加多行数据，参数是要添加的控件和数据
        public void AddListViewData(ListView listview, List<Dictionary<string, string>> data)
        {
            //开始更新列表
            listview.BeginUpdate();
            foreach (Dictionary<string, string> item in data)
            {
                ListViewItem list_item = new ListViewItem();
                bool need_init = true;
                foreach (KeyValuePair<string, string> cell in item)
                {
                    ListViewItem.ListViewSubItem list_sub_item = new ListViewItem.ListViewSubItem();
                    if (need_init)
                    {
                        list_item.Text = cell.Value;
                        need_init = false;
                    }
                    else
                    {
                        list_sub_item.Text = cell.Value;
                        list_item.SubItems.Add(list_sub_item);
                    }
                }
                listview.Items.Add(list_item);
            }
            //结束跟新列表
            listview.EndUpdate();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txt_path.Text = openFileDialog1.FileName;
            }
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            /*
            downloadList = new List<KeItem>();

            downloadList.Add(new KeItem("cate1", "文件1", "http://s1.91taoke.com/flv/ftp08/yw/chusan/mjmz/11xyua.flv"));
            downloadList.Add(new KeItem("cate1", "文件2", "http://s1.91taoke.com/flv/ftp08/yw/chusan/mjmz/12sjg.flv"));
            downloadList.Add(new KeItem("cate1", "文件3", "http://s1.91taoke.com/flv/ftp08/yw/chusan/mjmz/14xxwl.flv"));
            downloadList.Add(new KeItem("cate1", "文件4", "http://s1.91taoke.com/flv/ftp08/yw/chusan/mjmz/17bt.flv"));
            */
            downloadList = LoadData(txt_path.Text);
            InitWebClient();
            btn_download.Enabled = false;
        }
        //开始更新文件
        private void InitWebClient()
        {
            this.downWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downWebClient_DownloadProgressChanged);
            this.downWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(downWebClient_DownloadFileCompleted);

            DownloadFile();//开始下载文件
        }
        //下载完成事件
        void downWebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                recvSize += fileSize;//增加已接受文件大小

                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("taocan", downloadList[index].Cate);
                data.Add("name", downloadList[index].Name);
                data.Add("nid", downloadList[index].fileName);
                data.Add("url", downloadList[index].Url);
                data.Add("size", ConvertSize(fileSize));
                AddListViewData(this.listView1, data);
                //TODO 下载下一个文件
                index++;
                if (index < downloadList.Count)
                {
                    DownloadFile();
                }
                else
                {
                    //下载完成
                    MessageBox.Show("下载完毕!");
                    btn_download.Enabled = true;
                    index = 0;
                }

            }
        }
        //下载进度改变
        void downWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            ChangeLabelText(this.l_one_info, String.Format(CultureInfo.InvariantCulture, "正在下载:{0}  [ {1}/{2} ]", downloadList[index].fileName, ConvertSize(e.BytesReceived), ConvertSize(e.TotalBytesToReceive)));
            fileSize = e.TotalBytesToReceive;
            this.progress_one.Value = e.ProgressPercentage;
        }

        //下载文件
        private void DownloadFile()
        {
            try
            {
                ChangeLabelText(this.l_total_info, String.Format(CultureInfo.InvariantCulture, "下载进度 {0}/{1}  [ 已下载:{2} ]", index, downloadList.Count, ConvertSize(recvSize)));
                Uri remote = new Uri(downloadList[index].Url);
                string local = tempPath + downloadList[index].Cate +"\\"+ downloadList[index].fileName;
                if (!Directory.Exists(tempPath + downloadList[index].Cate + "\\"))
                    Directory.CreateDirectory(tempPath + downloadList[index].Cate + "\\");
                this.downWebClient.DownloadFileAsync(remote, local);

                this.progress_one.Value = 0;
            }
            catch (Exception)
            {
                
            }
        }

        private delegate void LableDelegate(Label lable, string text);
        public void ChangeLabelText(Label lable, string text)
        {
            if (lable.InvokeRequired)//不能访问就创建委托
            {
                LableDelegate d = new LableDelegate(ChangeLabelText);
                lable.Invoke(d, new object[] { lable, text });
            }
            else
            {
                lable.Text = text;
            }
        }


        public List<KeItem> LoadData(string filepath)
        {
            List<KeItem> list = new List<KeItem>();
            try
            {
                FileStream file = new FileStream(filepath, FileMode.Open);
                StreamReader sr = new StreamReader(file,Encoding.UTF8);
                string line = null;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    string[] url_filed = data[4].Split('|');
                    if (url_filed.Length > 1)
                    {
                        int i = 0;
                        foreach (string ll in url_filed)
                        {
                            KeItem item = new KeItem(data[0], data[1] + i, ll);
                            list.Add(item);
                            i++;
                        }
                    }
                    else
                    {
                        KeItem item = new KeItem(data[0], data[1],data[4]);
                        list.Add(item);
                    }
                    
                }
                sr.Close();
                file.Close();
            }
            catch (Exception)
            {
               
            }
            return list;
        }


        /// <summary> 
        /// 转换字节大小 
        /// </summary> 
        /// <param name="byteSize">输入字节数</param> 
        /// <returns>返回值</returns> 
        private static string ConvertSize(long byteSize)
        {
            string str = "";
            float tempf = (float)byteSize;
            if (tempf / 1024 > 1)
            {
                if ((tempf / 1024) / 1024 > 1)
                {
                    str = ((tempf / 1024) / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "MB";
                }
                else
                {
                    str = (tempf / 1024).ToString("##0.00", CultureInfo.InvariantCulture) + "KB";
                }
            }
            else
            {
                str = tempf.ToString(CultureInfo.InvariantCulture) + "B";
            }
            return str;
        }

    }
}
