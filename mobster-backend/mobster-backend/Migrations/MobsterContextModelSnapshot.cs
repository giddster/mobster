﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mobster_backend.Database;

#nullable disable

namespace mobster_backend.Migrations
{
    [DbContext(typeof(MobsterContext))]
    partial class MobsterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("forum")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FamilyUser", b =>
                {
                    b.Property<Guid>("FamiliesFamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FamilyMembersUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FamiliesFamilyId", "FamilyMembersUserId");

                    b.HasIndex("FamilyMembersUserId");

                    b.ToTable("FamilyUser", "forum");
                });

            modelBuilder.Entity("mobster_backend.Models.Admin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AdminId");

                    b.HasIndex("FamilyId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Admins", "forum");
                });

            modelBuilder.Entity("mobster_backend.Models.BlockedMember", b =>
                {
                    b.Property<Guid>("BlockedMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BlockedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("FamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BlockedMemberId");

                    b.HasIndex("FamilyId");

                    b.HasIndex("UserId");

                    b.ToTable("BlockedMembers", "forum");
                });

            modelBuilder.Entity("mobster_backend.Models.Family", b =>
                {
                    b.Property<Guid>("FamilyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("MemberCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("FamilyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Families", "forum");
                });

            modelBuilder.Entity("mobster_backend.Models.Post", b =>
                {
                    b.Property<Guid>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(15000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCensored")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ThreadId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PostId");

                    b.HasIndex("AuthorUserId");

                    b.HasIndex("ThreadId");

                    b.ToTable("Posts", "forum");
                });

            modelBuilder.Entity("mobster_backend.Models.Report", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ObjectUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SubjectUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ThreadId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReportId");

                    b.HasIndex("ObjectUserId");

                    b.HasIndex("PostId");

                    b.HasIndex("SubjectUserId");

                    b.HasIndex("ThreadId");

                    b.ToTable("Reports", "forum");
                });

            modelBuilder.Entity("mobster_backend.Models.Thread", b =>
                {
                    b.Property<Guid>("ThreadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(15000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FamilyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCensored")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ThreadId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("FamilyId");

                    b.ToTable("Threads", "forum");
                });

            modelBuilder.Entity("mobster_backend.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users", "forum");
                });

            modelBuilder.Entity("FamilyUser", b =>
                {
                    b.HasOne("mobster_backend.Models.Family", null)
                        .WithMany()
                        .HasForeignKey("FamiliesFamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mobster_backend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FamilyMembersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("mobster_backend.Models.Admin", b =>
                {
                    b.HasOne("mobster_backend.Models.Family", "Family")
                        .WithOne("Admin")
                        .HasForeignKey("mobster_backend.Models.Admin", "FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mobster_backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Family");

                    b.Navigation("User");
                });

            modelBuilder.Entity("mobster_backend.Models.BlockedMember", b =>
                {
                    b.HasOne("mobster_backend.Models.Family", "Family")
                        .WithMany()
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mobster_backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Family");

                    b.Navigation("User");
                });

            modelBuilder.Entity("mobster_backend.Models.Post", b =>
                {
                    b.HasOne("mobster_backend.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mobster_backend.Models.Thread", "Thread")
                        .WithMany("Posts")
                        .HasForeignKey("ThreadId");

                    b.Navigation("Author");

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("mobster_backend.Models.Report", b =>
                {
                    b.HasOne("mobster_backend.Models.User", "ObjectUser")
                        .WithMany()
                        .HasForeignKey("ObjectUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mobster_backend.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");

                    b.HasOne("mobster_backend.Models.User", "SubjectUser")
                        .WithMany()
                        .HasForeignKey("SubjectUserId");

                    b.HasOne("mobster_backend.Models.Thread", "Thread")
                        .WithMany()
                        .HasForeignKey("ThreadId");

                    b.Navigation("ObjectUser");

                    b.Navigation("Post");

                    b.Navigation("SubjectUser");

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("mobster_backend.Models.Thread", b =>
                {
                    b.HasOne("mobster_backend.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mobster_backend.Models.Family", "Family")
                        .WithMany("Threads")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Family");
                });

            modelBuilder.Entity("mobster_backend.Models.Family", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Threads");
                });

            modelBuilder.Entity("mobster_backend.Models.Thread", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
