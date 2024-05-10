using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Models;

namespace API_Web_Shop_Electronic_TD.Mappers
{
	public static class LoaiSpMapper
	{
		public static DanhMucSpMD ToLoaiDo(this Loai Model)
		{
			return new DanhMucSpMD
			{
				TenLoai = Model.TenLoai,
				MaLoai = Model.MaLoai,
				Hinh = Model.Hinh,
				Mota = Model.MoTa,
			};
		}
		public static Loai ToLoaiDTO(this DanhMucSpMD Model)
		{

			return new Loai
			{
				TenLoai = Model.TenLoai,
				MaLoai = (int)Model.MaLoai,
				Hinh = Model.Hinh,
				MoTa = Model.Mota,
			};
		}
	}
}
