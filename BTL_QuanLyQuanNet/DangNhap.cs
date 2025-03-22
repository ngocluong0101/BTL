using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTL_QuanLyQuanNet.Dich_Vu;

namespace BTL_QuanLyQuanNet
{
    public partial class DangNhap: Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = "admin";
            string password = "admin";

            string inputusername = txtAccount.Text;
            string inputpassword = txtPassword.Text;

            if (inputusername == username && inputpassword == password)
            {
                DialogResult res = MessageBox.Show("Dang nhap thanh cong!", "Thong bao");
                Main formMain = new Main();
                formMain.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu, vui lòng nhập lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtAccount.Focus();
            }
        }

        private void chkHideShowPass_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkHideShowPass.Checked) txtPassword.UseSystemPasswordChar = false;
            else txtPassword.UseSystemPasswordChar = true;
        }
    }
}
