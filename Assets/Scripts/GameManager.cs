using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Variável estática (todas as instâncias dessa classe compartilham o mesmo valor)
    //Usada para armazenar a instância atual de GameManager
    //Permitindo acesso em outras classes apenas usando GameManager.instance
    public static GameManager instance;
    public float contador_timer = 90;
    public float pontoporitem = 5000;

    //Estas são as propriedades do GameManager
    //Propriedades funcionam como atalhos para acessar e manipular informações
    //Também permitem a execução de outros códigos ao buscar ou alterar a propriedade
    public string nomePersonagem
    {
        //ao usar GameManager.instance.nomePersonagem é retornado o nome do personagem armazenado na estrutura de dados
        get => saveGameManager.playerData.name;
        //ao usar GameManager.instance.nomePersonagem = "Fulano" é armazenado o nome do personagem e salvo em arquivo
        set
        {
            saveGameManager.playerData.name = value;
            saveGameManager.SaveGame();
        }
    }

    //Propriedade jogoIniciado que define quando um jogo já foi salvo com informações relevantes para continuar a gameplay
    public bool jogoIniciado
    {
        get => saveGameManager.playerData.jogoIniciado;
        set
        {
            saveGameManager.playerData.jogoIniciado = value;
            saveGameManager.SaveGame();
        }
    }

    //Propriedade de nível de música alterado pelo painel de Opções
    public float musicaVolume
    {
        get => saveGameManager.playerData.musicVolume;
        set
        {
            //altera a propriedade volume do Componente AudioSource do GameManager
            mAudioSource.volume = value;
            //altera o valor na estrutura de dados do SaveGame
            saveGameManager.playerData.musicVolume = value;
            //executa o método SaveGame salvando o jogo em arquivo
            saveGameManager.SaveGame();
        }
    }

    //Propriedade muted que altera a existência de som no jogo
    public bool muted
    {
        get => saveGameManager.playerData.muted;
        set
        {
            //A exclamação antes de value inverte a condição do bool que está recebendo
            //Se GameManager.instance.muted = true
            //então a propriedade enabled do componente de escuta recebe o oposto (false)
            mAudioListener.enabled = !value;
            //armazena o valor na estrutura de dados e salva
            saveGameManager.playerData.muted = value;
            saveGameManager.SaveGame();
        }
    } 

    //Tempo de exibição de mensagem de jogo em segundos 
    public float showGameMessageSeconds = 2.3f;

    //Quantidade máxima de coletáveis que serão exibidos
    public int maximoColetavel;

    //Componente do Gameobject GameManager para controle do muted
    private AudioListener mAudioListener;

    //Componente do Gameobject GameManager para controle de volume da música
    private AudioSource mAudioSource;

    //Controle do GameObject GameMessage que foi pré adicionado na scene atual
    //O GameMessage faz seu registro nesta propriedade 
    private GameMessageController gameMessage;

    //Classe responsável por salvar o jogo
    //O próprio GameManager cria um objeto dessa classe ao iniciar
    private SaveGameManager saveGameManager;

    //Gerenciador de níveis
    //Ao iniciar uma scene onde exista um GameObject LevelManager
    //este irá armazenar sua referência aqui
    private LevelManager levelManager;

    //Ao iniciar um GameObject adicionado na scene
    public void Start()
    {
        //se ainda não foi registrado nenhum GameManager
        //então esse seria o primeiro a ser adicionado
        if (instance == null)
        {
            //assume o controle do jogo e incia configuração

            //registra componente de escuta de Audio no jogo
            mAudioListener = GetComponent<AudioListener>();

            //registra componente de Musica do jogo
            mAudioSource = GetComponent<AudioSource>();

            //inicia um novo Gerenciador de SaveGame
            //o Gerenciador já busca pelo jogo salvo ao ser iniciado
            saveGameManager = new SaveGameManager();

            //ajusta o volume do componente conforme salvo em arquivo
            mAudioSource.volume = saveGameManager.playerData.musicVolume;

            //muta ou desmuta o jogo conforme salvo em arquivo
            mAudioListener.enabled = !saveGameManager.playerData.muted;

            //armaezena esse próprio primeiro GameManager na variável instance assumindo o controle do jogo
            instance = this;

            //Configura esse GameObject para não ser destruído ao trocar de scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Só é possível existir UM GameManager no jogo
            //Caso outro GameManager seja adicionado 
            //já vai existir outro GameManager adicionado na variável instance
            //Portanto qualquer tentativa novo registro resulta em remoção desse objeto
            Destroy(gameObject);
        }
    }

    //método de registro de LevelManager que é executado no OnStart do LevelManager em cada scene
    public void setLevelManager(LevelManager novoLevelManager)
    {
        levelManager = novoLevelManager;
    }

    //método para coletar um item
    //dessa forma podemos adicionar regras de jogo ao coletar qualquer item no jogo todo
    public void coletarItem(ColetavelName coletavelNome, GameObject coletavel)
    {
        if (levelManager) levelManager.coletarItem(coletavelNome, coletavel);
    }

    public bool itemEstaNaListaDeColetaveis(ColetavelName nomeColetavel)
    {
        return getLevelProximosColetaveisList().Contains(nomeColetavel);
    }

    //retorna a lista dos N máximos itens que o jogador pode coletar no momento
    public List<ColetavelName> getLevelProximosColetaveisList()
    {
        return levelManager ? levelManager.getLevelProximosColetaveisList() : new List<ColetavelName>();
    }
    public void LevelTimerUpdate(float time)
    {
        levelManager.timer_left = time;
    }
    public void LevelTimeOut()
    {
       carregarScene("GameOverScene");
    }

    public void SaveLevel(string nome, float score)
    {
        LevelData levelData = saveGameManager.playerData.levelDataList.Find(x => x.name == nome);

        if (levelData == null)
        {
            levelData = new LevelData();
            levelData.name = nome;
            saveGameManager.playerData.levelDataList.Add(levelData);
        }
        
        levelData.score += score;

        saveGameManager.SaveGame();
    }

    public float GetLevelScore()
    {
        return levelManager.score;
    }
    public void saveGame()
    {
        saveGameManager.SaveGame();
    }

    public void setGameMessageController(GameMessageController gameMessage)
    {
        this.gameMessage = gameMessage;
        this.gameMessage.displaySeconds = showGameMessageSeconds;
    }

    public void showGameMessage(string message)
    {
        if (gameMessage) gameMessage.showMessage(message);
    }

    public void carregarScene(string nomeScene)
    {
        SceneManager.LoadScene(nomeScene);
    }

    public void resetGame()
    {
        carregarScene("MenuScene");
    }
}
