using UnityEngine;
using UnityEngine.InputSystem;

public class ControleJogador : MonoBehaviour
{
    public float velocidade = 5.0f;
    public float sensibilidadeMouse = 0.05f; // Ajustado para ser mais suave
    
    private Transform cameraTransform;
    private float rotationX = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Encontra a câmera que está dentro do jogador automaticamente
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        // Movimentação Teclado
        var teclado = Keyboard.current;
        if (teclado != null)
        {
            Vector3 movimento = Vector3.zero;
            if (teclado.wKey.isPressed || teclado.upArrowKey.isPressed) movimento += transform.forward;
            if (teclado.sKey.isPressed || teclado.downArrowKey.isPressed) movimento -= transform.forward;
            if (teclado.aKey.isPressed || teclado.leftArrowKey.isPressed) movimento -= transform.right;
            if (teclado.dKey.isPressed || teclado.rightArrowKey.isPressed) movimento += transform.right;

            transform.position += movimento.normalized * velocidade * Time.deltaTime;
        }

        // Rotação Mouse
        var mouse = Mouse.current;
        if (mouse != null)
        {
            Vector2 deltaMouse = mouse.delta.ReadValue();
            float mouseX = deltaMouse.x * sensibilidadeMouse;
            float mouseY = deltaMouse.y * sensibilidadeMouse;

            // Rotaciona o corpo do jogador para os lados (Esquerda/Direita)
            transform.Rotate(0, mouseX, 0);

            // Rotaciona apenas a câmera para cima e para baixo
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            
            if (cameraTransform != null)
            {
                cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            }
        }
    }
}