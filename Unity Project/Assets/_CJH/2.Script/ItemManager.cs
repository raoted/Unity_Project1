﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemFactory;
    Queue<GameObject> itemPool;
    private int poolSize;
    private int[] itemType;
    private int[] maxItem;
    private int selectItem;

    public static ItemManager instance;
    private void Awake() => instance = this;

    // Start is called before the first frame update
    void Start()
    {
        itemFactory = new GameObject[3];
        itemType = new int[3];
        maxItem = new int[3];

        poolSize = 9;
        for (int i = 0; i < maxItem.Length; i++)
        {
            maxItem[i] = poolSize / maxItem.Length;
            itemType[i] = 0;
        }

        itemPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            selectItem = Random.Range(0, maxItem.Length);
            if (itemType[selectItem] >= maxItem[selectItem])
            {
                i--;
                continue;
            }
            else
            {
                GameObject item = itemFactory[selectItem];
                item.SetActive(false);
                item.hideFlags = HideFlags.HideInHierarchy;
                itemPool.Enqueue(item);
            }
        }
    }

    public void GetItem(Transform transform)
    {
        GameObject item = itemPool.Dequeue();
        item.transform.position = transform.position;
        item.SetActive(true);
    }
    public void InsertItem(GameObject gameObject)
    {
        gameObject.SetActive(false);
        itemPool.Enqueue(gameObject);
    }
}
