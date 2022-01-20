using awezomestore_administration_ms.Entities;
using awezomestore_administration_ms.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace awezomestore_administration_ms.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesRepository _addressRepository;

        public AddressesController(IAddressesRepository addressesRepository)
        {
            _addressRepository = addressesRepository ?? throw new ArgumentNullException(nameof(addressesRepository));
        }

        [HttpGet("{address_id}", Name = "GetAddresses")]
        [ProducesResponseType(typeof(Addresses), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Addresses>> GetAddresses(int address_id)
        {
            var addresses = await _addressRepository.GetAddresses(address_id);
            return Ok(addresses);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Addresses), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Addresses>> CreateAddress([FromBody] Addresses addresses)
        {
            await _addressRepository.CreateAddress(addresses);
            return CreatedAtRoute("GetAddresses", new { address_id = addresses.Address_id}, addresses);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Addresses), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Addresses>> UpdateAddress([FromBody] Addresses addresses)
        {
            return Ok(await _addressRepository.UpdateAddress(addresses));
        }

        [HttpDelete("{address_id}", Name = "DeleteAddress")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteAddress(int address_id)
        {
            return Ok(await _addressRepository.DeleteAddress(address_id));
        }
    }
}
