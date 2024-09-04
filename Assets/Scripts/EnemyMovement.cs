using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Rigidbody myBody;
    public float speed = 5f;

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
        FollowTarget();
    }

    public void FollowTarget()
    {
        if (!followPlayer)
        {
            return;
        }

        // Asegúrate de que el enemigo siempre se alinee horizontalmente con el jugador
        Vector3 targetPosition = playerTarget.position;
        targetPosition.y = transform.position.y; // Mantener la altura actual

        // Mirar al jugador
        transform.LookAt(targetPosition);

        // Calcular la dirección en el plano horizontal
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // Asegúrate de que la dirección solo tenga componentes X e Y

        // Mover en la dirección horizontal
        myBody.velocity = direction * speed;
    }

}
