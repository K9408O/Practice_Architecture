﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Architecture.Common._Model;

namespace Architecture.API.Repository._Repository.Interface
{
    public interface IMemberRepository
    {
        Task<int> AddAsync(Member member, CancellationToken ct = default);
        Task<bool> ExistsPhoneAsync(string phone, CancellationToken ct = default);
        Task<Member> GetByPhoneAsync(string phone, CancellationToken ct);
        Task<List<Member>> GetAllAsync(CancellationToken ct = default);
        Task UpdateAsync(Member member, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
