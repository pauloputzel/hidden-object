using UnityEngine;

public class LocalController : MonoBehaviour
{
    public string nomeScene;


    void OnMouseDown()
    {
        GameManager.instance.levelAtual = nomeScene;

        GameManager.instance.carregarScene(nomeScene);
    }
}
