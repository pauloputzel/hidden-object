using TMPro;
using UnityEngine;

public class LocalController : MonoBehaviour
{
    public string nomeLevelScene;
    public string nomeLocal;
    public CenarioMenu cenarioMenu;


    void OnMouseDown()
    {
        if (GameManager.instance.jogoPausado) return;

        cenarioMenu.mapaNomeText.text = nomeLocal;
        GameManager.instance.levelAtual = nomeLevelScene;
        cenarioMenu.fasesConcluidasText.text = $"FASES CONCLUÍDAS: {GameManager.instance.ultimaFaseConcluida}";
        cenarioMenu.scoreText.text = $"SCORE TOTAL: {GameManager.instance.levelScore}";

        cenarioMenu.continuarButton.onClick.RemoveAllListeners();
        cenarioMenu.continuarButton.onClick.AddListener(() => { GameManager.instance.carregarScene(nomeLevelScene); });
        cenarioMenu.SetActive(true);
    }
}
