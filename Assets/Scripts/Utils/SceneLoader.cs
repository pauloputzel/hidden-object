using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void loadSceneName(string sceneName)
    {
        GameManager.instance.carregarScene(sceneName);
    }
}
