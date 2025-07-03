using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloomBridge.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PredefinedNeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedNeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCustomNeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCustomNeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCustomNeeds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPredefinedNeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PredefinedNeedId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPredefinedNeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPredefinedNeeds_PredefinedNeeds_PredefinedNeedId",
                        column: x => x.PredefinedNeedId,
                        principalTable: "PredefinedNeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPredefinedNeeds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PredefinedNeeds",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { 1, "Managing anxiety" },
                    { 2, "Dealing with depression" },
                    { 3, "Processing grief and loss" },
                    { 4, "Coping with stress" },
                    { 5, "Building self-esteem" },
                    { 6, "Managing anger" },
                    { 7, "Improving relationships" },
                    { 8, "Setting boundaries" },
                    { 9, "Dealing with loneliness" },
                    { 10, "Family conflict resolution" },
                    { 11, "Social anxiety support" },
                    { 12, "Better sleep habits" },
                    { 13, "Nutrition and eating habits" },
                    { 14, "Exercise motivation" },
                    { 15, "Substance use support" },
                    { 16, "Time management" },
                    { 17, "Work-life balance" },
                    { 18, "Career guidance" },
                    { 19, "Burnout prevention" },
                    { 20, "Financial stress" },
                    { 21, "Finding purpose and meaning" },
                    { 22, "Building confidence" },
                    { 23, "Mindfulness and meditation" },
                    { 24, "Emotional regulation" },
                    { 25, "Personal goal setting" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCustomNeeds_UserId",
                table: "UserCustomNeeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPredefinedNeeds_PredefinedNeedId",
                table: "UserPredefinedNeeds",
                column: "PredefinedNeedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPredefinedNeeds_UserId",
                table: "UserPredefinedNeeds",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCustomNeeds");

            migrationBuilder.DropTable(
                name: "UserPredefinedNeeds");

            migrationBuilder.DropTable(
                name: "PredefinedNeeds");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
