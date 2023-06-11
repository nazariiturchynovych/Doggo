﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Doggo.Infrastructure.Migrations
{
    using Persistence;

    [DbContext(typeof(DoggoDbContext))]
    [Migration("20230609125511_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Doggo.Domain.Entities.DogOwner.Dog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Age")
                        .HasColumnType("double precision");

                    b.Property<int>("DogOwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("JobRequestId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("DogOwnerId");

                    b.ToTable("Dogs", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.DogOwner.DogOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("District")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("District");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("DogOwners", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Job.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DogOwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("WalkerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DogOwnerId");

                    b.HasIndex("WalkerId");

                    b.ToTable("Jobs", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.Documents.PersonalIdentifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PersonalIdentifierType")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PersonalIdentifiers", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.JobRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ChangedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DogId")
                        .HasColumnType("integer");

                    b.Property<int>("DogOwnerId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPersonalIdentifierRequired")
                        .HasColumnType("boolean");

                    b.Property<int>("JobId")
                        .HasColumnType("integer");

                    b.Property<int>("RequiredAge")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DogId");

                    b.HasIndex("DogOwnerId");

                    b.HasIndex("JobId")
                        .IsUnique();

                    b.ToTable("JobRequests", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.Schedules.RequiredSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<TimeOnly?>("From")
                        .HasColumnType("time without time zone");

                    b.Property<bool>("IsRegular")
                        .HasColumnType("boolean");

                    b.Property<int>("JobRequestId")
                        .HasColumnType("integer");

                    b.Property<TimeOnly?>("To")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("JobRequestId")
                        .IsUnique();

                    b.ToTable("RequiredSchedules");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.Property<int>("RoleType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("FacebookAuth")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("GoogleAuth")
                        .HasColumnType("boolean");

                    b.Property<bool>("InstagramAuth")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserToken", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Name", "LoginProvider");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Walker.Schedule.PossibleSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("integer");

                    b.Property<TimeOnly?>("From")
                        .HasColumnType("time without time zone");

                    b.Property<TimeOnly?>("To")
                        .HasColumnType("time without time zone");

                    b.Property<int>("WalkerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WalkerId");

                    b.ToTable("PossibleSchedules", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Walker.Walker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Walkers", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.DogOwner.Dog", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.DogOwner.DogOwner", "DogOwner")
                        .WithMany("Dogs")
                        .HasForeignKey("DogOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DogOwner");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.DogOwner.DogOwner", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.User.User", "User")
                        .WithOne("DogOwner")
                        .HasForeignKey("Doggo.Domain.Entities.DogOwner.DogOwner", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Job.Job", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.DogOwner.DogOwner", "DogOwner")
                        .WithMany("Jobs")
                        .HasForeignKey("DogOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.Walker.Walker", "Walker")
                        .WithMany("Jobs")
                        .HasForeignKey("WalkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DogOwner");

                    b.Navigation("Walker");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.Documents.PersonalIdentifier", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.User.User", "User")
                        .WithOne("PersonalIdentifier")
                        .HasForeignKey("Doggo.Domain.Entities.JobRequest.Documents.PersonalIdentifier", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.JobRequest", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.DogOwner.Dog", "Dog")
                        .WithMany("JobRequest")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.DogOwner.DogOwner", "DogOwner")
                        .WithMany("JobRequests")
                        .HasForeignKey("DogOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.Job.Job", "Job")
                        .WithOne("JobRequest")
                        .HasForeignKey("Doggo.Domain.Entities.JobRequest.JobRequest", "JobId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("DogOwner");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.Schedules.RequiredSchedule", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.JobRequest.JobRequest", "JobRequest")
                        .WithOne("RequiredSchedule")
                        .HasForeignKey("Doggo.Domain.Entities.JobRequest.Schedules.RequiredSchedule", "JobRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobRequest");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserRole", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.User.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.User.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Walker.Schedule.PossibleSchedule", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.Walker.Walker", "Walker")
                        .WithMany("PossibleSchedules")
                        .HasForeignKey("WalkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Walker");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Walker.Walker", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.User.User", "User")
                        .WithOne("Walker")
                        .HasForeignKey("Doggo.Domain.Entities.Walker.Walker", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.DogOwner.Dog", b =>
                {
                    b.Navigation("JobRequest");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.DogOwner.DogOwner", b =>
                {
                    b.Navigation("Dogs");

                    b.Navigation("JobRequests");

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Job.Job", b =>
                {
                    b.Navigation("JobRequest")
                        .IsRequired();
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.JobRequest", b =>
                {
                    b.Navigation("RequiredSchedule")
                        .IsRequired();
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.User", b =>
                {
                    b.Navigation("DogOwner")
                        .IsRequired();

                    b.Navigation("PersonalIdentifier")
                        .IsRequired();

                    b.Navigation("UserRoles");

                    b.Navigation("Walker")
                        .IsRequired();
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Walker.Walker", b =>
                {
                    b.Navigation("Jobs");

                    b.Navigation("PossibleSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
