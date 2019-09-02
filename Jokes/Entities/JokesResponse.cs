using System.Collections.Generic;

namespace Jokes.Entities
{
    public class JokesResponse
    {
        public List<string> Short { get; set; }
        public List<string> Medium { get; set; }
        public List<string> Long { get; set; }
    }
}