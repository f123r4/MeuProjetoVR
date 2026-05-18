using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public Camera referenciaCamera;
    [SerializeField] float velocidade       = 3f;
    [SerializeField] float raioColeta       = 1.5f;
    [SerializeField] float raioBotao = 1.5f;

    private CharacterController _cc;
    private float _velocidadeY;
    private BotaoPrincipalController botaoAtual = null;

    void Awake() => _cc = GetComponent<CharacterController>();

    void Update()
    {
        Mover();
        VerificarColeta();
        VerificarBotao();
    }

    void Mover()
    {
        if (_cc.isGrounded) _velocidadeY = -0.5f;
        else _velocidadeY += Physics.gravity.y * Time.deltaTime;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = transform.TransformDirection(new Vector3(h, 0, v)) * velocidade;
        _cc.Move((dir + Vector3.up * _velocidadeY) * Time.deltaTime);
    }

    void VerificarColeta()
    {
        foreach (var col in Physics.OverlapSphere(transform.position, raioColeta))
        {
            var ctrl = col.GetComponent<ObjetoColetavelController>();
            if (ctrl != null) ctrl.TentarColetar();
        }
    }

    void VerificarBotao()
    {
        BotaoPrincipalController botaoEncontrado = null;
        foreach (var col in Physics.OverlapSphere(transform.position, raioBotao))
        {
            var b = col.GetComponent<BotaoPrincipalController>();
            if (b != null) { botaoEncontrado = b; break; }
        }

        if (botaoEncontrado != null && botaoAtual == null)
        {
            botaoAtual = botaoEncontrado;
            botaoAtual.AoEntrarHoverProximidade();
        }
        else if (botaoEncontrado == null && botaoAtual != null)
        {
            botaoAtual.AoSairHoverProximidade();
            botaoAtual = null;
        }

        if (botaoAtual != null && (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)))
            botaoAtual.AoPressionarProximidade();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioColeta);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioBotao);
    }
}
