using UnityEngine;
using System.Collections;

public class ToolTipStrings {

	public static string StatInfo(StatType statType) {
		switch (statType) {
		case StatType.Agility:
			return "This statistic represents a character's speed and nimbleness. A high agility score lets you move further and faster in combat, dodge attacs and use lighter weapons effectively.";
			
		case StatType.Health:
			return "This is a representation of a character's overall physical state. A healthy body does not tire easily and has a greater resistance to injury and afflictions.";
			
		case StatType.Mind:
			return "This statistic represents a character's mental capacities, primarily force of will. A strong mind is important to cast and resist spells.";
			
		case StatType.Strength:
			return "This statistic represents raw, physical strength. A high strength value is necessary to use heavy weapons in combat.";
			
		case StatType.Swagger:
			return "This statistic represents a characters general <i>awesomeness</i>. A high swagger value will stun friend and foe alike with awe!";

		default:
			return "";
		}
	}

	public static string MeterBarInfo(MeterType meterType) {
		switch (meterType) {
		case MeterType.EXP:
			return "This meter shows a character's current amount of experience points and how close you are to levelling up.";

		case MeterType.HP:
			return "This meter shows a character's current amount of health points. A full meter means they are at max! ;-)";

		default:
			return "";
		}
	}

	public static string MeterBarLabel(MeterType meterType) {
		switch (meterType) {
		case MeterType.EXP:
			return "Experience meter";
			
		case MeterType.HP:
			return "Health point meter";
			
		default:
			return "";
		}
	}
}
