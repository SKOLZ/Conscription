using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Player[] players;
	public int currentPlayer;
	public int turnNumber;

	public void endTurn() {
		// TODO: ITERATE END OF TURN EFFECTS
		turnNumber++;
		currentPlayer = (currentPlayer + 1) % players.Length;
		getCurrentPlayer ().addMoreMana ((turnNumber + 1) / 2);
	}

	public Player getCurrentPlayer() {
		return players [currentPlayer];
	}
}
