using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReviews.Migrations
{
    public partial class CommentFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Reviews_ReviewId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Reviews_ReviewId",
                table: "Comment",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Reviews_ReviewId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Reviews_ReviewId",
                table: "Comment",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId");
        }
    }
}
