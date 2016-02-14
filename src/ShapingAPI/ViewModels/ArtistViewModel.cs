using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapingAPI.ViewModels
{
    public class ArtistViewModel
    {
        public ArtistViewModel()
        {
            Album = new HashSet<AlbumViewModel>();
        }

        public int ArtistId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AlbumViewModel> Album { get; set; }
    }
}
