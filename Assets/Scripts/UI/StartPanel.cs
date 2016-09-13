using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour {
	[SerializeField]
	private Text _bestScore;
	[SerializeField]
	private Animator _vigniette; 

	void Start() {
		if(PlayerPrefs.HasKey("Score")) {
			_bestScore.text = "Best score: " + PlayerPrefs.GetInt("Score").ToString();
			_bestScore.gameObject.SetActive(true);
		}
		_vigniette.SetBool("Show", true);
	}

	public void Play(int _difficulty) {
		GetComponent<AudioSource>().Play();
		PlayerPrefs.SetInt("Diff", _difficulty);
		_vigniette.SetBool("Show", false);
		StartCoroutine(WaitForVigniette());
	}

	IEnumerator WaitForVigniette() {
		yield return new WaitForSeconds(1);

		SceneManager.LoadScene(2);
	}
}
