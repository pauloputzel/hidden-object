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
        cenarioMenu.scorePlayedText.text = GameManager.instance.levelScore.ToString();
        cenarioMenu.cenarioImage.sprite = levelData.spriteCenario;

        cenarioMenu.continuarButton.onClick.RemoveAllListeners();
        cenarioMenu.continuarButton.onClick.AddListener(() => { GameManager.instance.carregarScene(levelData.nomeLevelScene); });

        if (GameManager.instance.fasesConcluidas < levelData.listaFases.Count)
        {
            cenarioMenu.continuarButton.interactable = true;
        } 
        else
        {
            cenarioMenu.continuarButton.interactable = false;
            cenarioMenu.continuarButton.GetComponentInChildren<TextMeshProUGUI>().text = "NÍVEL CONCLUÍDO";
        }

        cenarioMenu.SetActive(true);
    }
}
