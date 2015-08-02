using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitPortrait : MonoBehaviour {

	public Unit unit;
	// Use this for initialization
	void Start () {
	
	}

	public void Select() {
		GuiManager.instance.deselectUnit ();
		GameManager.instance.deselect ();
		GetComponent<Image> ().color = Color.green;
		GameManager.instance.selectUnit(unit);
		GuiManager.instance.selectUnit (unit);
		if(unit.player.mana >= unit.cost) {
			GameManager.instance.getCurrentPlayer().highlightSummonZone();
		}
	}
}
