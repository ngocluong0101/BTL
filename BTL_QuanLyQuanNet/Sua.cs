using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyQuanNet
{
    public partial class Sua: Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public Sua()
        {
            InitializeComponent();
        }

        private void txtTenDangNhap_Leave(object sender, EventArgs e)
        {
            db.moKN();
            string queryCheck = "select count(*) from KHACHHANG where Taikhoan = '" + txtTenDangNhap.Text + "'";
            SqlCommand cmdCheck = new SqlCommand(queryCheck, db.GetConnection());
            int res = (int)cmdCheck.ExecuteScalar();
            if (res == 0)
            {
                MessageBox.Show("Tài khoản không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Text = "";
                txtTenDangNhap.Focus();
            }
            db.dongKN();
        }

        private void txtMatKhauCu_Leave(object sender, EventArgs e)
        {
            db.moKN();
            string query = "select Matkhau from KHACHHANG where Taikhoan = '" + txtTenDangNhap.Text + "'";
            SqlCommand cmd = new SqlCommand(query, db.GetConnection());
            object res = cmd.ExecuteScalar();
            if (res != null)
            {
                if (res.ToString().Trim() != txtMatKhauCu.Text) 
                {
                    MessageBox.Show("Mật khẩu sai. Vui lòng nhập lại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhauCu.Text = "";
                    txtMatKhauCu.Focus();
                }
            }
            db.dongKN();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtMatKhauMoi.Text != txtNhapLaiMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu không giống nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            db.moKN();
            string query = "update KHACHHANG set Matkhau = '" + txtMatKhauMoi.Text + "' where Taikhoan = '" + txtTenDangNhap.Text + "'";
            string LichSu = "insert into LICHSU (Taikhoan, Mota) values ('" + txtTenDangNhap.Text + "', N'Đã cập nhật mật khẩu')"; 
            SqlCommand cmd = new SqlCommand(query, db.GetConnection());
            SqlCommand cmdLichSu = new SqlCommand(LichSu, db.GetConnection());
            cmd.ExecuteNonQuery();
            cmdLichSu.ExecuteNonQuery();
            db.dongKN();
            MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
