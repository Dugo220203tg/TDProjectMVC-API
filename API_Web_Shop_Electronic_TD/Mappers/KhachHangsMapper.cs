using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Models;
using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_Web_Shop_Electronic_TD.Mappers
{
	public static class KhachHangsMapper
	{	

		public static KhachHangsMD ToKhachHangDo(this KhachHang Model)
		{
			return new KhachHangsMD
			{
				UserName = Model.MaKh,
				Password = Model.MatKhau,
				Vaitro = Model.VaiTro,
				Email = Model.Email,
				RandomKey = Model.RandomKey
			};
		}
		public static KhachHang ToKhachHangCreateDTO(this DangKyMD Model)
		{
			return new KhachHang
			{
				MaKh = Model.UserName,
				MatKhau = Model.Password,
				VaiTro = Model.Vaitro,
				Email = Model.Email,
				HoTen = Model.Hoten,	
				HieuLuc = Model.HieuLuc,

			};
		}
		public static KhachHang ToAdminCreateDTO(this AdminDkMD Model)
		{
			return new KhachHang
			{
				MaKh = Model.UserName,
				MatKhau = Model.Password,
				VaiTro = 1,
				Email = Model.Email,
				HieuLuc = true,
				HoTen = "admin",
			};
		}
		public static KhachHang UpdateKhachHangFromUpdateKH(this UpdateKH model, KhachHang khachHang)
		{
			khachHang.MatKhau = model.Password;
			return khachHang;
		}
		public class AutoMapperProfile : Profile
		{
			public AutoMapperProfile()
			{
				CreateMap<KhachHangsMD, KhachHang>();
				//.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
				//.ReverseMap();
			}
		}
	}
}
