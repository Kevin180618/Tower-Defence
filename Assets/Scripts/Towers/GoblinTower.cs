using System.Collections;
using UnityEngine;

public class GoblinTower : MonoBehaviour
{
    public GameObject PlayerGoblin;

    public Transform P1;
    public Transform P2;
    public Transform P3;

    public GameObject Goblin1;
    public GameObject Goblin2;
    public GameObject Goblin3;

    [SerializeField] private float respawnDelay = 5f;

    private bool isRespawning = false;

    void Start()
    {
        Goblin1 = AddGoblins(P1);
        Goblin2 = AddGoblins(P2);
        Goblin3 = AddGoblins(P3);
    }

    private void Update()
    {
        if (!isRespawning &&
            Goblin1 == null &&
            Goblin2 == null &&
            Goblin3 == null)
        {
            StartCoroutine(RespawnGoblins());
        }
    }

    public GameObject AddGoblins(Transform pos)
    {
        GameObject goblin = Instantiate(
            PlayerGoblin,
            pos.position,
            pos.rotation
        );

        return goblin;
    }

    private IEnumerator RespawnGoblins()
    {
        isRespawning = true;

        yield return new WaitForSeconds(respawnDelay);

        Goblin1 = AddGoblins(P1);

        yield return new WaitForSeconds(Random.Range(3f, 6f));

        Goblin2 = AddGoblins(P2);

        yield return new WaitForSeconds(Random.Range(3f, 6f));

        Goblin3 = AddGoblins(P3);

        isRespawning = false;
    }
}