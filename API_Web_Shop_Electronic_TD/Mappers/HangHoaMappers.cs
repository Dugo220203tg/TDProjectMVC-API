using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Models;

namespace API_Web_Shop_Electronic_TD.Mappers
{
	public static class HangHoaMappers
	{
		public static HangHoaMD ToHangHoaDo(this HangHoa Model)
		{
			return new HangHoaMD
			{
			MaHH = Model.MaHh,
			TenHH = Model.TenHh,
			Hinh = Model.Hinh,
			MoTa = Model.MoTa,
			MoTaDonVi = Model.MoTaDonVi,
			MaLoai = Model.MaLoai,
			NgaySX = Model.NgaySx,
			GiamGia = Model.GiamGia,
			MaNCC = Model.MaNcc,
			DonGia = (double)Model.DonGia,
			SoLanXem = (double)Model.SoLanXem
			};
		}
		public static AllHangHoaMD ToAllHangHoaDo(this HangHoa Model)
		{
			if (Model == null)
			{
				// Handle the case where Model is null
				return null; // Or throw an exception, depending on your requirements
			}

			string maLoai = Model.MaLoaiNavigation != null ? Model.MaLoaiNavigation.TenLoai : null;
			string maNcc = Model.MaNccNavigation != null ? Model.MaNccNavigation.TenCongTy : null;

			return new AllHangHoaMD
			{
				MaHH = Model.MaHh,
				TenHH = Model.TenHh,
				Hinh = Model.Hinh,
				MoTa = Model.MoTa,
				MoTaDonVi = Model.MoTaDonVi,
				MaLoai = maLoai,
				NgaySX = Model.NgaySx,
				GiamGia = Model.GiamGia,
				MaNCC = maNcc,
				DonGia = (double)Model.DonGia,
				SoLanXem = (double)Model.SoLanXem
			};
		}

		public static HangHoa ToHangHoaCreateDTO(this HangHoaMD Model)
		{

			return new HangHoa
			{
				TenHh = Model.TenHH,
				Hinh = Model.Hinh,
				MoTa = Model.MoTa,
				MoTaDonVi = Model.MoTaDonVi,
				MaLoai = (int)Model.MaLoai,
				NgaySx = (DateOnly)Model.NgaySX,
				GiamGia = (double)Model.GiamGia,
				MaNcc = Model.MaNCC,
				DonGia = Model.DonGia
			};
		}



	}
}
