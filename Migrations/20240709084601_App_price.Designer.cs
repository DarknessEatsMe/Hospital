﻿// <auto-generated />
using System;
using Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Health.Migrations
{
    [DbContext(typeof(HealthContext))]
    [Migration("20240709084601_App_price")]
    partial class App_price
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Health.Models.Appointment", b =>
                {
                    b.Property<int>("AppId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("app_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppId"));

                    b.Property<DateOnly>("AppDate")
                        .HasColumnType("date")
                        .HasColumnName("app_date");

                    b.Property<int>("ClientCardId")
                        .HasColumnType("int")
                        .HasColumnName("client_card_id");

                    b.Property<string>("Course")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("course");

                    b.Property<int>("DiagId")
                        .HasColumnType("int")
                        .HasColumnName("diag_id");

                    b.Property<int>("DocServId")
                        .HasColumnType("int")
                        .HasColumnName("doc_serv_id");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.HasKey("AppId")
                        .HasName("PK__appointm__6F8A0A344CF798CF");

                    b.HasIndex("ClientCardId");

                    b.HasIndex("DiagId");

                    b.HasIndex("DocServId");

                    b.ToTable("appointment", (string)null);
                });

            modelBuilder.Entity("Health.Models.Category", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cat_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatId"));

                    b.Property<string>("CatName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("cat_name");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.HasKey("CatId")
                        .HasName("PK__categori__DD5DDDBD4DB03B38");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Health.Models.ClientsCard", b =>
                {
                    b.Property<int>("ClientCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("client_card_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientCardId"));

                    b.Property<string>("CardNum")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("card_num");

                    b.Property<DateOnly>("CreationDate")
                        .HasColumnType("date")
                        .HasColumnName("creation_date");

                    b.Property<int>("PersonInfoId")
                        .HasColumnType("int")
                        .HasColumnName("person_info_id");

                    b.HasKey("ClientCardId")
                        .HasName("PK__clients___7459A1D45D8C2643");

                    b.HasIndex(new[] { "CardNum" }, "UQ__clients___20E55F1C00169CB4")
                        .IsUnique();

                    b.HasIndex(new[] { "PersonInfoId" }, "UQ__clients___ECE7B8BF1B1C8731")
                        .IsUnique();

                    b.ToTable("clients_cards", (string)null);
                });

            modelBuilder.Entity("Health.Models.Diagnosis", b =>
                {
                    b.Property<int>("DiagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("diag_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiagId"));

                    b.Property<string>("DiagName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("diag_name");

                    b.Property<string>("MkbCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("char(3)")
                        .HasColumnName("mkb_code")
                        .IsFixedLength();

                    b.HasKey("DiagId")
                        .HasName("PK__diagnosi__B24842DBC08905BE");

                    b.ToTable("diagnosis", (string)null);
                });

            modelBuilder.Entity("Health.Models.Doctor", b =>
                {
                    b.Property<int>("DocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("doc_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocId"));

                    b.Property<int>("CatId")
                        .HasColumnType("int")
                        .HasColumnName("cat_id");

                    b.Property<int>("PersonInfoId")
                        .HasColumnType("int")
                        .HasColumnName("person_info_id");

                    b.Property<int>("SpecId")
                        .HasColumnType("int")
                        .HasColumnName("spec_id");

                    b.HasKey("DocId")
                        .HasName("PK__doctors__8AD029241BEEA753");

                    b.HasIndex("CatId");

                    b.HasIndex("SpecId");

                    b.HasIndex(new[] { "PersonInfoId" }, "UQ__doctors__ECE7B8BFB64BD201")
                        .IsUnique();

                    b.ToTable("doctors", (string)null);
                });

            modelBuilder.Entity("Health.Models.DoctorService", b =>
                {
                    b.Property<int>("DocServId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("doc_serv_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocServId"));

                    b.Property<int>("DocId")
                        .HasColumnType("int")
                        .HasColumnName("doc_id");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.Property<int>("ServId")
                        .HasColumnType("int")
                        .HasColumnName("serv_id");

                    b.HasKey("DocServId")
                        .HasName("PK__doctor_s__5AC797AE73155CA0");

                    b.HasIndex("DocId");

                    b.HasIndex("ServId");

                    b.ToTable("doctor_services", (string)null);
                });

            modelBuilder.Entity("Health.Models.Login", b =>
                {
                    b.Property<int>("DocId")
                        .HasColumnType("int")
                        .HasColumnName("doc_id");

                    b.Property<string>("Log")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("password");

                    b.HasKey("DocId")
                        .HasName("PK__logins__8AD0292421B59E92");

                    b.HasIndex(new[] { "Log" }, "UQ__logins__7838F272EFD6EE93")
                        .IsUnique();

                    b.ToTable("logins", (string)null);
                });

            modelBuilder.Entity("Health.Models.PersonInfo", b =>
                {
                    b.Property<int>("PersonInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("person_info_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonInfoId"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("adress");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<byte?>("Discount")
                        .HasColumnType("tinyint")
                        .HasColumnName("discount");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("f_name");

                    b.Property<string>("FatherName")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("father_name");

                    b.Property<string>("PassNum")
                        .IsRequired()
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("char(4)")
                        .HasColumnName("pass_num")
                        .IsFixedLength();

                    b.Property<string>("PassSeries")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("char(6)")
                        .HasColumnName("pass_series")
                        .IsFixedLength();

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("photo");

                    b.Property<string>("SName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("s_name");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit")
                        .HasColumnName("sex");

                    b.HasKey("PersonInfoId")
                        .HasName("PK__person_i__ECE7B8BEFFE5C5F9");

                    b.ToTable("person_info", (string)null);
                });

            modelBuilder.Entity("Health.Models.Service", b =>
                {
                    b.Property<int>("ServId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("serv_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServId"));

                    b.Property<string>("ServName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("serv_name");

                    b.HasKey("ServId")
                        .HasName("PK__services__1AFA505C372FB4D2");

                    b.ToTable("services", (string)null);
                });

            modelBuilder.Entity("Health.Models.Specialization", b =>
                {
                    b.Property<int>("SpecId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("spec_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecId"));

                    b.Property<string>("SpecName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("spec_name");

                    b.HasKey("SpecId")
                        .HasName("PK__speciali__F670C567B2C6004D");

                    b.ToTable("specializations", (string)null);
                });

            modelBuilder.Entity("Health.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ticket_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<DateTime>("AppDate")
                        .HasColumnType("datetime")
                        .HasColumnName("app_date");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date")
                        .HasColumnName("birthday");

                    b.Property<int?>("ClientCardId")
                        .HasColumnType("int")
                        .HasColumnName("client_card_id");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("f_name");

                    b.Property<string>("FatherName")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("father_name");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime")
                        .HasColumnName("issue_date");

                    b.Property<string>("PassNum")
                        .IsRequired()
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("char(4)")
                        .HasColumnName("pass_num")
                        .IsFixedLength();

                    b.Property<string>("PassSeries")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("char(6)")
                        .HasColumnName("pass_series")
                        .IsFixedLength();

                    b.Property<string>("SName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("s_name");

                    b.Property<int>("SpecId")
                        .HasColumnType("int")
                        .HasColumnName("spec_id");

                    b.HasKey("TicketId")
                        .HasName("PK__ticket__D596F96BE2267566");

                    b.HasIndex("ClientCardId");

                    b.HasIndex("SpecId");

                    b.ToTable("ticket", (string)null);
                });

            modelBuilder.Entity("Health.Models.Appointment", b =>
                {
                    b.HasOne("Health.Models.ClientsCard", "ClientCard")
                        .WithMany("Appointments")
                        .HasForeignKey("ClientCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__appointme__clien__5629CD9C");

                    b.HasOne("Health.Models.Diagnosis", "Diag")
                        .WithMany("Appointments")
                        .HasForeignKey("DiagId")
                        .IsRequired()
                        .HasConstraintName("FK__appointme__diag___5812160E");

                    b.HasOne("Health.Models.DoctorService", "DocServ")
                        .WithMany("Appointments")
                        .HasForeignKey("DocServId")
                        .IsRequired()
                        .HasConstraintName("FK__appointme__doc_s__571DF1D5");

                    b.Navigation("ClientCard");

                    b.Navigation("Diag");

                    b.Navigation("DocServ");
                });

            modelBuilder.Entity("Health.Models.ClientsCard", b =>
                {
                    b.HasOne("Health.Models.PersonInfo", "PersonInfo")
                        .WithOne("ClientsCard")
                        .HasForeignKey("Health.Models.ClientsCard", "PersonInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__clients_c__perso__4AB81AF0");

                    b.Navigation("PersonInfo");
                });

            modelBuilder.Entity("Health.Models.Doctor", b =>
                {
                    b.HasOne("Health.Models.Category", "Cat")
                        .WithMany("Doctors")
                        .HasForeignKey("CatId")
                        .IsRequired()
                        .HasConstraintName("FK__doctors__cat_id__3F466844");

                    b.HasOne("Health.Models.PersonInfo", "PersonInfo")
                        .WithOne("Doctor")
                        .HasForeignKey("Health.Models.Doctor", "PersonInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__doctors__person___3E52440B");

                    b.HasOne("Health.Models.Specialization", "Spec")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecId")
                        .IsRequired()
                        .HasConstraintName("FK__doctors__spec_id__3D5E1FD2");

                    b.Navigation("Cat");

                    b.Navigation("PersonInfo");

                    b.Navigation("Spec");
                });

            modelBuilder.Entity("Health.Models.DoctorService", b =>
                {
                    b.HasOne("Health.Models.Doctor", "Doc")
                        .WithMany("DoctorServices")
                        .HasForeignKey("DocId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__doctor_se__doc_i__44FF419A");

                    b.HasOne("Health.Models.Service", "Serv")
                        .WithMany("DoctorServices")
                        .HasForeignKey("ServId")
                        .IsRequired()
                        .HasConstraintName("FK__doctor_se__serv___45F365D3");

                    b.Navigation("Doc");

                    b.Navigation("Serv");
                });

            modelBuilder.Entity("Health.Models.Login", b =>
                {
                    b.HasOne("Health.Models.Doctor", "Doc")
                        .WithOne("Login")
                        .HasForeignKey("Health.Models.Login", "DocId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_doc_logins");

                    b.Navigation("Doc");
                });

            modelBuilder.Entity("Health.Models.Ticket", b =>
                {
                    b.HasOne("Health.Models.ClientsCard", "ClientCard")
                        .WithMany("Tickets")
                        .HasForeignKey("ClientCardId")
                        .HasConstraintName("FK__ticket__client_c__4E88ABD4");

                    b.HasOne("Health.Models.Specialization", "Spec")
                        .WithMany("Tickets")
                        .HasForeignKey("SpecId")
                        .IsRequired()
                        .HasConstraintName("FK__ticket__spec_id__4D94879B");

                    b.Navigation("ClientCard");

                    b.Navigation("Spec");
                });

            modelBuilder.Entity("Health.Models.Category", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("Health.Models.ClientsCard", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Health.Models.Diagnosis", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Health.Models.Doctor", b =>
                {
                    b.Navigation("DoctorServices");

                    b.Navigation("Login");
                });

            modelBuilder.Entity("Health.Models.DoctorService", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Health.Models.PersonInfo", b =>
                {
                    b.Navigation("ClientsCard");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Health.Models.Service", b =>
                {
                    b.Navigation("DoctorServices");
                });

            modelBuilder.Entity("Health.Models.Specialization", b =>
                {
                    b.Navigation("Doctors");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
