using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;    //총알 프리팹
    public GameObject lazerFactory;    //총알 프리팹

    [SerializeField] private AudioSource audio;

    public AudioClip normal;
    public AudioClip lazer;
    //레이져를 발사하기 위해서는 라인렌더러가 필요하다
    //선은 최소 2개의 점이 필요하다(시작점, 끝점)
    LineRenderer lr;    //라인렌더러 컴포넌트

    //총알 발사 시간 설정
    public float fireWait;
    public float fireTime;

    //일정시간동안만 레이져 보여주기
    public float rayTime = 0.3f;

    //총알 종류
    int bulletType;
    //1회 공격시 발사하는 총알의 개수
    int fireCount;

    //오브젝트 풀링
    int poolSize = 20;
    //총알 오브젝트 풀
    private Queue<GameObject> bulletPool;
    private Queue<GameObject> lazerPool;
    public static PlayerFire instance;
    private void Awake() => instance = this;
    // Start is called before the first frame update
    void Start()
    {
        normal = (AudioClip)Resources.Load("SE/HeavyWeapons3");
        lazer = (AudioClip)Resources.Load("SE/Laser13");

        audio = GetComponent<AudioSource>();
        audio.clip = normal;
        audio.volume = SoundMgr.Instance.MasterVolume * SoundMgr.Instance.SEVolume;
        audio.Stop();
        //라인렌더러 컴포넌트 추가
        lr = GetComponent<LineRenderer>();
        //중요!!!
        //게임오브젝트는 활성화 비활성화 => SetActive() 함수 사용
        //컴포넌트는 enabled 속성 사용

        fireTime = 0.0f;
        fireWait = 0.1f;

        //총알 종류 초기화
        bulletType = 0;
        //1회 공격시 발사하는 총알의 개수 초기화
        fireCount = 1;

        //오브젝트 풀링 초기화
        InitObjectPooling();
    }

    //오브젝트 풀링 초기화
    private void InitObjectPooling()
    {
        bulletPool = new Queue<GameObject>();
        lazerPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            MakeBullet();
            MakeLazer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletType == 0) { Fire(); }
        else if (bulletType == 1) { FireRay(); }
        //일정시간이 지나면 레이져 보여주는 기능 비활성화
        //if (lr.enabled) ShowRay();
    }

    //총알발사
    private void Fire()
    {
        if (Input.touchCount != 0 || Input.GetButton("Fire1"))
        {
            if (fireTime >= fireWait)
            {
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
                audio.Play();

                for (int i = 0; i < fireCount; i++)
                {
                    if (bulletPool.Count > 0)
                    {
                        GameObject bullet = bulletPool.Dequeue();
                        bullet.GetComponent<Bullet>().setAtk = 3.0f;
                        bullet.SetActive(true);
                        bullet.transform.position = transform.GetChild(0).GetChild(i).position;
                        bullet.transform.up = transform.GetChild(0).up;
                    }
                    else
                    {
                        MakeBullet();
                        i--;
                    }
                }
                fireTime = 0.0f;
            }
            else
            {
                fireTime += Time.deltaTime;
            }
        }
    }

    private void MakeBullet()
    {
        GameObject bullet = Instantiate(bulletFactory);
        bullet.hideFlags = HideFlags.HideInHierarchy;
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    private void MakeLazer()
    {
        GameObject lazer = Instantiate(lazerFactory);
        lazer.hideFlags = HideFlags.HideInHierarchy;
        lazer.SetActive(false);
        lazerPool.Enqueue(lazer);
    }

    //레이져발사
    private void FireRay()
    {
        //마우스왼쪽버튼 or 왼쪽컨트롤 키
        if (Input.GetButton("Fire1"))
        {
            if (fireTime >= fireWait)
            {
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
                audio.Play();

                for (int i = 0; i < fireCount; i++)
                {
                    if (lazerPool.Count > 0)
                    {
                        GameObject lazer = lazerPool.Dequeue();
                        lazer.GetComponent<Bullet>().setAtk = 1.0f;
                        lazer.SetActive(true);
                        lazer.transform.position = transform.GetChild(0).GetChild(i).position;
                        lazer.transform.up = transform.GetChild(0).up;
                    }
                    else
                    {
                        MakeLazer();
                        i--;
                    }
                }
                fireTime = 0.0f;
            }
            else
            {
                fireTime += Time.deltaTime;
            }
        }        
    }


    public void InsertBullet(GameObject bulletObject)
    {
        bulletObject.SetActive(false);
        bulletPool.Enqueue(bulletObject);
    }

    public void InsertLazer(GameObject lazerObject)
    {
        lazerObject.SetActive(false);
        lazerPool.Enqueue(lazerObject);
    }

    public void ChangeBullet(int i)
    {
        if (i == 0) { audio.clip = normal; }
        else { audio.clip = lazer; }

        if (i == 2)
        {
            transform.GetComponent<PlayerClone>().CreateClone();
        }
        else if (bulletType == i)
        { 
            if (fireCount == 3) { UIManager.instance.AddScore(5); }
            else
            {
                fireCount++;

                for (int count = 0; count < fireCount; count++)
                {
                    transform.GetChild(0).GetChild(count).gameObject.SetActive(true);
                    transform.GetChild(0).GetChild(count).position =
                        new Vector3(-0.25f * (fireCount - 1) + (0.5f * count) + transform.GetChild(0).position.x,
                        transform.GetChild(0).position.y, 0);
                }
            }
        }
        else
        {
            //새로 획득한 총알이 기존의 총알과 다르다면
            //1회 공격시 발사하는 총알의 개수는 1로 바꾼다.
            bulletType = i;
            transform.GetChild(0).GetChild(0).position = transform.GetChild(0).position;
            for (int count = 1; count < fireCount; count++)
            {
                transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
            fireCount = 1;
            if (i == 0) { Debug.Log("Normal"); }
            else { Debug.Log("Ray"); }
        }
    }
}
