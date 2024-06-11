using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public LevelManagerScriptableObject levelManagerData;

    public ColetavelPanelController coletavelPanelController;
    public TextMeshProUGUI scoreDisplayText;

    //Tempo base de duração de um nível em segundos
    public float contadorSegundos
    {
        get => levelManagerData.contadorSegundos;
        set => levelManagerData.contadorSegundos = value;
    }

    //Pontuação base de um item coletado
    public float pontoBasePorItem
    {
        get => levelManagerData.pontoBasePorItem;
        set => levelManagerData.pontoBasePorItem = value;

    }

    //Quantidade máxima de coletáveis que serão exibidos
    public int maximoColetavel
    {
        get => levelManagerData.maximoColetavel;
        set => levelManagerData.maximoColetavel = value;

    }

    public float timeLeft
    {
        get => _timeLeft;
        set => _timeLeft = value;
    }

    public float score
    {
        get => _score;
    }

    public List<ColetavelName> listaItensColetaveis
    {
        get => _itensColetaveisList;
    }

    private float _timeLeft;
    private float _score;
    private int faseAtual = 0;
    private List<ColetavelName> _itensColetaveisList = new List<ColetavelName>();
    private List<ColetavelName> itensColetadosList = new List<ColetavelName>();

    public void Start()
    {
        GameManager.instance.setLevelManager(this);
        _itensColetaveisList = levelManagerData.listaFases[faseAtual].listaColetaveis.Take(GameManager.instance.maximoColetavel).ToList();
    }

    public void Update()
    {
        scoreDisplayText.text = _score.ToString();
    }

    public void coletarItem(ColetavelName coletavelNome, GameObject coletavel)
    {

        //encontrando se o item esta entre a lista dos primeiros, Find busca o item para cada x (Coletavel) se o nome é o mesmo
        ColetavelName coletavelEncontrado = _itensColetaveisList.Find(x => x == coletavelNome);

        //Se Find não encontrar o item coletavelEncontrado será "Nenhum"
        if (coletavelEncontrado != ColetavelName.Nenhum)
        {
            //GameManager.instance.showGameMessage($"Parabéns {EnumUtils.GetEnumDescription(coletavelEncontrado)} encontrado!");
            _itensColetaveisList.Remove(coletavelEncontrado);

            itensColetadosList.Add(coletavelEncontrado);

            Destroy(coletavel);

            _score += levelManagerData.pontoBasePorItem;

            coletavelPanelController.criarListaDeItens();
        }

        if (levelManagerData.listaFases[faseAtual].listaColetaveis.Count == 0)
        {
            _score += Mathf.Floor((levelManagerData.contadorSegundos - _timeLeft) * 10000);

            GameManager.instance.SaveLevel(SceneManager.GetActiveScene().name, itensColetadosList, faseAtual, _score);

            GameManager.instance.carregarScene("GameOverScene");
        }
    }
}
