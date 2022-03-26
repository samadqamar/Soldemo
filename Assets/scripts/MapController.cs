using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject minimap;
    public GameObject map;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown( KeyCode.M ))
        {
            if (minimap.activeInHierarchy)
            {
                minimap.SetActive( false );
                map.SetActive( true );
            }
            else
            {
                minimap.SetActive( true );
                map.SetActive( false );
            }
        }   
    }
}
