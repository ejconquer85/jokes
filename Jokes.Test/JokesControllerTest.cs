using System.Collections.Generic;
using System.Threading.Tasks;
using Jokes.Controllers;
using Jokes.Entities;
using Jokes.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Jokes.Test
{
    public class JokesControllerTest
    {
        private readonly JokesController _jokesController;
        
        private readonly Mock<IJokesServices> _jokesServices = new Mock<IJokesServices>();

        public JokesControllerTest()
        {
            _jokesController = new JokesController(_jokesServices.Object);
        }
        
        [TearDown]
        public void Cleanup()
        {
            _jokesServices.Reset();
        }
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SearchJokesAsyncTest()
        {
            //Arrange
            ArrangeSearchJokes();
            
            //Act
            var result = await _jokesController.SearchJokes("test");
            
            // Asserts
            Assert.NotNull(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var jokes = okResult.Value as JokesResponse;
            
            Assert.NotNull(jokes);
            Assert.NotNull(jokes.Short);
            Assert.NotNull(jokes.Medium);
            Assert.NotNull(jokes.Long);
            
            Assert.AreEqual(200, okResult.StatusCode);
        }
        
        [Test]
        public async Task GetRandomJokesAsyncTest()
        {
            //Arrange
            ArrangeJoke();
            
            //Act
            var result = await _jokesController.GetRandomJoke();
            
            // Asserts
            Assert.NotNull(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            
            Assert.AreEqual(200, okResult.StatusCode);
        }
        
        private void ArrangeSearchJokes()
        {
            var jokes = 
                new JokesResponse
                {
                    Short = new List<string>{"ShortJoke 1", "ShortJoke 2"},
                    Medium = new List<string>{"MediumJoke 1", "MediumJoke 2"},
                    Long = new List<string>{"LongJoke 1", "LongJoke 2"},
                    
                };

            _jokesServices
                .Setup(mock => mock.SearchJokes(It.IsAny<string>()))
                .Returns(  Task.FromResult(jokes) );
            
        }

        private void ArrangeJoke()
        {
            var joke = new
            {
                id= "cFd21gNClyd",
                joke= "Why is no one friends with Dracula? Because he's a pain in the neck.",
                status= 200
            };
            
            _jokesServices
                .Setup(mock => mock.GetRandomJoke())
                .Returns( Task.FromResult<IActionResult>(new OkObjectResult(joke)) );
        }
    }
}