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
    public partial class ThemTaiKhoan: Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public ThemTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnTao_Click(object sender, EventArgs e)
        {   
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNhapLaiMatKhau.Text != txtMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu không giống nhau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            db.moKN();
            string query = "insert into KHACHHANG (Taikhoan, Matkhau) values ('" + txtTaiKhoan.Text + "', '" + txtMatKhau.Text + "')";
            string lichsu = "insert into LICHSU (Taikhoan, Mota) values ('" + txtTaiKhoan.Text + "', N'Đã được tạo')";
            SqlCommand cmd = new SqlCommand(query, db.GetConnection());
            SqlCommand cmdLichSu = new SqlCommand(lichsu, db.GetConnection());




            cmd.ExecuteNonQuery();
            cmdLichSu.ExecuteNonQuery();
            db.dongKN();
            MessageBox.Show("Tạo tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void ThemTaiKhoan_Load(object sender, EventArgs e)
        {

        }
    }
}
