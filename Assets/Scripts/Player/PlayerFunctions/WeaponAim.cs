using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
	void Update ()
    {
        Aim();
	}

    void Aim()
    {
        Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = Input.mousePosition - camPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
