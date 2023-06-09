using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doggo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Dogs_DogId",
                table: "JobRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Dogs_DogId",
                table: "JobRequests",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Dogs_DogId",
                table: "JobRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Dogs_DogId",
                table: "JobRequests",
                column: "DogId",
                principalTable: "Dogs",
                principalColumn: "Id");
        }
    }
}
