﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.Common._Model;
using Architecture.Common.DTO;

namespace Architecture.API.Service._Service.Interfaces
{
    public interface IMemberService
    {
        Task<List<MemberDto>> GetAllAsync(CancellationToken ct = default);
        Task<Member> GetByPhoneAsync(string phone, CancellationToken ct = default);
        Task<Guid> RegisterAsync(RegisterDto dto, CancellationToken ct = default);

        Task UpdateAsync(MemberDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);

    }
}
