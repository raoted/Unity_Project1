using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject normalFactory;
    [SerializeField] private GameObject lazerFactory;
    [SerializeField] private GameObject subFactory;
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
                itemType[selectItem]++;
                GameObject item;
                switch(selectItem)
                {
                    case 0:
                        item = Instantiate(normalFactory);
                        item.transform.GetChild(0).GetComponent<Item>().ItemType = ITEMTYPE.NORMAL;
                        item.SetActive(false);
                        item.hideFlags = HideFlags.HideInHierarchy;
                        itemPool.Enqueue(item);
                        break;
                    case 1:
                        item = Instantiate(lazerFactory);
                        item.transform.GetChild(0).GetComponent<Item>().ItemType = ITEMTYPE.RAY;
                        item.SetActive(false);
                        item.hideFlags = HideFlags.HideInHierarchy;
                        itemPool.Enqueue(item);
                        break;
                    case 2:
                        item = Instantiate(subFactory);
                        item.transform.GetChild(0).GetComponent<Item>().ItemType = ITEMTYPE.SUBWEAPON;
                        item.SetActive(false);
                        item.hideFlags = HideFlags.HideInHierarchy;
                        itemPool.Enqueue(item);
                        break;
                }
            }
        }
    }

    public void GetItem(Transform transform)
    {
        GameObject item = itemPool.Dequeue();
        item.transform.position = transform.position;
        item.transform.GetChild(0).GetComponent<Item>().UpSpeed = Random.Range(10.0f, 20.0f);
        item.transform.GetChild(0).GetComponent<Item>().CurTime = 0;
        item.SetActive(true);
    }
    public void InsertItem(GameObject gameObject)
    {
        gameObject.SetActive(false);
        itemPool.Enqueue(gameObject);
    }
}
