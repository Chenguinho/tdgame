using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wpIndex = 0;

    void Start()
    {
        target = Waypoints.waypoints[0];
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;

        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if(wpIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(this.gameObject);
            return;
        }
        wpIndex++;
        target = Waypoints.waypoints[wpIndex];
    }
}
