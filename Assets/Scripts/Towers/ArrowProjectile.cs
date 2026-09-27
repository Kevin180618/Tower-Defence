using UnityEngine;
using DG.Tweening;

public class ArrowProjectile : MonoBehaviour
{
    [Header("Arrow Settings")]
    [SerializeField] private bool IsBomb;
    [SerializeField] private float flightTime = 0.5f;
    [SerializeField] private float arcHeight = 3f;
    [SerializeField] private float damage = 10;

    [Header("Aim")]
    [SerializeField] private Vector3 aimOffset = new Vector3(0f, 1f, 0f);

    private Transform target;
    private Vector3 startPosition;
    private Vector3 lastTargetPosition;
    private Tween flightTween;
    private bool hasHit;

    private void Start()
    {
        if (IsBomb)
        {
            damage = TowerManager.inst.BombTowerUpgrades[TowerManager.inst.BombTowerUpgradeNum].Damage;
        }
        else
        {
            damage = TowerManager.inst.ArcherTowerUpgrades[TowerManager.inst.AcherTowerUpgradeNum].Damage;
        }
    }

    public void Launch(Transform enemy)
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        target = enemy;
        startPosition = transform.position;
        lastTargetPosition = enemy.position + aimOffset;

        flightTween = DOVirtual
            .Float(0f, 1f, flightTime, UpdateFlight)
            .SetEase(Ease.Linear)
            .SetLink(gameObject)          
            .OnComplete(OnArrowReachedTarget);
    }

    private void UpdateFlight(float t)
    {
        if (target != null)
            lastTargetPosition = target.position + aimOffset;

        Vector3 newPosition = Vector3.Lerp(startPosition, lastTargetPosition, t);

        newPosition.y += arcHeight * 4f * t * (1f - t);

        Vector3 direction = newPosition - transform.position;
        if (direction.sqrMagnitude > 0.00001f)
            transform.rotation = Quaternion.LookRotation(direction.normalized);

        transform.position = newPosition;
    }

    private void OnArrowReachedTarget()
    {
        if (hasHit) return;
        hasHit = true;

        if (target != null && target.TryGetComponent(out EnemyHp enemyHp))
            enemyHp.TakeDamage(damage);
        if (IsBomb)
        {
            Instantiate(PlayingManager.inst.BlastParticle.gameObject, this.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        flightTween?.Kill();
    }
}