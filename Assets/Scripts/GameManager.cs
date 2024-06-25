using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameManagerScriptableObject gameManagerData;
    public GameObject gameMessagePrefab;
    public AudioSource SomColeta;

    public bool jogoPausado
    {
        get => _jogoPausado;
        set => _jogoPausado = value;
    }

    public float contadorTimerSegundos
    {
        get => levelManager ? levelManager.contadorSegundos : 90f;
        set { 
            if (levelManager) levelManager.contadorSegundos = value; 
        }
    }

    public float pontoBasePorItem
    {
        get => levelManager ? levelManager.pontoBasePorItem : 5000f;
    }

    public int maximoColetavel
    {
        get => levelManager ? levelManager.maximoColetavel : 9;
    }

    public float tempoDuracaoColetaItemSegundos
    {
        get => gameManagerData.tempoDuracaoColetaItemSegundos;
    }

    public List<ColetavelName> listaItensColetaveis
    {
        get => levelManager ? levelManager.listaItensColetaveis : null;
    }

    public float tempoMinimoLoadSegundos
    {
        get => gameManagerData.tempoMinimoLoadSegundos;
        set => gameManagerData.tempoMinimoLoadSegundos = value;
    }

    public float showGameMessageSeconds
    {
        get => gameManagerData.showGameMessageSeconds;
        set => gameManagerData.showGameMessageSeconds = value;
    }

    public string nomePersonagem
    {
        get => saveGameManager.playerData.name;         
        set
        {
            saveGameManager.playerData.name = value;
            saveGameManager.SaveGame();
        }
    }

    public bool primeiraFaseJogada
    {
        get
        {
            if (saveGameManager.playerData.levelDataList.Count > 0)
            {
                int qtdFases = 0;
                saveGameManager.playerData.levelDataList.ForEach(x => qtdFases += x.faseDataList.Count);
                return qtdFases > 0;
            }
            else
            {
                return false;
            }
        }
    }

    public bool segundaFaseJogada
    {
        get
        {
            if (saveGameManager.playerData.levelDataList.Count > 0)
            {
                int qtdFases = 0;
                saveGameManager.playerData.levelDataList.ForEach(x => qtdFases += x.faseDataList.Count);
                return qtdFases > 1;
            }
            else
            {
                return false;
            }
        }
    }

    public string scoreTotal
    {
        get
        {
            float scoreT = 0f;
            saveGameManager.playerData.levelDataList.ForEach(x => x.faseDataList.ForEach(y => scoreT += y.score ));
            return scoreT.ToString();
        }
    }

    public string levelAtual
    {
        get => _levelAtual;
        set
        {
            LevelData levelSelecionado = saveGameManager.playerData.levelDataList.Find(x => x.name == value);
            _faseAtual = levelSelecionado != null ? levelSelecionado.ultimaFaseConcluida : 0;
            _levelAtual = value;
        }
    }

    public int fasesConcluidas
    {
        get
        {
            LevelData levelSelecionado = saveGameManager.playerData.levelDataList.Find(x => x.name == _levelAtual);
            if (levelSelecionado != null) return levelSelecionado.faseDataList.Count;
            return 0;
        }
    }

    public int ultimaFaseConcluida
    {
        get
        {
            LevelData levelSelecionado = saveGameManager.playerData.levelDataList.Find(x => x.name == _levelAtual);
            if (levelSelecionado != null) return levelSelecionado.ultimaFaseConcluida;
            return 0;
        }
    }

    public float levelScore
    {
        get
        {
            LevelData levelSelecionado = saveGameManager.playerData.levelDataList.Find(x => x.name == _levelAtual);
            float scoreLevelTotal = 0f;
            if (levelSelecionado != null) levelSelecionado.faseDataList.ForEach(y => scoreLevelTotal += y.score);
            return scoreLevelTotal;
        }
    }

    public float faseScore
    {
        get
        {
            LevelData levelSelecionado = saveGameManager.playerData.levelDataList.Find(x => x.name == _levelAtual);
            if (levelSelecionado == null) return 0f;
            if (levelSelecionado.faseDataList.ElementAtOrDefault(_faseAtual) != null)
            {
                return levelSelecionado.faseDataList[_faseAtual].score;
            }

            return 0f;
        }
    }

    public int faseAtual
    {
        get => _faseAtual;
    }

    public String startDate
    {
        get => saveGameManager.playerData.startDate.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public float writeLetterSeconds
    {
        get => gameManagerData.escreverLetraDeDialogoACadaXSegundos;
    }
    
    public bool jogoIniciado                                //Propriedade jogoIniciado que define quando um jogo já foi salvo com informações relevantes para continuar a gameplay
    {
        get => saveGameManager.playerData.jogoIniciado;
        set
        {
            saveGameManager.playerData.jogoIniciado = value;
            saveGameManager.SaveGame();
        }
    }
    
    public float musicaVolume                               //Propriedade de nível de música alterado pelo painel de Opções
    {
        get => saveGameManager.playerData.musicVolume;
        set
        {
            mAudioSource.volume = value;                    //altera a propriedade volume do Componente AudioSource do GameManager
            saveGameManager.playerData.musicVolume = value; //altera o valor na estrutura de dados do SaveGame
            saveGameManager.SaveGame();                     //executa o método SaveGame salvando o jogo em arquivo
        }
    }
    
    public bool muted                                       //Propriedade muted que altera a existência de som no jogo
    {
        get => saveGameManager.playerData.muted;
        set
        {
            AudioListener.volume = value ? 0 : 1;                //A exclamação antes de value inverte a condição do bool que está recebendo Se GameManager.instance.muted = true então a propriedade enabled do componente de escuta recebe o oposto (false)
            saveGameManager.playerData.muted = value;       //armazena o valor na estrutura de dados e salva
            saveGameManager.SaveGame();
        }
    }

    public string nextSceneToLoad
    {
        get => sceneNameToLoad;
    }
    
    private AudioSource mAudioSource;                       //Componente do Gameobject GameManager para controle de volume da música
    private SaveGameManager saveGameManager;                //Classe responsável por salvar o jogo o próprio GameManager cria um objeto dessa classe ao iniciar
    private LevelManager levelManager;                      //Gerenciador de níveis Ao iniciar uma scene onde exista um GameObject LevelManager este irá armazenar sua referência aqui
    private string sceneNameToLoad = "MenuScene";
    private string _levelAtual = "";
    private int _faseAtual = 0;
    private bool _jogoPausado = false;
    private AudioClip somGeral;

    public void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        mAudioSource = GetComponent<AudioSource>();                      //registra componente de Musica do jogo
        saveGameManager = new SaveGameManager();                         //inicia um novo Gerenciador de SaveGame o Gerenciador já busca pelo jogo salvo ao ser iniciado
        mAudioSource.volume = saveGameManager.playerData.musicVolume;    //ajusta o volume do componente conforme salvo em arquivo
        muted = saveGameManager.playerData.muted;                        //muta ou desmuta o jogo conforme salvo em arquivo
        somGeral = mAudioSource.clip;
        instance = this;                                                 //armaezena esse próprio primeiro GameManager na variável instance assumindo o controle do jogo
        DontDestroyOnLoad(gameObject);                                   //Configura esse GameObject para não ser destruído ao trocar de scene
    }

    public void coletarItem(ColetavelName coletavelNome, GameObject coletavel)
    {
        if (!jogoPausado && levelManager) levelManager.coletarItem(coletavelNome, coletavel);
    }

    public void tocarSomColeta()
    {
        SomColeta.Play();
    }

     public void trocaTrilha(AudioClip audioclip)
    {
        mAudioSource.clip = audioclip;
        mAudioSource.Play();
    }

     public void tocarTrilhaPadrao()
    {
        mAudioSource.clip = somGeral;
        mAudioSource.Play();
    }

    public bool itemEstaNaListaDeColetaveis(ColetavelName nomeColetavel)
    {
        if (saveGameManager.playerData.levelDataList.Count == 0) return false;

        return saveGameManager.playerData.levelDataList.Exists(x => x.faseDataList.Exists(x => x.itensColetados.Contains(nomeColetavel)));
    }

    public void setLevelManager(LevelManager novoLevelManager)
    {
        levelManager = novoLevelManager;

    }

    public void LevelTimerUpdate(float time)
    {
        levelManager.timeLeft = time;
    }

    public void LevelTimeOut()
    {
        jogoPausado = true;
        levelManager.timeoutDisplayController.scoreDisplay.text = $"SCORE: {levelManager.score}";
        levelManager.timeoutDisplayController.continuarButton.onClick.AddListener(() => {
            jogoPausado = false;
            tocarTrilhaPadrao();
            carregarScene("MapaScene");
        });
        levelManager.timeoutDisplayController.gameObject.SetActive(true);
    }

    public void SaveLevel(string nome, List<ColetavelName> itensColetados, int faseAtual, float score)
    {
        LevelData levelData = saveGameManager.playerData.levelDataList.Find(x => x.name == nome);

        if (levelData == null)
        {
            levelData = new LevelData();
            levelData.name = nome;
        }

        FaseData fase = new FaseData();
        fase.name = $"{nome}_{faseAtual}";
        fase.itensColetados = itensColetados;
        fase.score = score;

        levelData.faseDataList.Add(fase);
        levelData.ultimaFaseConcluida = faseAtual + 1;
        saveGameManager.playerData.levelDataList.Add(levelData);

        saveGameManager.SaveGame();
    }

    public void newGame()
    {
        saveGameManager.NewGame();
    }

    public void saveGame()
    {
        saveGameManager.SaveGame();
    }

    public void showGameMessage(string message)
    {
        GameMessageController gameMsgCtl = Instantiate(gameMessagePrefab).GetComponent<GameMessageController>();
        gameMsgCtl.showMessage(message);
    }

    public void showGameMessage(string message, float tempoSeg, Transform canvasTransform, RectTransform rect)
    {
        GameMessageController gameMsgCtl = Instantiate(gameMessagePrefab, canvasTransform).GetComponent<GameMessageController>();
        gameMsgCtl.GetComponent<RectTransform>().position = rect.position;
        gameMsgCtl.showMessage(message, tempoSeg);
    }

    public void carregarScene(string nomeScene)
    {
        sceneNameToLoad = nomeScene;
        SceneManager.LoadScene("LoadScene");
    }

    public void resetGame()
    {
        carregarScene("MenuScene");
    }
}
