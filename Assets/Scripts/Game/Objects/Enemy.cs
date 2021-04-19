using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Atributos enemigo")]
    [HideInInspector]
    public float Speed;
    public float startSpeed = 10f;
    public float health = 100;
    public int money = 25;
    public GameObject deathEffect;

    void Start()
    {
        Speed = startSpeed;
    }

    public void TakeDamage(float x)
    {
        health -= x;

        if(health <= 0)
        {
            Die();
        }
    }

    public void Slow(float x)
    {
        Speed = startSpeed * (1f - x);
    }

    void Die()
    {
        CurrentGame.Money += money;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3.5f);

        Destroy(gameObject);
    }
}
