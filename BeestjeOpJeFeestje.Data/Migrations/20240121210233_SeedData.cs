using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Domain.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "beestje_type",
                columns: new[] { "naam" },
                values: new object[] { "Jungle" });

            migrationBuilder.InsertData(
                table: "beestje_type",
                columns: new[] { "naam" },
                values: new object[] { "Boerderij" });

            migrationBuilder.InsertData(
                table: "beestje_type",
                columns: new[] { "naam" },
                values: new object[] { "Sneeuw" });

            migrationBuilder.InsertData(
                table: "beestje_type",
                columns: new[] { "naam" },
                values: new object[] { "Woestijn" });

            migrationBuilder.InsertData(
                table: "beestje_type",
                columns: new[] { "naam" },
                values: new object[] { "VIP" });

            // CustomerCardTypes
            migrationBuilder.InsertData(
                table: "klantenkaart_type",
                columns: new[] { "type" },
                values: new object[] { "Zilver" });

            migrationBuilder.InsertData(
                table: "klantenkaart_type",
                columns: new[] { "type" },
                values: new object[] { "Goud" });

            migrationBuilder.InsertData(
                table: "klantenkaart_type",
                columns: new[] { "type" },
                values: new object[] { "Platina" });

            //Animals for jungle
            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 1, "Aap", "Jungle", 30.00, "aap.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 2, "Olifant", "Jungle", 40.00, "olifant.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 3, "Zebra", "Jungle", 45.00, "zebra.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 4, "Leeuw", "Jungle", 50.00, "leeuw.png" });

            //animals for farm
            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 5, "Hond", "Boerderij", 25.00, "hond.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 6, "Ezel", "Boerderij", 30.00, "ezel.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 7, "Koe", "Boerderij", 35.00, "koe.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 8, "Eend", "Boerderij", 20.00, "eend.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 9, "Kuiken", "Boerderij", 15.00, "kuiken.png" });

            //Animals for Snow
            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 10, "Pinguïn", "Sneeuw", 25.00, "pinguin.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 11, "IJsbeer", "Sneeuw", 30.00, "ijsbeer.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 12, "Zeehond", "Sneeuw", 20.00, "zeehond.png" });

            // Animals for Woestijn
            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 13, "Kameel", "Woestijn", 35.00, "kameel.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 14, "Slang", "Woestijn", 25.00, "slang.png" });

            // Animals for VIP
            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 15, "T-Rex", "VIP", 1000.00, "trex.png" });

            migrationBuilder.InsertData(
                table: "beestjes",
                columns: new[] { "id", "naam", "type", "prijs", "afbeelding" },
                values: new object[] { 16, "Unicorn", "VIP", 800.00, "unicorn.png" });

            // Users
            migrationBuilder.InsertData(
                table: "gebruiker",
                columns: new[] { "id", "voornaam", "tussenvoegsel", "achternaam", "adres", "email", "telefoonnummer", "klantenkaart" },
                values: new object[] { 1, "John", null, "Doe", "123 Main St", "john.doe@example.com", 0668822937, "Zilver" });

            migrationBuilder.InsertData(
                table: "gebruiker",
                columns: new[] { "id", "voornaam", "tussenvoegsel", "achternaam", "adres", "email", "telefoonnummer", "klantenkaart" },
                values: new object[] { 2, "Jane", null, "Smith", "456 Oak St", "jane.smith@example.com", 0668292017, "Goud" });

            migrationBuilder.InsertData(
                table: "gebruiker",
                columns: new[] { "id", "voornaam", "tussenvoegsel", "achternaam", "adres", "email", "telefoonnummer", "klantenkaart" },
                values: new object[] { 3, "Bob", "van", "Johnson", "789 Pine St", "bob.johnson@example.com", 0649174496, "Platina" });

            migrationBuilder.InsertData(
                table: "gebruiker",
                columns: new[] { "id", "voornaam", "tussenvoegsel", "achternaam", "adres", "email", "telefoonnummer", "klantenkaart" },
                values: new object[] { 4, "Alice", null, "Williams", "101 Elm St", "alice.williams@example.com", 0649988695, null });

            migrationBuilder.InsertData(
                table: "gebruiker",
                columns: new[] { "id", "voornaam", "tussenvoegsel", "achternaam", "adres", "email", "telefoonnummer", "klantenkaart" },
                values: new object[] { 5, "Charlie", "de", "Vries", "202 Maple St", "charlie.devries@example.com", 0631188475, "Zilver" });

            migrationBuilder.InsertData(
                table: "gebruiker",
                columns: new[] { "id", "voornaam", "tussenvoegsel", "achternaam", "adres", "email", "telefoonnummer", "klantenkaart" },
                values: new object[] { 6, "Daan", "van", "Uden", "Oude wei 16", "admin@boerderij.nl", 0642299596, "Platina" });

            // Bookings
            migrationBuilder.InsertData(
            table: "boekingen",
            columns: new[] { "id", "gebruiker_id", "datum", "totaal_prijs" },
            values: new object[] { 1, 1, new DateTime(2024, 2, 15), 120.00 });

            migrationBuilder.InsertData(
                table: "boekingen",
                columns: new[] { "id", "gebruiker_id", "datum", "totaal_prijs" },
                values: new object[] { 2, 3, new DateTime(2024, 3, 20), 65.00 });

            migrationBuilder.InsertData(
                table: "boekingen",
                columns: new[] { "id", "gebruiker_id", "datum", "totaal_prijs" },
                values: new object[] { 3, 2, new DateTime(2024, 4, 10), 300.00 });

            //accessories
            migrationBuilder.InsertData(
                table: "accessoires",
                columns: new[] { "id", "naam", "prijs" },
                values: new object[] { 1, "Hoepel", 3.00 });
            migrationBuilder.InsertData(
                table: "accessoires",
                columns: new[] { "id", "naam", "prijs" },
                values: new object[] { 2, "Krukje", 3.00 });

            migrationBuilder.InsertData(
                table: "accessoires",
                columns: new[] { "id", "naam", "prijs" },
                values: new object[] { 3, "Voedsel", 15.00 });

            migrationBuilder.InsertData(
                table: "accessoires",
                columns: new[] { "id", "naam", "prijs" },
                values: new object[] { 4, "Kauwspeeltjes", 10.00 });

            migrationBuilder.InsertData(
                table: "accessoires",
                columns: new[] { "id", "naam", "prijs" },
                values: new object[] { 5, "Zweep", 9.00 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
