using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    //꼬랑지가 플레이어를 따라다닐려면
    //플레이어의 위치를 알아야 한다
    public GameObject target;       //플레이어 오브젝트
    public float speed = 3.0f;       //꼬랑지 속도

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        //타겟 방향 구하기(벡터의 뺄셈)
        //방향 = 타겟 - 자기자신
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
