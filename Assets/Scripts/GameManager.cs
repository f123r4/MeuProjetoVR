using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HUDView hudView;
    public GameObject botaoPrincipal;
    public GameObject paredeEntrada;

    private int pontuacao = 0;
    private int totalObjetos = 3;
    private int objetosColetados = 0;

    private void Start()
    {
        if (botaoPrincipal != null) botaoPrincipal.SetActive(false);

        if (hudView != null)
        {
            hudView.AtualizarPontuacao(pontuacao);
            hudView.AtualizarObjetos(objetosColetados, totalObjetos);
            hudView.ExibirMensagem("Colete todos os objetos!");
        }
    }

    public bool TodosObjetosColetados() => objetosColetados >= totalObjetos;

    public void RegistrarColeta(int pontos)
    {
        pontuacao += pontos;
        objetosColetados++;

        if (hudView != null)
        {
            hudView.AtualizarPontuacao(pontuacao);
            hudView.AtualizarObjetos(objetosColetados, totalObjetos);

            if (objetosColetados >= totalObjetos)
            {
                hudView.ExibirMensagem("Parabéns! Entre na casa e pressione o botão!");
                if (botaoPrincipal != null) botaoPrincipal.SetActive(true);
                if (paredeEntrada != null) paredeEntrada.SetActive(false);
            }
            else
                hudView.ExibirMensagem("Objeto coletado! +" + pontos + " pontos");
        }
    }
}
