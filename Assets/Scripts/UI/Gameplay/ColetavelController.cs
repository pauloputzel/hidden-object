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
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        Debug.Log(hitInfo.collider.gameObject.name);
        if (hitInfo && gameObject == hitInfo.collider.gameObject)
        {
            GameManager.instance.coletarItem(nome, gameObject);
        }
    }
}
