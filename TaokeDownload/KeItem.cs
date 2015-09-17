using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaokeDownload
{
    public class KeItem
    {
        public string Cate;
        public string Name;
        public string Url;
        public string fileName;
        public KeItem(string cate,string name,string url)
        {
            Cate = cate;
            Name = name;
            Url = url;

            fileName = Name.Replace(" ","");
            string ext = Url.Substring(Url.LastIndexOf(".") + 1, (Url.Length - Url.LastIndexOf(".") - 1));   //扩展名
            fileName += "."+ext;
        }

    }
}
