using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PF2e_Kingmaker_Bot.KingmakerBusiness;

namespace PF2e_Kingmaker_Bot.Database;

public partial class KingmakerContext : DbContext
{
    public KingmakerContext()
    {
    }

    public KingmakerContext(DbContextOptions<KingmakerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Charter> Charters { get; set; }

    public virtual DbSet<Government> Governments { get; set; }

    public virtual DbSet<Heartland> Heartlands { get; set; }

    public virtual DbSet<Hex> Hexes { get; set; }

    public virtual DbSet<Kingdom> Kingdoms { get; set; }

    public virtual DbSet<Leader> Leaders { get; set; }

    public virtual DbSet<LeaderType> LeaderTypes { get; set; }

    public virtual DbSet<RessourceType> RessourceTypes { get; set; }

    public virtual DbSet<Settlement> Settlements { get; set; }

    public virtual DbSet<SettlementType> SettlementTypes { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillTraining> SkillTrainings { get; set; }

    public virtual DbSet<SkillTrainingType> SkillTrainingTypes { get; set; }

    public virtual DbSet<Structure> Structures { get; set; }

    public virtual DbSet<StructureType> StructureTypes { get; set; }

    public virtual DbSet<TerrainFeature> TerrainFeatures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(getSQLServerConnectionString());

    public string getSQLServerConnectionString()
    {
        string connectionString;

        try
        {
            connectionString = File.ReadAllText("./Secrets/DatabaseConnection.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading token file: {ex.Message}");
            return "";
        }

        return connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Charter>(entity =>
        {
            entity.HasKey(e => e.IDCharter);

            entity.ToTable("Charter");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Government>(entity =>
        {
            entity.HasKey(e => e.IDGovernment);

            entity.ToTable("Government");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Heartland>(entity =>
        {
            entity.HasKey(e => e.IDHeartland);

            entity.ToTable("Heartland");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Hex>(entity =>
        {
            entity.HasKey(e => e.IDHex);

            entity.ToTable("Hex");

            entity.HasOne(d => d.IDHeartlandNavigation).WithMany(p => p.Hexes)
                .HasForeignKey(d => d.IDHeartland)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hex_Heartland");

            entity.HasOne(d => d.IDKingdomNavigation).WithMany(p => p.Hexes)
                .HasForeignKey(d => d.IDKingdom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hex_Kingdom");

            entity.HasOne(d => d.IDRessourceTypeNavigation).WithMany(p => p.Hexes)
                .HasForeignKey(d => d.IDRessourceType)
                .HasConstraintName("FK_Hex_RessourceType");

            entity.HasOne(d => d.IDTerrainFeatureNavigation).WithMany(p => p.Hexes)
                .HasForeignKey(d => d.IDTerrainFeature)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hex_TerrainFeature");
        });

        modelBuilder.Entity<Kingdom>(entity =>
        {
            entity.HasKey(e => e.IDKingdom);

            entity.ToTable("Kingdom");

            entity.Property(e => e.KingdomLevel).HasDefaultValueSql("((1))");
            entity.Property(e => e.KingdomName).HasMaxLength(500);
            entity.Property(e => e.ServerChannelName).HasMaxLength(500);
            entity.Property(e => e.PlayersLevel).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.IDCharterNavigation).WithMany(p => p.Kingdoms)
                .HasForeignKey(d => d.IDCharter)
                .HasConstraintName("FK_Kingdom_Charter");

            entity.HasOne(d => d.IDGovernmentNavigation).WithMany(p => p.Kingdoms)
                .HasForeignKey(d => d.IDGovernment)
                .HasConstraintName("FK_Kingdom_Government");

            entity.HasOne(d => d.IDHeartlandNavigation).WithMany(p => p.Kingdoms)
                .HasForeignKey(d => d.IDHeartland)
                .HasConstraintName("FK_Kingdom_Heartland");
        });

        modelBuilder.Entity<Leader>(entity =>
        {
            entity.HasKey(e => e.IDLeader);

            entity.ToTable("Leader");

            entity.Property(e => e.IsPlayerCharacter)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(500);

            entity.HasOne(d => d.IDKingdomNavigation).WithMany(p => p.Leaders2)
                .HasForeignKey(d => d.IDKingdom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leader_Kingdom");

            entity.HasOne(d => d.IDLeaderTypeNavigation).WithMany(p => p.Leaders)
                .HasForeignKey(d => d.IDLeaderType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leader_LeaderType");
        });

        modelBuilder.Entity<LeaderType>(entity =>
        {
            entity.HasKey(e => e.IDLeaderType);

            entity.ToTable("LeaderType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<RessourceType>(entity =>
        {
            entity.HasKey(e => e.IDRessourceType);

            entity.ToTable("RessourceType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Settlement>(entity =>
        {
            entity.HasKey(e => e.IDSettlement);

            entity.ToTable("Settlement");

            entity.HasOne(d => d.IDHexNavigation).WithMany(p => p.Settlements)
                .HasForeignKey(d => d.IDHex)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settlement_Hex");

            entity.HasOne(d => d.IDKingdomNavigation).WithMany(p => p.Settlements)
                .HasForeignKey(d => d.IDKingdom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settlement_Kingdom");

            entity.HasOne(d => d.IDSettlementTypeNavigation).WithMany(p => p.Settlements)
                .HasForeignKey(d => d.IDSettlementType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settlement_SettlementType");
        });

        modelBuilder.Entity<SettlementType>(entity =>
        {
            entity.HasKey(e => e.IDSettlementType);

            entity.ToTable("SettlementType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.IDSkill).HasName("PK_Skill1");

            entity.ToTable("Skill");

            entity.Property(e => e.SkillName).HasMaxLength(50);
        });

        modelBuilder.Entity<SkillTraining>(entity =>
        {
            entity.HasKey(e => e.IDSkillTraining).HasName("PK_Skill");

            entity.ToTable("SkillTraining");

            entity.HasOne(d => d.IDKingdomNavigation).WithMany(p => p.SkillTrainings2)
                .HasForeignKey(d => d.IDKingdom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Skill_Kingdom");

            entity.HasOne(d => d.IDSkillNavigation).WithMany(p => p.SkillTrainings)
                .HasForeignKey(d => d.IDSkill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SkillTraining_Skill");

            entity.HasOne(d => d.IDSkillTrainingTypeNavigation).WithMany(p => p.SkillTrainings)
                .HasForeignKey(d => d.IDSkillTrainingType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SkillTraining_SkillTrainingType");
        });

        modelBuilder.Entity<SkillTrainingType>(entity =>
        {
            entity.HasKey(e => e.IDSkillTrainingType).HasName("PK_SkillTraining");

            entity.ToTable("SkillTrainingType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Structure>(entity =>
        {
            entity.HasKey(e => e.IDStructure);

            entity.ToTable("Structure");

            entity.HasOne(d => d.IDSettlementNavigation).WithMany(p => p.Structures)
                .HasForeignKey(d => d.IDSettlement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Structure_Settlement");

            entity.HasOne(d => d.IDStructureTypeNavigation).WithMany(p => p.Structures)
                .HasForeignKey(d => d.IDStructureType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Structure_StructureType");
        });

        modelBuilder.Entity<StructureType>(entity =>
        {
            entity.HasKey(e => e.IDStructureType);

            entity.ToTable("StructureType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TerrainFeature>(entity =>
        {
            entity.HasKey(e => e.IDTerrainFeature);

            entity.ToTable("TerrainFeature");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
