using DataAccess.Generic;
using Entities.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {

        private readonly IGenericRepository<Client> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientsController(IGenericRepository<Client> genericRepository, IUnitOfWork unitOfWork)
        {
            this._genericRepository = genericRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _genericRepository.GetAsync();
        }

        [HttpGet("direction")]
        public async Task<IEnumerable<Client>> Getfl(string direction)
        {
            if (direction != null && direction == "desc")
            {
                return await _genericRepository.GetAsync(null,  b => b.OrderByDescending(d=>d.LastName));
            } else
            {
                return await _genericRepository.GetAsync(null,  b => b.OrderBy(d => d.LastName));
            }
        }

        [HttpGet("{lastname}")]
        public async Task<IEnumerable<Client>> Get(string lastname)
        {
            return await _genericRepository.GetAsync(x => x.LastName == lastname);

        } 


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _genericRepository.CreateAsync(client);

            if (created)
                _unitOfWork.Commit();

            return Created("Created", new { Response = StatusCode(201) });
        }


    }
}
