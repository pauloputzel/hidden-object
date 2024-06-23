using TMPro;
using UnityEngine;

public class LocalController : MonoBehaviour
{
    [SerializeField] public LevelManagerScriptableObject levelData;
    public CenarioMenu cenarioMenu;


    void OnMouseDown()
    {
        if (GameManager.instance.jogoPausado) return;

        cenarioMenu.mapaNomeText.text = levelData.nomeLocal;
        GameManager.instance.levelAtual = levelData.nomeLevelScene;
        cenarioMenu.scoreText.text = levelData.pontuacaoMaxima.ToString();

        cenarioMenu.continuarButton.onClick.RemoveAllListeners();
        cenarioMenu.continuarButton.onClick.AddListener(() => { GameManager.instance.carregarScene(levelData.nomeLevelScene); });
        cenarioMenu.SetActive(true);
    }
}
