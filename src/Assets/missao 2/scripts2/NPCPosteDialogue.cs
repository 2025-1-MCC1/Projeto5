// NPCPosteDialogue.cs
using UnityEngine;
using TMPro;

public class NPCPosteDialogue : MonoBehaviour
{
    public string npcName;
    [TextArea] public string[] beforeMissionLines;
    [TextArea] public string[] afterMissionLines;

    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private string[] currentLines;
    private int dialogueIndex = 0;
    private bool isTalking = false;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isTalking)
                StartDialogue();
            else
                NextLine();
        }

        if (!MissaoPostesController.Instance.missaoConcluida && MissaoPostesController.Instance.MissaoCompleta())
        {
            MissaoPostesController.Instance.missaoConcluida = true;
        }
    }

    void StartDialogue()
    {
        if (!MissaoPostesController.Instance.missaoIniciada)
        {
            MissaoPostesController.Instance.IniciarMissao();
        }

        currentLines = MissaoPostesController.Instance.missaoConcluida ? afterMissionLines : beforeMissionLines;

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
    }
}