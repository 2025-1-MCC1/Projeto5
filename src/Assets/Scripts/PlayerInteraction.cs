using UnityEngine;
public class PlayerInteraction : MonoBehaviour
{
    private NPCInteraction npcNearby; // Guarda uma referência ao NPC mais próximo com quem o jogador pode interagir
    [SerializeField] private DialogueManager dialogueManager; // Referência ao gerenciador de diálogos, configurável pelo inspetor da Unity

    private void Start()
    {
        // Garante que o DialogueManager está referenciado corretamente ao iniciar o jogo
        if (dialogueManager == null) // Se não tiver sido atribuído pelo inspetor
        {
            dialogueManager = FindAnyObjectByType<DialogueManager>(); // Procura automaticamente um DialogueManager presente na cena
        }
    }

    private void Update()
    {
        // Verifica constantemente se o jogador está perto de um NPC e apertou a tecla "E"
        if (npcNearby != null && npcNearby.playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Garante que o painel de diálogo não está aberto antes de iniciar um novo diálogo
            if (!dialogueManager.painelDialogo.activeInHierarchy)
            {
                // Se a missão ainda não foi aceita
                if (!npcNearby.missaoAceita)
                {
                    // Define os nomes dos personagens que irão aparecer no diálogo
                    string[] nomes = { "Jorge", "Policial", "Policial", "Jorge" };

                    // Define as falas correspondentes aos nomes acima
                    string[] falas = {
                        "Olá! Está tudo bem por aqui?",
                        "Na verdade não. Estamos precisando de ajuda para limpar a área.",
                        "Você poderia, por favor, recolher os lixos espalhados pela rua?",
                        "Deixa comigo!" 
                    };

                    dialogueManager.IniciarDialogo(nomes, falas); // Inicia o diálogo usando os arrays de nomes e falas
                    npcNearby.AceitarMissao(); // Marca a missão como aceita no NPC
                }
                else if (npcNearby.missaoConcluida) // Se a missão já foi concluída
                {
                    // Define um diálogo final de agradecimento
                    string[] nomes = { "Policial" };
                    string[] falas = { "Muito obrigado pela ajuda! Agora a rua está limpa." };
                    dialogueManager.IniciarDialogo(nomes, falas); // Inicia esse diálogo final
                }
                // Caso a missão esteja em andamento, mas ainda não concluída, nenhum diálogo é disparado
            }
        }
    }

    private void OnTriggerEnter(Collider other) // Detecta quando algo entra na área de interação do jogador
    {
        if (other.CompareTag("NPC")) // Se for um NPC
        {
            npcNearby = other.GetComponent<NPCInteraction>(); // Pega a referência do script NPCInteraction desse NPC
        }
    }

    private void OnTriggerExit(Collider other) // Detecta quando algo sai da área de interação do jogador
    {
        if (other.CompareTag("NPC")) // Se for um NPC
        {
            npcNearby = null; // Remove a referência ao NPC, pois ele não está mais por perto
        }
    }
}
