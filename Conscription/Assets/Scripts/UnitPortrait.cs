using UnityEngine;
using System.Collections;

public class UnitPortrait : MonoBehaviour {

	public Unit unit;
	// Use this for initialization
	void Start () {
	
	}

	public void Select() {
		if (GameManager.instance.selected == null) {
			GameManager.instance.getCurrentPlayer().highlightSummonZone();
			GameManager.instance.selected = unit;
		}
	}
}
