using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class SubWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletFactory;
    private float coolTime;
    public float fireTime;
    // Start is called before the first frame update
    void Start()
    {
        coolTime = 0;
        fireTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;
        if(coolTime >= 1.0f)
        {
            Instantiate(bulletFactory, transform.GetChild(0));
            coolTime = 0;
        }
    }
}
