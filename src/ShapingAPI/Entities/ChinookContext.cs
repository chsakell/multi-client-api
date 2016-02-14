using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace ShapingAPI.Entities
{
    public partial class ChinookContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlServer(@"Server=(localdb)\v11.0;Database=Chinook;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasIndex(e => e.ArtistId).HasName("IFK_AlbumArtistId");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.HasOne(d => d.Artist).WithMany(p => p.Album).HasForeignKey(d => d.ArtistId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.SupportRepId).HasName("IFK_CustomerSupportRepId");

                entity.Property(e => e.Address).HasMaxLength(70);

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Company).HasMaxLength(80);

                entity.Property(e => e.Country).HasMaxLength(40);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(40);

                entity.HasOne(d => d.SupportRep).WithMany(p => p.Customer).HasForeignKey(d => d.SupportRepId);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.ReportsTo).HasName("IFK_EmployeeReportsTo");

                entity.Property(e => e.Address).HasMaxLength(70);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Country).HasMaxLength(40);

                entity.Property(e => e.Email).HasMaxLength(60);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.State).HasMaxLength(40);

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation).HasForeignKey(d => d.ReportsTo);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(e => e.CustomerId).HasName("IFK_InvoiceCustomerId");

                entity.Property(e => e.BillingAddress).HasMaxLength(70);

                entity.Property(e => e.BillingCity).HasMaxLength(40);

                entity.Property(e => e.BillingCountry).HasMaxLength(40);

                entity.Property(e => e.BillingPostalCode).HasMaxLength(10);

                entity.Property(e => e.BillingState).HasMaxLength(40);

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("numeric");

                entity.HasOne(d => d.Customer).WithMany(p => p.Invoice).HasForeignKey(d => d.CustomerId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<InvoiceLine>(entity =>
            {
                entity.HasIndex(e => e.InvoiceId).HasName("IFK_InvoiceLineInvoiceId");

                entity.HasIndex(e => e.TrackId).HasName("IFK_InvoiceLineTrackId");

                entity.Property(e => e.UnitPrice).HasColumnType("numeric");

                entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLine).HasForeignKey(d => d.InvoiceId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Track).WithMany(p => p.InvoiceLine).HasForeignKey(d => d.TrackId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<PlaylistTrack>(entity =>
            {
                entity.HasKey(e => new { e.PlaylistId, e.TrackId });

                entity.HasIndex(e => e.TrackId).HasName("IFK_PlaylistTrackTrackId");

                entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistTrack).HasForeignKey(d => d.PlaylistId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Track).WithMany(p => p.PlaylistTrack).HasForeignKey(d => d.TrackId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasIndex(e => e.AlbumId).HasName("IFK_TrackAlbumId");

                entity.HasIndex(e => e.GenreId).HasName("IFK_TrackGenreId");

                entity.HasIndex(e => e.MediaTypeId).HasName("IFK_TrackMediaTypeId");

                entity.Property(e => e.Composer).HasMaxLength(220);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UnitPrice).HasColumnType("numeric");

                entity.HasOne(d => d.Album).WithMany(p => p.Track).HasForeignKey(d => d.AlbumId);

                entity.HasOne(d => d.Genre).WithMany(p => p.Track).HasForeignKey(d => d.GenreId);

                entity.HasOne(d => d.MediaType).WithMany(p => p.Track).HasForeignKey(d => d.MediaTypeId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<sysdiagrams>(entity =>
            {
                entity.HasKey(e => e.diagram_id);

                entity.Property(e => e.definition).HasColumnType("varbinary");
            });
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLine { get; set; }
        public virtual DbSet<MediaType> MediaType { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistTrack> PlaylistTrack { get; set; }
        public virtual DbSet<Track> Track { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}