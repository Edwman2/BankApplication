#                                         OVERVIEW
# Authors: 
Simon Eke, Edwin Torres, Mattias Åström, Jacob Daoud, Alex Gorie
# BankApplication.
# Backlog:
Story Points
- Som systemägare vill jag att alla användare ska logga in med ett unikt användarnamn och lösenord.
- Som administratör på banken vill jag kunna skapa nya användare i systemet.
- Som systemägare vill jag att användare som misslyckas med inloggningen tre gånger ska låsas ute från att logga in i systemet.
- Som användare vill jag kunna se en lista med alla mina bankkonton och saldot på dessa.
- Som användare vill jag kunna föra över pengar mellan två av mina konton.
- Som användare vill jag kunna föra över penger till andra kunder i banken..
- Som användare vill jag kunna öppna upp nya konton i banken.
- Som användare vill jag kunna ha ett visst konto i en annan valuta.
- Som bankägare vill jag att överföringar mellan konton med olika valutor växlas enligt rätt växelkurs, vilken uppdateras dagligen av administratören på banken.
- Som användare vill jag kunna öppna sparkonto i banken och när jag sätter in dem få se hur mycket ränta jag kommer kunna få på pengarna jag sätter in.
- Som användare vill jag kunna låna pengar av banken och direkt få se hur mycket ränta jag kommer behöva betala på det lånet.
- Som bankägare vill jag begränsa hur mycket varje kund kan låna av banken så att de maximalt kan låna 5 ggr de pengar de redan har hos banken.
- Som användare vill jag kunna se en logg på alla överföringar m.m. som skett på mina konton.
- Som bankägare vill jag att appen ser snygg ut med tydliga menyer, färgsättning och en snygg logga i ASCII art som syns när användaren loggar in.
- Som bankägare vill jag inte att transaktioner sker direkt när användarna lägger in dem utan i stället var 15:e minut så att vi har kontroll på när de sker.
*   Extra uppgifter:
- Som systemägare vill jag att varje användare ska kunna uppdatera sina personliga uppgifter (t.ex. e-postadress, telefonnummer).
- Som bankägare vill jag kunna se statistik om användarnas transaktioner (t.ex. totala belopp överförda, mest populära konton, etc.).
- Som användare vill jag kunna få notifieringar via e-post eller SMS när stora transaktioner sker (över en viss summa) på mitt konto.
- Som administratör vill jag kunna sätta regler för hur låneräntor ska beräknas beroende på olika faktorer (t.ex. användarens kreditvärdighet).



#                                                 TODO-LIST

~~ * "Unit"-test Copy Main - to ensure that the relations between calls and classes and objects works for the simulated test.~~
* Remove/Change/Add Comments in code.
* Discuss if the made Unit-test covers the functionality of the program.
* Complete Unit-test?
* Merge branch: Design_UI_Simon to Copy_Main.
* Put the appropriate calls to Methods/Classes etc. in the appropriate places.
* Discuss if we need to create class for User Input.
* Call User Input where we need to.
* Run with no errors.
* Merge Copy_Main to Main.
* Finished program :)

#                                                   Implement if possible.
* Make Account into an abstract class or interface with different types of accounts implementing/inheriting from.
* Create a separate file maybe .txt to store users and accounts?
* Make each property/field private.
* Try to implement and check the code for inheritance, abstraction, polymorphism and encapsupsulation.
* Have all the program's account related methods go through Account Manager.
* Change the spelling and naming to a consistent way.
* Add Enum to appropriate places and put to use.
* Change AccountNumber from a string to a unique string like GUID or our own randomly generated one with added check.
* 






