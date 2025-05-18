using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;  // Singleton para acesso global

    public NPCDialogue npc;                 // Referência ao NPC para atualizar diálogo
    public int totalSensors = 3;            // Total de sensores para instalar
    private int installedSensors = 0;       // Sensores instalados até agora

    public GameObject garbageTruck;         // Caminhão de lixo que será ativado após missão

    private void Awake()
    {
        // Configura singleton para garantir uma única instância do MissionManager
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);  // Caso já exista uma instância, destrói este objeto
    }

    // Chamar este método quando um sensor for instalado
    public void RegisterSensorInstalled()
    {
        installedSensors++;

        // Atualiza o texto da missão na UI
        UIManager.Instance.UpdateMissionText(installedSensors, totalSensors);

        // Verifica se todos os sensores foram instalados para completar a missão
        if (installedSensors >= totalSensors)
        {
            CompleteMission();
        }
    }

    // Método chamado para completar a missão
    private void CompleteMission()
    {
        UIManager.Instance.ShowMessage("Todos os sensores foram instalados!");

        // Ativa o caminhão de lixo e inicia seu movimento (se tiver o componente)
        if (garbageTruck != null)
        {
            garbageTruck.SetActive(true);
            GarbageTruckMover mover = garbageTruck.GetComponent<GarbageTruckMover>();
            if (mover != null)
            {
                mover.StartMoving();
            }
        }

        FinalizeMission();
    }

    // Marca a missão como concluída e atualiza o NPC
    private void FinalizeMission()
    {
        UIManager.Instance.ShowMessage("Missão concluída!");

        if (npc != null)
        {
            npc.MarkMissionAsComplete();
        }
    }
}
