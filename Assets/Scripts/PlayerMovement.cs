using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

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

        // Movendo o jogador
        controller.Move(move * speed * Time.deltaTime);

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
