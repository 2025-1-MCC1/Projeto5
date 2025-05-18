using UnityEngine;

public class WiFiMissionController : MonoBehaviour
{
    public static WiFiMissionController Instance; // Instância única do controlador (Singleton)

    public enum MissionState
    {
        NotStarted, // Missão ainda não começou
        InProgress, // Missão está em andamento
        Completed   // Missão foi concluída
    }

    public MissionState missionState = MissionState.NotStarted; // Estado atual da missão

    public int totalAntennas = 3; // Quantidade total de antenas necessárias
    private int antennasActivated = 0; // Quantidade de antenas já ativadas

    private void Awake()
    {
        // Garante que só exista uma instância deste objeto na cena
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroi duplicatas
        }
        else
        {
            Instance = this; // Define esta como a instância única
        }
    }

    public void StartMission()
    {
        if (missionState == MissionState.NotStarted) // Se a missão ainda não começou
        {
            missionState = MissionState.InProgress; // Altera o estado para "em andamento"
            Debug.Log("Missão de Wi-Fi iniciada."); // Mostra mensagem no console
        }
    }

    public void AntennaActivated()
    {
        if (missionState != MissionState.InProgress) // Se a missão não está em andamento, não faz nada
            return;

        antennasActivated++; // Incrementa o número de antenas ativadas
        Debug.Log($"Antena ativada ({antennasActivated}/{totalAntennas})"); // Mostra progresso no console

        if (antennasActivated >= totalAntennas) // Se todas as antenas foram ativadas
        {
            missionState = MissionState.Completed; // Marca a missão como concluída
            Debug.Log("Missão de Wi-Fi concluída!"); // Mensagem no console
        }
    }

    public bool IsMissionComplete()
    {
        return missionState == MissionState.Completed; // Retorna se a missão foi concluída
    }

    public bool IsMissionStarted()
    {
        return missionState == MissionState.InProgress; // Retorna se a missão está em andamento
    }
}
