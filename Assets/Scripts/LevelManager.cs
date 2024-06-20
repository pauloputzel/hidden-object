using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelManagerScriptableObject levelManagerData;

    public ColetavelPanelController coletavelPanelController;
    public TextMeshProUGUI scoreDisplayText;
    public ComboProgressBar comboProgressBar;
    public GameObject canvas;
    public DetalhesColetavelPanelController detalhesColetavelPanel;

    public float contadorSegundos                           //Tempo base de duração de um nível em segundos
    {
        get => levelManagerData.contadorSegundos;
        set => levelManagerData.contadorSegundos = value;
    }

    public float pontoBasePorItem                           //Pontuação base de um item coletado
    {
        get => levelManagerData.pontoBasePorItem;
        set => levelManagerData.pontoBasePorItem = value;

    }

    public int maximoColetavel                              //Quantidade máxima de coletáveis que serão exibidos
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
    private int qtdItensColetadosSemErrar = 0;
    private bool comboCheio = false;
    private List<ColetavelName> _itensColetaveisList = new List<ColetavelName>();
    private List<ColetavelName> itensColetadosList = new List<ColetavelName>();

    public void Start()
    {
        GameManager.instance.setLevelManager(this);
        faseAtual = GameManager.instance.faseAtual;
        _itensColetaveisList = levelManagerData.listaFases[faseAtual].listaColetaveis.Take(GameManager.instance.maximoColetavel).ToList();
        GameManager.instance.jogoPausado = false;
    }

    public void Update()
    {
        scoreDisplayText.text = _score.ToString();
    }

    public void coletarItem(ColetavelName coletavelName, GameObject coletavel)
    {
        ColetavelName coletavelEncontrado = _itensColetaveisList.Find(x => x == coletavelName); //verificando se o item esta entre a lista dos primeiros, Find busca o item para cada x (Coletavel) se o nome é o mesmo

        if (coletavelEncontrado == ColetavelName.Nenhum) {
            EsvaziarCombo();
            return; 
        }

        EncherCombo();
        IniciarAnimacaoDeColeta(coletavel, coletavelEncontrado);
        CalcularScoreEMostrarTextPontosColetados();

        if (_itensColetaveisList.Count == 0)
        {
            _score += Mathf.Floor((levelManagerData.contadorSegundos - _timeLeft) * 10000);
            GameManager.instance.SaveLevel(SceneManager.GetActiveScene().name, itensColetadosList, faseAtual, _score);
            GameManager.instance.carregarScene("GameOverScene");
        }
    }

    private void EsvaziarCombo()
    {
        comboCheio = false;
        qtdItensColetadosSemErrar = 0;
        comboProgressBar.SetEmpty();
    }

    private void EncherCombo()
    {
        qtdItensColetadosSemErrar++;

        if (qtdItensColetadosSemErrar > levelManagerData.quantidadeItensParaComboCheio)
        {
            comboCheio = true;
        }

        int qtd = Mathf.Min(qtdItensColetadosSemErrar, levelManagerData.quantidadeItensParaComboCheio);
        float percent = ((float) qtd / (float) levelManagerData.quantidadeItensParaComboCheio) * 100f;
        comboProgressBar.UpdateValue(percent);
    }

    private void IniciarAnimacaoDeColeta(GameObject coletavel, ColetavelName coletavelEncontrado)
    {
        GameObject textoNaLista = coletavelPanelController.encontrarTextDaLista(coletavelEncontrado);
        Image coletavelImage = detalhesColetavelPanel.coletavelImage;

        coletavel.transform.SetParent(canvas.transform);
        coletavel.GetComponent<SpriteRenderer>().sortingLayerName = "Coletando";

        coletavel.GetComponent<ColetavelController>().ExecutarAnimacao(detalhesColetavelPanel, coletavelImage.transform, textoNaLista.transform, () => {
            _itensColetaveisList.Remove(coletavelEncontrado); //removendo o objeto coletado da lista de coletáveis da fase
            itensColetadosList.Add(coletavelEncontrado); //adiciona o item coletado na lista de itens coletados na fase
            //coletavelPanelController.criarListaDeItens();

            textoNaLista.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
            Destroy(coletavel);
        });
    }

    private void CalcularScoreEMostrarTextPontosColetados()
    {
        //Calcular Score
        float scoreItemColetado = levelManagerData.pontoBasePorItem;
        if (comboCheio) scoreItemColetado *= 2;
        _score += scoreItemColetado;
        levelManagerData.textoAnimadoPontosColetadosPrefab.GetComponent<TextMeshProUGUI>().text = scoreItemColetado.ToString();

        //Mostrar Pontos na Tela
        GameObject textoPontosColetados = Instantiate(levelManagerData.textoAnimadoPontosColetadosPrefab, canvas.transform);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 100f;
        textoPontosColetados.GetComponent<RectTransform>().position = worldPosition;
    }
}
