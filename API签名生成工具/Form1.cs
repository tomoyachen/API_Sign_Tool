using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace API签名生成工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.Text = "API签名生成工具";
            init();
            //为实例化Form1赋值
            form1 = this;

        }

        //实例化Form1
        public static Form1 form1;

        private void init() {
            textBox3.Text = "${key}";
            textBox3.Text = GetConfigValue("key_defalut", "${key}");
            checkBox1.CheckState = CheckState.Unchecked;
            checkBox2.CheckState = CheckState.Unchecked;
            checkBox3.CheckState = CheckState.Checked;
            if (GetConfigValue("urlencode_checkBox", "false") == "true")
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            if (GetConfigValue("toupper_checkBox", "false") == "true")
            {
                checkBox2.CheckState = CheckState.Checked;
            }
            if (GetConfigValue("sort_checkBox", "true") == "false")
            {
                checkBox3.CheckState = CheckState.Unchecked;
            }
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] arr2 = splitTxt(textBox1.Text);
            String tmp = null;
            for (int i = 0; i < arr2.Length; i++) {
                //Console.WriteLine("-->" + arr2[i] + "<--");
                //tmp += arr2[i];
                if (arr2[i] != "&" && arr2[i] != "{&" && arr2[i] != "}&") {
                    tmp += arr2[i];
                }
            }
            /*
            //清除多余空行导致的&符号
            while (tmp.Length != 0 && tmp.Substring(0, 1) == "&") {
                tmp = tmp.Substring(1, tmp.Length -1);
            }
            //清除多余空行导致的&符号
            while (tmp.Length != 0 && tmp.Contains("&&")) {
                tmp = tmp.Trim().Replace("&&", "&");
            }
            */
            if (tmp != null && tmp != "" && tmp.Length != 0 && tmp.Substring(tmp.Length - 1, 1) == "&")
            {
                tmp = tmp.Substring(0, tmp.Length - 1);
                //加上key值
                tmp += textBox3.Text;
            }
            else {
                tmp = "请在上侧文本框粘入字段列，点击[生成]按钮！";
            }

            //tmp += textBox3.Text;
            //输出到文本框
            textBox2.Text = tmp;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }


        private String[] splitTxt(String Str)
        {
            String[] arr = Str.Split(Environment.NewLine.ToCharArray());
            String[] arr2;
            if (arr.Length % 2 != 0)
            {
                arr2 = new String[arr.Length / 2 + 1];
            }
            else {
                arr2 = new String[arr.Length / 2];
            }

            for (int i = 0; i < arr2.Length; i++)
            {
                //Console.WriteLine("arr.length: " + arr.Length);
                //Console.WriteLine("arr2.length: " + arr2.Length);
                arr2[i] = arr[i*2].Trim().Replace("\"", "").Replace(":", "=");
                if (arr2[i].Length != 0 && arr2[i].Substring(arr2[i].Length - 1, 1) == ",")
                {
                    arr2[i] = arr2[i].Substring(0, arr2[i].Length - 1) + "&";
                }else {
                    arr2[i] = arr2[i] + "&";
                }

                if (checkBox2.CheckState == CheckState.Checked)
                {
                    if (arr2[i].Contains("="))
                    {
                        string[] tmpArray = arr2[i].Split('=');
                        arr2[i] = tmpArray[0].ToUpper() + "=" + tmpArray[1];
                    }
                    
                }
                if (checkBox1.CheckState == CheckState.Checked) {
                    //urlencode暂行办法。本来是直接替换${}格式。现在对明文也进行urlencode标识。
                    //arr2[i] = arr2[i].Replace("${", "${__urlencode(${").Replace("}", "})}");
                    if (arr2[i].Contains("="))
                    {
                        string[] tmpArray = arr2[i].Split('=');
                        if (tmpArray[1].Length != 0 && tmpArray[1].Substring(tmpArray[1].Length - 1, 1) == "&")
                        {
                            arr2[i] = tmpArray[0] + "=" + "${__urlencode(" + tmpArray[1].Substring(0, tmpArray[1].Length - 1) + ")}" + "&";
                        }
                        else
                        {
                            arr2[i] = tmpArray[0] + "=" + "${__urlencode(" + tmpArray[1] + ")}";
                        }
                    }
                    
                }
                }
            if (checkBox3.CheckState == CheckState.Checked)
            {
                Array.Sort(arr2);
            }
            return arr2;
        }

 
        /// 读取指定key的值
        public static string GetConfigValue(string key, string default_value)
        {
            if (System.Configuration.ConfigurationSettings.AppSettings[key] == null)
                return default_value;
            else
                return System.Configuration.ConfigurationSettings.AppSettings[key].ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 about = new Form2();
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog();
        }

        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            init();

        }
        public static Form3 form3;
        private void 参数化模版ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (form3 == null || form3.IsDisposed)
            {
                form3 = new Form3();
                form3.StartPosition = FormStartPosition.CenterScreen;
                form3.Show();
                //另一种Form3调用Form1的方式
                //form3.Owner = this;
            }
            else
            {
                form3.Activate();
                form3.WindowState = FormWindowState.Normal;
            }
            

            //Form3 canshuhua = new Form3();
            //父窗口可以操作
            //canshuhua.StartPosition = FormStartPosition.CenterScreen;
            //canshuhua.Show();
            //对话框，父窗体无法操作
            //canshuhua.StartPosition = FormStartPosition.CenterParent;
            //canshuhua.ShowDialog();
            //为了Form3中能使用Form1 form1 = (Form1)this.Owner;
            //canshuhua.Owner = this;
            
        }

        //实验室
        public void textBox1ByForm1(String str) {
            textBox1.Text = str;
        }

        private static string GetMD5(string myString)
        {   
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }

            return byte2String;
            

        }

        private void 生成MD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String md5Str = GetMD5(textBox2.Text);
            
            Form4 md5Form = new Form4(md5Str);
            md5Form.StartPosition = FormStartPosition.CenterParent;
            md5Form.ShowDialog();

        }
    }
}
