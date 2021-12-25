using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VoteDbContext.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoteStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    NumberOfVoters = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 12, 25, 17, 49, 40, 847, DateTimeKind.Local).AddTicks(2609)),
                    Published = table.Column<DateTime>(type: "datetime", nullable: true),
                    VoteStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vote_VoteStatus_VoteStatus",
                        column: x => x.VoteStatus,
                        principalTable: "VoteStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NumberOfVoters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Vote_Id",
                        column: x => x.Id,
                        principalTable: "Vote",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagVote",
                columns: table => new
                {
                    TagId = table.Column<long>(type: "bigint", nullable: false),
                    VoteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagVote", x => new { x.TagId, x.VoteId });
                    table.ForeignKey(
                        name: "FK_TagVote_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagVote_Vote_VoteId",
                        column: x => x.VoteId,
                        principalTable: "Vote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagVote_VoteId",
                table: "TagVote",
                column: "VoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VoteStatus",
                table: "Vote",
                column: "VoteStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "TagVote");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "VoteStatus");
        }
    }
}
