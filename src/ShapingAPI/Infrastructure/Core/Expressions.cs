using ShapingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShapingAPI.Infrastructure.Core
{
    public class Expressions
    {
        public static Expression<Func<Track, object>>[] LoadTrackNavigations()
        {
            Expression<Func<Track, object>>[] _navigations = {
                    t => t.Album,
                    t => t.Genre,
                    t => t.InvoiceLine,
                    t => t.MediaType
            };

            return _navigations;
        }

        public static Expression<Func<Customer, object>>[] LoadCustomerNavigations()
        {
            Expression<Func<Customer, object>>[] _navigations = {
                         c => c.Invoice,
                         c => c.SupportRep
             };

            return _navigations;
        }

        public static Expression<Func<Album, object>>[] LoadAlbumNavigations()
        {
            Expression<Func<Album, object>>[] _navigations = {
                         a => a.Track,
                         a => a.Artist
             };

            return _navigations;
        }

        public static Expression<Func<Artist, object>>[] LoadArtistNavigations()
        {
            Expression<Func<Artist, object>>[] _navigations = {
                         a => a.Album
             };

            return _navigations;
        }
    }
}
