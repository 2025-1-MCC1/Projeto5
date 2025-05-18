using UnityEngine;
using TMPro;
using System.Collections; // Importa suporte para corrotinas (IEnumerator)

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Instância singleton para acesso fácil

    public TextMeshProUGUI interactionText; // Texto que aparece para interação (ex: "Pressione E")
    public TextMeshProUGUI missionProgressText; // Texto que mostra progresso da missão
    public GameObject messagePanel; // Painel que mostra mensagens temporárias
    public TextMeshProUGUI messageText; // Texto dentro do painel de mensagens

    private void Awake()
    {
        if (Instance == null) Instance = this; // Define a instância singleton na inicialização
    }

    public void ShowInteractionText(string text)
    {
        interactionText.text = text; // Atualiza o texto de interação
        interactionText.gameObject.SetActive(true); // Exibe o texto na tela
    }

    public void HideInteractionText()
    {
        interactionText.gameObject.SetActive(false); // Esconde o texto de interação
    }

    public void UpdateMissionText(int current, int total)
    {
        missionProgressText.text = $"Sensores instalados: {current}/{total}"; // Atualiza texto do progresso da missão
    }

    public void ShowMessage(string message)
    {
        messageText.text = message; // Define o texto da mensagem
        messagePanel.SetActive(true); // Mostra o painel de mensagem
        Invoke(nameof(HideMessage), 3f); // Agenda esconder a mensagem após 3 segundos
    }

    void HideMessage()
    {
        messagePanel.SetActive(false); // Esconde o painel de mensagem
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup group, float targetAlpha, float duration)
    {
        float startAlpha = group.alpha; // Guarda o alfa inicial do grupo de UI
        float time = 0f; // Tempo acumulado do fade

        while (time < duration) // Enquanto não completar o tempo definido
        {
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration); // Interpola alfa para efeito fade
            time += Time.deltaTime; // Incrementa tempo
            yield return null; // Espera até o próximo frame
        }

        group.alpha = targetAlpha; // Garante que alfa final seja definido
        group.interactable = targetAlpha > 0; // Permite interatividade se visível
        group.blocksRaycasts = targetAlpha > 0; // Bloqueia ou libera clique conforme visibilidade
    }
}
