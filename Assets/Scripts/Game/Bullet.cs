using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject effect;

    public void Chase(Transform _target)
    {
        target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceInFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceInFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceInFrame, Space.World);

        transform.LookAt(target);

    }

    void HitTarget()
    {
        GameObject e = (GameObject) Instantiate(effect, transform.position, transform.rotation);
        Destroy(e, 5f);

        if(explosionRadius > 0f)
        {
            Explode();
        } else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider h in hit)
        {
            if(h.tag == "Enemy")
            {
                Damage(h.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }
}
