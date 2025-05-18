using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject painelDialogo; // Painel na UI que exibe o diálogo
    public TextMeshProUGUI nomePersonagemText; // Componente de texto que mostra o nome do personagem
    public TextMeshProUGUI textoDialogo; // Componente de texto que mostra a fala do personagem

    private string[] nomes; // Array que guarda os nomes dos personagens que estão falando
    private string[] falas; // Array que guarda as falas dos personagens
    private int indexAtual; // Índice atual da fala que está sendo exibida
    private bool dialogoAtivo; // Indica se o diálogo está em andamento

    void Update()
    {
        // Se o diálogo estiver ativo e o jogador apertar "E", avança para a próxima fala
        if (dialogoAtivo && Input.GetKeyDown(KeyCode.E))
        {
            AvancarDialogo(); // Chama a função que mostra a próxima fala
        }
    }

    public void IniciarDialogo(string[] novosNomes, string[] novasFalas) // Inicia um novo diálogo
    {
        nomes = novosNomes; // Recebe os nomes dos personagens
        falas = novasFalas; // Recebe as falas correspondentes
        indexAtual = 0; // Começa pelo primeiro índice
        dialogoAtivo = true; // Ativa o estado de diálogo

        painelDialogo.SetActive(true); // Ativa o painel de diálogo na tela
        MostrarFalaAtual(); // Exibe a primeira fala
    }

    void AvancarDialogo() // Avança para a próxima fala
    {
        indexAtual++; // Vai para o próximo índice
        if (indexAtual < falas.Length) // Se ainda houver falas disponíveis
        {
            MostrarFalaAtual(); // Exibe a próxima fala
        }
        else
        {
            FinalizarDialogo(); // Se não houver mais falas, finaliza o diálogo
        }
    }

    void MostrarFalaAtual() // Mostra a fala e o nome do personagem atual
    {
        if (indexAtual < falas.Length && indexAtual < nomes.Length) // Garante que o índice está dentro dos limites
        {
            textoDialogo.text = falas[indexAtual]; // Atualiza o texto com a fala atual
            nomePersonagemText.text = nomes[indexAtual]; // Atualiza o nome do personagem atual
        }
    }

    void FinalizarDialogo() // Finaliza o diálogo e oculta o painel
    {
        painelDialogo.SetActive(false); // Esconde o painel de diálogo
        dialogoAtivo = false; // Marca que o diálogo terminou
    }
}
