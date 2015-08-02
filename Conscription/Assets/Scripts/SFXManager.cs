using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

	public AudioClip summon;
	public AudioClip[] attacks;
	public AudioClip[] deaths;
	public AudioClip[] needMoreMana;
	public AudioClip gameEnd;

	private AudioSource audio;

	public static SFXManager instance;

	public void Start() {
		audio = GetComponent<AudioSource> ();
		instance = this;
	}

	public void playGameEnd() {
		playAudio (gameEnd);
	}
	
	public void playSummon() {
		playAudio (summon);
	}
	
	public void playNeedMoreMana() {
		int index = Random.Range (0, needMoreMana.Length);
		playAudio (needMoreMana[index]);
	}
	
	public void playAttack() {
		int index = Random.Range (0, attacks.Length);
		playAudio (attacks[index]);
	}
	
	public void playDeath() {
		int index = Random.Range (0, deaths.Length);
		playAudio (deaths[index]);
	}

	private void playAudio(AudioClip ac) {
		if (!audio.isPlaying) {
			audio.clip = ac;
			audio.Play();
		}
	}
}
