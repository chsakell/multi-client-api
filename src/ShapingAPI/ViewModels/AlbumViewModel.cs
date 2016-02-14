using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapingAPI.ViewModels
{
    public class AlbumViewModel
    {
        public AlbumViewModel()
        {
            Track = new HashSet<TrackViewModel>();
        }

        public int AlbumId { get; set; }
        public string ArtistName { get; set; }
        public string Title { get; set; }
        public virtual ICollection<TrackViewModel> Track { get; set; }
    }
}
