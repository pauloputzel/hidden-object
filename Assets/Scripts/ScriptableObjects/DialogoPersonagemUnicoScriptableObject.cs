using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

[System.Serializable]
public class Dialogo
{
    [TextArea(3, 8)]
    public string texto;
}

[CreateAssetMenu(fileName = "DialogoPersonagemUnico", menuName = "ScriptableObjects/DialogoPersonagemUnico")]
public class DialogoPersonagemUnicoScriptableObject : ScriptableObject
{
    [SerializeField]
    public Sprite personagemSprite;

    [SerializeField]
    public List<Dialogo> listaDialogo = new List<Dialogo>();
}
