using UnityEngine;

public class MissionTrigger3 : MonoBehaviour
{
    private bool playerInRange = false; // Indica se o jogador está dentro da área de ativação
    private bool missionStarted = false; // Indica se a missão já começou

    public GameObject missionObjects; // Objeto pai que contém as lixeiras ou objetos da missão
    public GameObject dialoguePanel; // Painel de diálogo do NPC (caso exista UI)

    void Start()
    {
        // Não é necessário inicializar nada no Start neste caso
    }

    void Update()
    {
        if (playerInRange && !missionStarted && Input.GetKeyDown(KeyCode.E)) // Se o jogador está perto, a missão ainda não começou e apertou E
        {
            missionStarted = true; // Marca a missão como iniciada
            dialoguePanel.SetActive(true); // Ativa o painel de diálogo do NPC
            missionObjects.SetActive(true); // Ativa os objetos da missão (ex: lixeiras)
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se quem entrou no trigger foi o jogador
        {
            playerInRange = true; // Marca que o jogador está na área
            UIManager.Instance.ShowInteractionText("Fale com o NPC (E)"); // Mostra texto de interação na tela
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se quem saiu do trigger foi o jogador
        {
            playerInRange = false; // Marca que o jogador saiu da área
            UIManager.Instance.HideInteractionText(); // Esconde o texto de interação da tela
        }
    }
}
