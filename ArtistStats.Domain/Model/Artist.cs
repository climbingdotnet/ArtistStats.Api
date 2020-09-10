using System;
using System.Collections.Generic;

namespace ArtistStats.Domain.Model
{
    public class Artist
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Songs { get; set; }
    }
}
