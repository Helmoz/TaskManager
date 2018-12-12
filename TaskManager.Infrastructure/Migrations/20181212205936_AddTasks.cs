using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Infrastructure.Migrations
{
    public partial class AddTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Tags",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Details = table.Column<string>(maxLength: 300, nullable: true),
                    CreatorId = table.Column<int>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    CompletedById = table.Column<int>(nullable: true),
                    IsPriority = table.Column<bool>(nullable: false),
                    IsSpecial = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Users_CompletedById",
                        column: x => x.CompletedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskId",
                table: "Users",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TaskId",
                table: "Tags",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CompletedById",
                table: "Task",
                column: "CompletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CreatorId",
                table: "Task",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ProjectId",
                table: "Task",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Task_TaskId",
                table: "Tags",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Task_TaskId",
                table: "Users",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Task_TaskId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Task_TaskId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Users_TaskId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TaskId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Tags");
        }
    }
}
