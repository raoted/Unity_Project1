using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //보스 총알발사 (총알패턴)
    //1. 플레이어를 향해서 총알발사
    //2. 회전총알 발사

    public GameObject bulletFactory;        //총알 프리팹
    public GameObject target;               //플레이어 타겟
    public float fireTime = 1.0f;             //1초에 한번씩 총알발사  
    float curTime = 0.0f;
    public float fireTime1 = 1.5f;            //1.5초에 한번씩 총알발사
    float curTime1 = 0.0f;
    public int bulletMax = 10;

    void Update()
    {
        AutoFire1();
        AutoFire2();
    }

    //플레이어를 향해서 총알발사
    private void AutoFire1()
    {
        //타겟이 없을때 에러발생하니 예외처리
        if(target != null)
        {
            curTime += Time.deltaTime;
            if (curTime > fireTime)
            {
                //총알공장에서 총알생성
                GameObject bullet = Instantiate(bulletFactory);
                //총알생성 위치
                bullet.transform.position = transform.position;
                //플레이어를 향하는 방향 구하기 (벡터의 뺄셈)
                Vector3 dir = target.transform.position - transform.position;
                dir.Normalize();
                //총구의 방향도 맞춰준다(이게 중요함)
                bullet.transform.up = dir;
                //타이머 초기화
                curTime = 0.0f;
            }
        }
    }

    //회전 총알발사
    private void AutoFire2()
    {
        //타겟이 없을때 에러발생하니 예외처리
        if (target != null)
        {
            curTime1 += Time.deltaTime;
            if (curTime1 > fireTime)
            {
                //총알 최대갯수만큼
                for(int i = 0;i < bulletMax; i++)
                {
                    //총알공장에서 총알생성
                    GameObject bullet = Instantiate(bulletFactory);
                    //총알생성 위치
                    bullet.transform.position = transform.position;
                    //360도 방향으로 총알발사
                    float angle = 360.0f / bulletMax;
                    //총구의 방향도 맞춰준다(이게 중요함)
                    bullet.transform.eulerAngles = new Vector3(0, 0, i * angle);

                }

                //타이머 초기화
                curTime1 = 0.0f;
            }
        }
    }
}
