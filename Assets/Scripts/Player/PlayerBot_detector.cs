using UnityEngine;

public class PlayerBot_detector : MonoBehaviour
{
    public PlayerBot playerBot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemyBug"))
        {
            playerBot.AddEnemy(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("enemyBug"))
        {
            playerBot.RemoveEnemy(other.transform);
        }
    }
}