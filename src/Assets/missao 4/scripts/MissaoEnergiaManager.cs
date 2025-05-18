using System.Collections.Generic;
using UnityEngine;

public class MissaoEnergiaManager : MonoBehaviour
{
    public static MissaoEnergiaManager Instance; // Instância única do gerenciador (padrão Singleton)

    [Header("Configuração da Missão")]
    [Tooltip("Total de prédios onde instalar painéis")]
    public int totalInstalacoes = 3; // Número total de instalações necessárias para concluir a missão

    private int instalacoesConcluidas = 0; // Contador de quantas instalações já foram feitas
    private bool missaoLiberada = false; // Define se a missão já foi liberada ou não

    [Header("Referências Visuais")]
    public GameObject centroDeControle; // Objeto que será ativado ao concluir a missão

    [Tooltip("Lista de prédios que vão brilhar após a liberação da missão")]
    public List<HighlightController> prediosQueDevemBrilhar; // Lista de prédios que terão destaque visual

    void Awake()
    {
        if (Instance == null) // Se ainda não existe instância...
            Instance = this; // Define esta como a instância principal
        else
            Destroy(gameObject); // Destroi objetos duplicados para manter uma única instância
    }

    public void LiberarMissao()
    {
        missaoLiberada = true; // Marca a missão como liberada
        Debug.Log("Missão de Energia Renovável liberada!"); // Exibe mensagem no console

        AtivarMissaoEnergia(); // Chama função para ativar efeitos visuais da missão
    }

    public bool MissaoLiberada()
    {
        return missaoLiberada; // Retorna se a missão já foi liberada
    }

    public void RegistrarInstalacao()
    {
        instalacoesConcluidas++; // Incrementa o número de instalações realizadas
        Debug.Log($"Painéis instalados: {instalacoesConcluidas}/{totalInstalacoes}"); // Mostra progresso no console

        if (instalacoesConcluidas >= totalInstalacoes) // Se todas as instalações foram feitas...
            ConcluirMissao(); // Finaliza a missão
    }

    private void ConcluirMissao()
    {
        Debug.Log("Missão de Energia Renovável concluída!"); // Exibe mensagem de missão concluída

        if (centroDeControle != null) // Se o objeto centroDeControle foi definido...
            centroDeControle.SetActive(true); // Ativa o objeto visualmente na cena
    }

    private void AtivarMissaoEnergia()
    {
        foreach (HighlightController predio in prediosQueDevemBrilhar) // Para cada prédio na lista...
        {
            if (predio != null) // Se o prédio não for nulo...
                predio.AtivarBrilho(); // Ativa o efeito de brilho nele
        }
    }
}