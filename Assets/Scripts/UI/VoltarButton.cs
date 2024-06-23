using UnityEngine;

public class VoltarButton : MonoBehaviour
{
    public void VoltarParaScene(string sceneName)
    {
        GameManager.instance.jogoPausado = true;
        GameManager.instance.carregarScene(sceneName);
    }
}
