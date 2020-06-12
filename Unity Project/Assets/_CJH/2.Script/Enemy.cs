using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    //위에서 아래로 떨어지기만 한다 (똥피하기 느낌)
    //충돌처리 (에너미랑 플레이어, 에너미랑 플레이어 총알)
    public float speed;
    private float time = 0;
    //폭발 이미지를 보여주기 위한 변수
    public GameObject fxFactory;
    //피격 유무를 저장하는 변수
    private bool isDestroy;
    public bool IsDestroy
    {
        get { return isDestroy; }
        set { isDestroy = value; }
    }
    void Start()
    {
        isDestroy = false;
        fxFactory = transform.GetChild(1).gameObject;
        fxFactory.SetActive(false);
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroy)
        {
        }
        else
        {
            //아래로 이동해라
            transform.Translate(Vector3.down.normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if(Random.Range(0, 3) == 3) { ItemManager.instance.GetItem(gameObject.transform); }
            gameObject.GetComponent<BoxCollider>().enabled = false;

            if (!isDestroy)
            {
                UIManager.instance.AddScore();
                ShowEffect();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("DestroyZone")) 
        {
            fxFactory.SetActive(false);
            gameObject.SetActive(false);
            isDestroy = false;
            EnemyManager.instance.InsertPool(gameObject);
        }
        else
        {
            if (collision.gameObject.CompareTag("Player")) { collision.gameObject.SetActive(false); }
            ShowEffect();
        }

        //자기자신도 없애고
        //충돌된 오브젝트도 없앤다
        //Destroy(gameObject, 1.0f); //1초후에 오브젝트 삭제
    }

    void ShowEffect()
    {
        isDestroy = true;
        
        transform.GetChild(0).gameObject.SetActive(false);
        fxFactory.SetActive(true);
        if (time >= 1.0f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            time = 0.0f;
            fxFactory.SetActive(false);
            gameObject.SetActive(false);
            isDestroy = false;
            EnemyManager.instance.InsertPool(gameObject);
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
