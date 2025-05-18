using UnityEngine;
using UnityEngine.UI;

public class SolarPanel : MonoBehaviour
{
    public float velocidadeCarregamento = 10f; // Velocidade que o painel solar carrega energia por segundo
    public float energiaMaxima = 100f; // Quantidade máxima de energia que o painel pode armazenar

    private float energiaAtual = 0f; // Armazena a energia atual carregada
    private Slider barraEnergia; // Referência para o Slider da UI que mostra a energia

    void Start()
    {
        barraEnergia = GetComponentInChildren<Slider>(); // Procura um componente Slider entre os filhos deste GameObject
        if (barraEnergia != null) // Se encontrou o Slider
            barraEnergia.maxValue = energiaMaxima; // Define o valor máximo da barra como a energia máxima
    }

    void Update()
    {
        if (DiaNoite.Instance != null && DiaNoite.Instance.EstaDeDia()) // Se o sistema DiaNoite existe e está de dia
        {
            energiaAtual += velocidadeCarregamento * Time.deltaTime; // Aumenta a energia atual conforme o tempo e velocidade
            energiaAtual = Mathf.Clamp(energiaAtual, 0f, energiaMaxima); // Limita a energia entre 0 e o máximo

            if (barraEnergia != null) // Se a barra de energia foi encontrada
                barraEnergia.value = energiaAtual; // Atualiza a barra com o valor atual da energia
        }
    }
}
