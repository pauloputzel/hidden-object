using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float musicaVolume;

    public bool mudo = false;

    public float displayingGameMessageSeconds = 2.3f;

    public int maximoColetavel;

    public List<ColetavelName> listaColetaveis;

    public List<ColetavelName> listaColetados = new List<ColetavelName>();

    private List<ColetavelName> originalListColetaveis;

    private AudioListener mAudioListener;

    private AudioSource mAudioSource;

    private Personagem personagem;

    private GameMessageController gameMessage;

    public void Start()
    {
        if (instance == null)
        {
            mAudioListener = GetComponent<AudioListener>();
            mAudioSource = GetComponent<AudioSource>();
            originalListColetaveis = listaColetaveis;

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setPersonagem(PersonagemName personagemName)
    {
        personagem = new Personagem(personagemName);
    }

    public void setGameMessage(GameMessageController gameMessage)
    {
        this.gameMessage = gameMessage;
        this.gameMessage.displayingSeconds = displayingGameMessageSeconds;
    }

    public void displayGameMessage(string message)
    {
        if (gameMessage) gameMessage.showMessage(message);
    }

    public void carregarMapaPersonagem()
    {
        carregarScene(personagem.getMapaScene());
    }

    public void carregarScene(string nomeScene)
    {
        SceneManager.LoadScene(nomeScene);
    }

    public void resetGame()
    {
        listaColetaveis = originalListColetaveis;
        carregarScene("MenuScene");
    }

    public void setMusicaVolume(float volume)
    {
        musicaVolume = volume;
        mAudioSource.volume = volume;
    }

    public void setMudo(bool mudo)
    {
        this.mudo = mudo;
        mAudioListener.enabled = !mudo;
    }

    public List<ColetavelName> getListaColevateis()
    {
        //Pega os primeiros X itens da lista
        return listaColetaveis.Take(maximoColetavel).ToList();
    }

    public void coletarItem(ColetavelName coletavelNome, GameObject coletavel)
    {
        if (personagem.name == PersonagemName.Todd)
        {
            displayGameMessage("Todd parece não entender porque você está apontando"); 
            return;
        }

        //separando os X primeiros itens da lista de itens para respeitar regra do jogo
        List<ColetavelName> primeirosMaxItens = getListaColevateis();

        //encontrando se o item esta entre a lista dos primeiros, Find busca o item para cada x (Coletavel) se o nome é o mesmo
        ColetavelName coletavelEncontrado = primeirosMaxItens.Find(x => x == coletavelNome);

        //Se Find não encontrar o item coletavelEncontrado será "Nenhum"
        if (coletavelEncontrado == ColetavelName.Nenhum)
        {
            string message = $"Esse item não está na lista de primeiros {maximoColetavel} itens coletaveis";
            Debug.Log(message);
            displayGameMessage(message);

        } else
        {
            string message = $"Parabéns {GetEnumDescription(coletavelEncontrado)} encontrado!";
            Debug.Log(message);
            displayGameMessage(message);
            listaColetaveis.Remove(coletavelEncontrado);
            listaColetados.Add(coletavelEncontrado);
            Destroy(coletavel);
        }

        if (listaColetaveis.Count == 0)
        {
            carregarScene("GameOverScene");
        }
    }

    public static string GetEnumDescription(Enum en)
    {
        Type type = en.GetType();

        MemberInfo[] memInfo = type.GetMember(en.ToString());

        if (memInfo != null && memInfo.Length > 0)
        {
            object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return en.ToString();
    }
}
