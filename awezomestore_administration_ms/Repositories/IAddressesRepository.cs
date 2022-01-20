using awezomestore_administration_ms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awezomestore_administration_ms.Repositories
{
    public interface IAddressesRepository
    {    
        Task<Addresses> GetAddresses(int address_id);
        Task<bool> CreateAddress(Addresses addresses);
        Task<bool> UpdateAddress(Addresses addresses);
        Task<bool> DeleteAddress(int address_id);
    }
}
