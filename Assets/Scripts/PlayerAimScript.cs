using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		/*
		Vector3 upAxis = new Vector3 (0.0f, 0.0f, 1.0f);
		Vector3 mouseScreenPosition = Input.mousePosition;

		mouseScreenPosition.z = transform.position.z;
		Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint (mouseScreenPosition);

		float dist = Vector2.Distance (transform.position, mouseWorldSpace);

		if (dist > 1.0f) {
			Debug.Log ("!!!" + dist);
			transform.LookAt (mouseWorldSpace, upAxis);

			transform.eulerAngles = new Vector3 (0.0f, 0.0f, -transform.eulerAngles.z);
		}*/

		Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		diff.Normalize ();

		float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0.0f, 0.0f, rotZ - 90.0f);
	}
}
