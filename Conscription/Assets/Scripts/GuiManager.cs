using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuiManager : MonoBehaviour {
	
	public Text roundTimer;
	public Text[] playersMana;
	public Image selectedUnitImage;
	public Text selectedUnitAttack;
	public Text selectedUnitHealth;

	public GameObject benchedUnitSquare;
	public GameObject benchBar;
	public int benchMargin;
	public int benchFirstX;
	public int benchCount;

	public void updateGuiTimer(float timer) {
		float seconds = Mathf.Floor(timer % 60);
		string secondsText = seconds < 10 ? "0" + seconds : seconds.ToString();
		float minutes = Mathf.Floor(timer / 60);
		string minutesText;
		if (minutes < 1) {
			minutesText = "00";
		} else if (minutes < 10) {
			minutesText = "0" + minutes;
		} else {
			minutesText = minutes.ToString();
		}
		roundTimer.text = minutesText + ":" + secondsText;
	}

	public void setPlayerMana(int playerIndex, int Mana) {
		playersMana [playerIndex].text = Mana.ToString ();
	}

	public void selectUnit(Unit unit) {
		selectedUnitAttack.text = unit.attack.ToString ();
		selectedUnitHealth.text = unit.health.ToString ();
		selectedUnitImage.overrideSprite = unit.image;
	}

	public void deselectUnit() {
		selectedUnitAttack.text = "-";
		selectedUnitHealth.text = "-";
		selectedUnitImage.overrideSprite = null;
	}

	public void updateBench(Unit[] benchedUnits) {
		destroyBench ();

	}

	public void destroyBench() {
		benchCount = 0;
		// i = 1 to ignore the bench square prefab
		for(int i = 1; i < benchBar.transform.childCount ; i++) {
			Destroy(benchBar.transform.GetChild (i));
		}
	}

}
