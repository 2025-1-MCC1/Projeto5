using UnityEngine;
using TMPro; // Importante para usar TextMeshPro

public class DialogueManager3 : MonoBehaviour
{
    public static DialogueManager3 Instance;
    public TextMeshProUGUI dialogueText;  // Texto do diálogo com TMP

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        dialogueText.text = message;  // Exibe a mensagem na UI
    }
}
