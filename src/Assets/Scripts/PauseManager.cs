using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;        // Painel do menu de pausa (UI)
    public string nomeCenaMenu = "Menu";  // Nome da cena do menu principal

    private bool jogoPausado = false;     // Controle do estado de pausa

    void Update()
    {
        // Detecta quando a tecla ESC é pressionada para alternar entre pausar e continuar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
            {
                ContinuarJogo();  // Se estiver pausado, continua o jogo
            }
            else
            {
                PausarJogo();    // Se estiver rodando, pausa o jogo
            }
        }
    }

    public void PausarJogo()
    {
        pauseMenuUI.SetActive(true);       // Mostra o menu de pausa
        Time.timeScale = 0f;               // Congela o tempo do jogo (tudo para)
        jogoPausado = true;                // Marca como pausado
        Cursor.lockState = CursorLockMode.None;  // Libera o cursor para o usuário usar o mouse no menu
        Cursor.visible = true;             // Mostra o cursor
    }

    public void ContinuarJogo()
    {
        pauseMenuUI.SetActive(false);      // Esconde o menu de pausa
        Time.timeScale = 1f;               // Retoma o tempo normal do jogo
        jogoPausado = false;               // Marca como não pausado
        Cursor.lockState = CursorLockMode.Locked;  // Trava o cursor no centro da tela para controle do jogo
        Cursor.visible = false;            // Oculta o cursor
    }

    public void VoltarParaMenu()
    {
        Time.timeScale = 1f;               // Garante que o tempo está normal antes de trocar de cena
        SceneManager.LoadScene(nomeCenaMenu); // Carrega a cena do menu principal
    }

    public void SairDoJogo()
    {
        Debug.Log("Saindo...");            // Mensagem no console para depuração
        Application.Quit();                // Fecha o jogo (funciona apenas em builds finais)
    }
}
