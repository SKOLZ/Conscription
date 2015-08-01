using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float minFov = 15f;
	public float maxFov = 90f;
	public float scrollingSensitivity = 10f;
	public float panningSensitivity = 1f;

	public float minPanningX;
	public float maxPanningX;
	public float minPanningZ;
	public float maxPanningZ;

	private Vector3 mouseOrigin;
	private bool isPanning;
	private Quaternion startingRotation;

	void Start () {
		startingRotation = transform.rotation;
	}

	void Update () {
		if (Input.GetMouseButtonDown(2)) {
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}
		if (!Input.GetMouseButton (2)) {
			isPanning = false;
		}

		if (isPanning) {
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			Vector3 move = new Vector3(pos.x * panningSensitivity, pos.y * panningSensitivity, 0);
			transform.rotation = Quaternion.Euler(90, startingRotation.y, startingRotation.z);
			transform.Translate(move, Space.Self);
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPanningX, maxPanningX), transform.position.y, Mathf.Clamp(transform.position.z, minPanningZ, maxPanningZ));
			transform.rotation = startingRotation;
		} else {
			float scrollMovement;
			if ((scrollMovement = Input.GetAxis ("Mouse ScrollWheel")) != 0) {
				float currentFov = Camera.main.fieldOfView;
				currentFov += scrollMovement * scrollingSensitivity * -1; //the -1 is used to invert the scroll. 
				currentFov = Mathf.Clamp (currentFov, minFov, maxFov);
				Camera.main.fieldOfView = currentFov;
			}
		}
	}
}
