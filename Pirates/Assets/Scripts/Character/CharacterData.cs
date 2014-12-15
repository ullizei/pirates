using UnityEngine;
using System.Collections;

public enum CharacterJob {
	Pirate,
	Monster
}


[System.Serializable]
public class CharacterData {

	public string name;
	public string description;
	//public CharacterJob job;

	public int level;
	public int exp;
	public int hp;

	public CharacterStats stats;

	public CharacterData() {

		name = "NEW CHARACTER";
		description = "";
		stats = new CharacterStats();
	}

	public CharacterData(string _name, CharacterStats _stats) {
		name = _name;
		stats = _stats;
	}
}



/* Fiender!

Jag försöker lista ut om jag måste frikoppla grundstatsen från character-klassen och ha en separat enemy-klass...

Exakt vad är det som ska skilja characters från enemies?
Karaktärerna har jobb... men det skulle ju kunna finnas fiendejobb också? "Monster" skulle kunna vara ett jobb. Jag kommer ju säkerligen vilja ha fiender som är andra pirater, då är det ju ingen skillnad alls?

Det jag kommer att tänka på är att fiender skulle kunna ha ett habitat, typ att de bara finns till havs, eller i grottor, så att man kan spawna fram random enemies som ändå passar in i sin miljö. Man vill ju inte kunna möta en jättebläckfisk på land.

Man kanske också skulle vilja kunna ge dem specialförmågor, tex immunitet mot spells eller physical damage, eller att de kan flyga, osv.
Fast de grejerna vill man nog kunna langa på karaktärerna också! Om jag tex har en papegojkaraktär ska ju den vara flying.

Nu har jag dessutom en level-variabel... Den vill jag ju inte ska vara statisk, utan jag vill ju att om jag definierar ett skelettmonster, så ska jag kunna skapa den i olika levels beroende på vilken level spelaren har...
*/
