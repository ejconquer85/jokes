using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Jokes.Entities;
using Jokes.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Jokes.Services
{
    public class JokesServices : IJokesServices
    {
        
        private readonly string _baseUrl;

        public JokesServices(IConfiguration configuration)
        {
            _baseUrl = configuration["JokesUrl"];
        }

        public async Task<IActionResult> GetRandomJoke()
        {
            var data =  await GetData(_baseUrl);
            
            if (data != null)
            {
                return new OkObjectResult(data);
            }
            
            return new NotFoundResult();
        }

        private async Task<string> GetData(string requestUrl)
        {
            //The 'using' will help to prevent memory leaks.
            //Create a new instance of HttpClient

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var res =  await client.GetAsync(requestUrl) )
                using (var content = res.Content)
                {
                    var data = await content.ReadAsStringAsync();
                    return data;
                }
            }

        }

        public async Task<JokesResponse> SearchJokes(string searchTerm)
        {
            var requestUrl = $"{_baseUrl}search?limit=30&term={searchTerm}";
            
            var data = await GetData(requestUrl);

            var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(data);

            var jokesResponse = new JokesResponse
            {
                Short = searchResponse.Results.Where(p => p.Joke.Split(' ').Length < 10).Select(p => p.Joke)
                    .ToList(),
                Medium = searchResponse.Results.Where(p => p.Joke.Split(' ').Length < 20).Select(p => p.Joke)
                    .ToList(),
                Long = searchResponse.Results.Where(p => p.Joke.Split(' ').Length >= 20).Select(p=> p.Joke)
                    .ToList()
            };

            jokesResponse.Medium = jokesResponse.Medium.Where(p => p.Split(' ').Length >= 10).ToList();

            return jokesResponse;
        }
    }
}
