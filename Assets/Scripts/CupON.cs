using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupON : MonoBehaviour {
    private GameObject cupOffPrefab;

	// Use this for initialization
	void Start () {
        cupOffPrefab = Resources.Load("Prefabs/CupOFF") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        var dist = Vector3.Distance(collider.transform.position, transform.position);
        Utils.Instance.DelayCall(this, 1f,
            () => 
            {
                var newDist = Vector3.Distance(collider.transform.position, transform.position);
                if (newDist < dist * 0.6f)
                {
                    var cupOff = Instantiate(cupOffPrefab);
                    Utils.Instance.SetParent(cupOff.transform, transform.parent);
                    cupOff.transform.localPosition = transform.localPosition;
                    cupOff.transform.localRotation = transform.localRotation;
                    cupOff.transform.localScale = transform.localScale;
                    Destroy(gameObject);
                }
            }
        );
    }
}
