using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //자기 자신도 없애고
        //충돌된 오브젝트도 없앤다.
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
