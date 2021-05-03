using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [Range(-1f, 1f)] public float scrollSpeed = 0.5f;
    private float _offset;
    private Material _mat;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    
    void Update()
    {
        _offset += (Time.deltaTime * scrollSpeed) / 10f;
        _mat.SetTextureOffset(MainTex, new Vector2(_offset, 0));
    }
}
