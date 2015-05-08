using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication120
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("rtfText");

            dt.Rows.Add(new string[] { @"{\rtf1 Test \par Test}" });
            dt.Rows.Add(new string[] { @"{\rtf1 Test2 \par Test2}" });
            dt.Rows.Add(new string[] { @"{\rtf1 Test3 \par Test3}" });

            bindingSource1.DataSource = dt;

            myTextControl1.DataBindings.Add(new Binding("Rtf", bindingSource1, "rtfText", true));
        }
    }

    public class MyTextControl : TXTextControl.TextControl
    {
        public MyTextControl()
        {
            this.CreateControl();
        }

        public string Rtf
        {
            get
            {
                string data;
                this.Save(out data, TXTextControl.StringStreamType.RichTextFormat);
                return data;
            }
            set
            {
                if (value != "")
                    this.Load(value, TXTextControl.StringStreamType.RichTextFormat);
            }
        }
    }
 
}
