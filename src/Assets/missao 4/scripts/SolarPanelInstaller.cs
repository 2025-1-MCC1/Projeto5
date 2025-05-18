using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SolarPanelInstaller : MonoBehaviour
{
    public GameObject painelSolarPrefab;  // Prefab do painel solar a ser instalado
    public Transform pontoDeInstalacao;   // Posição e rotação onde o painel será colocado

    private bool jogadorNaArea = false;   // Se o jogador está perto o suficiente para instalar
    private bool painelInstalado = false; // Para garantir que instala só uma vez
    private bool playerInRange = false;   // Duplicado de jogadorNaArea (pode remover um)

    void Update()
    {
        // Verifica se o jogador está na área, painel ainda não foi instalado e apertou Q
        if (jogadorNaArea && !painelInstalado && Input.GetKeyDown(KeyCode.Q))
        {
            // Confirma se a missão está liberada pelo gerenciador
            if (MissaoEnergiaManager.Instance != null &&
                MissaoEnergiaManager.Instance.MissaoLiberada())
            {
                InstalarPainel();
            }
            else
            {
                Debug.Log("A missão de energia renovável ainda não está liberada!");
            }
        }
    }

    void InstalarPainel()
    {
        painelInstalado = true;

        // Instancia o painel solar na posição e rotação especificada
        if (painelSolarPrefab != null && pontoDeInstalacao != null)
        {
            Instantiate(painelSolarPrefab, pontoDeInstalacao.position, pontoDeInstalacao.rotation);
        }

        // Registra a instalação no gerenciador da missão
        if (MissaoEnergiaManager.Instance != null)
        {
            MissaoEnergiaManager.Instance.RegistrarInstalacao();
        }

        // Inicia a coroutine para carregar a cena de fim do jogo após 15 segundos
        StartCoroutine(CarregarCenaFimJogo());
    }

    void OnTriggerEnter(Collider other)
    {
        // Ativa flag quando jogador entra na área e mostra texto de interação
        if (other.CompareTag("Player"))
        {
            jogadorNaArea = true;
            playerInRange = true;  // Pode remover essa variável duplicada para simplificar
            UIManager.Instance?.ShowInteractionText("Pressione Q para instalar o painel.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Desativa flag quando jogador sai da área
        if (other.CompareTag("Player"))
        {
            jogadorNaArea = false;
        }

        // Remove o destaque visual (brilho) se existir
        HighlightController highlight = GetComponent<HighlightController>();
        if (highlight != null)
        {
            highlight.DesativarBrilho();
        }
    }

    IEnumerator CarregarCenaFimJogo()
    {
        yield return new WaitForSeconds(15f); // Espera 15 segundos antes de mudar a cena
        SceneManager.LoadScene("TextoFim");
    }
}
