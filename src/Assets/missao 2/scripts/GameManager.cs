using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject sensorPrefab; // Prefab do sensor que será instanciado na mão do jogador
    public Transform playerHand; // Referência à posição da mão do jogador para segurar o sensor
    public int totalSensores = 5; // Total de sensores que devem ser instalados

    public int sensoresInstalados = 0; // Contador de sensores instalados até agora
    private bool missaoIniciada = false; // Controla se a missão já foi iniciada
    private GameObject sensorAtualNaMao; // Referência ao sensor que o jogador está segurando

    void Update()
    {
        if (missaoIniciada && sensoresInstalados < totalSensores) // Se missão iniciada e sensores ainda faltam instalar
        {
            if (Input.GetKeyDown(KeyCode.Q)) // Quando o jogador aperta a tecla Q
            {
                TentarInstalarSensor(); // Tenta instalar o sensor
            }
        }
    }

    public void IniciarMissao()
    {
        if (!missaoIniciada) // Se a missão ainda não começou
        {
            missaoIniciada = true; // Marca missão como iniciada
            sensoresInstalados = 0; // Zera o contador de sensores instalados
            Debug.Log("Missão 2 aceita, instale os sensores com Q!"); // Mensagem no console
            CriarSensorNaMao(); // Cria o primeiro sensor na mão do jogador
        }
        else if (sensoresInstalados >= totalSensores) // Se missão já começou e sensores todos instalados
        {
            Debug.Log("Obrigado por instalar todos os sensores! Missão completa."); // Mensagem de agradecimento
        }
    }

    void CriarSensorNaMao()
    {
        if (sensorPrefab != null && playerHand != null) // Se o prefab e a referência da mão existem
        {
            sensorAtualNaMao = Instantiate(sensorPrefab, playerHand.position, playerHand.rotation); // Cria o sensor na posição e rotação da mão
            sensorAtualNaMao.transform.SetParent(playerHand); // Define o sensor como filho da mão para seguir seus movimentos
        }
    }

    void TentarInstalarSensor()
    {
        Collider[] colliders = Physics.OverlapSphere(playerHand.position, 2f); // Verifica objetos próximos num raio de 2 metros da mão

        foreach (Collider col in colliders) // Para cada objeto encontrado
        {
            if (col.CompareTag("Poste")) // Se o objeto for um poste
            {
                InstalarSensor(); // Instala o sensor
                return; // Sai do método após instalar
            }
        }

        Debug.Log("Você precisa estar perto de um poste para instalar o sensor!"); // Caso não esteja perto de poste, mostra aviso
    }

    void InstalarSensor()
    {
        if (sensorAtualNaMao != null) // Se o jogador estiver segurando um sensor
        {
            Destroy(sensorAtualNaMao);  // Remove o sensor da mão (sensor instalado)
            sensoresInstalados++; // Incrementa o contador de sensores instalados

            Debug.Log("Sensor instalado! " + sensoresInstalados + "/" + totalSensores); // Mostra progresso no console

            if (sensoresInstalados < totalSensores) // Se ainda faltam sensores para instalar
            {
                CriarSensorNaMao(); // Cria o próximo sensor na mão
            }
            else
            {
                Debug.Log("Você instalou todos os sensores! Volte e fale com o NPC."); // Missão concluída, instrução para o jogador
            }
        }
    }
}
