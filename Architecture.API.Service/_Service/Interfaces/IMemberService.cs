using System;
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
        Task<Guid> RegisterAsync(RegisterDto dto, CancellationToken ct = default);
    }
}
