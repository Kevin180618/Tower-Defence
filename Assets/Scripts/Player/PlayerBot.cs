using System.Collections.Generic;
using UnityEngine;

public class PlayerBot : MonoBehaviour
{
    public PlayerHP playerHP;
    [Header("Attack")]
    public float Damage = 10;
    public float attackRange = 1.5f;

    [Header("Movement")]
    public float MoveSpeed = 3f;
    public float ReturnSpeed = 3f;

    [Header("Animation")]
    public Animator anim;

    [Header("Current Target")]
    public Transform target;
    public GameObject EnemyTarget;

    private List<Transform> enemiesInRange = new List<Transform>();

    // Movement state
    public bool enemyDetected = false;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    public bool returningHome = false;


    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        Damage = TowerManager.inst.GoblinTowerUpgrades[TowerManager.inst.GobllinTowerUpgradeNum].Damage;
        attackRange = TowerManager.inst.GoblinTowerUpgrades[TowerManager.inst.GobllinTowerUpgradeNum].Range;
        playerHP.maxHealth = TowerManager.inst.GoblinTowerUpgrades[TowerManager.inst.GobllinTowerUpgradeNum].Health;
    }


    public void AddEnemy(Transform enemy)
    {
        if (enemy == null)
            return;

        if (!enemiesInRange.Contains(enemy))
            enemiesInRange.Add(enemy);
    }
    public void RemoveEnemy(Transform enemy)
    {
        if (enemy == null)
            return;

        enemiesInRange.Remove(enemy);
    }

    Transform GetNearestEnemy()
    {
        Transform nearest = null;
        float nearestDist = float.MaxValue;

        foreach (Transform e in enemiesInRange)
        {
            if (e == null)
                continue;

            float d = Vector3.Distance(transform.position, e.position);

            if (d < nearestDist)
            {
                nearestDist = d;
                nearest = e;
            }
        }

        return nearest;
    }



    void Update()
    {
        enemiesInRange.RemoveAll(e => e == null);

        if (enemiesInRange.Count > 0)
        {
            Transform nearestEnemy = GetNearestEnemy();

            if (nearestEnemy != null)
            {
                enemyDetected = true;
                returningHome = false;

                target = nearestEnemy;
                EnemyTarget = nearestEnemy.gameObject;

                MoveTowardsEnemy();
                return;
            }
        }

        if (enemyDetected)
        {
            enemyDetected = false;

            target = null;
            EnemyTarget = null;

            anim.SetBool("Attack", false);

            returningHome = true;
        }

        if (returningHome)
        {
            ReturnToSpawn();
        }
        else
        {

            anim.SetBool("RUN", false);
            anim.SetBool("Idle", true);
        }
    }


    void MoveTowardsEnemy()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position;

        targetPosition.y = transform.position.y;

        float distanceToEnemy =
            Vector3.Distance(transform.position, targetPosition);

        Vector3 direction = targetPosition - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion rotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                10f * Time.deltaTime
            );
        }


        if (distanceToEnemy <= attackRange)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("RUN", false);
            anim.SetBool("Attack", true);
            return;
        }


        anim.SetBool("Idle", false);
        anim.SetBool("Attack", false);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            MoveSpeed * Time.deltaTime
        );

        anim.SetBool("RUN", true);
    }


    void ReturnToSpawn()
    {
        float distanceHome =
            Vector3.Distance(transform.position, spawnPosition);

        if (distanceHome <= 0.05f)
        {
            transform.position = spawnPosition;
            transform.rotation = spawnRotation;

            returningHome = false;

            anim.SetBool("RUN", false);
            anim.SetBool("Idle", true);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            spawnPosition,
            ReturnSpeed * Time.deltaTime
        );

        Vector3 direction = spawnPosition - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion rotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                10f * Time.deltaTime
            );
        }

        anim.SetBool("Idle", false);
        anim.SetBool("RUN", true);
    }


    public void GetDamage()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;

        if (Vector3.Distance(transform.position, targetPosition) > attackRange)
            return;

        EnemyHp enemyHp = target.GetComponent<EnemyHp>();

        if (enemyHp != null)
        {
            enemyHp.TakeDamage(Damage);
        }
    }


    public void Die()
    {
        enemyDetected = false;
        returningHome = false;

        target = null;
        EnemyTarget = null;
        enemiesInRange.Clear();

        anim.SetBool("Idle", false);
        anim.SetBool("RUN", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Death", true);

        enabled = false;

        Destroy(gameObject, 2f);
    }
}