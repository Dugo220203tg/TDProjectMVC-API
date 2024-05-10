using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API_Web_Shop_Electronic_TD.Database;
using API_Web_Shop_Electronic_TD.Interfaces;
using API_Web_Shop_Electronic_TD.Repository;
using API_Web_Shop_Electronic_TD.Mappers;
using API_Web_Shop_Electronic_TD.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace API_Web_Shop_Electronic_TD.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class KhachHangsController : Controller
    {
		private readonly IKhachHangRepository KhachHangsRepository;
		private readonly Hshop2023Context db;


		public KhachHangsController( IKhachHangRepository KhachHangsRepository, Hshop2023Context db)
        {
			this.KhachHangsRepository = KhachHangsRepository;
			this.db = db;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var khachHangsMD = await KhachHangsRepository.GetAllAsync();
				var model = khachHangsMD.Select(s => s.ToKhachHangDo()).ToList();

				return Ok(model);
			}
			catch (Exception ex)
			{
				// Xử lý và thông báo lỗi
				return BadRequest("Đã xảy ra lỗi: " + ex.ToString());
			}
		}
		
		[HttpGet("{MaKh}")]
		public async Task<IActionResult> GetById([FromRoute] string MaKh)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var khachhang = await KhachHangsRepository.GetByIdAsync(MaKh);

			if (khachhang == null)
			{
				return NotFound();
			}

			return Ok(khachhang.ToKhachHangDo());
		}
		[HttpPost]
		public async Task<IActionResult> Post(AdminDkMD model)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					// Tạo một đối tượng chứa thông tin lỗi từ ModelState
					var errors = ModelState.Values.SelectMany(v => v.Errors)
												   .Select(e => e.ErrorMessage)
												   .ToList();
					return BadRequest(errors); // Trả về các thông tin lỗi
				}

				var createdModel = await KhachHangsRepository.CreateAsync(model);
				return Ok(createdModel);
			}
			catch (Exception ex)
			{
				// Xử lý và thông báo lỗi tại đây
				return BadRequest("Đã xảy ra lỗi: " + ex.ToString());
			}
		}

		[HttpPut]
		[Route("{MaKh}")]
		public async Task<IActionResult> Update([FromRoute] string MaKh, [FromBody] UpdateKH model)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var Model = await KhachHangsRepository.UpdateAsync(MaKh, model);
				return Ok();
			}
			catch (Exception ex)
			{
				// Xử lý và thông báo lỗi tại đây
				return BadRequest("Đã xảy ra lỗi: " + ex.ToString());
			}
		}
		[HttpDelete]
		[Route("{MaKh}")]
		public async Task<IActionResult> Delete([FromRoute] string MaKh)
		{
			// Kiểm tra tính hợp lệ của ModelState
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// Xóa bản ghi từ bảng "HangHoas"
			var deleteKH = await KhachHangsRepository.DeleteAsync(MaKh);

			// Nếu không tìm thấy bản ghi để xóa, trả về NotFound
			if (deleteKH == null)
				return NotFound();

			// Trả về phản hồi NoContent nếu xóa thành công
			return NoContent();
		}

	}
}
