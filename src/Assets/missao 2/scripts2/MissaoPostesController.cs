// MissaoPostesController.cs
using UnityEngine;

public class MissaoPostesController : MonoBehaviour
{
    public static MissaoPostesController Instance;

    public int totalSensores = 5;
    private int sensoresInstalados = 0;
    public bool missaoIniciada = false;
    public bool missaoConcluida = false;

    private void Awake()
    {
        Instance = this;
    }

    public void IniciarMissao()
    {
        missaoIniciada = true;
        sensoresInstalados = 0;
    }

    public void SensorInstalado()
    {
        sensoresInstalados++;

        if (sensoresInstalados >= totalSensores)
        {
            missaoConcluida = true;
            Debug.Log("Missao dos postes concluida!");
        }
    }

    public bool MissaoCompleta()
    {
        return missaoConcluida;
    }
}