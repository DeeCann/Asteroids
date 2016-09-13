using UnityEngine;
using System.Collections;

public class ObstaclesSpawner : MonoBehaviour {
	[SerializeField]
	private Transform _myEnemy;

	private float _spawnTimer = 0;

	void Start() {
		StartCoroutine(Spawn());
		switch(PlayerPrefs.GetInt("Diff")) {
			case 0: _spawnTimer = 2; break;
			case 1:	_spawnTimer = 1; break;
			case 2:	_spawnTimer = 0.5f; break;
		}
	}

	IEnumerator Spawn() {
		while(true) {
			if(transform.childCount == 0)
				yield break;

			for(int i = 0; i < transform.childCount; i++) {
				if(!transform.GetChild(i).GetComponent<Obstacle>().IsAttacking) {
					transform.GetChild(i).GetComponent<Obstacle>().Attack(_myEnemy);
					break;
				}
			}
			

			yield return new WaitForSeconds(_spawnTimer);
			yield return null;
		}
	}
}
