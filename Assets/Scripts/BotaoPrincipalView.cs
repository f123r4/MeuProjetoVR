using UnityEngine;

public class BotaoPrincipalView : MonoBehaviour
{
    private Renderer rend;

    private static readonly Color corNormal = Color.white;
    private static readonly Color corHover  = Color.yellow;
    private static readonly Color corAtivo  = Color.green;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void SetHover(bool hover)
    {
        if (rend != null)
            rend.material.color = hover ? corHover : corNormal;
    }

    public void SetAtivo()
    {
        if (rend != null)
            rend.material.color = corAtivo;
    }
}
