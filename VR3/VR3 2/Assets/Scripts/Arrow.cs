using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float scrollSpeed = 0.03f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = -Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, -1.72f));
    }
}
