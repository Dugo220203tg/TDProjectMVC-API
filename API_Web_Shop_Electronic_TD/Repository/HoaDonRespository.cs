using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Interfaces;
using API_Web_Shop_Electronic_TD.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Web_Shop_Electronic_TD.Repository
{
	public class HoaDonRespository : IHoaDon
	{
		private readonly Hshop2023Context db;
		public HoaDonRespository(Hshop2023Context db)
		{
			this.db = db;
		}
		public async Task<HoaDon?> DeleteAsync(int MaHd)
		{
			var Model = await db.HoaDons.FirstOrDefaultAsync(x => x.MaHd == MaHd);
			if (Model == null)
			{
				return null;
			}
			db.HoaDons.Remove(Model);
			await db.SaveChangesAsync();
			return Model;
		}

		public async Task<List<HoaDon>> GetAllAsync()
		{
			return await db.HoaDons
				.Include(h => h.MaTrangThaiNavigation)
				.ToListAsync();
		}

		public async Task<HoaDon?> GetByIdAsync(int MaHd)
		{
			return await db.HoaDons.FindAsync(MaHd);
		}

		public async Task<HoaDon?> UpdateAsync(int MaHd, HoaDonMD model)
		{
			// Lấy đối tượng HangHoa từ cơ sở dữ liệu
			var Model = await db.HoaDons.FirstOrDefaultAsync(x => x.MaHd == MaHd);

			// Kiểm tra xem HangHoaModel có null không
			if (Model == null)
			{
				return null; // Trả về null nếu không tìm thấy đối tượng HangHoa
			}

			// Cập nhật thông tin của HangHoaModel từ dữ liệu được gửi từ client
			Model.MaKh = model.MaKH;
			Model.NgayDat = model.NgayDat;
			Model.HoTen = model.HoTen;
			Model.DiaChi = model.DiaChi;
			Model.CachVanChuyen = model.CachVanChuyen;
			Model.PhiVanChuyen = model.PhiVanChuyen;
			Model.MaTrangThai = model.MaTrangThai;
			Model.DienThoai = model.DienThoai;

			// Lưu thay đổi vào cơ sở dữ liệu
			await db.SaveChangesAsync();

			// Trả về HangHoaModel đã được cập nhật
			return Model;
		}
	}
}
