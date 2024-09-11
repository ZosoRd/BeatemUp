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
        animator = GetComponent<Animator>(); // Obt�n el componente Animator
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (animator != null)
        {
            animator.SetTrigger("TakeDamage"); // Activa el trigger para la animaci�n de da�o
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
            animator.SetTrigger("Die"); // Activa el trigger para la animaci�n de muerte
        }

        // Inicia la corrutina para destruir el objeto despu�s de la animaci�n
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        if (animator != null)
        {
            // Obt�n la duraci�n de la animaci�n de muerte
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float animationDuration = stateInfo.length;

            // Espera la duraci�n de la animaci�n de muerte
            yield return new WaitForSeconds(animationDuration);
        }

        Destroy(gameObject); // Destruye el objeto despu�s de la animaci�n
    }
}
