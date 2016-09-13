using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public delegate void ShootingHandler();
	public static ShootingHandler OnShoot = delegate { };

	void OnEnable() {
		HUDScript.OnFire += Fire;
	}

	void OnDisable() {
		HUDScript.OnFire -= Fire;
	}

	void Fire () {
		if(transform.childCount > 0)
			transform.GetChild(0).GetComponent<Bullet>().Fly( transform.parent.transform.forward );

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 30)) {
			if(hit.collider.GetComponent<Obstacle>()) {
				hit.collider.GetComponent<Obstacle>().Explode( Vector3.Distance(hit.collider.transform.position, transform.position) );
				OnShoot();
			}
		}
	}
}
