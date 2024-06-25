using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmarButton : MonoBehaviour
{
    public TMP_InputField inputField;

    public void confirmarPersonagem()
    {
        Debug.Log(inputField.text);
        if (inputField.text == "")
        {
            GameManager.instance.showGameMessage("Escreva seu nome para continuar");
            return;
        }

        GameManager.instance.newGame();
        GameManager.instance.nomePersonagem = inputField.text;
        GameManager.instance.jogoIniciado = true;
        GameManager.instance.carregarScene("MapaScene");
    }
}
