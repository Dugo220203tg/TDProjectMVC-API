using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrangQuanLy.Models
{
    public class HoaDonViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHD { get; set; }
        public string? MaKH { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string CachThanhToan { get; set; }
        public string CachVanChuyen { get; set; }
        public float PhiVanChuyen { get; set; }
        public int MaTrangThai { get; set; }
        public string trangThai { get; set; }
        public string MaNV { get; set; }
        public string GhiChu { get; set; }
        public string DienThoai { get; set; }
    }
}
