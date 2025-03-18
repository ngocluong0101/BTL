using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QuanLyQuanNet.Quan_ly_may_tram
{
    public partial class QuanLyThoiGian: Form
    {
        public QuanLyThoiGian()
        {
            InitializeComponent();
            this.Load += QuanLyThoiGian_Load;
        }
        private void QuanLyThoiGian_Load(object sender, EventArgs e)
        {
            List<MayTinh> danhSach = new List<MayTinh>
{
    new MayTinh { May = "Máy 01", TenDangNhap = "khanhlinh123", Loai = "Tiêu chuẩn", ThoiGianConLai = "01:13", SoDu = "10.000 VND" },
    new MayTinh { May = "Máy 13", TenDangNhap = "admin", Loai = "Gaming", ThoiGianConLai = "03:47", SoDu = "45.000 VND" },
    new MayTinh { May = "Máy 09", TenDangNhap = "player01", Loai = "Chuyên nghiệp", ThoiGianConLai = "02:20", SoDu = "35.000 VND" },
    new MayTinh { May = "Máy 08", TenDangNhap = "guest01", Loai = "Gaming", ThoiGianConLai = "02:34", SoDu = "31.000 VND" },
    new MayTinh { May = "Máy 13", TenDangNhap = "proplayer", Loai = "Thi đấu", ThoiGianConLai = "01:50", SoDu = "33.000 VND" },
    new MayTinh { May = "Máy 06", TenDangNhap = "noob", Loai = "Tiêu chuẩn", ThoiGianConLai = "00:45", SoDu = "6.000 VND" },
    new MayTinh { May = "Máy 11", TenDangNhap = "streamerX", Loai = "Chuyên nghiệp", ThoiGianConLai = "04:10", SoDu = "62.000 VND" },
    new MayTinh { May = "Máy 08", TenDangNhap = "gamer123", Loai = "Gaming", ThoiGianConLai = "02:05", SoDu = "25.000 VND" },
    new MayTinh { May = "Máy 10", TenDangNhap = "hardcore", Loai = "Thi đấu", ThoiGianConLai = "03:30", SoDu = "63.000 VND" },
    new MayTinh { May = "Máy 09", TenDangNhap = "casual", Loai = "Tiêu chuẩn", ThoiGianConLai = "01:00", SoDu = "8.000 VND" },
    new MayTinh { May = "Máy 11", TenDangNhap = "tryhard", Loai = "Gaming", ThoiGianConLai = "01:25", SoDu = "17.000 VND" },
    new MayTinh { May = "Máy 17", TenDangNhap = "bigboss", Loai = "Chuyên nghiệp", ThoiGianConLai = "00:40", SoDu = "10.000 VND" },
    new MayTinh { May = "Máy 15", TenDangNhap = "elite", Loai = "Thi đấu", ThoiGianConLai = "02:45", SoDu = "49.000 VND" },
    new MayTinh { May = "Máy 02", TenDangNhap = "silver", Loai = "Gaming", ThoiGianConLai = "01:10", SoDu = "14.000 VND" },
    new MayTinh { May = "Máy 13", TenDangNhap = "newbie", Loai = "Tiêu chuẩn", ThoiGianConLai = "00:30", SoDu = "4.000 VND" }
};


            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = danhSach;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
    }


public class MayTinh
{
    public string May { get; set; }
    public string TenDangNhap { get; set; }
    public string Loai { get; set; }
    public string ThoiGianConLai { get; set; }
    public string SoDu { get; set; }
}

}
