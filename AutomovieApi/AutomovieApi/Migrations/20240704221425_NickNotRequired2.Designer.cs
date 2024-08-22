﻿// <auto-generated />
using System;
using AutomovieApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutomovieApi.Migrations
{
    [DbContext(typeof(PlatformDbContext))]
    [Migration("20240704221425_NickNotRequired2")]
    partial class NickNotRequired2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AutomovieApi.Entities.Announcement", b =>
                {
                    b.Property<int>("AnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnId"));

                    b.Property<string>("BodyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Damaged")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoorCount")
                        .HasColumnType("int");

                    b.Property<string>("Engine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("FuelConsumption")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Generation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFirstOwner")
                        .HasColumnType("bit");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.Property<bool>("RightHandDrive")
                        .HasColumnType("bit");

                    b.Property<int>("SeatCount")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Undamaged")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<double>("lan")
                        .HasColumnType("float");

                    b.Property<double>("lng")
                        .HasColumnType("float");

                    b.HasKey("AnId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("AutomovieApi.Entities.AnnouncementImages", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<int>("AnId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.HasIndex("AnId");

                    b.ToTable("AnnouncementImages");
                });

            modelBuilder.Entity("AutomovieApi.Entities.BodyType", b =>
                {
                    b.Property<int>("BodyTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BodyTypeID"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BodyTypeID");

                    b.ToTable("BodyTypes");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<int>("AnId")
                        .HasColumnType("int");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("AnId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("AutomovieApi.Entities.DriverAssistanceSystems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnId")
                        .HasColumnType("int");

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("featureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnId");

                    b.HasIndex("featureId");

                    b.ToTable("DriverAssistanceSystems");
                });

            modelBuilder.Entity("AutomovieApi.Entities.DriverAssistanceSystemsDataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DriverAssistanceSystemsDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.FavoriteAnnouncements", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("FavoriteAnnouncementId")
                        .HasColumnType("int");

                    b.Property<int>("AnId")
                        .HasColumnType("int");

                    b.Property<int>("AnnouncementAnId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "FavoriteAnnouncementId");

                    b.HasIndex("AnnouncementAnId");

                    b.ToTable("FavoriteAnnouncements");
                });

            modelBuilder.Entity("AutomovieApi.Entities.FavoriteUsers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("FavoriteUserId")
                        .HasColumnType("int");

                    b.Property<int>("FavoriteId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "FavoriteUserId");

                    b.HasIndex("FavoriteId");

                    b.ToTable("FavoriteUsers");
                });

            modelBuilder.Entity("AutomovieApi.Entities.FuelType", b =>
                {
                    b.Property<int>("FuelTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FuelTypeID"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FuelTypeID");

                    b.ToTable("FuelTypes");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Generation", b =>
                {
                    b.Property<int>("GenerationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenerationId"));

                    b.Property<decimal>("AvgPrice_0_50000")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AvgPrice_100001_150000")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AvgPrice_150001_200000")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AvgPrice_200001_250000")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AvgPrice_250001_300000")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("AvgPrice_50001_100000")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenerationId");

                    b.HasIndex("ModelId");

                    b.ToTable("Generations");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Model", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelId"));

                    b.Property<string>("BodyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModelId");

                    b.HasIndex("BrandId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Multimedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnId")
                        .HasColumnType("int");

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("featureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnId");

                    b.HasIndex("featureId");

                    b.ToTable("Multimedia");
                });

            modelBuilder.Entity("AutomovieApi.Entities.MultimediaDataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MultimediaDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Other", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnId")
                        .HasColumnType("int");

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("featureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnId");

                    b.HasIndex("featureId");

                    b.ToTable("Other");
                });

            modelBuilder.Entity("AutomovieApi.Entities.OtherDataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OtherDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Performance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnId")
                        .HasColumnType("int");

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("featureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnId");

                    b.HasIndex("featureId");

                    b.ToTable("Performance");
                });

            modelBuilder.Entity("AutomovieApi.Entities.PerformanceDataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PerformanceDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Safety", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnId")
                        .HasColumnType("int");

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("featureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnId");

                    b.HasIndex("featureId");

                    b.ToTable("Safety");
                });

            modelBuilder.Entity("AutomovieApi.Entities.SafetyDataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("feature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SafetyDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCompany")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nick")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Voivodeship")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("lan")
                        .HasColumnType("float");

                    b.Property<double>("lng")
                        .HasColumnType("float");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Announcement", b =>
                {
                    b.HasOne("AutomovieApi.Entities.User", "User")
                        .WithMany("Announcements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutomovieApi.Entities.AnnouncementImages", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Announcement", "Announcement")
                        .WithMany("Images")
                        .HasForeignKey("AnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Comment", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Announcement", "Announcement")
                        .WithMany("Comments")
                        .HasForeignKey("AnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutomovieApi.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutomovieApi.Entities.DriverAssistanceSystems", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Announcement", "Announcement")
                        .WithMany("DriverAssistanceSystems")
                        .HasForeignKey("AnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutomovieApi.Entities.DriverAssistanceSystemsDataset", "DriverAssistanceSystemsDataset")
                        .WithMany()
                        .HasForeignKey("featureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("DriverAssistanceSystemsDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.FavoriteAnnouncements", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Announcement", "Announcement")
                        .WithMany()
                        .HasForeignKey("AnnouncementAnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutomovieApi.Entities.User", "User")
                        .WithMany("FavoriteAnnouncements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutomovieApi.Entities.FavoriteUsers", b =>
                {
                    b.HasOne("AutomovieApi.Entities.User", "Favorite")
                        .WithMany()
                        .HasForeignKey("FavoriteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AutomovieApi.Entities.User", "User")
                        .WithMany("FavoriteUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Favorite");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Generation", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Model", "Model")
                        .WithMany("Generations")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Model", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Multimedia", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Announcement", "Announcement")
                        .WithMany("Multimedia")
                        .HasForeignKey("AnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutomovieApi.Entities.MultimediaDataset", "MultimediaDataset")
                        .WithMany()
                        .HasForeignKey("featureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("MultimediaDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Other", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Announcement", "Announcement")
                        .WithMany("Other")
                        .HasForeignKey("AnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutomovieApi.Entities.OtherDataset", "OtherDataset")
                        .WithMany()
                        .HasForeignKey("featureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("OtherDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Performance", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Announcement", "Announcement")
                        .WithMany("Performance")
                        .HasForeignKey("AnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutomovieApi.Entities.PerformanceDataset", "PerformanceDataset")
                        .WithMany()
                        .HasForeignKey("featureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("PerformanceDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Safety", b =>
                {
                    b.HasOne("AutomovieApi.Entities.Announcement", "Announcement")
                        .WithMany("Safety")
                        .HasForeignKey("AnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutomovieApi.Entities.SafetyDataset", "SafetyDataset")
                        .WithMany()
                        .HasForeignKey("featureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("SafetyDataset");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Announcement", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DriverAssistanceSystems");

                    b.Navigation("Images");

                    b.Navigation("Multimedia");

                    b.Navigation("Other");

                    b.Navigation("Performance");

                    b.Navigation("Safety");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Brand", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("AutomovieApi.Entities.Model", b =>
                {
                    b.Navigation("Generations");
                });

            modelBuilder.Entity("AutomovieApi.Entities.User", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("Comments");

                    b.Navigation("FavoriteAnnouncements");

                    b.Navigation("FavoriteUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
