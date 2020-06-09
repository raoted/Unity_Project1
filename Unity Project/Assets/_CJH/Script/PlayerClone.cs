using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    //아이템 먹어서 보조비행기가 생기도록 해야 한다
    //보조비행기는 일정시간마다 자동으로 총알발사 한다

    public GameObject clone;
    public GameObject bulletFactory;
    public float fireTime = 3.0f;
    float curTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CreateClone();
        AutoFire();
    }

    private void CreateClone()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            clone.SetActive(true);
        }
    }

    private void AutoFire()
    {
        //클론이 액티브상태일때 총알 자동발사 하기
        if(clone.activeSelf == true)
        {
            curTime += Time.deltaTime;
            if(curTime > fireTime)
            {
                //당연히 curTime 0으로 초기화
                curTime = 0.0f;

                //GameObject bullet1 = Instantiate(bulletFactory);
                //bullet1.transform.position = GameObject.Find("Sub1").transform.position;
                //bullet1.transform.position = clone.transform.Find("Sub1").position;
                //bullet1.transform.position = clone.transform.GetChild(0).position;

                //GameObject[] bullet = new GameObject[2];
                GameObject[] bullet = new GameObject[clone.transform.childCount];
                for(int i = 0; i < clone.transform.childCount; i++)
                {
                    bullet[i] = Instantiate(bulletFactory);
                    bullet[i].transform.position = clone.transform.GetChild(i).position;
                }
            }
        }
    }
}
