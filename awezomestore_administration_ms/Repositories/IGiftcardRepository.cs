using awezomestore_administration_ms.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace awezomestore_administration_ms.Repositories
{
    public interface IGiftcardRepository
    {
        Task<Giftcard> GetGiftcard(int giftcard_code);
        Task<bool> UpdateGiftcard(Giftcard giftcard);      
    }
}
