using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Obtén el componente Animator
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (animator != null)
        {
            animator.SetTrigger("TakeDamage"); // Activa el trigger para la animación de daño
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Activa el trigger para la animación de muerte
        }

        // Inicia la corrutina para destruir el objeto después de la animación
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        if (animator != null)
        {
            // Obtén la duración de la animación de muerte
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float animationDuration = stateInfo.length;

            // Espera la duración de la animación de muerte
            yield return new WaitForSeconds(animationDuration);
        }

        Destroy(gameObject); // Destruye el objeto después de la animación
    }
}
