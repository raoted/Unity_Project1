using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();   
    }

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
}
