using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Interfaces;
using API_Web_Shop_Electronic_TD.Models;
using Microsoft.EntityFrameworkCore;
using API_Web_Shop_Electronic_TD.Mappers;


namespace API_Web_Shop_Electronic_TD.Repository
{
	public class HangHoaRepository : IHangHoaRepository
	{
		private readonly Hshop2023Context db;
		public HangHoaRepository(Hshop2023Context db)
		{
			this.db = db;
		}
		public async Task<List<HangHoa>> GetAllAsync()
		{
			return await db.HangHoas.ToListAsync();
		}
		public async Task<HangHoa> CreateAsync(HangHoaMD model)
		{
			var Hanghoa = model.ToHangHoaCreateDTO(); // Sử dụng mapper để chuyển đổi từ KhachHangsMD sang KhachHang

			await db.HangHoas.AddAsync(Hanghoa);
			await db.SaveChangesAsync();

			return Hanghoa; // Trả về đối tượng KhachHang sau khi thêm vào cơ sở dữ liệu
		}

		public async Task<HangHoa?> DeleteAsync(int MaHh)
		{
			var HangHoaModel = await db.HangHoas.FirstOrDefaultAsync(x => x.MaHh == MaHh);
			if (HangHoaModel == null)
			{
				return null;
			}
			db.HangHoas.Remove(HangHoaModel);
			await db.SaveChangesAsync();
			return HangHoaModel;
		}


		public async Task<HangHoa?> GetByIdAsync(int Mahh)
		{
			return await db.HangHoas.FindAsync(Mahh);
		}
		public async Task<HangHoa?> UpdateAsync(int Mahh, UpdateHangHoaMD Model)
		{
			// Lấy đối tượng HangHoa từ cơ sở dữ liệu
			var HangHoaModel = await db.HangHoas.FirstOrDefaultAsync(x => x.MaHh == Mahh);

			// Kiểm tra xem HangHoaModel có null không
			if (HangHoaModel == null)
			{
				return null; // Trả về null nếu không tìm thấy đối tượng HangHoa
			}

			// Cập nhật thông tin của HangHoaModel từ dữ liệu được gửi từ client
			HangHoaModel.TenHh = Model.TenHH;
			HangHoaModel.Hinh = Model.Hinh;
			HangHoaModel.MoTa = Model.MoTa;
			HangHoaModel.MoTaDonVi = Model.MoTaDonVi;
			HangHoaModel.MaLoai = (int)Model.MaLoai;
			HangHoaModel.NgaySx = (DateOnly)Model.NgaySX;
			HangHoaModel.GiamGia = (double)Model.GiamGia;
			HangHoaModel.MaNcc = Model.MaNCC;
			HangHoaModel.DonGia = Model.DonGia;

			// Lưu thay đổi vào cơ sở dữ liệu
			await db.SaveChangesAsync();

			// Trả về HangHoaModel đã được cập nhật
			return HangHoaModel;
		}

	}
}
