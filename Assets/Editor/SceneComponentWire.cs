#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class SceneComponentWire : EditorWindow
{
    [MenuItem("Tools/Add Missing Components")]
    public static void AddComponents()
    {
        var gmGO = GameObject.Find("GameManager");
        if (gmGO != null)
        {
            AddIfMissing<GameManager>(gmGO);
            Debug.Log("[ComponentWire] GameManager: componente adicionado");
        }
        var esGO = GameObject.Find("EventSystem");
        if (esGO != null)
        {
            AddIfMissing<EventSystem>(esGO);
            AddIfMissing<StandaloneInputModule>(esGO);
            Debug.Log("[ComponentWire] EventSystem: componentes adicionados");
        }
        var hudGO = GameObject.Find("HUD_Canvas");
        if (hudGO != null)
        {
            var canvas = AddIfMissing<Canvas>(hudGO);
            canvas.renderMode = RenderMode.WorldSpace;
            AddIfMissing<CanvasScaler>(hudGO);
            AddIfMissing<GraphicRaycaster>(hudGO);
            AddIfMissing<HUDView>(hudGO);

            // Posicionar HUD na frente do player
            hudGO.transform.position    = new Vector3(0f, 1.6f, 2f);
            hudGO.transform.localScale  = new Vector3(0.002f, 0.002f, 0.002f);

            // Textos TMP
            SetupTMP("Texto_Pontuacao", "Pontos: 0");
            SetupTMP("Texto_Objetos",   "Objetos: 0/3");
            SetupTMP("Texto_Mensagem",  "Colete todos os objetos!");

            EditorUtility.SetDirty(hudGO);
            Debug.Log("[ComponentWire] HUD_Canvas: componentes e posição configurados");
        }
        var xrGO = GameObject.Find("XROrigin");
        if (xrGO != null)
        {
            AddIfMissing<PlayerController>(xrGO);
            xrGO.tag = "Player";
            EditorUtility.SetDirty(xrGO);
            Debug.Log("[ComponentWire] XROrigin: PlayerController e tag Player configurados");
        }
        string[] nomes = { "Chave", "Cristal", "Moeda" };
        int[]    pts   = { 10, 20, 30 };

        for (int i = 1; i <= 3; i++)
        {
            var go = GameObject.Find($"Objeto_Coletavel_0{i}");
            if (go == null) continue;

            AddIfMissing<BoxCollider>(go);
            AddIfMissing<ObjetoColetavelView>(go);
            var ctrl = AddIfMissing<ObjetoColetavelController>(go);
            ctrl.nomeObjeto = nomes[i - 1];
            ctrl.pontos     = pts[i - 1];

            // XRSimpleInteractable
            var xrInt = AddIfMissing<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>(go);
            // Conectar OnInteracaoXR via UnityEvent seria manual,
            // mas registramos o listener via código:
            xrInt.selectEntered.AddListener(ctrl.OnInteracaoXR);

            EditorUtility.SetDirty(go);
            Debug.Log($"[ComponentWire] Objeto_Coletavel_0{i} ({nomes[i-1]}): componentes adicionados");
        }
        var botaoGO = GameObject.Find("Botao_Principal");
        if (botaoGO != null)
        {
            AddIfMissing<CapsuleCollider>(botaoGO);
            AddIfMissing<BotaoPrincipalView>(botaoGO);
            var bCtrl = AddIfMissing<BotaoPrincipalController>(botaoGO);

            var xrInt = AddIfMissing<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>(botaoGO);
            xrInt.hoverEntered.AddListener(bCtrl.AoEntrarHover);
            xrInt.hoverExited.AddListener(bCtrl.AoSairHover);
            xrInt.selectEntered.AddListener(bCtrl.AoPressionar);

            EditorUtility.SetDirty(botaoGO);
            Debug.Log("[ComponentWire] Botao_Principal: componentes e eventos conectados");
        }
        SceneAutoWire.ConectarReferencias();
        UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
        Debug.Log("[ComponentWire] Tudo pronto! Salve com Ctrl+S.");
    }

    static T AddIfMissing<T>(GameObject go) where T : Component
    {
        var comp = go.GetComponent<T>();
        if (comp == null) comp = go.AddComponent<T>();
        return comp;
    }

    static void SetupTMP(string goName, string textoInicial)
    {
        var go = GameObject.Find(goName);
        if (go == null) return;
        var tmp = go.GetComponent<TextMeshProUGUI>();
        if (tmp == null) tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text      = textoInicial;
        tmp.fontSize  = 36;
        tmp.alignment = TextAlignmentOptions.Center;
        var rect = go.GetComponent<RectTransform>();
        if (rect) rect.sizeDelta = new Vector2(300f, 60f);
        EditorUtility.SetDirty(go);
    }
}
#endif
