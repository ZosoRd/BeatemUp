using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Referencia a la c�mara
    [SerializeField] private float moveDistance = 10f; // Distancia que la c�mara se mover�
    [SerializeField] private float moveSpeed = 5f; // Velocidad del movimiento de la c�mara
    [SerializeField] private UnityEvent onPlayerEnter; // Evento cuando el jugador entra en el �rea

    private bool shouldMove = false;
    private Vector3 targetPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnter.Invoke(); // Invoca cualquier acci�n asociada al evento de entrada
            StartCameraMovement(); // Inicia el movimiento de la c�mara
        }
    }

    private void Update()
    {
        if (shouldMove)
        {
            // Mueve la c�mara hacia la posici�n objetivo de manera suave
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f)
            {
                shouldMove = false; // Detiene el movimiento cuando llega a la posici�n objetivo
            }
        }
    }

    private void StartCameraMovement()
    {
        targetPosition = cameraTransform.position + new Vector3(moveDistance, 0, 0);
        shouldMove = true;
    }
}