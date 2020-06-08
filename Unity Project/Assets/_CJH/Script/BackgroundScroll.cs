using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Material material;
    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        material = transform.GetComponent<Renderer>().material;
        scrollSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float newOffsetY = material.mainTextureOffset.y + scrollSpeed * Time.deltaTime;
        material.mainTextureOffset = new Vector2(0, newOffsetY);
    }
}
