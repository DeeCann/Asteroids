using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour {
	[SerializeField]
	private Text _points;
	[SerializeField]
	private Animator _vigniette;

	public delegate void HUDScriptHandler();
	public static HUDScriptHandler OnFire = delegate { };

	void OnEnable() {
		Shooting.OnShoot += SetScore;
		RocketControl.OnFail += Back;
		_vigniette.SetBool("Show", true);
	}

	void OnDisable() {
		Shooting.OnShoot -= SetScore;
		RocketControl.OnFail -= Back;
	}
		
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
			Fire();
	}

	public void Fire() {
		OnFire();
	}

	public void Back() {
		_vigniette.SetBool("Show", false);
		StartCoroutine(WaitForVigniette());
	}

	private void SetScore() {
		GameControler.Instance.MyScore++;
		_points.text = GameControler.Instance.MyScore.ToString();
	} 

	IEnumerator WaitForVigniette() {
		yield return new WaitForSeconds(1);

		SceneManager.LoadScene(1);
	}
}
