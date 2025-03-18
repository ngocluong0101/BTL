using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace layout
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Dang nhap thanh cong!", "Thong bao");
            Form2 form2 = new Form2();
            form2.ShowDialog();
            this.Close();
        }

        private void chkHideShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHideShowPass.Checked) txtPassword.UseSystemPasswordChar = false;
            else txtPassword.UseSystemPasswordChar = true;
        }


    }
}
