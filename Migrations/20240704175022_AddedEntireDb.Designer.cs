﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OutOfOffice.Context;

#nullable disable

namespace OutOfOffice.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240704175022_AddedEntireDb")]
    partial class AddedEntireDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OutOfOffice.Entities.ApprovalRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ApprovalStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("LeaveRequestId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalStatusId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LeaveRequestId");

                    b.ToTable("approval_requests");
                });

            modelBuilder.Entity("OutOfOffice.Entities.ApprovalStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("approval_statuses");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("login");

                    b.Property<short>("OutOfOfficeBalance")
                        .HasColumnType("smallint")
                        .HasColumnName("out_of_office_balance");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("password");

                    b.Property<int?>("PeoplePartnerId")
                        .HasColumnType("integer");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<int>("SubdivisionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("PeoplePartnerId");

                    b.HasIndex("PositionId");

                    b.HasIndex("StatusId");

                    b.HasIndex("SubdivisionId");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("OutOfOffice.Entities.EmployeeRole", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer")
                        .HasColumnName("fk_employee_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("fk_role_id");

                    b.HasKey("EmployeeId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("employee_roles");
                });

            modelBuilder.Entity("OutOfOffice.Entities.EmployeeStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("employee_statuses");
                });

            modelBuilder.Entity("OutOfOffice.Entities.LeaveRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AbsenceReason")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("absence_reason");

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("LeaveRequestStatusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LeaveRequestStatusId");

                    b.ToTable("leave_requests");
                });

            modelBuilder.Entity("OutOfOffice.Entities.LeaveRequestStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("leave_request_statuses");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("position_name");

                    b.HasKey("Id");

                    b.ToTable("positions");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("ProjectManagerId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.HasKey("Id");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("ProjectStatusId");

                    b.HasIndex("ProjectTypeId");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("OutOfOffice.Entities.ProjectStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("project_statuses");
                });

            modelBuilder.Entity("OutOfOffice.Entities.ProjectTeam", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("EmployeeId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("project_teams");
                });

            modelBuilder.Entity("OutOfOffice.Entities.ProjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("project_types");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("role_name");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Subdivision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("subdivision_name");

                    b.HasKey("Id");

                    b.ToTable("subdivisions");
                });

            modelBuilder.Entity("OutOfOffice.Entities.ApprovalRequest", b =>
                {
                    b.HasOne("OutOfOffice.Entities.ApprovalStatus", "ApprovalStatus")
                        .WithMany()
                        .HasForeignKey("ApprovalStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.Employee", "Employee")
                        .WithMany("ApprovalRequests")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.LeaveRequest", "LeaveRequest")
                        .WithMany()
                        .HasForeignKey("LeaveRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovalStatus");

                    b.Navigation("Employee");

                    b.Navigation("LeaveRequest");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Employee", b =>
                {
                    b.HasOne("OutOfOffice.Entities.Employee", "PeoplePartner")
                        .WithMany()
                        .HasForeignKey("PeoplePartnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("OutOfOffice.Entities.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.EmployeeStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.Subdivision", "Subdivision")
                        .WithMany()
                        .HasForeignKey("SubdivisionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PeoplePartner");

                    b.Navigation("Position");

                    b.Navigation("Status");

                    b.Navigation("Subdivision");
                });

            modelBuilder.Entity("OutOfOffice.Entities.EmployeeRole", b =>
                {
                    b.HasOne("OutOfOffice.Entities.Employee", "Employee")
                        .WithMany("Roles")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("OutOfOffice.Entities.LeaveRequest", b =>
                {
                    b.HasOne("OutOfOffice.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.LeaveRequestStatus", "Status")
                        .WithMany()
                        .HasForeignKey("LeaveRequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Project", b =>
                {
                    b.HasOne("OutOfOffice.Entities.Employee", "ProjectManager")
                        .WithMany()
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.ProjectStatus", "Status")
                        .WithMany()
                        .HasForeignKey("ProjectStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.ProjectType", "Type")
                        .WithMany()
                        .HasForeignKey("ProjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectManager");

                    b.Navigation("Status");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("OutOfOffice.Entities.ProjectTeam", b =>
                {
                    b.HasOne("OutOfOffice.Entities.Employee", "Employee")
                        .WithMany("Projects")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutOfOffice.Entities.Project", "Project")
                        .WithMany("ProjectTeams")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Employee", b =>
                {
                    b.Navigation("ApprovalRequests");

                    b.Navigation("Projects");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("OutOfOffice.Entities.Project", b =>
                {
                    b.Navigation("ProjectTeams");
                });
#pragma warning restore 612, 618
        }
    }
}
