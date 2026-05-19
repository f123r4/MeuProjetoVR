using UnityEngine;

public class ObjetoColetavelView : MonoBehaviour
{
    [SerializeField] private float velocidadeRotacao = 90f;

    private void Update()
    {
        transform.Rotate(Vector3.up, velocidadeRotacao * Time.deltaTime, Space.World);
    }

    public void Coletar()
    {
        gameObject.SetActive(false);
    }
}
