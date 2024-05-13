# Beestje op je Feestje - Eindopdracht PRG6 2324
## Introductie
Deze opdracht is gemaakt met een mede student. Deze applicatie stelt gebruikers in staat om dieren te beheren en te boeken voor evenementen.

## Projectomschrijving
Beestje op je Feestje is een administratieve webapplicatie ontwikkeld in C# en ASP.NET MVC. Het stelt gebruikers in staat om via een boekingsproces beestjes te reserveren voor feestelijke gelegenheden. 
Eigenaren van de boerderij kunnen dieren beheren, en klanten kunnen deze boeken tegen vastgestelde prijzen.

## Functionele Eisen
### Beestjes
- CRUD operaties: Beheer van dieren inclusief creatie, lezing, update, en verwijdering.
- Attributen: Elk dier heeft een naam, type, prijs, en een verwijzing naar een afbeelding.

### Accounts
- Accountbeheer: De boerderij kan accounts aanmaken voor gasten, inclusief speciale klantenkaarten die extra rechten en kortingen bieden.

### Boeking Beheer
- Proces: Gebruikers selecteren dieren en een datum, voeren contactinformatie in, en bevestigen hun boeking.
- Validatie- en Kortingsregels: Specifieke regels voor boeking zoals maximale aantallen dieren afhankelijk van het type klantenkaart en kortingen op bepaalde dagen of combinaties van dieren.

## Technische Eisen
- Database: Microsoft SQL voor opslag van diergegevens en boekingsinformatie.
- Framework: ASP.NET Core MVC.
- Unit Testen: Beschikbaar voor alle validatieregels en kortingsregels.
