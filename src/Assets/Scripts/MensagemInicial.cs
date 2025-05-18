using UnityEngine;

public class MensagemInicial : MonoBehaviour
{
    public GameObject painelDica;
    public float tempoExibicao = 5f;

    void Start()
    {
        if (painelDica != null)
        {
            painelDica.SetActive(true);
            Invoke("EsconderDica", tempoExibicao); // esconde após X segundos
        }
    }

    void EsconderDica()
    {
        if (painelDica != null)
        {
            painelDica.SetActive(false);
        }
    }
}
