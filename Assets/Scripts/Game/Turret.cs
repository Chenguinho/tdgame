using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;

    [Header("Posibilidades de la torreta")]

    public float range = 15f;
    public float fireRate = 1f;

    [Header("No cambiar")]

    public float turnSpeed = 10f;
    private float fireCountdown = 0f;
    public Transform rotate;

    public GameObject bullet;
    public Transform firePoint;

    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }

    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float closest = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < closest)
            {
                closest = distance;
                closestEnemy = enemy;
            }
        }

        if(closestEnemy != null && closest <= range)
        {
            target = closestEnemy.transform;
        } else
        {
            target = null;
        }

    }

    void Update()
    {

        if(target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion look = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotate.rotation, look, Time.deltaTime * turnSpeed).eulerAngles;

        rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate(bullet, firePoint.position, firePoint.rotation);

        Bullet b = bulletGO.GetComponent<Bullet>();

        if(b != null)
        {
            b.Chase(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
