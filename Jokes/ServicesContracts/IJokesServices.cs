using System;
using System.Threading.Tasks;
using Jokes.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jokes.ServicesContracts
{
    public interface IJokesServices
    {
        Task<IActionResult> GetRandomJoke();
        Task<JokesResponse> SearchJokes(string searchTerm);
    }
}
