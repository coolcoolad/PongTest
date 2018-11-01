using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongBall : MonoBehaviour {
    private float speed = 2;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}

    // Update is called once per frame
    void Update () {
        // 定义3个值控制移动
        float xm = 0, ym = 0;
        //按键盘W向上移动
        if (Input.GetKey(KeyCode.W))
        {
            ym += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))//按键盘S向下移动
        {
            ym -= speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))//按键盘A向左移动
        {
            xm -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))//按键盘D向右移动
        {
            xm += speed * Time.deltaTime;
        }
        transform.Translate(new Vector3(xm, ym, 0));
    }
}
