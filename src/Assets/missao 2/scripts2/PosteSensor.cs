using UnityEngine;

public class PosteSensor : MonoBehaviour
{
    private bool sensorInstalado = false;
    private bool playerPerto = false;

    [Header("Referências")]
    public GameObject luzPoste;
    public GameObject sensorVisual;
    public Animator playerAnimator;
    public Transform jogador; // arraste o Player aqui no Inspector

    [Header("Configurações")]
    public float tempoAnimacao = 2f;
    public float distanciaParaAtivar = 6f;

    private void Update()
    {
        //  Parte da instalação (pressionar E)
        if (playerPerto && !sensorInstalado && MissaoPostesController.Instance.missaoIniciada && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(InstalarSensor());
        }

        //  Parte da luz controlada por proximidade (só após instalação)
        if (sensorInstalado && jogador != null && luzPoste != null)
        {
            float distancia = Vector3.Distance(jogador.position, transform.position);

            luzPoste.SetActive(distancia <= distanciaParaAtivar);
        }
    }

    private System.Collections.IEnumerator InstalarSensor()
    {
        sensorInstalado = true;
        playerAnimator?.SetTrigger("Interagir");
        yield return new WaitForSeconds(tempoAnimacao);

        if (sensorVisual != null) sensorVisual.SetActive(true);

        MissaoPostesController.Instance.SensorInstalado();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;
            UIManager.Instance?.ShowInteractionText("Pressione E para instalar sensor");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;
            UIManager.Instance?.HideInteractionText();
        }
    }
}
