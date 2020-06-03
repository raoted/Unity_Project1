using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyFactory;
    public int idx;
    public GameObject[] spawnPoint;
    float spawnTime = 1.0f;        //스폰타임
    float curTime = 0.0f;          //누적타임

    // Update is called once per frame
    void Update()
    {
        SpawEnemy();    
    }

    private void SpawEnemy()
    {
        //몇초에 한번씩 이벤트 발동
        //시간 누적타임으로 계산한다.
        //게임에서 자수 사용됨.

        curTime += Time.deltaTime;
        if(curTime > spawnTime)
        {
            spawnTime = Random.Range(0.5f, 2.0f);
            //누적된 시간을 0으로 초기하

            curTime = 0.0f;


            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = spawnPoint[Random.Range(0, idx)].transform.position;
        }
    }
}
