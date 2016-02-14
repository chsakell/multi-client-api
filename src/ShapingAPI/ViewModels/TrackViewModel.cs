using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapingAPI.ViewModels
{
    public class TrackViewModel
    {
        public TrackViewModel() { }

        public int TrackId { get; set; }
        public int? AlbumId { get; set; }
        public int? Bytes { get; set; }
        public string Composer { get; set; }
        public int? GenreId { get; set; }
        public int MediaTypeId { get; set; }
        public int Milliseconds { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
