using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject instructionsPanel;
	public GameObject playButton;
	public GameObject instructionsButton;
	public GameObject quitButton;

	public void startGame() {
		Application.LoadLevel("Main");
	}

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

	public void quit() {
		Application.Quit ();
	}

}
