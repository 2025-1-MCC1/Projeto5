using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class DiaNoite : MonoBehaviour
{
    [Header("Configurações de Tempo")]
    [SerializeField] private Transform luzDirecional;     // Referência à luz direcional (sol)
    [SerializeField] private int duracaoDoDia = 120;      // Duração de um dia no jogo em segundos
    [SerializeField] private TextMeshProUGUI horarioText; // Referência ao texto que exibe o horário na tela

    [Header("Céu e Lua")]
    [SerializeField] private Material skyboxDia;          // Skybox usada durante o dia
    [SerializeField] private Material skyboxNoite;        // Skybox usada durante a noite
    [SerializeField] private GameObject lua;              // Objeto da lua
    [SerializeField] private Transform luaTransform;      // Transform da lua (não está sendo usado diretamente aqui)

    [Header("Transição")]
    [SerializeField] private Light luzSol;                // Luz que representa o sol (usada para transição)
    [SerializeField] private float intensidadeDia = 1.2f; // Intensidade do sol durante o dia
    [SerializeField] private float intensidadeNoite = 0.1f; // Intensidade da lua durante a noite
    [SerializeField] private float velocidadeTransicao = 1f; // Velocidade da transição de luz

    private float segundos;                               // Segundos acumulados do dia atual
    private float multiplicador;                          // Multiplicador para converter tempo real em tempo do jogo

    private string[] nomesDasFases = { "Manhã", "Tarde", "Noite", "Madrugada" }; // Vetor com nomes dos períodos do dia

    void Start()
    {
        multiplicador = 86400f / duracaoDoDia;            // Calcula quantos segundos de jogo se passam por segundo real (86400s = 24h)
        segundos = 32400;
    }

    void Update()
    {
        segundos += Time.deltaTime * multiplicador;       // Incrementa o tempo do jogo baseado no tempo real
        if (segundos >= 86400f) segundos = 0f;            // Reinicia o contador ao completar 24h
        {
            GirarCeu();                                   // Atualiza a rotação do sol e da lua
            CalcularTempo();                              // Atualiza o texto do horário e nome do período
            AtualizarDiaNoite();                          // Troca o skybox, a lua e a intensidade da luz conforme o horário
            TransicaoLuzes();                             // Faz transição suave da intensidade da luz
        }
    }

    private void GirarCeu()
    {
        float rotacaoX = Mathf.Lerp(-90f, 270f, segundos / 86400f);             // Gira o sol em 360° ao longo do dia
        luzDirecional.rotation = Quaternion.Euler(rotacaoX, 0f, 0f);           // Aplica a rotação calculada ao sol

        if (lua != null)
        {
            float rotacaoLua = Mathf.Lerp(90f, 450f, segundos / 86400f);       // Rotação da lua (oposta ao sol)
            Quaternion rotacao = Quaternion.Euler(rotacaoLua, 0f, 0f);         // Cria rotação da lua
            Vector3 direcaoLua = rotacao * Vector3.forward;                    // Direção que a lua deve apontar

            lua.transform.position = direcaoLua * -600f + new Vector3(0f, 0f, 0f); // Posiciona a lua no céu em oposição ao sol
            lua.transform.LookAt(Vector3.zero);                                // Faz a lua olhar para o centro da cena
        }
    }

    private void CalcularTempo()
    {
        int hora = (int)(segundos / 3600f);                                    // Converte os segundos acumulados em hora (0–23)
        string periodo = "";                                                  // Variável que irá armazenar o nome do período

        if (hora >= 6 && hora < 12)
            periodo = nomesDasFases[0];                                       // Manhã
        else if (hora >= 12 && hora < 18)
            periodo = nomesDasFases[1];                                       // Tarde
        else if (hora >= 18 && hora < 24)
            periodo = nomesDasFases[2];                                       // Noite
        else
            periodo = nomesDasFases[3];                                       // Madrugada

        string horaFormatada = TimeSpan.FromSeconds(segundos).ToString(@"hh\:mm"); // Converte os segundos acumulados para o formato hh:mm
        horarioText.text = horaFormatada + " - " + periodo;                   // Atualiza o texto com horário e nome do período
    }

    private void AtualizarDiaNoite()
    {
        int hora = (int)(segundos / 3600f);                                    // Converte os segundos acumulados em hora (0–23)
        bool ehDia = hora >= 6 && hora < 18;                                   // Define se é dia com base na hora (entre 6h e 18h)

        if (ehDia)
            RenderSettings.skybox = skyboxDia;                                // Troca o skybox para dia
        else
            RenderSettings.skybox = skyboxNoite;                              // Troca o skybox para noite

        if (lua != null)
            lua.SetActive(!ehDia);                                            // Ativa a lua apenas à noite

        if (ehDia)
            luzDirecional.GetComponent<Light>().intensity = 1.2f;            // Ajusta a intensidade do sol durante o dia
        else
            luzDirecional.GetComponent<Light>().intensity = 0.2f;            // Ajusta a intensidade do sol durante a noite
    }

    private void TransicaoLuzes()
    {
        int hora = (int)(segundos / 3600f);                                    // Converte os segundos em hora
        bool ehDia = hora >= 6 && hora < 18;                                   // Verifica se é dia

        float alvoSol;                                                         // Intensidade alvo do sol
        if (ehDia)
            alvoSol = intensidadeDia;                                          // Define intensidade do sol para o dia
        else
            alvoSol = intensidadeNoite;                                        // Define intensidade do sol para a noite

        luzSol.intensity = Mathf.Lerp(luzSol.intensity, alvoSol, Time.deltaTime * velocidadeTransicao); // Faz a transição suave da intensidade do sol
    }

    public static DiaNoite Instance;

    void Awake()
    {
        Instance = this;
    }

    public bool EstaDeDia()
    {
        int hora = (int)(segundos / 3600f);
        return hora >= 6 && hora < 18;
    }

    public int GetHoraAtual()
    {
        return (int)(segundos / 3600f);
    }
}
