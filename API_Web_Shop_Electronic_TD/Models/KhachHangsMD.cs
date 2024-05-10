using System.ComponentModel.DataAnnotations;

namespace API_Web_Shop_Electronic_TD.Models
{
	public class KhachHangsMD
	{
		[Display(Name = "User Name")]
		[Required(ErrorMessage = "*")]
		[MaxLength(20, ErrorMessage = "Max 20 keys")]
		public string UserName { get; set; }
		[Display(Name = "Password")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public int Vaitro { get; set; }
		public string Email { get; set; }
		public string RandomKey { get; set; }

	}
	public class DangNhapMD
	{
		[Display(Name = "User Name")]
		[Required(ErrorMessage = "*")]
		[MaxLength(20, ErrorMessage = "Max 20 keys")]
		public string UserName { get; set; }
		[Display(Name = "Password")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public int Vaitro { get; set; }

	}
	public class DangKyMD
	{
		[Display(Name = "User Name")]
		[Required(ErrorMessage = "*")]
		[MaxLength(20, ErrorMessage = "Max 20 keys")]
		public string UserName { get; set; }
		[Display(Name = "Password")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public int Vaitro { get; set; }
		public string Email { get; set; }
		public bool HieuLuc { get; set; }
		public string Hoten { get; set; }

	}
	public class AdminDkMD
	{
		[Display(Name = "User Name")]
		[Required(ErrorMessage = "*")]
		[MaxLength(20, ErrorMessage = "Max 20 keys")]
		public string UserName { get; set; }
		[Display(Name = "Password")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public string Email { get; set; }

	}
	public class UpdateKH
	{
		
		[Display(Name = "Password")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
