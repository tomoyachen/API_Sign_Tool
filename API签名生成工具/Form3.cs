using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace API签名生成工具
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.Text = "参数化模版";
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] arr2 = splitTxt(textBox1.Text);
            String tmp = null;
            int hopeLen = 0;
            for (int i = 0; i < arr2.Length; i++)
            {
                if (arr2[i] != null && arr2[i] != "" && arr2[i].Length != 0)
                {
                    hopeLen++;
                }

            }
            int actualLen = 0;
            for (int i = 0; i < arr2.Length; i++)
            {
                if (arr2[i] != null && arr2[i] != "" && arr2[i].Length != 0) {
                    String a = JSONTxt(arr2[i]);
                    actualLen++;
                    if (actualLen < hopeLen)
                    {
                        a += ",";
                    }
                    //tmp += "\r\n";
                    tmp = tmp + "\t" + a + "\r\n";
                }
                    
            }

            Boolean isEmpty = true;
            if (tmp != null && tmp != "" && tmp.Length != 0)
            {
                tmp = "{\r\n" + tmp + "\t}";
            }
            else {
                tmp = "请在左侧文本框粘入字段列，点击[→]按钮！";
                isEmpty = false;
            }

            //输出到文本框
            textBox2.Text = tmp;

            //实验室
            if (isEmpty) {
                //另一种Form3调用Form1的方式
                //Form1 form1 = (Form1)this.Owner;
                //form1.textBox1ByForm1(textBox2.Text);
                Form1.form1.textBox1ByForm1(textBox2.Text);
            }
            





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
            else
            {
                arr2 = new String[arr.Length / 2];
            }
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr[i * 2].Trim().Replace("\"", "").Replace("\'", "");
            }
            return arr2;
        }

        private String JSONTxt(String Str)
        {
            String JSON = null;
            JSON = "\"" + Str + "\"" + ":" + "\"" + "${" + Str + "}" + "\"";
            return JSON;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
