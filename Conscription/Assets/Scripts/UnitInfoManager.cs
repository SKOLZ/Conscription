using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitInfoManager : MonoBehaviour {

	public Unit target;
	public Text attackText;
	public Text healthText;
	
	private Transform t;

	// Use this for initialization
	void Start () {
		t = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null && !target.summoned) {
			return;
		}
		if (target == null) {
			Debug.Log("boom");
			Destroy (this.gameObject);
			return;
		}
		t.position = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);
		t.LookAt (Camera.main.transform);
		attackText.text = target.attack.ToString();
		healthText.text = target.health.ToString();
	}
}
