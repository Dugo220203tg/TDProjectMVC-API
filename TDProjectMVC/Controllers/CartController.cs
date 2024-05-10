using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TDProjectMVC.Data;
using TDProjectMVC.Helpers;
using TDProjectMVC.Services;
using TDProjectMVC.ViewModels;
namespace TDProjectMVC.Controllers
{
	public class CartController : Controller
	{
		private readonly Hshop2023Context db;
		private readonly IVnPayService _vnPayservice;


		public CartController(Hshop2023Context context) {
			db = context;
		}
		public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
		public IActionResult Index()
		{
			return View(Cart);
		}
		public IActionResult MiniCart()
		{
			return View(Cart);
		}
		#region Thêm cart
		//[HttpPost]
		//public IActionResult AddToCart(int id, int quantity = 1, string type = "Normal")
		//{
		//	var gioHang = Cart;
		//	var item = gioHang.SingleOrDefault(p => p.MaHH == id);
		//	if (item == null)
		//	{
		//		var hanghoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
		//		if (hanghoa == null)
		//		{
		//			TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
		//			return Redirect("/404");
		//		}
		//		item = new CartItem
		//		{
		//			MaHH = hanghoa.MaHh,
		//			TenHH = hanghoa.TenHh,
		//			DonGia = hanghoa.DonGia ?? 0,
		//			Hinh = hanghoa.Hinh ?? String.Empty,
		//			SoLuong = quantity
		//		};
		//		gioHang.Add(item);
		//	}
		//	else
		//	{
		//		item.SoLuong += quantity;
		//	}
		//	HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
		//	//return RedirectToAction("Index", "Product");
		//          return RedirectToAction("_LayoutAdmin", gioHang);

		//      }

		[HttpPost]
		public IActionResult AddToCart(int id, int quantity = 1, string type = "Normal")
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaHH == id);

			if (item == null)
			{
				var hanghoa = db.HangHoas.SingleOrDefault(p => p.MaHh == id);
				if (hanghoa == null)
				{
					return Json(new { success = false, message = $"Không tìm thấy hàng hóa có mã {id}" });
				}

				item = new CartItem
				{
					MaHH = hanghoa.MaHh,
					TenHH = hanghoa.TenHh,
					DonGia = hanghoa.DonGia ?? 0,
					Hinh = hanghoa.Hinh ?? String.Empty,
					SoLuong = quantity
				};
				gioHang.Add(item);
			}
			else
			{
				item.SoLuong += quantity;
			}

			HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
			return Json(new { success = true, cartCount = gioHang.Sum(i => i.SoLuong) });
		}
		//Hiển thị dữ liệu trong giỏ hàng
		public IActionResult GetCartData()
        {
            var cartData = new
            {
                CardProducts = Cart,
                TotalQuantity = Cart.Sum(p => p.SoLuong),
                TotalAmount = Cart.Sum(p => p.SoLuong * p.DonGia)
            };

            return Json(cartData);
        }
        public IActionResult RemoveCart(int id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaHH == id);
			if (item != null)
			{
				gioHang.Remove(item);
				HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}

		public IActionResult IncreaseQuantity(int id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaHH == id);
			if (item != null)
			{
				item.SoLuong++; // Tăng số lượng lên 1 đơn vị
				HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}
		public IActionResult MinusQuantity(int id)
		{
			var gioHang = Cart; // Giả sử Cart là một thuộc tính hoặc một biến mà bạn đã khai báo ở nơi khác trong controller
			var item = gioHang.SingleOrDefault(p => p.MaHH == id); // Tìm kiếm sản phẩm trong giỏ hàng với id đã được truyền vào
			if (item != null)
			{
				item.SoLuong--; // Giảm số lượng đi 1 đơn vị
				HttpContext.Session.Set(MySetting.CART_KEY, gioHang); // Lưu lại giỏ hàng vào session
			}
			return RedirectToAction("Index"); // Chuyển hướng người dùng đến trang Index
		}

		#endregion
		#region Thanh Toan (checkOut)
		[Authorize]
		[HttpGet]
		public IActionResult Checkout()
		{
			if (Cart.Count == 0)
			{
				return Redirect("/");
			}

			return View(Cart);
		}
		[Authorize]
		[HttpPost]
		public IActionResult Checkout(CheckOutVM model, string payment = "COD")
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (payment == "Thanh toán VNPay")
					{
						var vnPayModel = new VnPaymentRequestModel
						{
							Amount = Cart.Sum(p => p.ThanhTien),
							CreatedDate = DateTime.Now,
							Description = $"{model.HoTen} {model.DienThoai}",
							FullName = model.HoTen,
							OrderId = new Random().Next(1000, 100000)
						};
						return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
					}
					var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;
					if (customerId == null)
					{
						// Nếu không tìm thấy thông tin khách hàng, quay về trang đăng nhập hoặc hiển thị thông báo lỗi
						return RedirectToAction("DangNhap", "KhachHang");
					}

					var khachHang = db.KhachHangs.FirstOrDefault(kh => kh.MaKh == customerId);
					if (khachHang == null )
					{
						// Nếu không tìm thấy thông tin khách hàng và không chọn giống khách hàng, hiển thị thông báo lỗi
						TempData["Message"] = "Không tìm thấy thông tin khách hàng.";
						return RedirectToAction("Index", "Product");
					}

					var hoadon = new HoaDon
					{
						MaKh = customerId,
						HoTen = model.GiongKhachHang ? khachHang.HoTen : model.HoTen,
						DiaChi = model.GiongKhachHang ? khachHang.DiaChi : model.DiaChi,
						DienThoai = model.GiongKhachHang ? khachHang.DienThoai : model.DienThoai,
						NgayDat = DateTime.Now,
						CachThanhToan = "COD",
						CachVanChuyen = "GRAB",
						MaTrangThai = 0,
						GhiChu = model.GhiChu
					};

					// Bắt đầu giao dịch cơ sở dữ liệu
					using (var transaction = db.Database.BeginTransaction())
					{
						try
						{
							db.HoaDons.Add(hoadon);
							db.SaveChanges();

							foreach (var item in Cart)
							{
								var chiTietHd = new ChiTietHd
								{
									MaHd = hoadon.MaHd,
									MaHh = item.MaHH,
									SoLuong = item.SoLuong,
									DonGia = item.DonGia,
									GiamGia = 0
								};
								db.ChiTietHds.Add(chiTietHd);
							}
							db.SaveChanges();

							// Làm sạch giỏ hàng sau khi đặt hàng
							Cart.Clear();
							HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, Cart);

							// Commit giao dịch cơ sở dữ liệu
							transaction.Commit();

							// Chuyển hướng đến trang thành công
							return View("MiniCart");
						}
						catch (Exception ex)
						{
							// Nếu có lỗi xảy ra trong quá trình thêm hóa đơn hoặc chi tiết hóa đơn, rollback giao dịch
							transaction.Rollback();
							TempData["Message"] = "Đã xảy ra lỗi trong quá trình xử lý đơn hàng. Vui lòng thử lại sau.";
                            return BadRequest(ex.ToString());

                        }
                    }
				}
				catch (Exception ex)
				{
					// Xử lý các lỗi không mong muốn
					TempData["Message"] = "Đã xảy ra lỗi không mong muốn. Vui lòng thử lại sau.";
					return BadRequest(ex.ToString());

				}
			}

			// Trả về view nếu ModelState không hợp lệ
			return View(Cart);
		}
		#endregion
		//#region Paypal payment
		//[Authorize]
		//[HttpPost("/Cart/create-paypal-order")]
		//public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
		//{
		//	// Thông tin đơn hàng gửi qua Paypal
		//	var tongTien = Cart.Sum(p => p.ThanhTien).ToString();
		//	var donViTienTe = "USD";
		//	var maDonHangThamChieu = "DH" + DateTime.Now.Ticks.ToString();

		//	try
		//	{
		//		var response = await _paypalClient.CreateOrder(tongTien, donViTienTe, maDonHangThamChieu);

		//		return Ok(response);
		//	}
		//	catch (Exception ex)
		//	{
		//		var error = new { ex.GetBaseException().Message };
		//		return BadRequest(error);
		//	}
		//}

		//[Authorize]
		//[HttpPost("/Cart/capture-paypal-order")]
		//public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken)
		//{
		//	try
		//	{
		//		var response = await _paypalClient.CaptureOrder(orderID);

		//		// Lưu database đơn hàng của mình

		//		return Ok(response);
		//	}
		//	catch (Exception ex)
		//	{
		//		var error = new { ex.GetBaseException().Message };
		//		return BadRequest(error);
		//	}
		//}

		//#endregion
		[Authorize]
		public IActionResult PaymentFail()
		{
			return View();
		}

		[Authorize]
		public IActionResult PaymentCallBack()
		{
			var response = _vnPayservice.PaymentExecute(Request.Query);

			if (response == null || response.VnPayResponseCode != "00")
			{
				TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
				return RedirectToAction("PaymentFail");
			}


			// Lưu đơn hàng vô database

			TempData["Message"] = $"Thanh toán VNPay thành công";
			return RedirectToAction("PaymentSuccess");
		}
	}
}

