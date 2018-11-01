using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToWelcome : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("Assets/Scenes/Welcome.unity");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
