using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tikitapp.Website.Migrations {
	/// <inheritdoc />
	public partial class ReplaceCountryCodeWithCultureInfoName : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {

			migrationBuilder.AddColumn<string>(
				name: "CultureInfoName",
				table: "Venues",
				type: "varchar(16)",
				unicode: false,
				maxLength: 16,
				nullable: false,
				defaultValue: "");
                
            migrationBuilder.Sql(@"
UPDATE Venues SET CultureInfoName = 'pt-PT' WHERE CountryCode = 'PT';
UPDATE Venues SET CultureInfoName = 'en-GB' WHERE CountryCode = 'GB';
UPDATE Venues SET CultureInfoName = 'nn-NO' WHERE CountryCode = 'NO';
UPDATE Venues SET CultureInfoName = 'sv-SE' WHERE CountryCode = 'SE';
UPDATE Venues SET CultureInfoName = 'lt-LT' WHERE CountryCode = 'LT';
UPDATE Venues SET CultureInfoName = 'sr-Latn-RS' WHERE CountryCode = 'RS';
            ");

            migrationBuilder.DropColumn(
				name: "CountryCode",
				table: "Venues");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropColumn(
				name: "CultureInfoName",
				table: "Venues");

			migrationBuilder.AddColumn<string>(
				name: "CountryCode",
				table: "Venues",
				type: "varchar(2)",
				unicode: false,
				maxLength: 2,
				nullable: false,
				defaultValue: "");
		}
	}
}
