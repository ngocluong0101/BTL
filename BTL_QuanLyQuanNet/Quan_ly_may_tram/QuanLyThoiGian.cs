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

namespace BTL_QuanLyQuanNet.Quan_ly_may_tram
{
    public partial class QuanLyThoiGian: Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public QuanLyThoiGian()
        {
            InitializeComponent();
        }
        public void Load_Data()
        {
            db.moKN();
            string query = "select QLTG.Somay, QLTG.Taikhoan, QLTG.Loai, QLTG.Thoigianconlai, KH.Sodu as Sodu " 
                            + " from QuanLyThoiGian QLTG, KHACHHANG KH "
                            + " where QLTG.Taikhoan = KH.Taikhoan";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
            db.dongKN();
        }


        private void QuanLyThoiGian_Load_1(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void btnTatMay_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một máy để tắt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string somay = dataGridView1.SelectedRows[0].Cells["Somay"].Value.ToString();
            db.moKN();
            string query = "delete from QuanLyThoiGian where Somay = '" + somay + "'";
            SqlCommand cmd = new SqlCommand(query, db.GetConnection());
            cmd.ExecuteNonQuery();
            db.dongKN();
            Load_Data();
        }

        private void btnNapTien_Click(object sender, EventArgs e)
        {
            NapTien formNap = new NapTien();
            formNap.ShowDialog();
            Load_Data();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
