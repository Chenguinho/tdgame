using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    private Transform target;
    private int wpIndex = 0;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.waypoints[0];
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;

        transform.Translate(direction.normalized * enemy.Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        enemy.Speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wpIndex >= Waypoints.waypoints.Length - 1)
        {
            PathEnd();
            return;
        }
        wpIndex++;
        target = Waypoints.waypoints[wpIndex];
    }

    void PathEnd()
    {
        if(CurrentGame.Lives > 0)
            CurrentGame.Lives--;

        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }

}
