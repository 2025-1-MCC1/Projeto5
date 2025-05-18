using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public string npcName; // Nome do NPC para mostrar na UI

    [Header("Diálogos")]
    [TextArea] public string[] beforeMissionLines; // Linhas de diálogo antes da missão
    [TextArea] public string[] afterMissionLines;  // Linhas de diálogo depois da missão

    public GameObject dialoguePanel; // Painel UI do diálogo
    public TextMeshProUGUI nameText; // Texto para mostrar o nome do NPC
    public TextMeshProUGUI dialogueText; // Texto para mostrar a fala do NPC

    private string[] currentLines; // Diálogos atuais que vão ser mostrados
    private int dialogueIndex = 0; // Índice da linha atual do diálogo
    private bool isTalking = false; // Flag que indica se o diálogo está ativo
    private bool playerInRange = false; // Indica se o jogador está perto do NPC

    [Header("Estado da missão")]
    public bool missionCompleted = false; // Indica se a missão foi concluída

    void Update()
    {
        // Se o jogador estiver perto e apertar 'E', inicia ou avança o diálogo
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
                StartDialogue(); // Começa o diálogo
            else
                NextLine(); // Avança para a próxima linha
        }

        // Tecla de teste para marcar a missão como concluída manualmente
        if (Input.GetKeyDown(KeyCode.M))
        {
            missionCompleted = true;
            Debug.Log("Missão marcada como concluída manualmente.");
        }
    }

    void StartDialogue()
    {
        // Define qual conjunto de falas usar conforme status da missão
        currentLines = missionCompleted ? afterMissionLines : beforeMissionLines;

        if (currentLines.Length == 0) return; // Sai se não houver falas

        dialoguePanel.SetActive(true); // Exibe o painel de diálogo
        nameText.text = npcName; // Mostra o nome do NPC
        dialogueText.text = currentLines[0]; // Mostra a primeira fala
        isTalking = true; // Marca que está falando
        dialogueIndex = 0; // Reseta o índice do diálogo
    }

    void NextLine()
    {
        dialogueIndex++; // Vai para a próxima fala

        if (dialogueIndex < currentLines.Length)
        {
            dialogueText.text = currentLines[dialogueIndex]; // Mostra a próxima fala
        }
        else
        {
            dialoguePanel.SetActive(false); // Fecha o painel ao fim do diálogo
            isTalking = false; // Marca que terminou de falar
            dialogueIndex = 0; // Reseta o índice para próximo diálogo
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Detecta quando o jogador entra na área do NPC
        if (other.CompareTag("Player"))
        {
            playerInRange = true; // Marca jogador perto
            UIManager.Instance?.ShowInteractionText("Fale com o NPC (E)"); // Mostra texto de interação
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Detecta quando o jogador sai da área do NPC
        if (other.CompareTag("Player"))
        {
            playerInRange = false; // Marca jogador fora da área
            UIManager.Instance?.HideInteractionText(); // Esconde texto de interação
        }
    }

    public void MarkMissionAsComplete()
    {
        missionCompleted = true; // Método para marcar missão concluída externamente
    }
}
