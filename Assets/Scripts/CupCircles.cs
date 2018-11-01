using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCircles : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var prefab = Resources.Load("Prefabs/CupON");
        var offset = new GameObject();
        offset.name = "offset";
        var layerNum = 4;
        var key = "cupLayer";
        if (GlobalDictionary.Instance.Get(key) != null)
            layerNum = (int)GlobalDictionary.Instance.Get(key);
        else
            GlobalDictionary.Instance.Set(key, layerNum);
        var iOffset = 0f;
        var interval = 0.3f;
        var sum = Vector3.zero;
        for (var i = 0; i < layerNum; i++)
        {
            var jOffset = 0f;
            for (var j = 0; j <= i; j++)
            {
                var marker = Instantiate(prefab) as GameObject;
                Utils.Instance.SetParent(marker.transform, offset.transform);
                marker.transform.localPosition = Quaternion.AngleAxis(60, Vector3.forward) * Vector3.right * iOffset
                    + Quaternion.AngleAxis(-60, Vector3.forward) * Vector3.right * jOffset;
                sum += marker.transform.localPosition;
                jOffset += interval;
            }
            iOffset += interval;
        }
        sum /= (1 + layerNum) * layerNum * 0.5f;
        Utils.Instance.SetParent(offset.transform, transform);
        offset.transform.localPosition = -sum;
        offset.transform.localScale = Vector3.one;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
