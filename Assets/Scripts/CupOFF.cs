using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupOFF : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var prefab = Resources.Load("Prefabs/CupSuccess");
        var cupSuccess = Instantiate(prefab) as GameObject;
        cupSuccess.transform.position = transform.position;
        Utils.Instance.FadeMaterial(this, true, cupSuccess, 1, () => { Destroy(cupSuccess); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
