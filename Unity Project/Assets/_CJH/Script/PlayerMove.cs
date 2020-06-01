using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//화면 안에서만 이동하게 하는 방법
//첫번째 방법 :카메라 밖에 큐브를 배치하여 RigidBody 이용

//두번째 방법 : 캐스팅

//세번째 방법 : 메인카메라의 뷰포트를 가져와서 처리(이번에 사용할 것)
//스크린 좌표 : 왼쪽 하단(0, 0), 우측 상단(maxX, maxY)
//뷰포트 좌표 : 왼쪽 하단(0, 0), 우측 상단(1.0, 1.0)

public class PlayerMove : MonoBehaviour
{
    //플레이어 이동
    [SerializeField] private float speed = 5.0f;
    Vector3 cameraPos;
    public Vector2 margin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //플레이어 이동과 관련된 함수
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
       
        transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);

        cameraPos = Camera.main.WorldToViewportPoint(transform.position);
        cameraPos.x = Mathf.Clamp(cameraPos.x, 0.0f + margin.x, 1.0f - margin.x);
        cameraPos.y = Mathf.Clamp(cameraPos.y, 0.0f + margin.y, 1.0f - margin.y);

        transform.position = Camera.main.ViewportToWorldPoint(cameraPos);
        //MoveInScreen();
    }

    private void MoveInScreen()
    {
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, -2.3f, 2.3f);
        position.y = Mathf.Clamp(position.y, -4.5f, 4.5f);

        transform.position = position;
    }
}
