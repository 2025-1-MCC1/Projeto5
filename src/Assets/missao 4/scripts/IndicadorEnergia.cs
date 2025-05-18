using UnityEngine;

public class IndicadorEnergia : MonoBehaviour
{
    public Renderer cuboRenderer; // Referência ao componente Renderer do cubo
    public float energiaAtual = 0f; // Energia acumulada no momento
    public float energiaMaxima = 100f; // Energia máxima possível
    public Color corFraca = Color.red; // Cor que representa pouca energia
    public Color corForte = Color.green; // Cor que representa energia cheia
    private DiaNoite diaNoite; // Referência ao sistema de dia e noite

    void Start()
    {
        if (cuboRenderer == null) // Se o Renderer não foi atribuído manualmente
            cuboRenderer = GetComponent<Renderer>(); // Pega o Renderer do próprio objeto

        diaNoite = FindObjectOfType<DiaNoite>(); // Procura automaticamente o script DiaNoite na cena
    }

    void Update()
    {
        if (diaNoite != null && diaNoite.EstaDeDia()) // Se o sistema DiaNoite existe e está de dia
        {
            energiaAtual += Time.deltaTime * 5f; // Aumenta a energia atual com o tempo
            energiaAtual = Mathf.Clamp(energiaAtual, 0, energiaMaxima); // Garante que o valor fique dentro do limite

            float t = energiaAtual / energiaMaxima; // Calcula a proporção de energia carregada
            Color corAtual = Color.Lerp(corFraca, corForte, t); // Interpola a cor entre fraca e forte com base na energia
            cuboRenderer.material.color = corAtual; // Aplica a cor resultante no material do cubo
        }
    }
}
