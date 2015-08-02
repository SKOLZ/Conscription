using UnityEngine;
using System.Collections;

public class UnitPortrait : MonoBehaviour {

	public Unit unit;
	// Use this for initialization
	void Start () {
	
	}

	public void Select() {
		GameManager.instance.selectUnit(unit);
		GuiManager.instance.selectUnit (unit);
		if(unit.player.mana >= unit.cost) {
			GameManager.instance.getCurrentPlayer().highlightSummonZone();
		}
	}
}
