using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdminPortal.Migrations
{
    /// <inheritdoc />
    public partial class dificultRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("26636693-5e7d-470d-ab08-0006b1abacfc"), "Hard" },
                    { new Guid("2e0eaf1b-b7ef-42a7-a914-9c1da89ebff3"), "Medium" },
                    { new Guid("66c1acdb-cafc-44e4-be04-b8d9162101e2"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2a1d2fad-3446-4796-8524-5fbbea4c15e0"), "MZ", "Mohammed Zaid", "https://images.pexels.com/photos/26082992/pexels-photo-26082992/free-photo-of-alberoxalbero.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("598403e6-88c6-40a6-a51d-1abcd1e23dc3"), "AA", "Ahmed Alzabr", "https://images.pexels.com/photos/26082992/pexels-photo-26082992/free-photo-of-alberoxalbero.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("9c10ca47-6534-4557-b641-67d45ac5758b"), "SA", "Samerh Ali", "https://images.pexels.com/photos/26082992/pexels-photo-26082992/free-photo-of-alberoxalbero.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("b0d641f2-f07d-45fe-afc0-1a84eca538c3"), "AS", "Ahmed Saleh", "https://images.pexels.com/photos/26082992/pexels-photo-26082992/free-photo-of-alberoxalbero.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("26636693-5e7d-470d-ab08-0006b1abacfc"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("2e0eaf1b-b7ef-42a7-a914-9c1da89ebff3"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("66c1acdb-cafc-44e4-be04-b8d9162101e2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2a1d2fad-3446-4796-8524-5fbbea4c15e0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("598403e6-88c6-40a6-a51d-1abcd1e23dc3"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9c10ca47-6534-4557-b641-67d45ac5758b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b0d641f2-f07d-45fe-afc0-1a84eca538c3"));
        }
    }
}
