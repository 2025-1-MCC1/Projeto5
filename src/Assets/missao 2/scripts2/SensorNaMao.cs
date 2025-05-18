// SensorNaMao.cs
using UnityEngine;

public class SensorNaMao : MonoBehaviour
{
    public GameObject sensorPrefab;
    public Transform playerHand;
    private GameObject sensorAtual;

    void Update()
    {
        if (MissaoPostesController.Instance.missaoIniciada && !MissaoPostesController.Instance.missaoConcluida && sensorAtual == null)
        {
            sensorAtual = Instantiate(sensorPrefab, playerHand.position, playerHand.rotation);
            sensorAtual.transform.SetParent(playerHand);
        }

        if (MissaoPostesController.Instance.missaoConcluida && sensorAtual != null)
        {
            Destroy(sensorAtual);
        }
    }
}
