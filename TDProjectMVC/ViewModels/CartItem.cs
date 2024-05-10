namespace TDProjectMVC.ViewModels
{
	public class CartItem
	{
		public int MaHH { get; set; }
		public string TenHH { get; set; }
		public string Hinh { get; set; }
		public double DonGia { get; set; }
		public double MaH { get; set; }
		public int SoLuong { get; set; }
		public double ThanhTien => SoLuong * DonGia;
	}
	public class CartMini
	{
		public int MaHH { get; set; }
		public string TenHH { get; set; }
		public string Hinh { get; set; }
		public double DonGia { get; set; }
		public double MaH { get; set; }
		public int SoLuong { get; set; }
		public double ThanhTien => SoLuong * DonGia;
	}
}
