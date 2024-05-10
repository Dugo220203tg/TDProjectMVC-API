using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Interfaces;
using API_Web_Shop_Electronic_TD.Mappers;
using API_Web_Shop_Electronic_TD.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace API_Web_Shop_Electronic_TD.Repository
{

	public class KhachHangsRepository : IKhachHangRepository
	{
		private readonly Hshop2023Context db;

		public KhachHangsRepository(Hshop2023Context db)
		{
			this.db = db;

		}
		public async Task<List<KhachHang>> GetAllAsync()
		{
			return await db.KhachHangs.ToListAsync();
		}

		public async Task<KhachHang> CreateAsync(AdminDkMD model)
		{
			var khachHangexit = await db.KhachHangs.SingleOrDefaultAsync(kh => kh.MaKh == model.UserName);
			if (khachHangexit != null)
			{
				return null; // Trả về null nếu UserName đã được sử dụng
			}
			var khachHang = model.ToAdminCreateDTO(); // Sử dụng mapper để chuyển đổi từ KhachHangsMD sang KhachHang
			await db.KhachHangs.AddAsync(khachHang);
			await db.SaveChangesAsync();
			return khachHang; // Trả về đối tượng KhachHang sau khi thêm vào cơ sở dữ liệu
		}
		public async Task<KhachHang?> DeleteAsync(string MaKh)
		{
			var model = await db.KhachHangs.FirstOrDefaultAsync(x => x.MaKh == MaKh);
			if (model == null)
			{
				return null;
			}
			db.KhachHangs.Remove(model);
			await db.SaveChangesAsync();
			return model;
		}



		public async Task<KhachHang?> GetByIdAsync(string MaKh)
		{
			return await db.KhachHangs.FindAsync(MaKh);
		}

		public async Task<KhachHang?> UpdateAsync(string MaKh, UpdateKH model)
		{
			// Lấy đối tượng KhachHang từ cơ sở dữ liệu
			var khachHangModel = await db.KhachHangs.FirstOrDefaultAsync(x => x.MaKh == MaKh);

			// Kiểm tra xem đối tượng khách hàng có tồn tại không
			if (khachHangModel == null)
			{
				return null; // Trả về null nếu không tìm thấy đối tượng khách hàng
			}

			// Cập nhật thông tin của đối tượng KhachHang từ đối tượng UpdateKH
			khachHangModel = model.UpdateKhachHangFromUpdateKH(khachHangModel);

			// Lưu thay đổi vào cơ sở dữ liệu
			await db.SaveChangesAsync();

			// Trả về đối tượng KhachHang đã được cập nhật
			return khachHangModel;
		}

	}
}
