using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloomBridge.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedTherapistsAndFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Therapists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentClients = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TherapistFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TherapistId = table.Column<int>(type: "INTEGER", nullable: false),
                    PredefinedNeedId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistFields_PredefinedNeeds_PredefinedNeedId",
                        column: x => x.PredefinedNeedId,
                        principalTable: "PredefinedNeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TherapistFields_Therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Therapists",
                columns: new[] { "Id", "Capacity", "CurrentClients", "Name" },
                values: new object[,]
                {
                    { 1, 3, 1, "Minerva McGonagall" },
                    { 2, 2, 2, "Remus Lupin" },
                    { 3, 4, 2, "Pomona Sprout" },
                    { 4, 5, 3, "Filius Flitwick" },
                    { 5, 1, 0, "Sybill Trelawney" }
                });

            migrationBuilder.InsertData(
                table: "TherapistFields",
                columns: new[] { "Id", "PredefinedNeedId", "TherapistId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 4, 1 },
                    { 3, 6, 1 },
                    { 4, 4, 2 },
                    { 5, 6, 2 },
                    { 6, 11, 2 },
                    { 7, 7, 3 },
                    { 8, 10, 3 },
                    { 9, 14, 3 },
                    { 10, 5, 4 },
                    { 11, 10, 4 },
                    { 12, 18, 4 },
                    { 13, 21, 5 },
                    { 14, 23, 5 },
                    { 15, 25, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TherapistFields_PredefinedNeedId",
                table: "TherapistFields",
                column: "PredefinedNeedId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistFields_TherapistId",
                table: "TherapistFields",
                column: "TherapistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TherapistFields");

            migrationBuilder.DropTable(
                name: "Therapists");
        }
    }
}
