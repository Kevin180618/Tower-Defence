using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour {

    public Enemy enemy;
    public float maxhealth = 100;
    [SerializeField] private float health;
    [SerializeField] private Slider HealthSlider;

    private void Start()
    {
        health = maxhealth;

        HealthSlider.value = health / maxhealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        HealthSlider.value = health / maxhealth;
        Debug.Log("Slider value" + HealthSlider.value);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!enemy.IsDied)
        {
            enemy.Die();
            gameObject.layer = 0;
        }
    }

}
