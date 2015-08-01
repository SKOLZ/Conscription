using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Unit[] units;

	public string playerName;
	public string activePlayerName;
	public int mana;
	public int maxMana;
	public int manaCap;
	public Unit lord;

	public void summonUnit(Unit unit) {
		if (unit.cost <= mana) {
			placeUnit (unit);
			mana -= unit.cost;
		}
	}

	public void placeUnit(Unit unit) {
		// TODO: PLACE ON MAP
	}

	public void addMoreMana(int turnMana) {
		maxMana = ((turnMana + maxMana) > manaCap) ? manaCap : (turnMana + maxMana); 
		mana = ((turnMana + mana) > maxMana) ? maxMana : (turnMana + mana);  
	}

}
