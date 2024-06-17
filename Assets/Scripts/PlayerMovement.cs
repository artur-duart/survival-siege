using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 12f;
    public float gravity = -19.62f; // -9.81 * 2
    public float jumpHeight = 3f;
    public float smoothTime = 0.1f; // Tempo para suavização do movimento

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Componentes e variáveis auxiliares
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isMoving;
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    private Vector3 currentMovement;
    private Vector3 currentMovementVelocity;

    void Start()
    {
        // Inicializando o CharacterController
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verifica se o jogador está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Resetando a velocidade vertical quando no chão
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Captura de inputs de movimento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Criação do vetor de movimento baseado nos inputs
        Vector3 move = transform.right * x + transform.forward * z;

        // Suavizando o movimento
        currentMovement = Vector3.SmoothDamp(currentMovement, move, ref currentMovementVelocity, smoothTime);

        // Movendo o jogador
        controller.Move(currentMovement * speed * Time.deltaTime);

        // Verificação de pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Aplicação da fórmula de pulo
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplicando a gravidade
        velocity.y += gravity * Time.deltaTime;

        // Aplicando o movimento vertical (pulo e queda)
        controller.Move(velocity * Time.deltaTime);

        // Determinando se o jogador está se movendo
        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
            // Para usar depois
        }
        else
        {
            isMoving = false;
            // Para usar depois
        }

        lastPosition = gameObject.transform.position;
    }
}
