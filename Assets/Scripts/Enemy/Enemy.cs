using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    public EnemyHp enemyHp;

    [Header("Attack")]
    public int Creature_Damage = 10;
    public float attackRange = 1.5f;

    [Header("Castle Attack")]
    public float castleAttackRange = 2f;

    [Header("Movement")]
    public float Speed = 2f;
    public float PlayerMoveSpeed = 3f;

    [Header("Spline")]
    public SplineContainer spline;
    public float splineOffset = 1.5f;

    private float offset;

    private float splineDistance = 0f;

    [Header("Enemy")]
    public GameObject Enemybug;
    public EnemyHp Enemy_Hp;

    [Header("Animation")]
    public Animator anim;

    [Header("Player")]
    public Transform target;
    public GameObject EnemyTarget;

    private List<Transform> playersInRange = new List<Transform>();

    [Header("Follow Points")]
    public Transform splineFollowPoint;
    public Transform playerFollowPoint;

    private float previous_Speed;
    public bool IsDied;
    public bool playerDetected = false;


    void Start()
    {
        anim = GetComponent<Animator>();

        if (Enemybug != null)
            Enemy_Hp = Enemybug.GetComponent<EnemyHp>();

        previous_Speed = Speed;

        offset = UnityEngine.Random.Range(-splineOffset, splineOffset);

        if (spline != null)
        {
            float t;

            SplineUtility.GetNearestPoint(
                spline.Spline,
                transform.position,
                out float3 nearest,
                out t
            );

            splineDistance = t;
        }
    }


    void AttackCastle()
    {
        if (EnemyTarget == null)
            return;

        castle targetCastle = EnemyTarget.GetComponent<castle>();

        if (targetCastle != null)
        {
            targetCastle.TakeDamage(Creature_Damage);
        }
    }


    void AttackPlayer()
    {
        if (target == null)
            return;

        PlayerHP playerHp = target.GetComponent<PlayerHP>();

        if (playerHp != null)
        {
            playerHp.TakeDamage(Creature_Damage);
        }
    }


    public void GetDamage()
    {
        // Attacking a player
        if (playerDetected && target != null)
        {
            Vector3 targetPosition = target.position;
            targetPosition.y = transform.position.y;

            if (Vector3.Distance(transform.position, targetPosition) <= attackRange)
            {
                AttackPlayer();
            }

            return;
        }

        // Attacking the castle
        if (EnemyTarget != null && EnemyTarget.CompareTag("Castle"))
        {
            Vector3 targetPosition = EnemyTarget.transform.position;
            targetPosition.y = transform.position.y;

            if (Vector3.Distance(transform.position, targetPosition) <= castleAttackRange)
            {
                AttackCastle();
            }
        }
    }


    public void AddPlayer(Transform player)
    {
        if (player == null)
            return;

        if (!playersInRange.Contains(player))
            playersInRange.Add(player);
    }

    public void RemovePlayer(Transform player)
    {
        if (player == null)
            return;

        playersInRange.Remove(player);
    }

    Transform GetNearestPlayer()
    {
        Transform nearest = null;
        float nearestDist = float.MaxValue;

        foreach (Transform p in playersInRange)
        {
            if (p == null)
                continue;

            float d = Vector3.Distance(transform.position, p.position);

            if (d < nearestDist)
            {
                nearestDist = d;
                nearest = p;
            }
        }

        return nearest;
    }


    void Update()
    {
        if (IsDied)
            return;

        if(PlayingManager.inst.IsgameOver)
        {
            PlayerLoose();
            return;
        }

        playersInRange.RemoveAll(p => p == null);

        if (playersInRange.Count > 0)
        {
            Transform nearestPlayer = GetNearestPlayer();

            if (nearestPlayer != null)
            {
                playerDetected = true;
                target = nearestPlayer;
                EnemyTarget = nearestPlayer.gameObject;

                if (playerFollowPoint != null)
                {
                    playerFollowPoint.position = target.position;
                }

                MoveTowardsPlayerFollowPoint();
                return;
            }
        }

        if (playerDetected)
        {
            PlayerDestroyed();
        }


        if (EnemyTarget != null && EnemyTarget.CompareTag("Castle"))
        {
            MoveTowardsCastle();
            return;
        }


        FollowSplinePoint();


        // Castle destroyed
        if (EnemyTarget != null &&
            EnemyTarget.CompareTag("Castle_Destroyed"))
        {
            EnemyTarget = null;
            target = null;

            Speed = previous_Speed;

            anim.SetBool("Attack", false);
            anim.SetBool("RUN", true);
        }
    }


    void FollowSplinePoint()
    {
        if (spline == null)
            return;

        splineDistance += Speed * Time.deltaTime;

        float splineLength = spline.CalculateLength();

        float normalizedT = splineDistance / splineLength;

        if (normalizedT >= 1f)
        {
            anim.SetBool("RUN", false);
            anim.SetBool("Victory", true);
            return;
        }
        Vector3 splinePosition =
            spline.EvaluatePosition(normalizedT);

        Vector3 tangent =
            spline.EvaluateTangent(normalizedT);

        tangent.Normalize();

        Vector3 side =
            Vector3.Cross(Vector3.up, tangent).normalized;

        Vector3 followPosition =
            splinePosition + side * offset;

        followPosition.y = transform.position.y;

        if (splineFollowPoint != null)
        {
            splineFollowPoint.position = followPosition;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            splineFollowPoint != null
                ? splineFollowPoint.position
                : followPosition,
            Speed * Time.deltaTime
        );

        if (tangent != Vector3.zero)
        {
            Quaternion rotation =
                Quaternion.LookRotation(tangent);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotation,
                10f * Time.deltaTime
            );
        }

        anim.SetBool("RUN", true);
    }


    void MoveTowardsPlayerFollowPoint()
    {
        if (playerFollowPoint == null)
            return;

        Vector3 targetPosition = playerFollowPoint.position;

        targetPosition.y = transform.position.y;

        float distanceToPlayer =
            Vector3.Distance(transform.position, targetPosition);

        Vector3 direction =
            targetPosition - transform.position;

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

        if (distanceToPlayer <= attackRange)
        {
            anim.SetBool("RUN", false);
            anim.SetBool("Attack", true);
            return;
        }


        anim.SetBool("Attack", false);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            PlayerMoveSpeed * Time.deltaTime
        );

        anim.SetBool("RUN", true);
    }

    void MoveTowardsCastle()
    {
        if (EnemyTarget == null)
            return;

        Vector3 targetPosition = EnemyTarget.transform.position;

        targetPosition.y = transform.position.y;

        float distanceToCastle =
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

        if (distanceToCastle <= castleAttackRange)
        {
            Speed = 0;

            anim.SetBool("RUN", false);
            anim.SetBool("Attack", true);
            return;
        }

        anim.SetBool("Attack", false);

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            PlayerMoveSpeed * Time.deltaTime
        );

        anim.SetBool("RUN", true);
    }

    public void PlayerDestroyed()
    {
        playerDetected = false;

        EnemyTarget = null;
        target = null;

        Speed = previous_Speed;

        anim.SetBool("Attack", false);
    }

    public void Die()
    {
        Speed = 0;
        IsDied = true;
        anim.SetBool("RUN", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Death", true);

        PlayingManager.inst.Subtractenemy();

        Destroy(gameObject, 2f);
    }

    public void PlayerLoose()
    {
        Speed = 0;

        anim.SetBool("RUN", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Death", false);
        anim.SetBool("Idle", true);
    }
}