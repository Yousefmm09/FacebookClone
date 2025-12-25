using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacebookClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_otp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtpEmails_AspNetUsers_UserName",
                table: "OtpEmails");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "OtpEmails",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OtpEmails_UserName",
                table: "OtpEmails",
                newName: "IX_OtpEmails_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtpEmails_AspNetUsers_UserId",
                table: "OtpEmails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtpEmails_AspNetUsers_UserId",
                table: "OtpEmails");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OtpEmails",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_OtpEmails_UserId",
                table: "OtpEmails",
                newName: "IX_OtpEmails_UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_OtpEmails_AspNetUsers_UserName",
                table: "OtpEmails",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
