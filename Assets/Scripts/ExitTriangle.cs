using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTriangle : MonoBehaviour {

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
                    SceneManager.LoadScene("Assets/Scenes/Welcome.unity");
                }
            }
        );
    }
}
