using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    public float maxHealth = 100;
    private float health;
    private bool isDead = false;

    [SerializeField] private Slider HealthSlider;

    [Header("Bot Link")]
    public PlayerBot playerBot;

    void Awake()
    {
        health = maxHealth;
        HealthSlider.value = health / maxHealth;

        if (playerBot == null)
            playerBot = GetComponent<PlayerBot>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        health -= damage;
        HealthSlider.value = health/ maxHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        Debug.Log(gameObject.name + " died");

        if (playerBot != null)
        {
            playerBot.Die();
        }
        else
        {
            Destroy(gameObject);
        }
    }

}