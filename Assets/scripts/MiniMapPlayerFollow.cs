using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPlayerFollow : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Transform>();
        }
        Vector3 newposition = player.position;
        newposition.y = transform.position.y;
        transform.position = newposition;
        transform.rotation = Quaternion.Euler( 90f, player.eulerAngles.y, 0f );
    }
}
