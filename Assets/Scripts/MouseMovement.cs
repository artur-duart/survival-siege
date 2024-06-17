using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header("Mouse Settings")]
    // Sensibilidade do mouse para os eixos X e Y
    public float mouseSensitivityX = 500f;
    public float mouseSensitivityY = 500f;

    [Header("Rotation Limits")]
    // Limites para a rotação vertical
    public float topClamp = -90f;
    public float bottomClamp = 90f;

    // Variáveis para armazenar a rotação acumulada
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        // Travando o cursor no meio da tela e deixando-o invisível
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Pegando as coordenadas do mouse e aplicando a sensibilidade
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // Ajustando a rotação vertical (olhar para cima e para baixo)
        xRotation -= mouseY;
        // Limitando a rotação vertical entre os limites definidos
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Ajustando a rotação horizontal (olhar para os lados)
        yRotation += mouseX;

        // Calculando a rotação alvo
        Quaternion targetRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Aplicando a rotação suavemente ao objeto
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * 10f);
    }
}
