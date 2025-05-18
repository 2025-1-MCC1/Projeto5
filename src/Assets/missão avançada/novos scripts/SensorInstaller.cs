using UnityEngine;

public class SensorInstaller : MonoBehaviour
{
    public GameObject sensorVisual; // Objeto visual do sensor (luz verde, antena, etc.)
    private bool isInstalled = false; // Indica se o sensor já foi instalado
    private bool playerInRange = false; // Indica se o jogador está perto do sensor

    private void Start()
    {
        sensorVisual.SetActive(false); // Esconde o visual do sensor no começo
    }

    private void Update()
    {
        // Se o jogador estiver perto, o sensor não instalado e apertar 'E'
        if (playerInRange && !isInstalled && Input.GetKeyDown(KeyCode.E))
        {
            InstallSensor(); // Executa a instalação do sensor
        }
    }

    private void InstallSensor()
    {
        isInstalled = true; // Marca sensor como instalado
        sensorVisual.SetActive(true); // Mostra o visual do sensor instalado
        MissionManager.Instance.RegisterSensorInstalled(); // Registra no gerenciador de missão
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta quando o jogador entra na área de interação
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Marca que o jogador está próximo
            UIManager.Instance.ShowInteractionText("Pressione E para instalar o sensor"); // Mostra texto de interação
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detecta quando o jogador sai da área de interação
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Marca que o jogador saiu da área
            UIManager.Instance.HideInteractionText(); // Esconde texto de interação
        }
    }
}
