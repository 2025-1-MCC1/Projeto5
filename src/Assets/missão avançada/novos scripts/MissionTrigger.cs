using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    private bool playerInRange = false; // Indica se o jogador está perto do trigger
    private bool missionStarted = false; // Indica se a missão já foi iniciada

    public GameObject missionObjects; // Objeto pai que contém as lixeiras ou objetos da missão
    public GameObject dialoguePanel;  // Painel UI para mostrar o diálogo

    void Start()
    {
        // Pode deixar vazio se não houver inicialização necessária
    }

    void Update()
    {
        // Verifica se o jogador está perto, a missão ainda não iniciou e pressionou 'E'
        if (playerInRange && !missionStarted && Input.GetKeyDown(KeyCode.E))
        {
            missionStarted = true;           // Marca a missão como iniciada
            dialoguePanel.SetActive(true);   // Mostra o painel de diálogo do NPC
            missionObjects.SetActive(true);  // Ativa as lixeiras ou objetos relacionados à missão
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Quando algum collider entrar no trigger, verifica se é o jogador
        if (other.CompareTag("Player"))
        {
            playerInRange = true;                        // Marca que o jogador está próximo
            UIManager.Instance.ShowInteractionText("Fale com o NPC (E)");  // Mostra texto de interação na UI
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quando o jogador sair do trigger
        if (other.CompareTag("Player"))
        {
            playerInRange = false;                       // Marca que o jogador saiu da área
            UIManager.Instance.HideInteractionText();   // Esconde o texto de interação na UI
        }
    }
}
