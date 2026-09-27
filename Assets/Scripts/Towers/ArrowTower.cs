using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private bool IsBomb;
    [SerializeField] private float attackRange = 8f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Arrow")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;

    private float attackTimer;


    private void Start()
    {
        if (IsBomb)
        {
            attackRange = TowerManager.inst.BombTowerUpgrades[TowerManager.inst.BombTowerUpgradeNum].Range;
            attackRate = TowerManager.inst.BombTowerUpgrades[TowerManager.inst.BombTowerUpgradeNum].RateOfFire;
        }
        else
        {
            attackRange = TowerManager.inst.ArcherTowerUpgrades[TowerManager.inst.AcherTowerUpgradeNum].Range;
            attackRate = TowerManager.inst.ArcherTowerUpgrades[TowerManager.inst.AcherTowerUpgradeNum].RateOfFire;
        }
    }
    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            Transform target = FindTarget();

            if (target != null)
            {
                FireArrow(target);

                // Time between attacks
                attackTimer = 1f / attackRate;
            }
        }
    }

    private Transform FindTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(
            transform.position,
            attackRange,
            enemyLayer
        );

        if (enemies.Length == 0)
            return null;

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider enemy in enemies)
        {
            float distance = Vector3.Distance(
                transform.position,
                enemy.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    private void FireArrow(Transform target)
    {
        GameObject arrowObject = Instantiate(
            arrowPrefab,
            firePoint.position,
            Quaternion.identity
        );

        ArrowProjectile arrow =
            arrowObject.GetComponent<ArrowProjectile>();

        if (arrow != null)
        {
            arrow.Launch(target);
        }
        else
        {
            Debug.LogError(
                "ArrowProjectile component is missing on Arrow Prefab!"
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            attackRange
        );
    }
}