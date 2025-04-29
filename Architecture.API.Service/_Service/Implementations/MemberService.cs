using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.API.Repository._Repository.Implementations;
using Architecture.API.Repository._Repository.Interface;
using Architecture.API.Service._Service.Interfaces;
using Architecture.Common._Model;
using Architecture.Common.DTO;

namespace Architecture.API.Service._Service.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repo;

        public MemberService(IMemberRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
        {
            // 範例商業規則：手機需唯一
            if (await _repo.ExistsPhoneAsync(dto.Phone, ct))
                throw new InvalidOperationException("此手機已註冊");

            // DTO → Entity 映射
            var member = new Member
            {
                Id = Guid.NewGuid(),
                Name = dto.Name.Trim(),
                Phone = dto.Phone,
                Tel = string.IsNullOrWhiteSpace(dto.Tel) ? null : dto.Tel.Trim(),
                Gender = dto.Gender,
                Birthday = dto.Birthday.Date
            };

            await _repo.AddAsync(member, ct);
            return member.Id;
        }
        //單一搜尋
        public async Task<Member> GetByPhoneAsync(string phone, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("手機號碼不可空白");

            phone = phone.Trim(); // ★重要！去掉前後空白

            return await _repo.GetByPhoneAsync(phone, ct);
        }



    }
}
