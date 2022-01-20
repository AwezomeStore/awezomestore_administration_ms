using awezomestore_administration_ms.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awezomestore_administration_ms.Repositories
{
    public class GiftcardRepository : IGiftcardRepository
    {
        private readonly IConfiguration _configuration;

        public GiftcardRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        
        public async Task<Giftcard> GetGiftcard(int giftcard_code)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var giftcard = await connection.QueryFirstOrDefaultAsync<Giftcard>
                ("SELECT * FROM Giftcard WHERE Giftcard_code = @Giftcard_code", new { Giftcard_code = giftcard_code });

            if (giftcard == null)
                return new Giftcard
                { Giftcard_code = "Gifcard no valido", Giftcard_amount = 0 };

            return giftcard;
        }

        public async Task<bool> UpdateGiftcard(Giftcard giftcard)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Giftcard SET Giftcard_code = @Gifcard_code, Giftcard_amount = @Giftcard_amount, Giftcard_exp = @Giftcard_exp" +
                            new
                            {
                                Giftcard_code = giftcard.Giftcard_code,
                                Giftcard_amount = giftcard.Giftcard_amount,
                                Giftcard_exp = giftcard.Gifcard_exp
                            });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
