using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalController : MonoBehaviour
{
    public string nomeScene;

    void OnMouseDown()
    {
        GameManager.instance.carregarScene(nomeScene);
    }
}
