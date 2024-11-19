using System;
using System.Collections.Generic;
using Health.Models;
using Microsoft.EntityFrameworkCore;

namespace Health;

public partial class HealthContext : DbContext
{
    public HealthContext()
    {
    }

    public HealthContext(DbContextOptions<HealthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ClientsCard> ClientsCards { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorService> DoctorServices { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<PersonInfo> PersonInfos { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost\\DEV;Database=health;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppId).HasName("PK__appointm__6F8A0A344CF798CF");

            entity.ToTable("appointment");

            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.AppDate).HasColumnName("app_date");
            entity.Property(e => e.ClientCardId).HasColumnName("client_card_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Course)
                .IsUnicode(false)
                .HasColumnName("course");
            entity.Property(e => e.DiagId).HasColumnName("diag_id");
            entity.Property(e => e.DocServId).HasColumnName("doc_serv_id");

            entity.HasOne(d => d.ClientCard).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClientCardId)
                .HasConstraintName("FK__appointme__clien__5629CD9C");

            entity.HasOne(d => d.Diag).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DiagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__appointme__diag___5812160E");

            entity.HasOne(d => d.DocServ).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DocServId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__appointme__doc_s__571DF1D5");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__categori__DD5DDDBD4DB03B38");

            entity.ToTable("categories");

            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.CatName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cat_name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<ClientsCard>(entity =>
        {
            entity.HasKey(e => e.ClientCardId).HasName("PK__clients___7459A1D45D8C2643");

            entity.ToTable("clients_cards");

            entity.HasIndex(e => e.CardNum, "UQ__clients___20E55F1C00169CB4").IsUnique();

            entity.HasIndex(e => e.PersonInfoId, "UQ__clients___ECE7B8BF1B1C8731").IsUnique();

            entity.Property(e => e.ClientCardId).HasColumnName("client_card_id");
            entity.Property(e => e.CardNum)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("card_num");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.PersonInfoId).HasColumnName("person_info_id");

            entity.HasOne(d => d.PersonInfo).WithOne(p => p.ClientsCard)
                .HasForeignKey<ClientsCard>(d => d.PersonInfoId)
                .HasConstraintName("FK__clients_c__perso__4AB81AF0");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.DiagId).HasName("PK__diagnosi__B24842DBC08905BE");

            entity.ToTable("diagnosis");

            entity.Property(e => e.DiagId).HasColumnName("diag_id");
            entity.Property(e => e.DiagName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("diag_name");
            entity.Property(e => e.MkbCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("mkb_code");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("PK__doctors__8AD029241BEEA753");

            entity.ToTable("doctors");

            entity.HasIndex(e => e.PersonInfoId, "UQ__doctors__ECE7B8BFB64BD201").IsUnique();

            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.PersonInfoId).HasColumnName("person_info_id");
            entity.Property(e => e.SpecId).HasColumnName("spec_id");

            entity.HasOne(d => d.Cat).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__doctors__cat_id__3F466844");

            entity.HasOne(d => d.PersonInfo).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.PersonInfoId)
                .HasConstraintName("FK__doctors__person___3E52440B");

            entity.HasOne(d => d.Spec).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.SpecId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__doctors__spec_id__3D5E1FD2");
        });

        modelBuilder.Entity<DoctorService>(entity =>
        {
            entity.HasKey(e => e.DocServId).HasName("PK__doctor_s__5AC797AE73155CA0");

            entity.ToTable("doctor_services");

            entity.Property(e => e.DocServId).HasColumnName("doc_serv_id");
            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ServId).HasColumnName("serv_id");

            entity.HasOne(d => d.Doc).WithMany(p => p.DoctorServices)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("FK__doctor_se__doc_i__44FF419A");

            entity.HasOne(d => d.Serv).WithMany(p => p.DoctorServices)
                .HasForeignKey(d => d.ServId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__doctor_se__serv___45F365D3");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.DocId).HasName("PK__logins__8AD0292421B59E92");

            entity.ToTable("logins");

            entity.HasIndex(e => e.Log, "UQ__logins__7838F272EFD6EE93").IsUnique();

            entity.Property(e => e.DocId)
                .ValueGeneratedNever()
                .HasColumnName("doc_id");
            entity.Property(e => e.Log)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.Doc).WithOne(p => p.Login)
                .HasForeignKey<Login>(d => d.DocId)
                .HasConstraintName("fk_doc_logins");
        });

        modelBuilder.Entity<PersonInfo>(entity =>
        {
            entity.HasKey(e => e.PersonInfoId).HasName("PK__person_i__ECE7B8BEFFE5C5F9");

            entity.ToTable("person_info");

            entity.Property(e => e.PersonInfoId).HasColumnName("person_info_id");
            entity.Property(e => e.Adress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("adress");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.FName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("f_name");
            entity.Property(e => e.FatherName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("father_name");
            entity.Property(e => e.PassNum)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pass_num");
            entity.Property(e => e.PassSeries)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pass_series");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.SName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("s_name");
            entity.Property(e => e.Sex).HasColumnName("sex");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServId).HasName("PK__services__1AFA505C372FB4D2");

            entity.ToTable("services");

            entity.Property(e => e.ServId).HasColumnName("serv_id");
            entity.Property(e => e.ServName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("serv_name");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecId).HasName("PK__speciali__F670C567B2C6004D");

            entity.ToTable("specializations");

            entity.Property(e => e.SpecId).HasColumnName("spec_id");
            entity.Property(e => e.SpecName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("spec_name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__ticket__D596F96BE2267566");

            entity.ToTable("ticket");

            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.AppDate)
                .HasColumnType("datetime")
                .HasColumnName("app_date");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.ClientCardId).HasColumnName("client_card_id");
            entity.Property(e => e.FName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("f_name");
            entity.Property(e => e.PassNum)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pass_num");
            entity.Property(e => e.PassSeries)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pass_series");
            entity.Property(e => e.FatherName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("father_name");
            entity.Property(e => e.IssueDate)
                .HasColumnType("datetime")
                .HasColumnName("issue_date");
            entity.Property(e => e.SName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("s_name");
            entity.Property(e => e.SpecId).HasColumnName("spec_id");

            entity.HasOne(d => d.ClientCard).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ClientCardId)
                .HasConstraintName("FK__ticket__client_c__4E88ABD4");

            entity.HasOne(d => d.Spec).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SpecId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ticket__spec_id__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
