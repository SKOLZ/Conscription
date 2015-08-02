using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject instructionsPanel;
	public GameObject playButton;
	public GameObject instructionsButton;
	public GameObject quitButton;

	//MAIN MENU FUNCTIONS

	public void openInstructions() {
		instructionsPanel.SetActive (true);
		playButton.SetActive (false);
		instructionsButton.SetActive (false);
		quitButton.SetActive (false);
	}

	public void closeInstructions() {
		instructionsPanel.SetActive (false);
		playButton.SetActive (true);
		instructionsButton.SetActive (true);
		quitButton.SetActive (true);
	}

	// INGAME FUNCTIONS

	public void goToMainMenu() {
		Application.LoadLevel("Menu");
	}

	//SHARED FUNCTIONS

	public void startGame() {
		Application.LoadLevel("Main");
	}

	public void quit() {
		Application.Quit ();
	}
}
