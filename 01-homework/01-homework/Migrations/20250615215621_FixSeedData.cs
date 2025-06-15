using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _01_homework.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 3,
                column: "EnrollmentDate",
                value: new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2024, 12, 15, 23, 55, 35, 741, DateTimeKind.Local).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2024, 12, 15, 23, 55, 35, 741, DateTimeKind.Local).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 3,
                column: "EnrollmentDate",
                value: new DateTime(2025, 3, 15, 23, 55, 35, 741, DateTimeKind.Local).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1,
                column: "EnrollmentDate",
                value: new DateTime(2023, 6, 15, 23, 55, 35, 732, DateTimeKind.Local).AddTicks(140));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2,
                column: "EnrollmentDate",
                value: new DateTime(2024, 6, 15, 23, 55, 35, 740, DateTimeKind.Local).AddTicks(6520));
        }
    }
}
