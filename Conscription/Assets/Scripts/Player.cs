﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	public Unit[] units;
	public List<Unit> benchedUnits = new List<Unit>();
	public List<Tile> summoningZone = new List<Tile>();

	public string playerName;
	public string activePlayerName;
	public int mana;
	public int maxMana;
	public int manaCap;
	public Unit lord;
	public Color color;

	public void summonUnit(Unit unit, Tile tile) {
		if (unit.cost <= mana) {
			placeUnit (unit, tile);
			SFXManager.instance.playSummon();
			mana -= unit.cost;
			clearSummonZone();
			GameManager.instance.deselect();
			GuiManager.instance.setPlayerMana (GameManager.instance.currentPlayer, mana);
		} else {
			SFXManager.instance.playNeedMoreMana();
		}
	}

	public void highlightSummonZone() {
		foreach(Tile tile in summoningZone) {
			tile.transform.GetComponent<Renderer>().material.color = Tile.highlightMoveColor;
			tile.colorBuffer = Tile.highlightMoveColor;
		}
	}

	public void clearSummonZone() {
		foreach(Tile tile in summoningZone) {
			tile.transform.GetComponent<Renderer>().material.color = Tile.defaultColor;
			tile.colorBuffer = Tile.defaultColor;
		}
	}

	public void placeUnit(Unit unit, Tile tile) {
		unit.summoned = true;
		unit.moved = true;
		unit.attacked = true;
		tile.occupant = unit;
		unit.currentTile = tile;
		unit.transform.position = tile.transform.position + new Vector3 (0, unit.transform.position.y, 0);
		unit.moveDestination = unit.transform.position;
		UnitInfoManager uim = ((GameObject)Instantiate(GameManager.instance.uimPrefab, new Vector3(unit.transform.position.x + 1f, unit.transform.position.y + 1f, unit.transform.position.z  + 0.5f), GameManager.instance.uimPrefab.transform.rotation)).GetComponent<UnitInfoManager>();
		uim.target = unit;
		GameManager.instance.units.Add (unit);
		benchedUnits.Remove (unit);
		GuiManager.instance.portraits.Remove (unit.portrait);
		Destroy (unit.portrait.gameObject);

	}

	public void addMoreMana(int turnMana) {
		maxMana = ((turnMana + maxMana) > manaCap) ? manaCap : (turnMana + maxMana); 
		mana = ((turnMana + mana) > maxMana) ? maxMana : (turnMana + mana);  
	}

}
