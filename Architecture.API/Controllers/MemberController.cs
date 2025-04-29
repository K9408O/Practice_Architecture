using System;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using Architecture.API.Service._Service.Interfaces;
using Architecture.Common.DTO;
using System.Web.Http.Cors;
using System.Net;
using System.Diagnostics;


namespace Architecture.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] 
    [RoutePrefix("api/member")]
    public class MemberController : ApiController
    {
        private readonly IMemberService _service;

        public MemberController(IMemberService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok("API is alive");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegisterDto dto, CancellationToken ct = default)
        {
            // ✅ 這裡先印出收到的 dto 資料
            Debug.WriteLine("===== 收到前端傳來的資料 =====");
            Debug.WriteLine($"Name: {dto.Name}");
            Debug.WriteLine($"Phone: {dto.Phone}");
            Debug.WriteLine($"Tel: {dto.Tel}");
            Debug.WriteLine($"Gender: {dto.Gender}");
            Debug.WriteLine($"Birthday: {dto.Birthday}");
            Debug.WriteLine("================================");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("❗ ModelState驗證失敗！");
                return BadRequest(ModelState);
            }

            try
            {
                Debug.WriteLine("✅ 開始呼叫 Service 層 RegisterAsync");

                var id = await _service.RegisterAsync(dto, ct);

                Debug.WriteLine($"✅ 註冊成功，會員ID: {id}");

                return Ok(new { Message = "註冊成功", MemberId = id });
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Debug.WriteLine($"❗ InvalidOperationException 回傳訊息: {ex.Message}");
                return Content(HttpStatusCode.BadRequest, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❗ Exception 回傳訊息: {ex.Message}");
                return Content(HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("search")]
        public async Task<IHttpActionResult> Search(SearchDto dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var member = await _service.GetByPhoneAsync(dto.Phone, ct);
                if (member == null)
                    return NotFound();

                return Ok(new MemberDto
                {
                    Id = member.Id,
                    Name = member.Name,
                    Phone = member.Phone,
                    Tel = member.Tel,
                    Gender = member.Gender,
                    Birthday = member.Birthday
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { message = ex.Message });
            }
        }
        //搜尋全部
        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAll(CancellationToken ct = default)
        {
            var members = await _service.GetAllAsync(ct);
            return Ok(members); // ✅ 直接回傳 JSON 陣列
        }


    }
}