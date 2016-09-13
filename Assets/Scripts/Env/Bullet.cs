using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private Transform _myParent;
	private Vector3 _spaenPosition;
	private Vector3 destination;
	private bool _isOut = false;

	void Start() {
		_myParent = transform.parent;
	}

	void Update() {
		if(_isOut) {
			transform.position = Vector3.MoveTowards(transform.position, transform.position + destination, 1.5f);
		}
	}

	public void Fly( Vector3 _destination ) {
		GetComponent<AudioSource>().Play();
		destination = _destination;
		transform.parent = null;
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<ParticleSystem>().Play();
		StartCoroutine(BackToParent());
		_isOut = true;
	}

	IEnumerator BackToParent() {
		yield return new WaitForSeconds(1);

		_isOut = false;
		GetComponent<ParticleSystem>().Stop();
		GetComponent<MeshRenderer>().enabled = false;

		transform.SetParent(_myParent);
		transform.position = _myParent.position;
	}
}
