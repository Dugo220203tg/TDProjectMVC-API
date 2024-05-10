using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Models;

namespace API_Web_Shop_Electronic_TD.Interfaces
{
	public interface IHoaDon
	{
		Task<List<HoaDon>> GetAllAsync();
		Task<HoaDon?> GetByIdAsync(int MaHd);
		//Task<HoaDon> CreateAsync(HangSpMD model);
		Task<HoaDon?> UpdateAsync(int MaHd, HoaDonMD model);
		Task<HoaDon?> DeleteAsync(int MaHd);
	}
}
