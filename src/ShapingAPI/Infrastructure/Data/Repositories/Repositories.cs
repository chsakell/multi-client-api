using Microsoft.Data.Entity;
using ShapingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapingAPI.Infrastructure.Data.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(ChinookContext context)
            : base(context)
        { }

        public IEnumerable<Album> LoadAll()
        {
            IQueryable<Album> query = this._dbSet;

            query = query.Include(a => a.Track);

            return query.ToList();
        }

        public Album Load(int artistId)
        {
            IQueryable<Album> query = this._dbSet;

            query = query.Include(a => a.Track);

            return query.FirstOrDefault(a => a.AlbumId == artistId);
        }
    }

    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(ChinookContext context)
            : base(context)
        {

        }

        public IEnumerable<Artist> LoadAll()
        {
            IQueryable<Artist> query = this._dbSet;

            query = query.Include(a => a.Album).ThenInclude(al => al.Track);

            return query.ToList();
        }

        public Artist Load(int artistId)
        {
            IQueryable<Artist> query = this._dbSet;

            query = query.Include(a => a.Album).ThenInclude(al => al.Track);

            return query.FirstOrDefault(a => a.ArtistId == artistId);
        }
    }

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ChinookContext context)
            : base(context)
        { }

        public IEnumerable<Customer> LoadAll()
        {
            IQueryable<Customer> query = this._dbSet;

            query = query.Include(c => c.Invoice).ThenInclude(i => i.InvoiceLine);

            return query.ToList();
        }

        public Customer Load(int customerId)
        {
            IQueryable<Customer> query = this._dbSet;

            query = query.Include(c => c.Invoice).ThenInclude(i => i.InvoiceLine);

            return query.FirstOrDefault(c => c.CustomerId == customerId);
        }
    }

    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ChinookContext context)
            : base(context)
        { }
    }

    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(ChinookContext context)
            : base(context)
        { }
    }

    public class InvoiceLineRepository : Repository<InvoiceLine>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(ChinookContext context)
            : base(context)
        { }
    }

    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ChinookContext context)
            : base(context)
        { }

        public IEnumerable<Invoice> LoadAll()
        {
            IQueryable<Invoice> query = this._dbSet;

            query = query.Include(i => i.Customer).ThenInclude(c => c.Invoice);
            query = query.Include(i => i.InvoiceLine);

            return query.ToList();
        }

        public Invoice Load(int invoiceId)
        {
            IQueryable<Invoice> query = this._dbSet;

            query = query.Include(i => i.Customer).ThenInclude(c => c.Invoice);
            query = query.Include(i => i.InvoiceLine);

            return query.FirstOrDefault(i => i.InvoiceId == invoiceId);
        }
    }

    public class MediaTypeRepository : Repository<MediaType>, IMediaTypeRepository
    {
        public MediaTypeRepository(ChinookContext context)
            : base(context)
        { }
    }

    public class PlaylistTrackRepository : Repository<PlaylistTrack>, IPlaylistTrackRepository
    {
        public PlaylistTrackRepository(ChinookContext context)
            : base(context)
        { }
    }

    public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(ChinookContext context)
            : base(context)
        { }
    }

    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        public TrackRepository(ChinookContext context)
            : base(context)
        { }
    }
}