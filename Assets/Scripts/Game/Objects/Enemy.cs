using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Atributos enemigo")]
    [HideInInspector]
    public float Speed, Health;
    public float startSpeed = 10f;
    public float startHealth = 100;
    public int money = 25;
    public int points = 10;
    public GameObject deathEffect;

    [Header("Barra de vida")]
    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        Speed = startSpeed;
        Health = startHealth;
    }

    public void TakeDamage(float x)
    {
        Health -= x;

        healthBar.fillAmount = Health / startHealth;

        if(Health <= 0 && !isDead)
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

        isDead = true;

        CurrentGame.Money += money;
        CurrentGame.Points += points;
        WaveSpawner.enemiesAlive--;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3.5f);

        Destroy(gameObject);
    }
}
