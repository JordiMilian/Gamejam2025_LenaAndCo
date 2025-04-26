using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentAigua : MonoBehaviour
{
    [Range(-0.5f, 0.5f)]
    public float VelAguaX = 0f;

    [Range(-0.5f, 0.5f)]
    public float VelAguaY = 0f;
    public Vector2 vectoragua;

    public Material aiguaMat;

    public float speedmulti = 1;

    // Update is called once per frame
    void Update()
    {
        vectoragua.x += VelAguaX * Time.deltaTime * speedmulti;
        vectoragua.y += VelAguaY * Time.deltaTime * speedmulti;
        aiguaMat.mainTextureOffset = vectoragua;


    }
}
