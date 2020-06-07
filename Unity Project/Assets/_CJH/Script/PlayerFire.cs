using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //레이저를 발사하기 위해서는 라인렌더러가 필요
    //선은 최소 2개의 점이 필요하다(시작점, 끝점)

    private int layerMask;
    private float currentTime = 0.0f;
    private float disableTime = 0.3f;

    RaycastHit hit;

    Vector3 direction;

    LineRenderer lr;
    public GameObject bulletFactory;
    // Start is called before the first frame update
    void Start()
    {
        //라인렌더러 컴포넌트 추출
        lr = GetComponent<LineRenderer>();
        //라인렌더러 라인 색 설정
        lr.SetColors(Color.red, Color.yellow);
        //레이어마스크 설정
        layerMask = (1 << 5) | (1 << 10);
    }

    // Update is called once per frame
    void Update()
    {
        //Fire();
        FireRay();
        if (lr.enabled)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= disableTime)
            {
                currentTime = 0;
                lr.enabled = false;
            }

            if(hit.transform.gameObject.layer == 9) { Destroy(hit.transform.gameObject); }
            //라인 시작점, 끝점
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position + Vector3.up * hit.distance);
        }
    }

    //총알 발사
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletFactory, transform.GetChild(0));
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetChild(1).gameObject.SetActive(!transform.GetChild(1).gameObject.activeSelf);
        }
    }

    //레이저 발사
    private void FireRay()
    {
        Physics.Raycast(transform.position, Vector3.up, out hit, 50.0f, ~layerMask);
        if (Input.GetButtonDown("Fire1"))
        {
            if (!lr.enabled)
            {
                //라인렌더러 컴포넌트 활성화
                lr.enabled = true;
            }
        }
    }
}
