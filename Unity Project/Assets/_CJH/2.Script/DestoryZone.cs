using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryZone : MonoBehaviour
{
    PlayerFire playerFire;

    private void Start()
    {
        playerFire = GameObject.Find("Player").GetComponent<PlayerFire>();
    }

    //트리거 감지 후 해당 오브젝트 삭제
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            PlayerFire.instance.InsertBullet(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            ItemManager.instance.InsertItem(other.gameObject);
        }
    }
}
