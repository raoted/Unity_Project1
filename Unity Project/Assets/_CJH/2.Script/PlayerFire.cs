using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;    //총알 프리팹
    public GameObject firePoint;         //총알 발사위치

    [SerializeField] private AudioSource audio;
    //레이져를 발사하기 위해서는 라인렌더러가 필요하다
    //선은 최소 2개의 점이 필요하다(시작점, 끝점)
    LineRenderer lr;    //라인렌더러 컴포넌트

    //총알 발사 시간 설정
    public float fireWait;
    public float fireTime;

    //일정시간동안만 레이져 보여주기
    public float rayTime = 0.3f;
    float timer = 0.0f;

    //오브젝트 풀링
    //오브젝트 풀링에 사용할 최대 총알 갯수
    int poolSize = 20;

    private Queue<GameObject> bulletPool;

    public static PlayerFire instance;
    private void Awake() => instance = this;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //라인렌더러 컴포넌트 추가
        lr = GetComponent<LineRenderer>();
        //중요!!!
        //게임오브젝트는 활성화 비활성화 => SetActive() 함수 사용
        //컴포넌트는 enabled 속성 사용

        fireTime = 0.0f;
        fireWait = 0.1f;

        //오브젝트 풀링 초기화
        InitObjectPooling();
    }

    //오브젝트 풀링 초기화
    private void InitObjectPooling()
    {
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            MakeBullet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Fire();
        //FireRay();
        //일정시간이 지나면 레이져 보여주는 기능 비활성화
        //if (lr.enabled) ShowRay();
    }

    private void ShowRay()
    {
        timer += Time.deltaTime;
        if (timer > rayTime)
        {
            lr.enabled = false;
            timer = 0.0f;
        }
    }

    //총알발사
    private void Fire()
    {
        if(Input.touchCount != 0)
        {
            if (fireTime >= fireWait)
            {
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
                audio.Play();

                if (bulletPool.Count > 0)
                {
                    GameObject bullet = bulletPool.Dequeue();
                    bullet.SetActive(true);
                    bullet.transform.position = firePoint.transform.position;
                    bullet.transform.up = firePoint.transform.up;
                    fireTime = 0.0f;
                }
                else
                {
                    MakeBullet();
                }
            }
            else
            {
                fireTime += Time.deltaTime;
            }
        }
    }

    private void MakeBullet()
    {
        GameObject bullet = Instantiate(this.bulletFactory);
        bullet.hideFlags = HideFlags.HideInHierarchy;
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
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
                if (hitInfo.collider.name != "Top")
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

    IEnumerator BulletFire()
    {
        if(audio.isPlaying)
        {
            audio.Stop();
        }
        audio.Play();

        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.up = firePoint.transform.up;
        }
        else
        {
            //총알 오브젝트 생성
            GameObject bullet = Instantiate(this.bulletFactory);
            bullet.SetActive(false);
            //bulletPool에 총알 오브젝트 삽입
            bulletPool.Enqueue(bullet);
        }

        yield return new WaitForSeconds(0.5f);
    }

    public void InsertBullet(GameObject bulletObject)
    {
        bulletObject.SetActive(false);
        bulletPool.Enqueue(bulletObject);
    }
}
