# CampSleepaway

Camp Sleepaway 
Camp Sleepaway, ett mysigt amerikansk sommarläger, har fått problem. Det går bra men det har blivit svårt att hålla reda på vem som är/sover var, vem är ansvarig och att rätt personer får besöka (dvs rätt anhöriga kommer på besök till lägerdeltagare). 

Som det ser ut nu så känns det nästan som om folk försvinner och dyker upp utan kontroll. Föräldrar har bland annat hotat att stämma lägret om de inte får träffa sina barn när de kommer på besök. Det måste vara pappershanteringens fel. Därför har man kallat in THS-konsulterna för att lösa problemet.

Skapa en EF Core applikation (Enkel menybaserad console app) för att hålla reda på Camp Sleepaways gäster, släktningar och personal och byggnader.

Databasen skall innehålla minst följande tabeller och deras lämpliga relationer och constraints. Namnge entiteter och tabeller med lämpliga plural och singularnamn

Databasen skall ge en ögonblicksbild av situationen men även kunna användas för historik

Camper – Lägerdeltagare
NextOfKin – Släktingar till Campers, endast dessa får besöka campers
Counselor - Lägerledare
Cabin – Stuga

Camper, NextOFKin och Councelors skall ha minst ett fält som inte finns i de övriga personrollerna. 
Det måste finnas relationer/relationsentiteter med datum för start och stopp för när tex campern bor i en stuga och när en councelor är ansvarig för en stuga. Datumintervallen kan variera både för campers och för councelors. Ni kan strunta i klockslag annan än för Visits om ni satsar på VG.
Med start och stoppdatum avses när en camper eller councelor flyttar in/blir ansvarig för en stuga och det datum när personen flyttar ut/ej längre är ansvarig för en stuga.


En deltagare sover endast i en stuga men en stuga kan ha många deltagare, dock max 4 samt en Counselor.  En stuga får inte fyllas med deltagare om den inte har en councelor. Councelors får bytas ut.
En Counselor ansvarar för en stuga och endast en.
Councelors, Cabins, Campers kan existera för sig själva utan varandra.
NextOfKin måste höra till en camper. 
En camper får ha valfritt antal NextOfKin, dvs så många som man vill ange. Det är frivilligt för er som programmerare att tillåta en NextOfKin som har flera campers eller ej.

Councelors får vara NextOfKin MEN då har de en egen rad i NextOFKin tabellen, personer kan ha flera roller men då ligger de i respektive tabell som motsvarar rollen. Vi hanterar inte detta i denna applikation.





G kriteria
Skapa en EF code first applikation där alla ovanstående entiteter hanteras och där ni utnyttjar annotations för att tex requiredfält och nycklar.

Välj lämpliga fält och datatyper samt grundläggande constraints för alla entiteter

Fyll på applikationen med seed-data minst 18 studenter, 3 counselors, minst 4 studenter skall ha next of kin, 3 cabins, 
Koppla studenterna till cabins, ge cabins counselors
Visa att man kan CRUDa ovanstående – enkel consoleapp
Skapa en rapportfunktion där man kan söka efter studenter baserat på stuga eller counselor, om det visar sig att en stuga saknar councelors så skall rapporten varna för det - med användarinterface
Skapa en rapportfunktion som kan visa upp studenter med eventuella next of kins baserat sorterat på cabins - med användarinterface
Med användarinterface avses en enkel console app. Ni behöver inte implementera avancerad felhantering av användarinput.


VG

Lägg till en Visit entitet med hjälp av migrations
Hantera uppdateringen av NextOfKin .

Visit är kopplad till en enskild Camper, och en eller flera next of kin. Programmet måste hantera att endast de next of kin som är kopplade till just denna camper får läggas in. 
Visit har ett startdatum och klockslag samt ett slutddatum och klockslag. Visits får vara max 3 timmar, starta tidigast klockan 10.00 och sluta senast klockan 20.00

Seeda databasen med minst 3 giltiga besök

Lägg in lämpliga constraints och med hjälp av FluentAPI

När man kommer på besök så skall man kunna verifieras mot infon i visits. Samt få information om vilken stuga man skall bege sig till och vilken counselor man kan kontakta.
