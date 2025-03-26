using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFlow : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    Renderer rend;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        material = rend.material;
    }

    // Update is called once per frame
    void Update()
    {
        float moveThis = Time.time * scrollSpeed;

        material.SetTextureOffset("_BaseMap", new Vector2(0, moveThis));
        //rend.material.SetTextureOffset("_MainTex", new Vector2(0, moveThis));
    }
}
