using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 100;
    public float attackCooldown = 1f;
    private float lastAttackTime;
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
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
                comboTimer = 0f;
            }
        }
    }

    void Attack()
    {
        // Informar al script de movimiento que el jugador est� atacando
        if (playerMovement != null)
        {
            playerMovement.SetAttacking(true);
        }

        switch (comboState)
        {
            case 0:
                animator.SetTrigger("Combo1");
                lastAttackTime = Time.time;
                comboState = 1;
                break;
            case 1:
                animator.SetTrigger("Combo2");
                lastAttackTime = Time.time;
                comboState = 2;
                break;
            case 2:
                animator.SetTrigger("Combo3");
                lastAttackTime = Time.time;
                comboState = 0;
                break;
            default:
                break;
        }

         Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

         foreach (Collider enemy in hitEnemies)
         {
             Enemy enemyScript = enemy.GetComponent<Enemy>();
             if (enemyScript != null)
             {
                 enemyScript.TakeDamage(attackDamage);
             }
         }
        StartCoroutine(ResetAttackState());
    }

    private IEnumerator ResetAttackState()
    {
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

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
