using NSoup;
using NSoup.Nodes;
using NSoup.Select;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TaokeThief
{
    public partial class MainForm : Form
    {
        //网络访问
        HttpHelper http = new HttpHelper();
        public MainForm()
        {
            InitializeComponent();
            //初始化签到人员的列表
            InitListView(this.listView1, new string[] { "课程名称:180","视频名称:180", "NID:80", "CODE:80", "下载地址:460"});
            //new Thread(LoadAllUrl).Start();
        }

        //获取指定课程的所有NID
        public List<Taoke> GetTaokeNIDList(string html)
        {
            List<Taoke> ke_list = new List<Taoke>();
            Document doc = NSoupClient.Parse(html);
            Element ul = doc.GetElementById("kecheng_li");
            Elements lis = ul.GetElementsByTag("li");

            foreach (Element item in lis)
            {
                if (item.HasAttr("id"))//如果包含id属性的话
                {
                    string nid = item.Attr("id");

                    nid=nid.Substring(nid.IndexOf("_")+1, nid.Length - nid.IndexOf("_")-1);
                    Element ename = item.GetElementById(string.Format("a_{0}", nid));
                    string name = ename.Text();
                    Taoke taoke = new Taoke(nid, name);
                    ke_list.Add(taoke);
                }
            }
            return ke_list;
        }
        public string GetTaokeName(string html)
        {
            Document doc = NSoupClient.Parse(html);
            Element title = doc.GetElementById("baofang_name");
            if (title != null)
            {
                return title.Text().Remove(0,5);
            }
            return null;

        }
        public string GetTaokeCode(string id,string nid)
        {
            string expr = string.Format(@"ck_player\({0},'(.*)'\)",nid);
            Regex reg = new Regex(expr);
            string html=GetPlayHtml(id,nid);
            Match m=reg.Match(html);
            if (m.Groups.Count==2)
            {
                return m.Groups[1].Value;
            }
            return null;
        }

        public string GetHtml(string id)
        {
            string url = string.Format("http://www.91taoke.com/Taocan/taocan_play/id/{0}",id);
            HttpItem item = new HttpItem()
            {
                URL = url,
                Method = "GET",
                IsToLower = false,   
                Timeout = 100000,   
                ReadWriteTimeout = 60000,  
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
                ContentType = "text/html",
                Allowautoredirect = false,
                ResultType = ResultType.String
            };

            HttpResult result = http.GetHtml(item);
            return result.Html;
        }
        public string GetPlayHtml(string id,string nid)
        {
            string url = string.Format("http://www.91taoke.com/index.php?m=Taocan&a=taocan_play&id={0}&nid={1}",id ,nid);
            HttpItem item = new HttpItem()
            {
                URL = url,
                Method = "GET",
                IsToLower = false,
                Timeout = 100000,
                ReadWriteTimeout = 60000,
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
                ContentType = "text/html",
                Allowautoredirect = false,
                ResultType = ResultType.String
            };

            HttpResult result = http.GetHtml(item);
            return result.Html;
        }
        //获取下载链接
        public string GetDownloadUrl(string code)
        {
            string url = string.Format("http://www.91taoke.com/index.php?m=Taocan&a=taocan_url&id={0}", code);
            HttpItem item = new HttpItem()
            {
                URL = url,
                Method = "GET",
                IsToLower = false,
                Timeout = 100000,
                ReadWriteTimeout = 60000,
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
                ContentType = "text/html",
                Allowautoredirect = false,
                ResultType = ResultType.String
            };

            HttpResult result = http.GetHtml(item);
            return result.Html;
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

        public void RunJob(object id)
        {
            try
            {
                string html = GetHtml(id.ToString());
                List<Taoke> nlist = GetTaokeNIDList(html);
                string taocan = GetTaokeName(html);
                //是否能并行
                foreach (Taoke item in nlist)
                {
                    string code = GetTaokeCode(id.ToString(), item.Nid);
                    //获取文件地址
                    string url = GetDownloadUrl(code);
                    //显示在界面上
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    data.Add("taocan", taocan);
                    data.Add("name", item.Name);
                    data.Add("nid", item.Nid);
                    data.Add("code", code);
                    data.Add("url", url);
                    string f_data = string.Format("{0},{1},{2},{3},{4}", taocan, item.Name, item.Nid, code, url);
                    WriteToFile(f_data);
                    AddListViewData(this.listView1, data);
                }
            }
            catch (Exception)
            {

            }
            
            //MessageBox.Show("完成!");
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            string[] url = ke_url.Text.Split('/');

            object id=(object)url[url.Length-1];
            Thread thr = new Thread(RunJob);
            thr.Start(id);
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            string html = "ck_player(6107,'vHNyNNWVkU');";
            string expr = string.Format(@"ck_player\({0},'(.*)'\)", 6107);
            Regex reg = new Regex(expr);
            Match m=reg.Match(html);
        }


        //从url获取课程id
        public List<string> GetCousreList(string url)
        {
            List<string> list = new List<string>();
            HttpItem item = new HttpItem()
            {
                URL = url,
                Method = "GET",
                IsToLower = false,
                Timeout = 100000,
                ReadWriteTimeout = 60000,
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
                ContentType = "text/html",
                Allowautoredirect = false,
                ResultType = ResultType.String
            };
            
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            Document doc = NSoupClient.Parse(html);
            try
            {
                Elements uls = doc.GetElementsByClass("xhr_wksp_left");
                if (uls.Count == 0)
                    return list;
                Element ul = uls[0];

                Elements lis = ul.GetElementsByTag("li");

                foreach (Element e in lis)
                {

                    Elements divs = e.GetElementsByClass("xhr_wksp_r_h");
                    if (divs.Count == 0)
                        continue;
                    Element a = divs[0].GetElementsByTag("a")[0];

                    string[] array = a.Attr("href").Split('/');
                    string courseid = array[array.Length - 1];
                    list.Add(courseid);


                }
            }
            catch (Exception)
            {
                return list;
            }
            return list;

        }
        //从文件读取url
        public List<string> GetCateUrlFromFile(string filename)
        {
            List<string> list = new List<string>();

            if (filename != "")
            {
                try
                {
                    FileStream file = new FileStream(filename, FileMode.Open);
                    StreamReader sr = new StreamReader(file);
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                    sr.Close();
                    file.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("打开文件错误!");
                }
            }

            return list;
        }
        public void FetchFromFile(object filepath)
        {
            //获取url
            List<string> url_list=GetCateUrlFromFile(filepath.ToString());
            foreach (string url in url_list)
            {
                FetchFromUrl(url);
            }

        }
        public void FetchFromUrl(object url)
        {
            List<string> id_list = GetCousreList(url.ToString());
            foreach (string item in id_list)
            {
                //单线程
                RunJob(item);
            }

        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            object url = (object)cate_url_txt.Text;
            Thread thr = new Thread(FetchFromUrl);
            thr.Start(url);
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                file_path.Text = openFileDialog1.FileName;
            }
        }

        private void load_file_Click(object sender, EventArgs e)
        {
            object path = (object)file_path.Text;
            Thread thr = new Thread(FetchFromFile);
            thr.Start(path);
        }

        public void WriteToFile(string line)
        {
            try
            {
                FileStream file = new FileStream("d://data.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(line);
                sw.Close();
                file.Close();
            }
            catch (Exception)
            {
            }
        }
        public void WriteToLog(string line)
        {
            try
            {
                FileStream file = new FileStream("d://log.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(line);
                sw.Close();
                file.Close();
            }
            catch (Exception)
            {
            }
        }

        public void LoadAllUrl()
        {

            Dictionary<string, string> combine = new Dictionary<string, string>();
            //学段
            string[] ids = new string[] { "3", "1063", "2075" };

            Queue<string> idqueue = new Queue<string>();

            for (int i = 0; i < ids.Length; i++)
            {
                idqueue.Enqueue(ids[i]);
                combine.Add(ids[i], ids[i]);
            }
            //end for

            while (idqueue.Count != 0)
            {
                string nowid = idqueue.Dequeue();
                string url = string.Format("http://www.91taoke.com/Taocan/taocan_list/id/{0}", nowid);
                List<string> list = GetCousreList(url);
                HttpItem item = new HttpItem()
                {
                    URL = url,
                    Method = "GET",
                    IsToLower = false,
                    Timeout = 100000,
                    ReadWriteTimeout = 60000,
                    UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",
                    ContentType = "text/html",
                    Allowautoredirect = false,
                    ResultType = ResultType.String
                };

                HttpResult result = http.GetHtml(item);

                Document doc = NSoupClient.Parse(result.Html);
                Elements ems = doc.Body.GetElementsByAttributeValue("class", "menuList cf");

                Elements xueduanlinks = ems[0].GetElementsByTag("a");

                List<string> xueduan = new List<string>();

                foreach (Element e in xueduanlinks)
                {
                    string href = e.Attr("href").ToString();
                    xueduan.Add(href.Substring(href.IndexOf(',') + 1, href.Length - href.IndexOf(',') - 2));
                }

                Elements nianjilinks = ems[1].GetElementsByTag("a");

                List<string> nianji = new List<string>();

                foreach (Element e in nianjilinks)
                {
                    string href = e.Attr("href").ToString();
                    nianji.Add(href.Substring(href.IndexOf(',') + 1, href.Length - href.IndexOf(',') - 2));
                }


                Elements xuekelinks = ems[2].GetElementsByTag("a");
                List<string> xueke = new List<string>();

                foreach (Element e in xuekelinks)
                {
                    string href = e.Attr("href").ToString();
                    xueke.Add(href.Substring(href.IndexOf(',') + 1, href.Length - href.IndexOf(',') - 2));
                }


                Elements versionlinks = ems[3].GetElementsByTag("a");
                List<string> version = new List<string>();

                foreach (Element e in versionlinks)
                {
                    string href = e.Attr("href").ToString();
                    version.Add(href.Substring(href.IndexOf(',') + 1, href.Length - href.IndexOf(',') - 2));
                }

                if (nowid.Split(',').Length == 1)
                {
                    for (int j = 0; j < nianji.Count; j++)
                    {
                        if (!combine.ContainsKey(nowid + "," + nianji[j]))
                        {
                            combine.Add(nowid + "," + nianji[j], nowid + "," + nianji[j]);
                            idqueue.Enqueue(nowid + "," + nianji[j]);
                        }
                        for (int k = 0; k < xueke.Count; k++)
                        {
                            if (!combine.ContainsKey(nowid + "," + nianji[j] + "," + xueke[k]))
                            {
                                combine.Add(nowid + "," + nianji[j] + "," + xueke[k], nowid + "," + nianji[j] + "," + xueke[k]);
                                idqueue.Enqueue(nowid + "," + nianji[j] + "," + xueke[k]);
                            }

                            for (int l = 0; l < version.Count; l++)
                            {
                                if (!combine.ContainsKey(nowid + "," + nianji[j] + "," + xueke[k] + "," + version[l]))
                                {
                                    combine.Add(nowid + "," + nianji[j] + "," + xueke[k] + "," + version[l], nowid + "," + nianji[j] + "," + xueke[k] + "," + version[l]);
                                    idqueue.Enqueue(nowid + "," + nianji[j] + "," + xueke[k] + "," + version[l]);
                                }
                            }
                        }
                    }
                }
                if (nowid.Split(',').Length == 2)
                {
                    for (int k = 0; k < xueke.Count; k++)
                    {
                        if (!combine.ContainsKey(nowid + "," + xueke[k]))
                        {
                            combine.Add(nowid + "," + xueke[k], nowid + "," + "," + xueke[k]);
                            idqueue.Enqueue(nowid + "," + xueke[k]);
                        }

                        for (int l = 0; l < version.Count; l++)
                        {
                            if (!combine.ContainsKey(nowid + "," + xueke[k] + "," + version[l]))
                            {
                                combine.Add(nowid + "," + xueke[k] + "," + version[l], nowid + "," + xueke[k] + "," + version[l]);
                                idqueue.Enqueue(nowid + "," + xueke[k] + "," + version[l]);
                            }
                        }
                    }
                }
                if (nowid.Split(',').Length == 3)
                {
                    for (int l = 0; l < version.Count; l++)
                    {
                        if (!combine.ContainsKey(nowid + "," + version[l]))
                        {
                            combine.Add(nowid + "," + version[l], nowid + "," + version[l]);
                            idqueue.Enqueue(nowid + "," + version[l]);
                        }
                    }
                }

                //for (int i = 0; i < xueduan.Count; i++)
                //{
                //    if (!combine.ContainsKey(xueduan[i]))
                //    {
                //        combine.Add(xueduan[i], xueduan[i]);
                //        idqueue.Enqueue(xueduan[i]);
                //    }
                //    for (int j = 0; j < nianji.Count; j++)
                //    {
                //        if (!combine.ContainsKey(xueduan[i] + "," + nianji[j]))
                //        {
                //            combine.Add(xueduan[i] + "," + nianji[j], xueduan[i] + "," + nianji[j]);
                //            idqueue.Enqueue(xueduan[i] + "," + nianji[j]);
                //        }
                //        for (int k = 0; k < xueke.Count; k++)
                //        {
                //            if (!combine.ContainsKey(xueduan[i] + "," + nianji[j] + "," + xueke[k]))
                //            {
                //                combine.Add(xueduan[i] + "," + nianji[j] + "," + xueke[k], xueduan[i] + "," + nianji[j] + "," + xueke[k]);
                //                idqueue.Enqueue(xueduan[i] + "," + nianji[j] + "," + xueke[k]);
                //            }

                //            for (int l = 0; l < version.Count; l++)
                //            {
                //                if (!combine.ContainsKey(xueduan[i] + "," + nianji[j] + "," + xueke[k] + "," + version[l]))
                //                {
                //                    combine.Add(xueduan[i] + "," + nianji[j] + "," + xueke[k] + "," + version[l], xueduan[i] + "," + nianji[j] + "," + xueke[k] + "," + version[l]);
                //                    idqueue.Enqueue(xueduan[i] + "," + nianji[j] + "," + xueke[k] + "," + version[l]);
                //                }
                //            }
                //        }
                //    }
                //}

                WriteToLog(idqueue.Count + "----" + combine.Count + "----" + nowid + "----" + url);

            }

            
            foreach (var item in combine)
            {
                try
                {
                    FileStream file = new FileStream("d://url.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(file);
                    sw.WriteLine(string.Format("http://www.91taoke.com/Taocan/taocan_list/id/{0}", item.Value));
                    sw.Close();
                    file.Close();
                }
                catch (Exception)
                {
                }
                
            }
            
            
        }


    }



}
