using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interaction : MonoBehaviour
{
    
    public LayerMask layer;
    public float distance;
    public Transform player;
    public GameObject obj;
    public Camera cam;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Transform>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Transform>();
        }
        if (Input.GetMouseButtonDown( 0 ))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay( Input.mousePosition );
            if (Physics.Raycast( ray, out hit, distance, layer ))
            {
                
                player.position = hit.point;
                Debug.Log( hit.transform.position );
                Debug.Log( hit.transform.name );
            }
        }

    }
    
}
