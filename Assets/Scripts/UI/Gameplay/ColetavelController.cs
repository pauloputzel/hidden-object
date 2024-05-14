using UnityEngine;

public class ColetavelController : MonoBehaviour
{
    public ColetavelName nome;

    private void Start()
    {
        //if (!GameManager.instance.itemEstaNaListaDeColetaveis(nome)) Destroy(gameObject);
    }

    void OnMouseDown()
    {
        GameManager.instance.coletarItem(nome, gameObject);
    }
}
