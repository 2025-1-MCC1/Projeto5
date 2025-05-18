using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public string Contexto = "Contexto"; // Nome da cena que será carregada

    public void TrocarCena() // Função pública que será chamada para trocar de cena
    {
        SceneManager.LoadScene(Contexto); // Carrega a cena com o nome armazenado na variável 'Contexto'
    }
    public void SairDoJogo()
    {
        Debug.Log("Saindo..."); //sai do jogo
        Application.Quit();
    }
}
