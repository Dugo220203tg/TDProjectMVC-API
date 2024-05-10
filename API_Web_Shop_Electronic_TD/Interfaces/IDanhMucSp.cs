using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Models;

namespace API_Web_Shop_Electronic_TD.Interfaces
{
	public interface IDanhMucSp
	{
		Task<List<Loai>> GetAllAsync();
		Task<Loai?> GetByIdAsync(int MaLoai);
		Task<Loai?> GetByNameAsync(string TenLoai);
		Task<Loai> CreateAsync(DanhMucSpMD model);
		Task<Loai?> UpdateAsync(int MaLoai, DanhMucSpMD model);
		Task<Loai?> DeleteAsync(int MaLoai);
	};
	public interface IHangSp
	{
		Task<List<NhaCungCap>> GetAllAsync();
		Task<NhaCungCap?> GetByIdAsync(string MaNCC);
		Task<NhaCungCap> CreateAsync(HangSpMD model);
		Task<NhaCungCap?> UpdateAsync(string MaNCC, HangSpMD model);
		Task<NhaCungCap?> DeleteAsync(string MaNCC);
	};
}

