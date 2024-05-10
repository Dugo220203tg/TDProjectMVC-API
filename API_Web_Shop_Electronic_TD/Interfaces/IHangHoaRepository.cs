using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Models;

namespace API_Web_Shop_Electronic_TD.Interfaces
{
	public interface IHangHoaRepository
	{
		Task<List<HangHoa>> GetAllAsync();
		Task<HangHoa?> GetByIdAsync(int Mahh);
		Task<HangHoa> CreateAsync(HangHoaMD model);
		Task<HangHoa?> UpdateAsync(int Mahh, UpdateHangHoaMD model);
		Task<HangHoa?> DeleteAsync(int Mahh);
	}
}
