using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
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
        public void Load_Data()
        {
            db.moKN();
            string query = "select Thoigian, Mota, Sotien from TK_ThuNhap";
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
            string query = $"select Thoigian, Mota, Sotien from TK_ThuNhap where Thoigian between '{TuNgay}' and '{DenNgay}'";
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

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintHoaDon);
            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDocument
            };

            if (previewDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void PrintHoaDon(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontTitle = new Font("Arial", 16, FontStyle.Bold);
            Font fontNormal = new Font("Arial", 12);
            int startX = 50, startY = 50, lineHeight = 25;

            // In tiêu đề hóa đơn
            g.DrawString("HÓA ĐƠN THỐNG KÊ THU NHẬP", fontTitle, Brushes.Black, startX, startY);
            startY += lineHeight * 2;

            // In khoảng thời gian
            g.DrawString($"Từ ngày: {dtpTuNgay.Value:dd/MM/yyyy}  Đến ngày: {dtpDenNgay.Value:dd/MM/yyyy}", fontNormal, Brushes.Black, startX, startY);
            startY += lineHeight * 2;

            // In tiêu đề bảng (Căn chỉnh cột hợp lý hơn)
            g.DrawString("Thời gian", fontNormal, Brushes.Black, startX, startY);
            g.DrawString("Mô tả", fontNormal, Brushes.Black, startX + 200, startY);
            g.DrawString("Số tiền", fontNormal, Brushes.Black, startX + 450, startY);
            startY += lineHeight;

            // In dữ liệu từ DataGridView
            foreach (DataGridViewRow row in dgvThuNhap.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    // Lấy ngày thay vì ngày giờ
                    string ngay = Convert.ToDateTime(row.Cells[0].Value).ToString("dd/MM/yyyy");
                    string moTa = row.Cells[1].Value.ToString();
                    string soTien = Convert.ToInt32(row.Cells[2].Value).ToString("N0") + " đ"; // Định dạng số tiền

                    // In từng cột với khoảng cách hợp lý
                    g.DrawString(ngay, fontNormal, Brushes.Black, startX, startY);
                    g.DrawString(moTa, fontNormal, Brushes.Black, startX + 200, startY);
                    g.DrawString(soTien, fontNormal, Brushes.Black, startX + 450, startY);

                    startY += lineHeight;
                }
            }

            // In tổng thu
            startY += lineHeight;
            g.DrawString($"Tổng thu: {txtTongThu.Text}", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, startX, startY);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Load_Data();
        }
    }
}