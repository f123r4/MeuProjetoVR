using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BotaoPrincipalController : MonoBehaviour
{
    private BotaoPrincipalView view;

    void Awake()
    {
        view = GetComponent<BotaoPrincipalView>();
    }

    public void AoEntrarHoverProximidade()
    {
        view?.SetHover(true);
        var gm = FindObjectOfType<GameManager>();
        gm?.hudView?.ExibirMensagem("Pressione E para ativar!");
    }

    public void AoSairHoverProximidade()
    {
        view?.SetHover(false);
        var gm = FindObjectOfType<GameManager>();
        gm?.hudView?.ExibirMensagem("Colete todos os objetos!");
    }

    public void AoPressionarProximidade()
    {
        view?.SetAtivo();
        var gm = FindObjectOfType<GameManager>();
        gm?.hudView?.ExibirMensagem("Botao ativado!");
    }

    public void AoEntrarHover(HoverEnterEventArgs args) => AoEntrarHoverProximidade();
    public void AoSairHover(HoverExitEventArgs args)    => AoSairHoverProximidade();
    public void AoPressionar(SelectEnterEventArgs args) => AoPressionarProximidade();
}
