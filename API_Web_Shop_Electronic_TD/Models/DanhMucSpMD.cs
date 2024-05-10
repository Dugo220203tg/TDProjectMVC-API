using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Web_Shop_Electronic_TD.Models
{
	public class DanhMucSpMD
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MaLoai { get; set; }
		public string? TenLoai { get; set; }
		public string Hinh { get; set; }
		public string Mota { get; set; }
	}
	public class HangSpMD
	{
		[Key]
		public string MaNCC { get; set; }
		public string? TenCongTy { get; set; }
		public string Mota { get; set; }
		public string DiaChi { get; set; }
		public string Email { get; set; }
		public string NguoiLienLac { get; set; }
		public string DienThoai { get; set; }
		public string Logo { get; set; }

	}
}
