using UnityEngine;

public class HighlightController : MonoBehaviour
{
    private Renderer rend; // Referência ao componente Renderer do objeto
    private Material originalMaterial; // Armazena o material original do objeto
    public Material highlightMaterial; // Material que será usado para destacar o objeto

    void Start()
    {
        rend = GetComponent<Renderer>(); // Obtém o componente Renderer do objeto
        if (rend != null) // Se encontrou o Renderer
        {
            originalMaterial = rend.material; // Armazena o material original
        }
    }

    public void AtivarBrilho()
    {
        if (rend != null && highlightMaterial != null) // Se o Renderer e o material de destaque estão definidos
        {
            rend.material = highlightMaterial; // Aplica o material de destaque no objeto
        }
    }

    public void DesativarBrilho()
    {
        if (rend != null && originalMaterial != null) // Se o Renderer e o material original estão definidos
        {
            rend.material = originalMaterial; // Restaura o material original do objeto
        }
    }
}
