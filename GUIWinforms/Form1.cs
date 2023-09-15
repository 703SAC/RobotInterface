using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIWinforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        private void btn_URRun_Click(object sender, EventArgs e)
        {
            int[] shape = { cmb_Product1.SelectedIndex, cmb_Product2.SelectedIndex };

            int[] pickCounts = { Convert.ToInt32(txt_PickCount1.Text), Convert.ToInt32(txt_PickCount2.Text) };
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

    }
}
