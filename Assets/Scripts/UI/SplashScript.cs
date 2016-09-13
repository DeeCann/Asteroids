using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour {

	void Start () {
		StartCoroutine(WaitForSFX());
	}

	IEnumerator WaitForSFX() {
		yield return new WaitForSeconds(1);

		SceneManager.LoadScene(1);
	}

}
