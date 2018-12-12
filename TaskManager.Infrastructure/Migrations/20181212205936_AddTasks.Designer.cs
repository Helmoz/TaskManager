﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.Infrastructure.Context;

namespace TaskManager.Infrastructure.Migrations
{
    [DbContext(typeof(TaskManagerDbContext))]
    [Migration("20181212205936_AddTasks")]
    partial class AddTasks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskManager.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Deadline");

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int>("Progress");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TaskManager.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("TaskId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TaskManager.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompletedById");

                    b.Property<int?>("CreatorId");

                    b.Property<string>("Details")
                        .HasMaxLength(300);

                    b.Property<bool>("IsCompleted");

                    b.Property<bool>("IsPriority");

                    b.Property<bool>("IsSpecial");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("CompletedById");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("PhotoUrl");

                    b.Property<int?>("ProjectId");

                    b.Property<int?>("TaskId");

                    b.Property<string>("UId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TaskId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskManager.Models.Tag", b =>
                {
                    b.HasOne("TaskManager.Models.Project")
                        .WithMany("Tags")
                        .HasForeignKey("ProjectId");

                    b.HasOne("TaskManager.Models.Task")
                        .WithMany("Tags")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("TaskManager.Models.Task", b =>
                {
                    b.HasOne("TaskManager.Models.User", "CompletedBy")
                        .WithMany()
                        .HasForeignKey("CompletedById");

                    b.HasOne("TaskManager.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("TaskManager.Models.Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.HasOne("TaskManager.Models.Project")
                        .WithMany("Members")
                        .HasForeignKey("ProjectId");

                    b.HasOne("TaskManager.Models.Task")
                        .WithMany("AssignedTo")
                        .HasForeignKey("TaskId");
                });
#pragma warning restore 612, 618
        }
    }
}