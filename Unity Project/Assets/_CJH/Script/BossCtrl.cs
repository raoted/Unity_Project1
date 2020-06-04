using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    [SerializeField] int bulletMax;
    float currentTime;
    public float attackCoolTime = 1.0f;
    public GameObject target;
    public GameObject bulletFactory;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletMax = 30;
        currentTime = 0.0f;
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 1.0f)
        {
            AutoFire1();
            AutoFire2();
            currentTime = 0.0f;
        }
    }

    void Fire()
    {
        Instantiate(bulletFactory, transform.GetChild(0), true);
        currentTime = 0.0f;
    }

    //플레이어를 향해 총알 발사
    void AutoFire1()
    {
        if (currentTime > attackCoolTime)
        {
            if (target != null)
            {
                //총알공장에서 총알생성
                GameObject bullet = Instantiate(bulletFactory);
                //총알생성 위치
                bullet.transform.position = transform.position;
                //플레이어를 향하는 방향 구하기(벡터의 뺄셈)
                Vector3 dir = target.transform.position - transform.position;
                dir.Normalize();
                //총구의 방향 바꿔주기;
                bullet.transform.up = dir;
            }
        }
    }

    void AutoFire2()
    {
        if (currentTime > attackCoolTime)
        {
            //총알공장에서 총알생성
            for (int i = 0; i < bulletMax; i++)
            {
                GameObject bullet = Instantiate(bulletFactory, transform);
                //총알생성 위치
                bullet.transform.position = transform.position;
                //플레이어를 향하는 방향 구하기(벡터의 뺄셈)
                float angle = 360.0f / bulletMax;
                //총구의 방향 바꿔주기;
                bullet.transform.eulerAngles = new Vector3(0, 0, angle * i);
            }
        }
    }
}
