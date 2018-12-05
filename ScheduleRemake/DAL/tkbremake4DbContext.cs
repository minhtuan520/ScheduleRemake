using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Models.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL
{
    public partial class tkbremake4DbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public tkbremake4DbContext()
        {
        }

        public tkbremake4DbContext(DbContextOptions<tkbremake4DbContext> options)
            : base(options)
        {
        }
        public string CurrentUserId { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Change> Change { get; set; }
        public virtual DbSet<Dieukien> Dieukien { get; set; }
        public virtual DbSet<Giaovien> Giaovien { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Lop> Lop { get; set; }
        public virtual DbSet<Monhoc> Monhoc { get; set; }
        public virtual DbSet<Phancong> Phancong { get; set; }
        public virtual DbSet<Tkb> Tkb { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=tkbremake4Db;Integrated Security=True");
        //            }
        //        }
        #region Configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.User);

                entity.ToTable("ACCOUNT");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Inf)
                    .HasColumnName("INF")
                    .HasMaxLength(132);

                entity.Property(e => e.Pass)
                    .HasColumnName("PASS")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Change>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Time, e.Action, e.Target });

                entity.ToTable("CHANGE");

                entity.Property(e => e.User)
                    .HasColumnName("USER")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .HasColumnName("TIME")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Action)
                    .HasColumnName("ACTION")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Target)
                    .HasColumnName("TARGET")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dieukien>(entity =>
            {
                entity.HasKey(e => new { e.Gv, e.Mh, e.L, e.Tiet, e.Thu });

                entity.ToTable("DIEUKIEN");

                entity.Property(e => e.Gv)
                    .HasColumnName("GV")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Mh)
                    .HasColumnName("MH")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.L)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Tiet).HasColumnName("TIET");

                entity.Property(e => e.Thu).HasColumnName("THU");
            });

            modelBuilder.Entity<Giaovien>(entity =>
            {
                entity.HasKey(e => e.Gv);

                entity.ToTable("GIAOVIEN");

                entity.Property(e => e.Gv)
                    .HasColumnName("GV")
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.Ho)
                    .HasColumnName("HO")
                    .HasMaxLength(30);

                entity.Property(e => e.Inf)
                    .HasColumnName("INF")
                    .HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .HasColumnName("TEN")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Hieuluc, e.Gv, e.Mh, e.L, e.Tiet });

                entity.ToTable("LOG");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Hieuluc)
                    .HasColumnName("HIEULUC")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Gv)
                    .HasColumnName("GV")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Mh)
                    .HasColumnName("MH")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.L)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Tiet).HasColumnName("TIET");
            });

            modelBuilder.Entity<Lop>(entity =>
            {
                entity.HasKey(e => e.L);

                entity.ToTable("LOP");

                entity.Property(e => e.L)
                    .HasMaxLength(5)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Monhoc>(entity =>
            {
                entity.HasKey(e => e.Mh);

                entity.ToTable("MONHOC");

                entity.Property(e => e.Mh)
                    .HasColumnName("MH")
                    .HasMaxLength(5)
                    .ValueGeneratedNever();

                entity.Property(e => e.Cb).HasColumnName("CB");

                entity.Property(e => e.Lap).HasColumnName("LAP");

                entity.Property(e => e.Ten)
                    .HasColumnName("TEN")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Phancong>(entity =>
            {
                entity.HasKey(e => new { e.Gv, e.Mh, e.L });

                entity.ToTable("PHANCONG");

                entity.Property(e => e.Gv)
                    .HasColumnName("GV")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Mh)
                    .HasColumnName("MH")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.L)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tkb>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Hieuluc, e.Gv, e.Mh, e.L, e.Tiet, e.Thu });

                entity.ToTable("TKB");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Hieuluc)
                    .HasColumnName("HIEULUC")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Gv)
                    .HasColumnName("GV")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Mh)
                    .HasColumnName("MH")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.L)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Tiet).HasColumnName("TIET");

                entity.Property(e => e.Thu).HasColumnName("THU");
            });
        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                DateTime now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = CurrentUserId;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = CurrentUserId;
            }
        } 
        #endregion
    }
}
