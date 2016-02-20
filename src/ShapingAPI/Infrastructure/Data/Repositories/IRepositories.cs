using ShapingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapingAPI.Infrastructure.Data.Repositories
{
    public interface IAlbumRepository : IRepository<Album> {
        IEnumerable<Album> LoadAll();
        Album Load(int albumId);
    }

    public interface IArtistRepository : IRepository<Artist>
    {
        IEnumerable<Artist> LoadAll();
        Artist Load(int artistId);
    }

    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> LoadAll();
        Customer Load(int customerId);
    }

    public interface IEmployeeRepository : IRepository<Employee> { }

    public interface IGenreRepository : IRepository<Genre> { }

    public interface IInvoiceLineRepository : IRepository<InvoiceLine> { }

    public interface IInvoiceRepository : IRepository<Invoice>
    {
        IEnumerable<Invoice> LoadAll();
        Invoice Load(int invoiceId);
    }

    public interface IMediaTypeRepository : IRepository<MediaType> { }

    public interface IPlaylistTrackRepository : IRepository<PlaylistTrack> { }

    public interface IPlaylistRepository : IRepository<Playlist> { }

    public interface ITrackRepository : IRepository<Track> { }

}
