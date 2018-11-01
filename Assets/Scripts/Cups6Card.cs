﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Cups6Card : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        var dist = Vector3.Distance(collider.transform.position, transform.position);
        Utils.Instance.DelayCall(this, 0.5f,
            () =>
            {
                var newDist = Vector3.Distance(collider.transform.position, transform.position);
                if (newDist < dist)
                {
                    GlobalDictionary.Instance.Set("cupLayer", 3);
                    SceneManager.LoadScene("Assets/Scenes/BeerPongReady.unity");
                }
            }
        );
    }
}
