using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivityX = 500f;
    public float mouseSensitivityY = 500f;

    [Header("Rotation Limits")]
    public float topClamp = -90f;
    public float bottomClamp = 90f;

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
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Ajustando a rotação horizontal (olhar para os lados)
        yRotation += mouseX;

        // Aplicando a rotação ao objeto
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
