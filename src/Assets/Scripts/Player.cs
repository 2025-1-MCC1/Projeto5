using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Configurações Movimento")]
    public float moveSpeed = 7f;           // Velocidade de caminhada
    public float runSpeed = 12f;           // Velocidade de corrida
    public float jumpHeight = 2f;          // Altura do pulo
    public float gravity = -20f;           // Força da gravidade aplicada no personagem
    public float mouseSensitivity = 3f;   // Sensibilidade do mouse para rotação da câmera
    public float rotationSpeed = 10f;      // Velocidade para rotacionar o personagem suavemente

    [Header("Configurações Câmera")]
    public Transform cameraPivot;          // Pivô para rotação vertical da câmera (geralmente vazio no personagem)
    public Transform cam;                  // Transform da câmera (para direção)
    public float minY = -70f;              // Limite mínimo do ângulo vertical da câmera
    public float maxY = 45f;               // Limite máximo do ângulo vertical da câmera

    public Animator animator;              // Componente Animator para animações do personagem
    private CharacterController controller; // Componente CharacterController para movimentação física
    private Vector3 velocity;              // Vetor que controla a velocidade atual (gravidade e pulo)
    private bool isGrounded;               // Indica se o personagem está no chão
    private bool isJumping = false;        // Controla o estado de pulo para animação

    public float rotationX = 0f;           // Rotação horizontal acumulada (mouse X)
    public float rotationY = 0f;           // Rotação vertical acumulada (mouse Y)

    void Start()
    {
        controller = GetComponent<CharacterController>();   // Pega o componente CharacterController
        Cursor.lockState = CursorLockMode.Locked;           // Trava o cursor no centro da tela

        // Inicializa a rotação da câmera com a rotação atual do pivô
        Vector3 angles = cameraPivot.rotation.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;

        animator = GetComponent<Animator>();                 // Pega o Animator
    }

    void Update()
    {
        isGrounded = controller.isGrounded;  // Atualiza se o personagem está no chão

        // Corrige o "grudar no chão" mantendo a velocidade vertical pequena se estiver no chão
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Entrada do teclado WASD ou setas
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Direções relativas à câmera, mas sem movimento vertical
        Vector3 camForward = cam.forward; camForward.y = 0f; camForward.Normalize();
        Vector3 camRight = cam.right; camRight.y = 0f; camRight.Normalize();

        // Direção final do movimento
        Vector3 move = camForward * inputZ + camRight * inputX;

        bool isMoving = move.magnitude >= 0.1f;                 // Verifica se está se movendo
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving; // Verifica se está correndo

        float currentSpeed = isRunning ? runSpeed : moveSpeed; // Define velocidade atual

        if (isMoving)
        {
            // Move o personagem baseado na direção e velocidade
            controller.Move(move * currentSpeed * Time.deltaTime);

            // Rotaciona o personagem suavemente na direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Controle do pulo
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))  // Se apertar barra de espaço e estiver no chão
            {
                // Calcula a velocidade para o pulo baseado na altura desejada e gravidade
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumping = true;  // Marca que está pulando (para animação)
            }
            else if (isJumping)
            {
                // Quando aterrissar, desliga o pulo
                isJumping = false;
            }
        }

        // Aplica gravidade constantemente
        velocity.y += gravity * Time.deltaTime;

        // Aplica o movimento vertical da gravidade/pulo
        controller.Move(velocity * Time.deltaTime);

        // Rotação da câmera com o mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX += mouseX;                           // Rotaciona horizontalmente
        rotationY -= mouseY;                           // Rotaciona verticalmente invertido
        rotationY = Mathf.Clamp(rotationY, minY, maxY);  // Limita ângulo vertical

        cameraPivot.rotation = Quaternion.Euler(rotationY, rotationX, 0f); // Aplica rotação da câmera

        // Atualiza as variáveis do Animator para controlar as animações
        animator.SetBool("Mover", isMoving);
        animator.SetBool("Correndo", isRunning);
        animator.SetBool("Pular", isJumping);
    }

    // Método para ativar/desativar controle de movimento (ex: travar controle durante diálogo)
    public void LockMovement(bool value)
    {
        this.enabled = !value;
    }

    // Método para ativar animação de interação
    public void PlayInteractionAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Interagir");
        }
    }
}
