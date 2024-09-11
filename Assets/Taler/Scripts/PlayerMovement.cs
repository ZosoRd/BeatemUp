using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    public float jumpForce = 5f; // Fuerza del salto
    public float groundCheckDistance = 0.2f; // Distancia para comprobar si el jugador está en el suelo
    public LayerMask groundLayer; // Capa que representa el suelo
    public Animator animator; // Referencia al componente Animator

    private Vector3 moveDirection; // Dirección en la que el jugador se mueve
    private float blendValue; // Valor del parámetro de mezcla en el Animator
    private bool isGrounded; // Indica si el jugador está en el suelo
    private Rigidbody rb; // Componente Rigidbody del jugador

    [SerializeField] string parameterName = "blend";
    private bool isAttacking; // Variable para saber si el jugador está atacando
    float horizontalInput; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado en el objeto.");
        }
    }

    void Update()
    {
        // Comprobar si el jugador está en el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Comprobar si el jugador está atacando
        if (!isAttacking)
        {
            // Obtener la entrada del usuario para los ejes horizontal y vertical
            horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calcular la dirección del movimiento en función de las entradas
            moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            // Actualizar el parámetro de mezcla en el Animator
            UpdateBlendValue();

            

            // Manejar el salto
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }
    }

    private void FixedUpdate()
    {
        // Mover y rotar al jugador
        MovePlayer(horizontalInput);
    }

    void UpdateBlendValue()
    {
        // La mezcla depende de la magnitud del vector de dirección de movimiento
        blendValue = moveDirection.magnitude;

        // Pasar el valor de mezcla al Animator
        if (animator != null)
        {
            animator.SetFloat(parameterName, blendValue);
        }
    }

    void MovePlayer(float horizontalInput)
    {
        // Mover al jugador en la dirección deseada
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Rotar el jugador en función de la entrada horizontal
        if (horizontalInput < 0) // Movimiento a la izquierda
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else if (horizontalInput > 0) // Movimiento a la derecha
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }

    void Jump()
    {
        // Aplicar una fuerza hacia arriba para saltar
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Métodos públicos para controlar el estado de ataque
    public void SetAttacking(bool attacking)
    {
        isAttacking = attacking;
    }
}