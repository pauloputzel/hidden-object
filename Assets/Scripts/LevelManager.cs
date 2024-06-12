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
    public ComboProgressBar comboProgressBar;

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
    private int qtdItensColetadosSemErrar = 0;
    private bool comboCheio = false;
    private List<ColetavelName> _itensColetaveisList = new List<ColetavelName>();
    private List<ColetavelName> itensColetadosList = new List<ColetavelName>();

    public void Start()
    {
        GameManager.instance.setLevelManager(this);
        //faseAtual = GameManager.instance.faseAtual;
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
            EncherCombo();
            //removendo o objeto coletado da lista de coletáveis da fase
            _itensColetaveisList.Remove(coletavelEncontrado);

            //adiciona o item coletado na lista de itens coletados na fase
            itensColetadosList.Add(coletavelEncontrado);

            Destroy(coletavel);

            //aqui determinamos o score para cada item coletado
            _score += levelManagerData.pontoBasePorItem;

            coletavelPanelController.criarListaDeItens();

        } else
        {
            EsvaziarCombo();
        }

        if (_itensColetaveisList.Count == 0)
        {
            _score += Mathf.Floor((levelManagerData.contadorSegundos - _timeLeft) * 10000);

            GameManager.instance.SaveLevel(SceneManager.GetActiveScene().name, itensColetadosList, faseAtual, _score);

            GameManager.instance.carregarScene("GameOverScene");
        }
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

    private void EsvaziarCombo()
    {
        comboCheio = false;
        qtdItensColetadosSemErrar = 0;
        comboProgressBar.SetEmpty();
    }
}
