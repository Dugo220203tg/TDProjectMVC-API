﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;
using System.Text;
using TrangQuanLy.Helpers;
using TrangQuanLy.Models;

namespace TrangQuanLy.Controllers
{
	public class AdminController : Controller
	{
        private readonly HttpClient _client;
        Uri baseAddress = new Uri("https://localhost:7109/api");
        public AdminController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        #region --- DANG NHAP ---


        [HttpPost]
        public async Task<IActionResult> DangNhap(AdminViewModel model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            try
            {
                AdminViewModel khachhangs = new AdminViewModel();
                HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/KhachHangs/GetById/" + model.UserName).Result;
                if (respone.IsSuccessStatusCode)
                {
                    // Deserialize response data
                    string data = respone.Content.ReadAsStringAsync().Result;
                    khachhangs = JsonConvert.DeserializeObject<AdminViewModel>(data);

                    // Validate user
                    if (khachhangs != null)
                    {
                        // Check if user is active
                        if (khachhangs.Vaitro == 1)
                        {
                            // Check if password is correct
                            if (khachhangs.Password == model.Password.ToMd5Hash(khachhangs.RandomKey))
                                //if (khachhangs.Password == model.Password)
                            {
                                // Authentication successful, create claims
                                var claims = new List<Claim>
                                    {
                                    new Claim(ClaimTypes.Email, khachhangs.Email),
                                    new Claim(ClaimTypes.Name, khachhangs.UserName),
                                    new Claim("CustomerID", khachhangs.UserName),
                                    new Claim(ClaimTypes.Role, "Customer")
                                    };
                                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                                // Sign in user
                                await HttpContext.SignInAsync(claimsPrincipal);

                                // Redirect user to returnUrl if it's a local URL, otherwise redirect to home
                                if (Url.IsLocalUrl(ReturnUrl))
                                {
                                    return Redirect(ReturnUrl);
                                }
                                else
                                {
                                    return Redirect("/HangHoa/Index");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("loi", "Mật khẩu không đúng");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("loi", "Tài khoản không hoạt động");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("loi", "Thông tin đăng nhập không đúng");
                    }
                }
                else
                {
                    ModelState.AddModelError("loi", "Lỗi khi gửi yêu cầu đăng nhập đến API");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("loi", "Lỗi: " + ex.Message);
            }

            // If ModelState is invalid or authentication fails, return view
            return View();
        }

        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        #endregion
        #region --- DANG KY ---

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangKy(AdminViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/KhachHangs/Post", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Tạo tài khoản admin thành công!";
                    return RedirectToAction("DangNhap", "Admin");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return View();
        }


        #endregion
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    List<AdminViewModel> khachhang = new List<AdminViewModel>();
        //    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/KhachHangs/GetAll").Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = response.Content.ReadAsStringAsync().Result;
        //        khachhang = JsonConvert.DeserializeObject<List<AdminViewModel>>(data);
        //    }

        //    return View(khachhang);
        //}
    }
}
