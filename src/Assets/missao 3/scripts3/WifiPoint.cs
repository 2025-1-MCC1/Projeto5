using UnityEngine;

public class WiFiPoint : MonoBehaviour
{
    private bool activated = false;           // Controla se o ponto Wi-Fi já foi ativado
    private bool playerInRange = false;       // Controla se o jogador está próximo
    public GameObject visualFeedback;         // Objeto visual que aparece ao ativar (ex: luz, ícone)
    public GameObject particlesOnActivation;  // Partículas que aparecem na ativação

    private void Update()
    {
        // Verifica se o jogador está perto, o ponto não está ativado,
        // e o jogador apertou E para ativar
        if (playerInRange && !activated && Input.GetKeyDown(KeyCode.E))
        {
            // Verifica se o controlador da missão existe e a missão está em andamento
            if (WiFiMissionController.Instance != null &&
                WiFiMissionController.Instance.missionState == WiFiMissionController.MissionState.InProgress)
            {
                // Inicia a coroutine que lida com a ativação e animação
                StartCoroutine(ActivateWithInteraction());
            }
        }
    }

    private System.Collections.IEnumerator ActivateWithInteraction()
    {
        activated = true; // Marca como ativado para não ativar novamente

        // Busca o script do jogador para travar o movimento e ativar animação
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.LockMovement(true);           // Trava o movimento do jogador
            player.PlayInteractionAnimation();  // Toca animação de interação (ex: mexer na antena)
        }

        yield return new WaitForSeconds(2f);     // Espera 2 segundos durante a animação

        // Ativa feedback visual (luz, ícone)
        if (visualFeedback != null)
            visualFeedback.SetActive(true);

        // Instancia partículas no ponto de ativação
        if (particlesOnActivation != null)
            Instantiate(particlesOnActivation, transform.position, Quaternion.identity);

        // Notifica o controlador da missão que um ponto foi ativado
        WiFiMissionController.Instance.AntennaActivated();

        Debug.Log("Antena ativada com interação: " + gameObject.name);

        // Libera o movimento do jogador
        if (player != null)
        {
            player.LockMovement(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Quando jogador entra no trigger, ativa flag e mostra texto de interação
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            UIManager.Instance?.ShowInteractionText("Pressione E para ativar o Wi-Fi");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Quando jogador sai do trigger, desativa flag e esconde texto
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            UIManager.Instance?.HideInteractionText();
        }
    }
}
