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
    public List<Fase> listaFases = new List<Fase>();

    //Tempo base de duração de um nível em segundos
    public float contadorSegundos = 90;

    //Quantidade máxima de coletáveis que serão exibidos
    public int maximoColetavel;

    //Pontuação base de um item coletado
    public float pontoBasePorItem = 5000;

    //Quantidade de itens necessários coletar para encher o combo
    public int quantidadeItensParaComboCheio = 3;

    public GameObject textoAnimadoPontosColetadosPrefab;
}
