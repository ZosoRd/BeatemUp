using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    private int comboState = 0;
    public float maxComboDelay = 1.5f;
    private float comboTimer;

    private PlayerMovement playerMovement; // Referencia al script de movimiento

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); 

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement no encontrado en el objeto.");
        }
    }

    void Update()
    {
        comboTimer += Time.deltaTime;

        if (comboTimer >= maxComboDelay)
        {
            comboState = 0;
            comboTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
            comboTimer = 0f;
        }
    }

    void Attack()
    {
        // Informar al script de movimiento que el jugador está atacando
        if (playerMovement != null)
        {
            playerMovement.SetAttacking(true);
        }

        switch (comboState)
        {
            case 0:
                animator.SetTrigger("Boxing");
                comboState = 1;
                break;
            case 1:
                animator.SetTrigger("Hook Punch");
                comboState = 2;
                break;
            case 2:
                animator.SetTrigger("Kick");
                comboState = 0;
                break;
            default:
                break;
        }

        // Cambiado de OverlapCircleAll a OverlapSphere
        // Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // foreach (Collider enemy in hitEnemies)
        // {
            // Asegúrate de que el componente Enemy esté presente en el enemigo
            // Enemy enemyScript = enemy.GetComponent<Enemy>();
            // if (enemyScript != null)
            // {
                // enemyScript.TakeDamage(attackDamage);
            // }
        // }


        // Informar al script de movimiento que el ataque ha terminado después de un pequeño retraso
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
        // Esperar el tiempo de ataque antes de permitir el movimiento nuevamente
        yield return new WaitForSeconds(0.1f);
        if (playerMovement != null)
        {
            playerMovement.SetAttacking(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Cambiado de DrawWireSphere a DrawWireSphere para 3D
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
