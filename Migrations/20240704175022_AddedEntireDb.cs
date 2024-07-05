using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OutOfOffice.Entities;

#nullable disable

namespace OutOfOffice.Migrations
{
    /// <inheritdoc />
    public partial class AddedEntireDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "approval_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approval_statuses", x => x.id);
                });
            
            migrationBuilder.InsertData(
                table: "approval_statuses",
                columns: new[] { "status" },
                values: new object[,]
                {
                    {ApprovalStatusType.Approved},
                    {ApprovalStatusType.New},
                    {ApprovalStatusType.Canceled},
                    {ApprovalStatusType.Rejected}
                }
            );

            migrationBuilder.CreateTable(
                name: "employee_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_statuses", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "employee_statuses",
                columns: new[] { "status" },
                values: new object[,]
                {
                    {EmployeeStatusType.Active},
                    {EmployeeStatusType.Inactive},
                    {EmployeeStatusType.OnVacation},
                    {EmployeeStatusType.Deleted}
                }
            );

            migrationBuilder.CreateTable(
                name: "leave_request_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_request_statuses", x => x.id);
                });
            
            migrationBuilder.InsertData(
                table: "leave_request_statuses",
                columns: new[] { "status" },
                values: new object[,]
                {
                    {LeaveStatusType.Approved},
                    {LeaveStatusType.Canceled},
                    {LeaveStatusType.Created},
                    {LeaveStatusType.Rejected},
                    {LeaveStatusType.Submitted}
                }
            );

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    position_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.id);
                });
            
            migrationBuilder.InsertData(
                table: "positions",
                columns: new[] { "position_name" },
                values: new object[,]
                {
                    {"Junior"},
                    {"Middle"},
                    {"Senior"},
                    {"Lead"}
                }
            );

            migrationBuilder.CreateTable(
                name: "project_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_statuses", x => x.id);
                });
            
            migrationBuilder.InsertData(
                table: "project_statuses",
                columns: new[] { "status" },
                values: new object[,]
                {
                    {ProjectStatusType.Active},
                    {ProjectStatusType.Canceled},
                    {ProjectStatusType.Inactive},
                    {ProjectStatusType.New}
                }
            );

            migrationBuilder.CreateTable(
                name: "project_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_types", x => x.id);
                });
            
            migrationBuilder.InsertData(
                table: "project_types",
                columns: new[] { "type" },
                values: new object[,]
                {
                    {"Application"},
                    {"Service"},
                    {"Website"}
                }
            );

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });
            
            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "role_name" },
                values: new object[,]
                {
                    {"Admin"},
                    {"HR"},
                    {"EMP"},
                    {"PR"}
                }
            );

            migrationBuilder.CreateTable(
                name: "subdivisions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subdivision_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subdivisions", x => x.id);
                });
            
            migrationBuilder.InsertData(
                table: "subdivisions",
                columns: new[] { "subdivision_name" },
                values: new object[,]
                {
                    {"HR"},
                    {"IT"},
                    {"PR"},
                    {"Sales"},
                    {"Security"},
                    {"Support"}
                }
            );

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    out_of_office_balance = table.Column<short>(type: "smallint", nullable: false),
                    password = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SubdivisionId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    PeoplePartnerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_employee_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "employee_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employees_employees_PeoplePartnerId",
                        column: x => x.PeoplePartnerId,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employees_positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employees_subdivisions_SubdivisionId",
                        column: x => x.SubdivisionId,
                        principalTable: "subdivisions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employee_roles",
                columns: table => new
                {
                    fk_employee_id = table.Column<int>(type: "integer", nullable: false),
                    fk_role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_roles", x => new { x.fk_employee_id, x.fk_role_id });
                    table.ForeignKey(
                        name: "FK_employee_roles_employees_fk_employee_id",
                        column: x => x.fk_employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employee_roles_roles_fk_role_id",
                        column: x => x.fk_role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    absence_reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    LeaveRequestStatusId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_leave_requests_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_leave_requests_leave_request_statuses_LeaveRequestStatusId",
                        column: x => x.LeaveRequestStatusId,
                        principalTable: "leave_request_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectStatusId = table.Column<int>(type: "integer", nullable: false),
                    ProjectTypeId = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProjectManagerId = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_projects_employees_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_projects_project_statuses_ProjectStatusId",
                        column: x => x.ProjectStatusId,
                        principalTable: "project_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_projects_project_types_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalTable: "project_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approval_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment = table.Column<string>(type: "text", nullable: true),
                    ApprovalStatusId = table.Column<int>(type: "integer", nullable: false),
                    LeaveRequestId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approval_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_approval_requests_approval_statuses_ApprovalStatusId",
                        column: x => x.ApprovalStatusId,
                        principalTable: "approval_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_approval_requests_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_approval_requests_leave_requests_LeaveRequestId",
                        column: x => x.LeaveRequestId,
                        principalTable: "leave_requests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_teams",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_teams", x => new { x.EmployeeId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_project_teams_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_teams_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_approval_requests_ApprovalStatusId",
                table: "approval_requests",
                column: "ApprovalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_approval_requests_EmployeeId",
                table: "approval_requests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_approval_requests_LeaveRequestId",
                table: "approval_requests",
                column: "LeaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_roles_fk_role_id",
                table: "employee_roles",
                column: "fk_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_employees_login",
                table: "employees",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_PeoplePartnerId",
                table: "employees",
                column: "PeoplePartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_PositionId",
                table: "employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_StatusId",
                table: "employees",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_SubdivisionId",
                table: "employees",
                column: "SubdivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_requests_EmployeeId",
                table: "leave_requests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_requests_LeaveRequestStatusId",
                table: "leave_requests",
                column: "LeaveRequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_project_teams_ProjectId",
                table: "project_teams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_ProjectManagerId",
                table: "projects",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_ProjectStatusId",
                table: "projects",
                column: "ProjectStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_ProjectTypeId",
                table: "projects",
                column: "ProjectTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approval_requests");

            migrationBuilder.DropTable(
                name: "employee_roles");

            migrationBuilder.DropTable(
                name: "project_teams");

            migrationBuilder.DropTable(
                name: "approval_statuses");

            migrationBuilder.DropTable(
                name: "leave_requests");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "leave_request_statuses");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "project_statuses");

            migrationBuilder.DropTable(
                name: "project_types");

            migrationBuilder.DropTable(
                name: "employee_statuses");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "subdivisions");
        }
    }
}
