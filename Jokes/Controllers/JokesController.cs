using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jokes.ServicesContracts;
using Microsoft.AspNetCore.Mvc;

namespace Jokes.Controllers
{
    

    [Route("api/[controller]")]
    public class JokesController : Controller
    {
        private readonly IJokesServices _iJokesService;


        public JokesController(IJokesServices iJokesService)
        {
            _iJokesService = iJokesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomJoke()
        {
            return await _iJokesService.GetRandomJoke();
        }
        
        [HttpGet]
        [Route("{searchTerm}")]
        public async Task<IActionResult> SearchJokes(string searchTerm)
        {
            return Ok( await _iJokesService.SearchJokes(searchTerm));
        }
    }
}
