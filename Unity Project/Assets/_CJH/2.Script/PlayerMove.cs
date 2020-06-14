using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //플레이어 이동
    public float speed = 5.0f;  //플레이어 이동속도
    public Vector2 margin;      //뷰포트좌표는 0.0f ~ 1.0f 사이의 값
    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        margin = new Vector2(0.08f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //플레이어 이동
    private void Move()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        Vector3 dir = new Vector3(h, v, 0);
        transform.Translate(dir * speed * Time.deltaTime);

        //플레이어가 화면 밖으로 이동 못하게 만들기
        MoveInScreen();
    }

    private void MoveInScreen()
    {
        //스크린좌표 : 왼쪽하단(0, 0), 우측상단(maxX, maxY)
        //뷰포트좌표 : 왼쪽하단(0, 0), 우측상단(1.0f, 1.0f)
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        //position.x = Mathf.Clamp(position.x, 0.0f, 1.0f);
        //position.y = Mathf.Clamp(position.y, 0.0f, 1.0f);
        position.x = Mathf.Clamp(position.x, 0.0f + margin.x, 1.0f - margin.x);
        position.y = Mathf.Clamp(position.y, 0.0f + margin.y, 1.0f - margin.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Item"))
        {
            int type = (int)other.gameObject.GetComponent<Item>().ItemType;
            gameObject.GetComponent<PlayerFire>().ChangeBullet(type);
            other.gameObject.SetActive(false);
            ItemManager.instance.InsertItem(other.gameObject);
        }
    }
}
