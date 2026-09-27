using System;
using UnityEngine;
using UnityEngine.UI;

public class castle : MonoBehaviour
{
    [SerializeField] private float MaxHealth;
    public float Health;
    [SerializeField] private Slider HealthSlider;

    private void Start()
    {
        Health = MaxHealth;
        HealthSlider.value = Health / MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        HealthSlider.value = Health/ MaxHealth;

        Debug.Log(gameObject.name + " took " + damage + " damage. HP: " + Health);

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayingManager.inst.GameOver();
        PlayingManager.inst.IsgameOver = true;

    }
}