using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RocketControl : MonoBehaviour {
	public delegate void RocketControlHandler();
	public static RocketControlHandler OnFail = delegate { };

	[SerializeField]
	private ParticleSystem _boostParticles;
	
	[SerializeField]
	private ParticleSystem _collisionParticles;
	
	[SerializeField]
	private AudioSource _destroySound;

	private bool _transfer = false;
	private bool _failed = false;

	private Quaternion newRotation = Quaternion.identity;
	
	void FixedUpdate () {
		if(!GetComponentInChildren<Renderer>().isVisible && !_transfer) {
			float transferY = transform.position.y;
			float transferZ = transform.position.z;

			if(Mathf.Abs(transform.position.y) > 5.5f)
				transferY = transform.position.y * -1;
			if(Mathf.Abs(transform.position.z) > 8f)
				transferZ = transform.position.z * -1;

			transform.position = new Vector3(transform.position.x, transferY, transferZ);

			StartCoroutine(StopTransfer());
		} else
			transform.position = new Vector3(0, Mathf.Clamp(transform.position.y, -17, 17), transform.position.z);


		if(InputEventHandler._isStartTouchAction) {
			_boostParticles.Play();
			if(!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();

			Vector3 pos = InputEventHandler._currentTouchPosition - transform.position;
			pos.x = 0;
			newRotation = Quaternion.LookRotation(pos);

			GetComponent<Rigidbody>().velocity += 0.1f * transform.forward;
		} else {
			transform.LookAt(transform.position + transform.forward);
			_boostParticles.Stop();
			GetComponent<AudioSource>().Stop();
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 20);
	}

	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<Obstacle>() && !_failed)
			Explode();
	}

	private void Explode() {
		_failed = true;
		GetComponent<CapsuleCollider>().enabled = false;
		GetComponentInChildren<MeshRenderer>().enabled = false;
		_boostParticles.Stop();
		_collisionParticles.Play();
		GetComponent<AudioSource>().Stop();
		_destroySound.Play();
		StartCoroutine(WaitAndReload());

		if(GameControler.Instance.MyScore > GameControler.Instance.MyBestScore)
			PlayerPrefs.SetInt("Score", GameControler.Instance.MyScore);

		OnFail();
	}

	IEnumerator StopTransfer() {
		_transfer = true;
		while(!GetComponentInChildren<Renderer>().isVisible)
			yield return null;

		_transfer = false;
	}

	IEnumerator WaitAndReload() {
		yield return new WaitForSeconds(2);

		SceneManager.LoadScene(1);
	}

}
