using UnityEngine;
using TMPro;
using System.Collections; // Necessário para usar Coroutines

public class ColetarLixo : MonoBehaviour // Classe que gerencia a coleta de lixo na missão
{
    public float interacaoRange = 2f; // Distância máxima para interagir com objetos
    public Transform handTransform; // Posição onde o lixo será segurado
    public TMP_Text contadorTexto; // Texto na tela que mostra a contagem de lixos
    public int totalLixosNaCena = 8; // Total de lixos necessários para concluir a missão

    private GameObject lixoSegurado; // Referência ao lixo que o jogador está segurando
    private int lixosColetados = 0; // Contador de quantos lixos já foram coletados
    private bool missaoLixoAtiva = false; // Indica se a missão está ativa

    private Coroutine esconderTextoCoroutine; // Guarda a coroutine de esconder texto, para evitar duplicatas
    private NPCInteraction npcResponsavel; // NPC que ativou essa missão, usado para marcar missão concluída

    void Start()
    {
     //   contadorTexto.gameObject.SetActive(false); // Esconde o contador no início do jogo
    }

    void Update()
    {
        if (!missaoLixoAtiva) return; // Se a missão não estiver ativa, não faz nada

        if (Input.GetKeyDown(KeyCode.Q)) // Se o jogador apertar a tecla "Q"
        {
            if (lixoSegurado == null) // Se não estiver segurando lixo
            {
                TentarPegarLixo(); // Tenta pegar um lixo próximo
            }
            else
            {
                TentarJogarNaCacamba(); // Se estiver com lixo, tenta jogar na caçamba
            }
        }
    }

    void TentarPegarLixo() // Verifica se há um lixo próximo e pega ele
    {
        Collider[] objetosProximos = Physics.OverlapSphere(transform.position, interacaoRange); // Pega todos os objetos num raio ao redor do jogador
        foreach (Collider col in objetosProximos) // Verifica cada objeto
        {
            if (col.CompareTag("Lixo")) // Se for um objeto com tag "Lixo"
            {
                lixoSegurado = col.gameObject; // Guarda a referência ao lixo
                lixoSegurado.transform.SetParent(handTransform); // Coloca o lixo como filho da mão
                lixoSegurado.transform.localPosition = Vector3.zero; // Centraliza na mão
                lixoSegurado.transform.localRotation = Quaternion.identity; // Reseta rotação

                Rigidbody rb = lixoSegurado.GetComponent<Rigidbody>(); // Pega o Rigidbody
                if (rb != null) rb.isKinematic = true; // Desativa a física pra não cair da mão

                return; // Sai do loop após pegar o lixo
            }
        }
    }

    void TentarJogarNaCacamba() // Verifica se há uma caçamba próxima para jogar o lixo
    {
        Collider[] objetosProximos = Physics.OverlapSphere(transform.position, interacaoRange); // Verifica objetos próximos
        foreach (Collider col in objetosProximos)
        {
            if (col.CompareTag("Caçamba")) // Se for uma caçamba
            {
                Destroy(lixoSegurado); // Remove o objeto lixo da cena
                lixoSegurado = null; // Limpa a referência

                lixosColetados++; // Adiciona +1 ao contador
                AtualizarContador(); // Atualiza o texto da contagem

                if (lixosColetados >= totalLixosNaCena) // Se terminou a missão
                {
                    missaoLixoAtiva = false; // Finaliza a missão

                    if (npcResponsavel != null)
                    {
                        npcResponsavel.missaoConcluida = true; // Marca a missão como concluída no NPC
                    }
                }

                return; // Sai do loop
            }
        }
    }

    void AtualizarContador() // Atualiza o texto com a quantidade de lixos coletados
    {
        if (contadorTexto != null)
        {
            if (lixosColetados >= totalLixosNaCena) // Se coletou todos os lixos
            {
                contadorTexto.text = "Todos os lixos coletados!"; // Mensagem final

                if (esconderTextoCoroutine != null) // Se já tiver uma coroutine rodando, para ela
                    StopCoroutine(esconderTextoCoroutine);

                esconderTextoCoroutine = StartCoroutine(EsconderTextoAposSegundos(5)); // Esconde o texto após 5 segundos
            }
            else
            {
                contadorTexto.text = $"{lixosColetados}/{totalLixosNaCena} lixos coletados"; // Atualiza contagem normal
            }
        }
    }

    IEnumerator EsconderTextoAposSegundos(float segundos) // Coroutine que esconde o texto depois de alguns segundos
    {
        yield return new WaitForSeconds(segundos); // Espera o tempo indicado
        contadorTexto.gameObject.SetActive(false); // Esconde o contador
    }

    // Ativa a missão e define o NPC que requisitou
    public void AtivarMissao(NPCInteraction npc)
    {
        missaoLixoAtiva = true; // Ativa o sistema de coleta
        npcResponsavel = npc; // Salva o NPC responsável pela missão
        contadorTexto.gameObject.SetActive(true); // Exibe o contador na tela
        AtualizarContador(); // Atualiza o contador com os valores iniciais
    }
}
