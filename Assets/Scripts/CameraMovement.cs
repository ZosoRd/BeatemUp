using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Referencia a la cámara
    [SerializeField] private float moveDistance = 10f; // Distancia que la cámara se moverá
    [SerializeField] private float moveSpeed = 5f; // Velocidad del movimiento de la cámara
    [SerializeField] private UnityEvent onPlayerEnter; // Evento cuando el jugador entra en el área

    private bool shouldMove = false;
    private Vector3 targetPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnter.Invoke(); // Invoca cualquier acción asociada al evento de entrada
            StartCameraMovement(); // Inicia el movimiento de la cámara
        }
    }

    private void Update()
    {
        if (shouldMove)
        {
            // Mueve la cámara hacia la posición objetivo de manera suave
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f)
            {
                shouldMove = false; // Detiene el movimiento cuando llega a la posición objetivo
            }
        }
    }

    private void StartCameraMovement()
    {
        targetPosition = cameraTransform.position + new Vector3(moveDistance, 0, 0);
        shouldMove = true;
    }
}