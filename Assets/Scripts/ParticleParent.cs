using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleParent : MonoBehaviour {
    private GameObject ball;
    private List<Transform> particleList = new List<Transform>();
    private int activeIdx = 0;

	// Use this for initialization
	void Start () {
        ball = GameObject.Find("/PingPongBall");
        foreach (Transform child in transform)
            particleList.Add(child);

        StartCoroutine(ChangePartical());
    }
	
	// Update is called once per frame
	void Update () {
        if (ball != null)
            transform.position = ball.transform.position;
	}

    private IEnumerator ChangePartical()
    {
        while (true)
        {
            for (int i = 0; i < particleList.Count; i++)
                if (i == activeIdx)
                    particleList[i].gameObject.SetActive(true);
                else
                    particleList[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(8*60);
            activeIdx = (activeIdx+1) % particleList.Count;
        }
    }
}
