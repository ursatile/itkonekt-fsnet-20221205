using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tikitapp.Website.Migrations {
	/// <inheritdoc />
	public partial class CreateShowVenueTicketTypeTables : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.CreateTable(
				name: "Venues",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Slug = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
					StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CountryCode = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Venues", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Shows",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					DoorsOpen = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					ShowStart = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Shows", x => x.Id);
					table.ForeignKey(
						name: "FK_Shows_Artists_ArtistId",
						column: x => x.ArtistId,
						principalTable: "Artists",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Shows_Venues_VenueId",
						column: x => x.VenueId,
						principalTable: "Venues",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "TicketTypes",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Price = table.Column<decimal>(type: "money", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_TicketTypes", x => x.Id);
					table.ForeignKey(
						name: "FK_TicketTypes_Shows_ShowId",
						column: x => x.ShowId,
						principalTable: "Shows",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Shows_ArtistId",
				table: "Shows",
				column: "ArtistId");

			migrationBuilder.CreateIndex(
				name: "IX_Shows_VenueId",
				table: "Shows",
				column: "VenueId");

			migrationBuilder.CreateIndex(
				name: "IX_TicketTypes_ShowId",
				table: "TicketTypes",
				column: "ShowId");

			migrationBuilder.CreateIndex(
				name: "IX_Venues_Slug",
				table: "Venues",
				column: "Slug",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "TicketTypes");

			migrationBuilder.DropTable(
				name: "Shows");

			migrationBuilder.DropTable(
				name: "Venues");
		}
	}
}
