using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 finishPos = Vector3.zero; // position to move to
    public float speed = 0.5f;

    private Vector3 startPos;
    private float trackPercent = 0; // how far along the "track" between start and finish
    private int direction = 1; // current movement direction

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // placement in the scene is the positon to move from
    }

    // Update is called once per frame
    void Update()
    {
        trackPercent += direction * speed * Time.deltaTime;
        float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
        float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
        transform.position = new Vector3(x, y, startPos.z);

        if((direction == 1 && trackPercent > .9f) || (direction == -1 && trackPercent < .1f)) { // changes direction at both start and end
            direction *= -1;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, finishPos);
    }
}
