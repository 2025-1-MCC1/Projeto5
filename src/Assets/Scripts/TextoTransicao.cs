using UnityEngine;
using UnityEngine.SceneManagement;  // Importa para controlar cenas (trocar de cena)

public class TextoTransicao : MonoBehaviour
{
    public float tempoMaximo = 15f;      // Tempo máximo em segundos para aguardar antes de trocar de cena automaticamente
    public string proximaCena = "Jogo";  // Nome da próxima cena que será carregada

    private float tempoDecorrido = 0f;   // Controle do tempo passado desde o início
    private bool carregandoCena = false; // Flag para evitar carregar a cena várias vezes

    void Update()
    {
        // Incrementa o tempo decorrido a cada frame com o tempo que passou desde o último frame
        tempoDecorrido += Time.deltaTime;

        // Se ainda não está carregando a cena e o tempo já passou do limite ou o jogador apertou espaço
        if (!carregandoCena && (tempoDecorrido >= tempoMaximo || Input.GetKeyDown(KeyCode.Space)))
        {
            carregandoCena = true;                 // Marca que está carregando para evitar múltiplas chamadas
            SceneManager.LoadScene(proximaCena);  // Carrega a cena especificada em proximaCena
        }
    }
}
