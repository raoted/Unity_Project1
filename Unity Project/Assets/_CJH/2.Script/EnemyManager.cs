//using System; //이놈이 있으면 Random함수를 사용불가
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //에너미매니져 역할?
    //에너미들을 공장에서 찍어낸다(에너미 프리팹)
    //에너미 스폰타임
    //에너미 스폰위치
    static public EnemyManager instance;
    public GameObject boss;
    public GameObject enemyFactory;    //에너미 공장 (에너미프리팹)
    //public GameObject spawnPoint;    //스폰위치
    public GameObject[] spawnPoints;   //스폰위치 여러개
    float spawnTime = 1.0f;             //스폰타임 (몇초에 한번씩 찍어낼거냐?)   
    float curTime = 0.0f;               //누적타임   

    private int spawned;
    private int callBoss;
    //오브젝트(에너미)풀 생성
    private Queue<GameObject> enemyPool;

    //오브젝트(에너미)풀 초기 크기 설정
    int poolSize = 10;

    //보스 몬스터 등장 여부
    private bool bossSpawn = false;
    public bool BossSpawn
    {
        get { return bossSpawn; }
        set { bossSpawn = value; }
    }

    private void Awake() => instance = this;

    void Start()
    {
        callBoss = 10;
        spawned = 0;
        enemyPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            MakeEnemy();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") != null && spawned <= 10)
        {
            //에너미 생성
            SpawnEnemy();
        }
        if(BossSpawn && !boss.activeSelf) { boss.SetActive(true); }
    }
    private void SpawnEnemy()
    {
        //몇초에 한번씩 이벤트 발동
        //시간 누적타임으로 계산한다
        //게임에서 정말 자주 사용함

        curTime += Time.deltaTime;
        if(curTime > spawnTime)
        {
            //에너미 생성
            if (enemyPool.Count > 0)
            {
                if(spawned >= callBoss)
                {
                    UIManager.instance.Alert();
                    spawned++;
                }
                spawned++;
                //누적된 현재시간을 0초로 초기화(반드시 해줘야 한다)
                curTime = 0.0f;
                //스폰타임을 랜덤으로
                spawnTime = Random.Range(0.5f, 2.0f);
                
                GameObject enemy = enemyPool.Dequeue();
                //enemy.transform.position = spawnPoint.transform.position;
                int index = Random.Range(0, spawnPoints.Length);
                enemy.transform.position = spawnPoints[index].transform.position;
                enemy.transform.Rotate(0, 0, 0);
                enemy.transform.up = spawnPoints[index].transform.up;
                enemy.transform.GetChild(0).gameObject.SetActive(true);
                enemy.GetComponent<Enemy>().IsDestroy = false;
                enemy.GetComponent<BoxCollider>().enabled = true;
                enemy.SetActive(true);
            }
            else
            {
                MakeEnemy();
                poolSize++;
            }
        }
    }
    void MakeEnemy()
    {
        GameObject enemy = Instantiate(enemyFactory);
        enemy.SetActive(false);
        enemy.hideFlags = HideFlags.HideInHierarchy;
        enemyPool.Enqueue(enemy);
    }
    public void InsertPool(GameObject _gameObject)
    {
        enemyPool.Enqueue(_gameObject);
    }
}
