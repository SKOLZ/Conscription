using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GuiManager : MonoBehaviour {
	
	public Text roundTimer;
	public Text[] playersMana;
	public Image selectedUnitImage;
	public Text selectedUnitAttack;
	public Text selectedUnitHealth;

	public GameObject benchedUnitSquare;
	public GameObject benchBar;
	public int benchMargin;
	public int benchXOffset;
	public int benchCount;
	public static GuiManager instance;

	void Awake (){
		instance = this;
	}

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

	public void updateBench(List<Unit> benchedUnits) {
		destroyBench ();
		for(int i = 0; i < benchedUnits.Count; i++) {
			GameObject square = (GameObject)Instantiate(benchedUnitSquare, new Vector3(benchXOffset + i * benchMargin, 5, benchedUnitSquare.transform.localPosition.z), benchedUnitSquare.transform.rotation);
			square.transform.SetParent(benchBar.transform);
			Unit unit = benchedUnits[i];
			GameObject avatar = square.transform.FindChild ("avatar").gameObject;
			avatar.GetComponent<Image>().overrideSprite = unit.image;
			GameObject stats = square.transform.FindChild ("stats").gameObject;
			GameObject attack = stats.transform.FindChild ("attackValue").gameObject;
			attack.GetComponent<Text>().text = unit.attack.ToString ();
			GameObject hp = stats.transform.FindChild ("healthValue").gameObject;
			hp.GetComponent<Text>().text = unit.health.ToString ();
			square.GetComponent<UnitPortrait>().unit = unit;
		}
	}

	public void destroyBench() {
		// i = 1 to ignore the bench square prefab
		for(int i = 1; i < benchBar.transform.childCount ; i++) {
			Destroy(benchBar.transform.GetChild (i));
		}
	}

}
