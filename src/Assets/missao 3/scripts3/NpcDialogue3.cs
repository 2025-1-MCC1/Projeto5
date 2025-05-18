using UnityEngine;
using TMPro;

public class NPCDialogue3 : MonoBehaviour
{
    public enum MissionState
    {
        NotStarted,
        InProgress,
        Completed
    }

    public string npcName;

    [Header("Diálogos")]
    [TextArea] public string[] beforeMissionLines; // Antes da missão começar
    [TextArea] public string[] progressLines;       // Durante a missão
    [TextArea] public string[] afterMissionLines;   // Após completar a missão

    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private string[] currentLines;
    private int dialogueIndex = 0;
    private bool isTalking = false;
    private bool playerInRange = false;

    [Header("Estado da missão")]
    public MissionState missionState = MissionState.NotStarted;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
                StartDialogue();
            else
                NextLine();
        }

        // Atualiza estado da missão automaticamente se for concluída
        if (missionState == MissionState.InProgress &&
            WiFiMissionController.Instance != null &&
            WiFiMissionController.Instance.IsMissionComplete())
        {
            missionState = MissionState.Completed;
            Debug.Log("NPC atualizado para pós-missão WiFi.");
        }
    }

    void StartDialogue()
    {
        switch (missionState)
        {
            case MissionState.NotStarted:
                currentLines = beforeMissionLines;

                // Inicia a missão
                if (WiFiMissionController.Instance != null)
                {
                    WiFiMissionController.Instance.StartMission();
                    missionState = MissionState.InProgress;
                    Debug.Log("Missão de antenas iniciada.");
                }
                break;

            case MissionState.InProgress:
                currentLines = progressLines.Length > 0 ? progressLines : beforeMissionLines;
                break;

            case MissionState.Completed:
                currentLines = afterMissionLines;
                break;
        }

        if (currentLines.Length == 0) return;

        dialoguePanel.SetActive(true);
        nameText.text = npcName;
        dialogueText.text = currentLines[0];
        isTalking = true;
        dialogueIndex = 0;
    }

    void NextLine()
    {
        dialogueIndex++;
        if (dialogueIndex < currentLines.Length)
        {
            dialogueText.text = currentLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
            isTalking = false;
            dialogueIndex = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            UIManager.Instance?.ShowInteractionText("Fale com o NPC (E)");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            UIManager.Instance?.HideInteractionText();
        }

        MissaoEnergiaManager.Instance.LiberarMissao();
    }

}