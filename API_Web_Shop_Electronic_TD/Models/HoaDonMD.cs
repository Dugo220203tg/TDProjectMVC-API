using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Web_Shop_Electronic_TD.Models
{
	public class HoaDonMD
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
		public string MaNV { get; set; }
		public string GhiChu { get; set; }
		public string DienThoai { get; set; }
		public string TrangThai {  get; set; }
	}
}
