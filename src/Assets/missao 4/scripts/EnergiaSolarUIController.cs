using UnityEngine;
using UnityEngine.UI;

public class EnergiaSolarUIController : MonoBehaviour
{
    [Header("Referências")]
    public Slider barraEnergia;         // O Slider da UI
    public DiaNoite sistemaTempo;       // Referência ao sistema de tempo (coloque na cena se ainda não tiver)

    [Header("Configurações")]
    public float velocidadeCarga = 0.3f;   // Velocidade de carga por segundo
    public float velocidadeDescarga = 0.2f; // Velocidade de descarga por segundo

    private void Start()
    {
        if (barraEnergia != null)
            barraEnergia.value = 0f;  // Começa vazia
    }

    private void Update()
    {
        if (sistemaTempo == null || barraEnergia == null) return;

        int hora = sistemaTempo.GetHoraAtual();  // Vamos adicionar esse método no script DiaNoite!

        bool ehDia = hora >= 6 && hora < 18;

        if (ehDia)
        {
            barraEnergia.value += velocidadeCarga * Time.deltaTime;
        }
        else
        {
            barraEnergia.value -= velocidadeDescarga * Time.deltaTime;
        }

        barraEnergia.value = Mathf.Clamp(barraEnergia.value, 0f, 1f); // Garante que vai ficar entre 0 e 1
    }
}