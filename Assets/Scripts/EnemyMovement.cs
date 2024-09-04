using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Rigidbody myBody;
    public float speed = 5f;

    public Transform detectionPoint;
    public float detectionRange = 10f;

    public Transform playerTarget;
    public bool followPlayer;

    public void Awake()
    {
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag("Player").transform;

    }

    void Start()
    {
        followPlayer = true;
    }

    void Update()
    {
        DetectPlayer();
        FollowTarget();
    }

    public void FollowTarget()
    {
        if (!followPlayer)
        {
            return;
        }


        Vector3 targetPosition = playerTarget.position;
        targetPosition.y = transform.position.y;

        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; 

        myBody.velocity = direction * speed;
    }

    public void DetectPlayer()
    {
        if (playerTarget ==  null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= detectionRange)
        {
            followPlayer = true;
        }
        else
        {
            followPlayer = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (detectionPoint == null)
            return;

        Gizmos.DrawWireSphere(detectionPoint.position, detectionRange);
    }
}
