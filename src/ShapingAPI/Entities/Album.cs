using System;
using System.Collections.Generic;

namespace ShapingAPI.Entities
{
    public partial class Album
    {
        public Album()
        {
            Track = new HashSet<Track>();
        }

        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Track> Track { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
