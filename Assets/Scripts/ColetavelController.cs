using UnityEngine;

public class ColetavelController : MonoBehaviour
{
    public ColetavelName nome;

    private void Start()
    {
        if (!GameManager.instance.listaColetaveis.Contains(nome)) Destroy(gameObject);
    }

    void OnMouseDown()
    {
        GameManager.instance.coletarItem(nome, gameObject);
    }
}
