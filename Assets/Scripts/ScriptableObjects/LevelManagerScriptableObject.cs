using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fase
{
    public string nome = "Fase";
    public List<ColetavelName> listaColetaveis;
}

[CreateAssetMenu(fileName = "LevelManagerData", menuName = "ScriptableObjects/LevelManagerData")]
public class LevelManagerScriptableObject : ScriptableObject
{
    [SerializeField]
    public string nomeLevelScene;

    [SerializeField]
    public string nomeLocal;

    [SerializeField]
    public Sprite spriteCenario;

    [SerializeField]
    public float contadorSegundos = 90;

    [SerializeField]
    public int maximoColetavel;

    [SerializeField]
    public float pontoBasePorItem = 5000;

    [SerializeField]
    public int quantidadeItensParaComboCheio = 3;

    [SerializeField]
    public GameObject textoAnimadoPontosColetadosPrefab;

    [SerializeField]
    public AudioClip trilhaSonora;

    [SerializeField]
    public List<Fase> listaFases = new List<Fase>();

    public int pontuacaoMaxima 
    {
        get 
        {
            float scorePotencial = 0f;
            listaFases.ForEach(x => scorePotencial += x.listaColetaveis.Count * pontoBasePorItem);

            scorePotencial += listaFases.Count * Mathf.Floor((contadorSegundos) * 10000);

            return Mathf.CeilToInt(scorePotencial);
        }
    }
}
