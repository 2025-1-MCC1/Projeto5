using UnityEngine;
public class NPCInteraction : MonoBehaviour
{
    public bool playerNearby = false; // Indica se o jogador está próximo do NPC
    public bool missaoAceita = false; // Indica se a missão foi aceita pelo jogador
    public bool missaoConcluida = false; // Indica se a missão foi concluída

    private void OnTriggerEnter(Collider other) // Detecta quando algo entra na área de colisão (trigger) do NPC
    {
        if (other.CompareTag("Player")) // Se quem entrou na área for o jogador
        {
            playerNearby = true; // Marca que o jogador está perto
        }
    }

    private void OnTriggerExit(Collider other) // Detecta quando algo sai da área de colisão (trigger) do NPC
    {
        if (other.CompareTag("Player")) // Se quem saiu for o jogador
        {
            playerNearby = false; // Marca que o jogador não está mais perto
        }
    }

    public void AceitarMissao() // Função chamada quando o jogador aceita a missão
    {
        missaoAceita = true; // Atualiza o estado da missão como aceita

        // Ativa o sistema de coleta de lixo no jogo
        FindAnyObjectByType<ColetarLixo>().AtivarMissao(this); // Chama o script de coleta e passa este NPC como referência
    }
}
