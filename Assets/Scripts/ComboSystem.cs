using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public Animator animator; // Asigna tu Animator en el inspector
    public float comboResetTime = 1.0f; // Tiempo para reiniciar el combo en segundos

    private int comboIndex = 0;
    private float lastPressTime;
    private bool isComboActive = false;

    private void Update()
    {
        // Detecta la entrada de la tecla "K"
        if (Input.GetKeyDown(KeyCode.K))
        {
            HandleCombo();
        }

        // Verifica si el tiempo desde la última presión ha pasado y reinicia el combo si es necesario
        if (isComboActive && Time.time - lastPressTime > comboResetTime)
        {
            ResetCombo();
        }
    }

    private void HandleCombo()
    {
        lastPressTime = Time.time;
        isComboActive = true;

        // Resetea el trigger de animación para evitar que se mantenga en el estado anterior
        ResetAnimatorTriggers();

        // Activa la animación correspondiente al índice del combo
        switch (comboIndex)
        {
            case 0:
                animator.SetTrigger("Combo1");
                break;
            case 1:
                animator.SetTrigger("Combo2");
                break;
            case 2:
                animator.SetTrigger("Combo3");
                break;
        }

        // Avanza al siguiente índice del combo
        comboIndex = (comboIndex + 1) % 3; // Solo hay 3 animaciones, por lo que usamos módulo 3
    }

    private void ResetAnimatorTriggers()
    {
        // Desactiva todos los triggers en el Animator para evitar estados de animación no deseados
        animator.ResetTrigger("Combo1");
        animator.ResetTrigger("Combo2");
        animator.ResetTrigger("Combo3");
    }

    private void ResetCombo()
    {
        comboIndex = 0;
        isComboActive = false;
    }
}
