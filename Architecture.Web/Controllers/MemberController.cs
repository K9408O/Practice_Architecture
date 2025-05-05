using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Architecture.Common.DTO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Architecture.Web.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        private readonly HttpClient _httpClient;

        public MemberController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44362/"); // API 的 Base URL
        }
        //註冊
        [HttpPost]
        public async Task<ContentResult> Register(RegisterDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/member/register", content);

            var result = await response.Content.ReadAsStringAsync();
            return Content(result, "application/json");
        }

        // ✅ 查詢單筆會員（POST /member/search）
        [HttpPost]
        public async Task<ContentResult> Search(SearchDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/member/search", content);

            if (!response.IsSuccessStatusCode)
            {
                return Content("null", "application/json");
            }

            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");  // << 不再用 Json() 包
        }
        // ✅ 查詢全部會員（GET /member/all）
        [HttpGet]
        public async Task<ContentResult> All()
        {
            var response = await _httpClient.GetAsync("api/member/all");

            if (!response.IsSuccessStatusCode)
            {
                return Content("[]", "application/json"); // 回傳空陣列
            }

            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json"); // ✅ 保留原始 ISO 格式日期
        }


        //編輯更新
        [HttpPost]
        public async Task<JsonResult> Update(MemberDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/member/update", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Json(new { success = true, message = "更新成功" });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = "更新失敗：" + error });
            }
        }

        //刪除
        [HttpPost]
        public async Task<JsonResult> Delete(IdDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/member/delete", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "刪除成功" });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = "刪除失敗：" + error });
            }
        }


    }

}
