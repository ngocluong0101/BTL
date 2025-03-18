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

namespace BTL_QuanLyQuanNet.THONG_KE
{
    public partial class ThongKe: Form
    {
        ConnectDatabase db = new ConnectDatabase();
        public ThongKe()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        private void Load_Data()
        {
            db.moKN();
            string query = "select * from TK_ThuNhap";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dgvThuNhap.DataSource = dt;
            db.dongKN();
        }
        private void Load_Data_TheoNgay()
        {
            string TuNgay = dtpTuNgay.Value.ToString("yyyy/MM/dd");
            string DenNgay = dtpDenNgay.Value.ToString("yyyy/MM/dd");
            db.moKN();
            string query = $"select * from TK_ThuNhap where Thoigian between '{TuNgay}' and '{DenNgay}'";
            SqlDataAdapter adt = new SqlDataAdapter(query, db.GetConnection());
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dgvThuNhap.DataSource = dt;
            db.dongKN();
            TongThu();
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            Load_Data_TheoNgay();
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            Load_Data_TheoNgay();
        }
        private void TongThu()
        {
            int res = 0;
            foreach (DataGridViewRow row in dgvThuNhap.Rows)
            {
                if (row.Cells["column3"].Value != null)
                {
                    res += Convert.ToInt32(row.Cells["column3"].Value);
                }
            }
            txtTongThu.Text = res.ToString("N0") + " đ";
        }
    }
}