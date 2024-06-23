using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mensagem
{
    [TextArea(3,8)]
    public string texto = "";
}

[CreateAssetMenu(fileName = "GameMessageLoading", menuName = "ScriptableObjects/GameMessageLoading")]
public class GameMessageLoadingScriptableObject : ScriptableObject
{
    [SerializeField]
    public float segundosEntreMensagens = 1f;

    [SerializeField]
    public List<Mensagem> listaMensagens = new List<Mensagem>();
}
