using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedCenter.Api.Migrations
{
    /// <inheritdoc />
    public partial class Add_PatientVisualMaps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientRegions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    VisualStateJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CenterId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRegions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientTeeth",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    ToothCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    VisualStateJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    CenterId = table.Column<long>(type: "bigint", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTeeth", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientRegions_CenterId",
                table: "PatientRegions",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRegions_PatientId_RegionCode",
                table: "PatientRegions",
                columns: new[] { "PatientId", "RegionCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientTeeth_CenterId",
                table: "PatientTeeth",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientTeeth_PatientId_ToothCode",
                table: "PatientTeeth",
                columns: new[] { "PatientId", "ToothCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientRegions");

            migrationBuilder.DropTable(
                name: "PatientTeeth");
        }
    }
}
