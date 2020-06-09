using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;    //총알 프리팹
    public GameObject firePoint;         //총알 발사위치

    //레이져를 발사하기 위해서는 라인렌더러가 필요하다
    //선은 최소 2개의 점이 필요하다(시작점, 끝점)
    LineRenderer lr;    //라인렌더러 컴포넌트
    
    //일정시간동안만 레이져 보여주기
    public float rayTime = 0.3f;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //라인렌더러 컴포넌트 추가
        lr = GetComponent<LineRenderer>();
        //중요!!!
        //게임오브젝트는 활성화 비활성화 => SetActive() 함수 사용
        //컴포넌트는 enabled 속성 사용
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        //FireRay();
        //레이져 보여주는 기능이 활성화 되어 있을때만
        //레이져를 보여준다
        //일정시간이 지나면 레이져 보여주는 기능 비활성화
        //if (lr.enabled) ShowRay();
    }

    private void ShowRay()
    {
        timer += Time.deltaTime;
        if(timer > rayTime)
        {
            lr.enabled = false;
            timer = 0.0f;
        }
    }

    //총알발사
    private void Fire()
    {
        //마우스왼쪽버튼 or 왼쪽컨트롤 키
        if(Input.GetButtonDown("Fire1"))
        {
            //총알공장(총알프리팹)에서 총알을 무한대로 찍어낼 수 있다
            //Instantiate() 함수로 프리팹 파일을 게임오브젝트로 만든다

            //총알 게임오브젝트 생성
            GameObject bullet = Instantiate(bulletFactory);
            //총알 오브젝트의 위치 지정
            //bullet.transform.position = transform.position;
            bullet.transform.position = firePoint.transform.position;
        }
    }

    //레이져발사
    private void FireRay()
    {
        //마우스왼쪽버튼 or 왼쪽컨트롤 키
        if (Input.GetButtonDown("Fire1"))
        {
            //라인렌더러 컴포넌트 활성화
            lr.enabled = true;
            //라인 시작점, 끝점
            lr.SetPosition(0, transform.position);
            //lr.SetPosition(1, transform.position + Vector3.up * 10);
            //라인의 끝점은 충돌된 지점으로 변경한다

            //Ray로 충돌처리
            Ray ray = new Ray(transform.position, Vector3.up);
            RaycastHit hitInfo; //Ray와 충돌된 오브젝트의 정보를 담는다
            //Ray랑 충돌된 오브젝트가 있다
            if (Physics.Raycast(ray, out hitInfo))
            {
                //레이져의 끝점 지정
                lr.SetPosition(1, hitInfo.point);
                //충돌된 오브젝트 모두 지우기
                //Destroy(hitInfo.collider.gameObject);

                //디스트로이존의 탑과는 충돌처리 되지 않도록 한다
                if(hitInfo.collider.name != "Top")
                {
                    Destroy(hitInfo.collider.gameObject);
                }

                //충돌된 에너미 오브젝트 삭제
                //프리팹으로 만든 오브젝트 같은경우는 생성될때 클론으로 생성된다
                //Contains("Enemy") => Enemy(clone) 이런것도 포함함
                //if (hitInfo.collider.name.Contains("Enemy"))
                //{
                //    Destroy(hitInfo.collider.gameObject);
                //}

            }
            else
            {
                //충돌된 오브젝트가 없으니 끝점을 정해준다
                lr.SetPosition(1, transform.position + Vector3.up * 10);
            }

        }
    }
}
