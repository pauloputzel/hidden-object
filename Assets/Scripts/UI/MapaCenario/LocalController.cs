using TMPro;
using UnityEngine;

public class LocalController : MonoBehaviour
{
    [SerializeField] public LevelManagerScriptableObject levelData;
    public CenarioMenu cenarioMenu;

    private SpriteRenderer sprRenderer;
    private MaterialPropertyBlock block;

    private void Start()
    {
        block = new MaterialPropertyBlock();
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        sprRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", new Color(1024, 847, 0, 1));
        block.SetFloat("_Thick", 3f);
        sprRenderer.SetPropertyBlock(block);
    }

    private void OnMouseExit()
    {
        sprRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", new Color(0, 0, 0, 1));
        block.SetFloat("_Thick", 0f);
        sprRenderer.SetPropertyBlock(block);
    }

    private void OnMouseDown()
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
