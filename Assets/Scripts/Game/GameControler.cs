using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour {
	
	private static GameControler _instance = null;
	public static GameControler Instance {
		get {
			if (_instance == null)
				_instance = GameObject.FindObjectOfType(typeof(GameControler)) as GameControler;
			
			return _instance;
		}
	}

	void Start() {
		MyBestScore = PlayerPrefs.GetInt("Score");
	}

	public int MyScore {
		set;
		get;
	}

	public int MyBestScore {
		set;
		get;
	}
}
