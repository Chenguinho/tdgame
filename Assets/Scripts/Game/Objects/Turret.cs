using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Balas (por defecto)")]

    public GameObject bullet;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Laser")]

    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damage = 25;
    public float slow = 0.35f;

    [Header("No cambiar")]

    public float turnSpeed = 10f;
    public Transform rotate;
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
            targetEnemy = closestEnemy.GetComponent<Enemy>();
        } else
        {
            target = null;
        }

    }

    void Update()
    {

        if(target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        } else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

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

    void Laser()
    {

        targetEnemy.TakeDamage(damage * Time.deltaTime);
        targetEnemy.Slow(slow);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
           
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 look = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(look);
        impactEffect.transform.position = target.position + look.normalized;

    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion look = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotate.rotation, look, Time.deltaTime * turnSpeed).eulerAngles;
        rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
