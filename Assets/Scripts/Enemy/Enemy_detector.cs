using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_detector : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.AddPlayer(other.transform);
        }

        if (other.CompareTag("Castle"))
        {
            enemy.EnemyTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.RemovePlayer(other.transform);
        }
    }
}