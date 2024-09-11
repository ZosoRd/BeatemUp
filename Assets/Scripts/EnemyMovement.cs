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

    private Animator animator;

    public void Awake()
    {
        myBody = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

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
        UpdateAnimation();
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

        RotateEnemy(direction);
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

    void UpdateAnimation()
    {
        // Verifica si el enemigo se está moviendo
        bool isMoving = myBody.velocity.magnitude > 0.1f;

        // Actualiza el parámetro en el Animator
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
        }
    }

    void RotateEnemy(Vector3 direction)
    {
        // Ajusta la rotación del enemigo dependiendo de la dirección
        if (direction.x < 0) // Movimiento a la izquierda
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f); // 90 grados a la derecha
        }
        else if (direction.x > 0) // Movimiento a la derecha
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f); // 90 grados a la izquierda
        }
        else
        {
            // Si el enemigo no se está moviendo horizontalmente, asegúrate de que esté orientado hacia adelante
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (detectionPoint == null)
            return;

        Gizmos.DrawWireSphere(detectionPoint.position, detectionRange);
    }
}
