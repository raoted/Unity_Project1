using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ITEMTYPE
{
    NORMAL = 0,
    RAY,
    SUBWEAPON,
    END = 3
};

public class Item : MonoBehaviour
{

    float upSpeed;      //호출시 위로 날아가는 속도
    public float UpSpeed
    {
        get { return upSpeed; }
        set { upSpeed = value; }
    }
    float curTime;
    public float CurTime
    {
        set { curTime = 0; }
    }
    ITEMTYPE itemType;
    public ITEMTYPE ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(curTime < Time.deltaTime)
        {
            transform.position += Vector3.up * upSpeed * Time.deltaTime;
            curTime += Time.deltaTime;
        }
    }
}
