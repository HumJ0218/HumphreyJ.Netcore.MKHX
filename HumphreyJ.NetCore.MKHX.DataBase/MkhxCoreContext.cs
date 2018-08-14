using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HumphreyJ.NetCore.MKHX.DataBase
{
    public partial class MkhxCoreContext : DbContext
    {
        private string connectionString = "Data Source=127.0.0.1;Initial Catalog=MkhxCore;Persist Security Info=True;User ID=mkhxcore;Password=mkhxcore";

        public MkhxCoreContext()
        {
        }

        public MkhxCoreContext(string username,string password)
        {
            connectionString = $"Data Source=127.0.0.1;Initial Catalog=MkhxCore;Persist Security Info=True;User ID={username};Password={password}";
        }

        public MkhxCoreContext(DbContextOptions<MkhxCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AffectTypeContent> AffectTypeContent { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<BattleRecords> BattleRecords { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<GameDataFiles> GameDataFiles { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<PvCounter> PvCounter { get; set; }

        public virtual DbSet<V_Article> V_Article { get; set; }
        public virtual DbSet<V_BattleRecords> V_BattleRecords { get; set; }
        public virtual DbSet<V_DungeonDeck> V_DungeonDeck { get; set; }
        public virtual DbSet<V_ElementTowerEarth> V_ElementTowerEarth { get; set; }
        public virtual DbSet<V_ElementTowerFire> V_ElementTowerFire { get; set; }
        public virtual DbSet<V_ElementTowerWater> V_ElementTowerWater { get; set; }
        public virtual DbSet<V_ElementTowerWind> V_ElementTowerWind { get; set; }
        public virtual DbSet<V_GameData> V_GameData { get; set; }
        public virtual DbSet<V_HeroHp> V_HeroHp { get; set; }
        public virtual DbSet<V_JourneyDeck> V_JourneyDeck { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AffectTypeContent>(entity =>
            {
                entity.HasKey(e => e.AffectType);

                entity.Property(e => e.AffectType).ValueGeneratedNever();

                entity.Property(e => e.AffectValue1).IsRequired();

                entity.Property(e => e.AffectValue2).IsRequired();

                entity.Property(e => e.Desc).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccessTime)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EditTime)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Server)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Thumbnail)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<BattleRecords>(entity =>
            {
                entity.HasKey(e => e.BattleId);

                entity.Property(e => e.BattleId)
                    .HasMaxLength(64)
                    .ValueGeneratedNever();

                entity.Property(e => e.AttackPlayerCards).IsRequired();

                entity.Property(e => e.AttackPlayerHp).HasColumnName("AttackPlayerHP");

                entity.Property(e => e.AttackPlayerName).IsRequired();

                entity.Property(e => e.AttackPlayerRunes).IsRequired();

                entity.Property(e => e.CreateTime).HasColumnType("smalldatetime");

                entity.Property(e => e.DefendPlayerCards).IsRequired();

                entity.Property(e => e.DefendPlayerHp).HasColumnName("DefendPlayerHP");

                entity.Property(e => e.DefendPlayerName).IsRequired();

                entity.Property(e => e.DefendPlayerRunes).IsRequired();

                entity.Property(e => e.Remark).IsRequired();

                entity.Property(e => e.Server).IsRequired();
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasColumnName("IP")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReplayTime).HasColumnType("datetime");

                entity.Property(e => e.SendTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ua)
                    .IsRequired()
                    .HasColumnName("UA")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<GameDataFiles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Server)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasKey(e => e.Url);

                entity.Property(e => e.Url)
                    .HasMaxLength(1024)
                    .ValueGeneratedNever();

                entity.Property(e => e.Categoriy)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<PvCounter>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ip).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Type).IsRequired();

                entity.Property(e => e.Ua).IsRequired();
            });
        }
    }
}
