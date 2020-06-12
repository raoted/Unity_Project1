using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEMTYPE
{
    NORMAL,
    RAY,
    SUBWEAPON,
    END
};
public class Item : MonoBehaviour
{
    
    [SerializeField] float upSpeed;      //호출시 위로 날아가는 속도
    float curTime;
    ITEMTYPE itemType;
    public ITEMTYPE ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        curTime = 0;
        upSpeed = Random.Range(10.0f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //이거 코루틴 처리
        if(curTime < Time.deltaTime)
        {
            Debug.Log(Physics.gravity.y + " " + upSpeed);
            transform.position += Vector3.up * upSpeed * Time.deltaTime;
            curTime += Time.deltaTime;
        }
    }
}
