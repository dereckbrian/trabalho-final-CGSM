using UnityEngine;
using Cinemachine;

public class cameraMoviment : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;

    [Header("Configurações de Zoom")]
    public float zoomSpeed = 10f; // Velocidade do zoom
    public float minZoom = 2f;    // O quão perto a câmera pode chegar
    public float maxZoom = 15f;   // O quão longe a câmera pode ir

    void Update()
    {
        // --- PARTE 1: ROTAÇÃO (Botão Direito) ---
        if (Input.GetMouseButton(1))
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X");
            freeLookCamera.m_YAxis.m_InputAxisValue = Input.GetAxis("Mouse Y");
        }
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = 0;
            freeLookCamera.m_YAxis.m_InputAxisValue = 0;
        }

        // --- PARTE 2: ZOOM (Rodinha do Mouse) ---
        // Pega o movimento da rodinha (scroll)
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Se moveu a rodinha
        if (scroll != 0)
        {
            // O Cinemachine FreeLook tem 3 anéis (Top, Middle, Bottom). 
            // Vamos ajustar o raio dos 3 ao mesmo tempo.
            for (int i = 0; i < 3; i++)
            {
                // Calcula o novo raio: Raio Atual - (Movimento * Velocidade)
                // Usamos "menos" porque rodar pra frente (positivo) tem que diminuir o raio (aproximar)
                float novoRaio = freeLookCamera.m_Orbits[i].m_Radius - (scroll * zoomSpeed);

                // Mathf.Clamp impede que o valor passe do mínimo ou do máximo
                freeLookCamera.m_Orbits[i].m_Radius = Mathf.Clamp(novoRaio, minZoom, maxZoom);
            }
        }
    }
}