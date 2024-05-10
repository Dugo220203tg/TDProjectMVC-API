using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Interfaces;
using API_Web_Shop_Electronic_TD.Mappers;
using API_Web_Shop_Electronic_TD.Models;
using API_Web_Shop_Electronic_TD.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Web_Shop_Electronic_TD.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class DanhMucSpController : Controller
	{

		private readonly Hshop2023Context db;
		private readonly IDanhMucSp DanhMucSpRepository;
		public DanhMucSpController(Hshop2023Context db, IDanhMucSp DanhMucSpRepository)
		{
			this.db = db;
			this.DanhMucSpRepository = DanhMucSpRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{

			var loais = await DanhMucSpRepository.GetAllAsync();
			var model = loais.Select(s => s.ToLoaiDo()).ToList();

			return Ok(model);
		}
		[HttpGet("{MaLoai}")]
		public async Task<IActionResult> GetById([FromRoute] int MaLoai)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var loais = await DanhMucSpRepository.GetByIdAsync(MaLoai);

			if (loais == null)
			{
				return NotFound();
			}

			return Ok(loais.ToLoaiDo());
		}
		[HttpGet("{TenLoai}")]
		public async Task<IActionResult> GetByName([FromRoute] string TenLoai)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var loais = await DanhMucSpRepository.GetAllAsync();

				// Lọc danh sách các loại sản phẩm dựa trên tên loại được cung cấp
				if (!string.IsNullOrEmpty(TenLoai))
				{
					loais = loais.Where(l => l.TenLoai.Contains(TenLoai)).ToList();
				}

				// Kiểm tra xem danh sách lọc có rỗng không
				if (loais.Count == 0)
				{
					return NotFound();
				}

				// Trả về kết quả tìm kiếm
				return Ok(loais);
			}
			catch (Exception ex)
			{
				return BadRequest("Đã xảy ra lỗi: " + ex.ToString());
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post(DanhMucSpMD model)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var createdModel = await DanhMucSpRepository.CreateAsync(model);
				return Ok(createdModel);
			}
			catch (Exception ex)
			{
				// Xử lý và thông báo lỗi tại đây
				return BadRequest("Đã xảy ra lỗi: " + ex.ToString());
			}
		}
		[HttpPut]
		[Route("{MaLoai}")]
		public async Task<IActionResult> Update([FromRoute] int MaLoai, [FromBody] DanhMucSpMD model)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var Model = await DanhMucSpRepository.UpdateAsync(MaLoai, model);
				if (Model == null)
				{
					return NotFound();
				}
				return Ok(Model.ToLoaiDo());
			}
			catch (Exception ex)
			{
				// Xử lý và thông báo lỗi tại đây
				return BadRequest("Đã xảy ra lỗi: " + ex.ToString());
			}
		}
		[HttpDelete]
		[Route("{MaLoai:int}")]
		public async Task<IActionResult> Delete([FromRoute] int MaLoai)
		{
			// Kiểm tra tính hợp lệ của ModelState
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Xóa bản ghi từ bảng "HangHoas"
			var loais = await DanhMucSpRepository.DeleteAsync(MaLoai);

			// Nếu không tìm thấy bản ghi để xóa, trả về NotFound
			if (loais == null)
				return NotFound();

			// Trả về phản hồi NoContent nếu xóa thành công
			return NoContent();
		}
	}
}
