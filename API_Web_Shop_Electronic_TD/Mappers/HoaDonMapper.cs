using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Models;

namespace API_Web_Shop_Electronic_TD.Mappers
{
	public static class HoaDonMapper
	{
		public static HoaDonMD ToHoaDonDo(this HoaDon Model)
		{
			{
				var hoaDonMD = new HoaDonMD
				{
					MaHD = Model.MaHd,
					MaKH = Model.MaKh,
					HoTen = Model.HoTen,
					NgayDat = Model.NgayDat,
					DiaChi = Model.DiaChi,
					CachThanhToan = Model.CachThanhToan,
					DienThoai = Model.DienThoai,
					GhiChu = Model.GhiChu,
					MaTrangThai = Model.MaTrangThai,
					CachVanChuyen = Model.CachVanChuyen,
					PhiVanChuyen = (float)Model.PhiVanChuyen
				};

				// Kiểm tra nếu MaTrangThaiNavigation không null thì gán TrangThai
				if (Model.MaTrangThaiNavigation != null)
				{
					hoaDonMD.TrangThai = Model.MaTrangThaiNavigation.TenTrangThai;
				}

				return hoaDonMD;
			}
		}
		public static HoaDon ToHoaDonDTO(this HoaDonMD Model)
		{

			return new HoaDon
			{
				MaHd = Model.MaHD,
				MaKh = Model.MaKH,
				NgayDat = Model.NgayDat,
				HoTen = Model.HoTen,
				DiaChi = Model.DiaChi,
				CachThanhToan = Model.CachThanhToan,
				DienThoai = Model.DienThoai,
				GhiChu = Model.GhiChu,
				MaTrangThai = Model.MaTrangThai,
				CachVanChuyen = Model.CachVanChuyen,
				PhiVanChuyen = (float)Model.PhiVanChuyen
			};
		}
	}
}
