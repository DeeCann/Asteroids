using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	private float _obstacleSpeed = 0;

	private Transform MyEnemyPosition {
		get;
		set;
	}

	public bool IsAttacking {
		set;
		get;
	}

	void Start() {
		switch(PlayerPrefs.GetInt("Diff")) {
		case 0: _obstacleSpeed = 0.02f; break;
		case 1:	_obstacleSpeed = 0.04f; break;
		case 2:	_obstacleSpeed = 0.06f; break;
		}
	}

	public void Attack(Transform _enemyPosition) {
		IsAttacking = true;
		MyEnemyPosition = _enemyPosition;
		GetComponent<SphereCollider>().enabled = true;
		GetComponentInChildren<MeshRenderer>().enabled = true;
		Vector3 newPosition = RandomAround(10, 15);
		transform.position = new Vector3(0, newPosition.y, newPosition.z);
		StartCoroutine(AttackCorutine());

	}

	public void Explode( float _distance ) {
		if(!GetComponentInChildren<MeshRenderer>().isVisible)
			return;
		
		StartCoroutine(ExlodeCorutine(_distance));
	}
		
	IEnumerator ExlodeCorutine(float _sleepTime) {
		yield return new WaitForSeconds(_sleepTime * 0.01f);
		IsAttacking = false;
		GetComponent<AudioSource>().Play();
		GetComponent<SphereCollider>().enabled = false;
		GetComponentInChildren<MeshRenderer>().enabled = false;
		GetComponentInChildren<ParticleSystem>().Play();
		StartCoroutine(Respawn());
	}

	IEnumerator AttackCorutine() {
		while(IsAttacking) {
			transform.position = Vector3.MoveTowards(transform.position, MyEnemyPosition.position, _obstacleSpeed);
			yield return null;
		}
	}

	IEnumerator Respawn() {
		yield return new WaitForSeconds(1);

		GetComponentInChildren<ParticleSystem>().Stop();
		transform.localPosition = Vector3.zero;
	}

	private Vector3 RandomAround(float minDist, float maxDist) {
		var v3 = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.right) * Vector3.forward * 15;
		return v3; 
	}

}
