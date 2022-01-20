using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using Dapper;
using awezomestore_administration_ms.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace awezomestore_administration_ms.Repositories
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly IConfiguration _configuration;

        public AddressesRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Addresses> GetAddresses(int address_id)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var addresses = await connection.QueryFirstOrDefaultAsync<Addresses>
                ("SELECT * FROM shipping_address WHERE Address_id = @Address_id", new { Address_id = address_id });

            if (addresses == null)
                return new Addresses
                { Address_Name = "No registra", Zipcode = 0, City = "No registra", Phone = 0, Departamento = "No registra", Address_details = "No registra" };

            return addresses;
        }

        public async Task<bool> CreateAddress(Addresses addresses)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO shipping_address (Address_Name, Zipcode, City, Phone, Departamento, Address_details)" +
                     "VALUES (@Address_Name, @Zipcode, @City, @Phone, @Departamento, @Address_details)",
                            new
                            {
                                Address_id = addresses.Address_id,
                                Address_Name = addresses.Address_Name,
                                Zipcode = addresses.Zipcode,
                                City = addresses.City,
                                Phone = addresses.Phone,
                                Departamento = addresses.Departamento,
                                Address_details = addresses.Address_details
                            });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateAddress(Addresses addresses)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                    ("UPDATE shipping_address SET Address_Name = @Address_Name, Zipcode = @Zipcode, City = @City, Phone = @Phone, Departamento = @Departamento," +
                    "Address_details = @Address_details WHERE Address_id = @Address_id",
                            new {
                                Address_Name = addresses.Address_Name,
                                Zipcode = addresses.Zipcode,
                                City = addresses.City,
                                Phone = addresses.Phone,
                                Departamento = addresses.Departamento,
                                Address_details = addresses.Address_details,
                                Address_id = addresses.Address_id
                            });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteAddress(int address_id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Shipping_Address WHERE Address_id = @Address_id",
                new { Address_id = address_id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}