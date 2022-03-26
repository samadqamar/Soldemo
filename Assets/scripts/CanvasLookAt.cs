using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    public GameObject cam;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag( "MainCamera" );
    }
    void Update()
    {
        transform.LookAt( cam.transform );
    }
}
