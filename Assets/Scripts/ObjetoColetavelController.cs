using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjetoColetavelController : MonoBehaviour
{
    [SerializeField] public string nomeObjeto = "Objeto";
    [SerializeField] public int pontos = 10;

    private bool coletado = false;

    public void TentarColetar()
    {
        if (coletado) return;

        coletado = true;

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
            gm.RegistrarColeta(pontos);

        ObjetoColetavelView view = GetComponent<ObjetoColetavelView>();
        if (view != null)
            view.Coletar();
        else
            gameObject.SetActive(false);
    }

    public void OnInteracaoXR(SelectEnterEventArgs args)
    {
        TentarColetar();
    }
}
