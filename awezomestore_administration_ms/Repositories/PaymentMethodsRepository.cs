using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using awezomestore_administration_ms.Entities;
using Npgsql;
using Dapper;

namespace awezomestore_administration_ms.Repositories
{
    public class PaymentMethodsRepository : IPaymentMethodsRepository
    {
        private readonly IConfiguration _configuration;

        public PaymentMethodsRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<PaymentMethods> GetPaymentMethods(int paymentMethod_id)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var paymentMethods = await connection.QueryFirstOrDefaultAsync<PaymentMethods>
                ("SELECT * FROM Payment_methods WHERE Payment_method_id = @Payment_method_id", new { Payment_method_id = paymentMethod_id });

            if (paymentMethods == null)
                return new PaymentMethods
                { Payment_code = "00ll", Payment_sc = "No registra", Email = "No registra" };

            return paymentMethods;
        }

        public async Task<bool> CreatePaymentMethod(PaymentMethods paymentMethods)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Payment_methods (Payment_code, Payment_exp, Payment_sc, Email)" +
                     "VALUES (@Payment_code, @Payment_exp, @Payment_sc, @Phone, @Email)",
                            new
                            {
                                Payment_code = paymentMethods.Payment_code,
                                Payment_exp = paymentMethods.Payment_exp,
                                Payment_sc = paymentMethods.Payment_sc,
                                Email = paymentMethods.Email
                            });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdatePaymentMethod(PaymentMethods paymentMethods)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE Payment_methods SET Payment_code = @Payment_code, Payment_exp= @Payment_exp, Payment_sc = @Payment_sc" +                   
                            new
                            {
                                Payment_code = paymentMethods.Payment_code,
                                Payment_exp = paymentMethods.Payment_exp,
                                Payment_sc = paymentMethods.Payment_sc,
                                Email = paymentMethods.Email,
                                Payment_method_id = paymentMethods.Payment_method_id
                            });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeletePaymentMethod(int paymentMethod_id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Paymente_methods WHERE Payment_method_id = @Payment_method_id",
                new { Payment_method_id = paymentMethod_id });

            if (affected == 0)
                return false;

            return true;
        }

    }
}
