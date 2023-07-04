﻿// <auto-generated />
using System;
using Doggo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Doggo.Infrastructure.Migrations
{
    [DbContext(typeof(DoggoDbContext))]
    partial class DoggoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Doggo.Domain.Entities.Chat.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean")
                        .HasColumnName("is_private");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("chats", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Chat.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("ChangedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("changed_date");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid")
                        .HasColumnName("chat_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Chat.UserChat", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid")
                        .HasColumnName("chat_id");

                    b.HasKey("UserId", "ChatId");

                    b.HasIndex("ChatId");

                    b.ToTable("user_chats", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Dog.Dog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<double>("Age")
                        .HasColumnType("double precision")
                        .HasColumnName("age");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("DogOwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("dog_owner_id");

                    b.Property<Guid?>("JobRequestId")
                        .HasColumnType("uuid")
                        .HasColumnName("job_request_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double?>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.HasKey("Id");

                    b.HasIndex("DogOwnerId");

                    b.ToTable("dogs", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.DogOwner.DogOwner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("district");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("District");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("dog_owners", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Job.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("ChangedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("changed_date");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<Guid>("DogId")
                        .HasColumnType("uuid")
                        .HasColumnName("dog_id");

                    b.Property<Guid>("DogOwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("dog_owner_id");

                    b.Property<Guid>("JobRequestId")
                        .HasColumnType("uuid")
                        .HasColumnName("job_request_id");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric")
                        .HasColumnName("salary");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid>("WalkerId")
                        .HasColumnType("uuid")
                        .HasColumnName("walker_id");

                    b.HasKey("Id");

                    b.HasIndex("DogId");

                    b.HasIndex("DogOwnerId");

                    b.HasIndex("JobRequestId");

                    b.HasIndex("WalkerId");

                    b.ToTable("jobs", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.JobRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime?>("ChangedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("changed_date");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("DogId")
                        .HasColumnType("uuid")
                        .HasColumnName("dog_id");

                    b.Property<Guid>("DogOwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("dog_owner_id");

                    b.Property<bool>("HasAcceptedJob")
                        .HasColumnType("boolean")
                        .HasColumnName("has_accepted_job");

                    b.Property<bool>("IsPersonalIdentifierRequired")
                        .HasColumnType("boolean")
                        .HasColumnName("is_personal_identifier_required");

                    b.Property<decimal>("PaymentTo")
                        .HasColumnType("numeric")
                        .HasColumnName("payment_to");

                    b.Property<int>("RequiredAge")
                        .HasColumnType("integer")
                        .HasColumnName("required_age");

                    b.HasKey("Id");

                    b.HasIndex("DogId");

                    b.HasIndex("DogOwnerId");

                    b.ToTable("job_requests", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.Schedule.RequiredSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("From")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("from");

                    b.Property<bool>("IsRegular")
                        .HasColumnType("boolean")
                        .HasColumnName("is_regular");

                    b.Property<Guid>("JobRequestId")
                        .HasColumnType("uuid")
                        .HasColumnName("job_request_id");

                    b.Property<DateTime>("To")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("to");

                    b.HasKey("Id");

                    b.HasIndex("JobRequestId")
                        .IsUnique();

                    b.ToTable("required_schedules", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.Documents.PersonalIdentifier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("PersonalIdentifierType")
                        .HasColumnType("integer")
                        .HasColumnName("personal_identifier_type");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("personal_identifiers", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.ToTable("role_claims", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("FacebookAuth")
                        .HasColumnType("boolean")
                        .HasColumnName("facebook_auth");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<bool>("GoogleAuth")
                        .HasColumnType("boolean")
                        .HasColumnName("google_auth");

                    b.Property<bool>("InstagramAuth")
                        .HasColumnType("boolean")
                        .HasColumnName("instagram_auth");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean")
                        .HasColumnName("is_approved");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("user_claims", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("user_logins", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.UserToken", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("Name", "LoginProvider");

                    b.ToTable("user_tokens", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Walker.Schedule.PossibleSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("From")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("from");

                    b.Property<DateTime>("To")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("to");

                    b.Property<Guid>("WalkerId")
                        .HasColumnType("uuid")
                        .HasColumnName("walker_id");

                    b.HasKey("Id");

                    b.HasIndex("WalkerId");

                    b.ToTable("possible_schedules", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Walker.Walker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("about");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("skills");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("walkers", (string)null);
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Chat.Message", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.Chat.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.User.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Chat.UserChat", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.Chat.Chat", "Chat")
                        .WithMany("UserChats")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.User.User", "User")
                        .WithMany("UserChats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Dog.Dog", b =>
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
                    b.HasOne("Doggo.Domain.Entities.Dog.Dog", "Dog")
                        .WithMany()
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.DogOwner.DogOwner", "DogOwner")
                        .WithMany("Jobs")
                        .HasForeignKey("DogOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.JobRequest.JobRequest", "JobRequest")
                        .WithMany("Jobs")
                        .HasForeignKey("JobRequestId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.Walker.Walker", "Walker")
                        .WithMany("Jobs")
                        .HasForeignKey("WalkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("DogOwner");

                    b.Navigation("JobRequest");

                    b.Navigation("Walker");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.JobRequest", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.Dog.Dog", "Dog")
                        .WithMany("JobRequests")
                        .HasForeignKey("DogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doggo.Domain.Entities.DogOwner.DogOwner", "DogOwner")
                        .WithMany("JobRequests")
                        .HasForeignKey("DogOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dog");

                    b.Navigation("DogOwner");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.Schedule.RequiredSchedule", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.JobRequest.JobRequest", "JobRequest")
                        .WithOne("RequiredSchedule")
                        .HasForeignKey("Doggo.Domain.Entities.JobRequest.Schedule.RequiredSchedule", "JobRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobRequest");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.User.Documents.PersonalIdentifier", b =>
                {
                    b.HasOne("Doggo.Domain.Entities.User.User", "User")
                        .WithOne("PersonalIdentifier")
                        .HasForeignKey("Doggo.Domain.Entities.User.Documents.PersonalIdentifier", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("Doggo.Domain.Entities.Chat.Chat", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserChats");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.Dog.Dog", b =>
                {
                    b.Navigation("JobRequests");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.DogOwner.DogOwner", b =>
                {
                    b.Navigation("Dogs");

                    b.Navigation("JobRequests");

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("Doggo.Domain.Entities.JobRequest.JobRequest", b =>
                {
                    b.Navigation("Jobs");

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

                    b.Navigation("Messages");

                    b.Navigation("PersonalIdentifier")
                        .IsRequired();

                    b.Navigation("UserChats");

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
