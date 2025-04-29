using Architecture.API.Repository._Repository.Interface;
using Architecture.Common._Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace Architecture.API.Repository._Repository.Implementations
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connStr;

        public MemberRepository()                         // ★ 無參數建構式
        {
            _connStr = ConfigurationManager
                       .ConnectionStrings["DefaultConnection"]
                       .ConnectionString;
        }

        public async Task<int> AddAsync(Member member, CancellationToken ct = default)
        {
            const string sql = @"
                            INSERT INTO Member (Id, Name, Phone, Tel, Gender, Birthday)
                            VALUES (@Id, @Name, @Phone, @Tel, @Gender, @Birthday);";

            var conn = new SqlConnection(_connStr);
            await conn.OpenAsync(ct);

            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = member.Id;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 30).Value = member.Name;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 10).Value = member.Phone;
            cmd.Parameters.Add("@Tel", SqlDbType.NVarChar, 10).Value = (object)member.Tel ?? DBNull.Value;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 1).Value = member.Gender;
            cmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = member.Birthday.Date;

            return await cmd.ExecuteNonQueryAsync(ct);
        }

        public async Task<bool> ExistsPhoneAsync(string phone, CancellationToken ct = default)
        {
            const string sql = "SELECT 1 FROM Member WHERE Phone = @Phone";

            var conn = new SqlConnection(_connStr);
            await conn.OpenAsync(ct);

            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 10).Value = phone;

            bool result = await cmd.ExecuteScalarAsync(ct) != null;

            return result;
        }
        //單一查詢
        public async Task<Member> GetByPhoneAsync(string phone, CancellationToken ct = default)
        {
            Console.WriteLine($"🔍 查詢 Phone = '{phone}'");
            const string sql = @"
        SELECT TOP 1 Id, Name, Phone, Tel, Gender, Birthday
        FROM Member
        WHERE Phone = @Phone";

            using (var conn = new SqlConnection(_connStr))
            {
                await conn.OpenAsync(ct);

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = phone;

                    using (var reader = await cmd.ExecuteReaderAsync(ct))
                    {
                        if (await reader.ReadAsync(ct))
                        {
                            return new Member
                            {
                                Id = reader.GetGuid(0),
                                Name = reader.GetString(1),
                                Phone = reader.GetString(2),
                                Tel = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Gender = reader.GetString(4),
                                Birthday = reader.GetDateTime(5)
                            };
                        }
                    }
                }
            }
            return null;
        }


    }
}