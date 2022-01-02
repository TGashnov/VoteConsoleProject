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
                    NumberOfVoters = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2022, 1, 2, 17, 2, 15, 480, DateTimeKind.Local).AddTicks(5906)),
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
                    NumberOfVoters = table.Column<int>(type: "int", nullable: true),
                    VoteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Vote_VoteId",
                        column: x => x.VoteId,
                        principalTable: "Vote",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagDbDTOVoteDbDTO",
                columns: table => new
                {
                    TagsTagId = table.Column<long>(type: "bigint", nullable: false),
                    VotesVoteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDbDTOVoteDbDTO", x => new { x.TagsTagId, x.VotesVoteId });
                    table.ForeignKey(
                        name: "FK_TagDbDTOVoteDbDTO_Tag_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagDbDTOVoteDbDTO_Vote_VotesVoteId",
                        column: x => x.VotesVoteId,
                        principalTable: "Vote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_VoteId",
                table: "Answer",
                column: "VoteId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDbDTOVoteDbDTO_VotesVoteId",
                table: "TagDbDTOVoteDbDTO",
                column: "VotesVoteId");

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
                name: "TagDbDTOVoteDbDTO");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "VoteStatus");
        }
    }
}
