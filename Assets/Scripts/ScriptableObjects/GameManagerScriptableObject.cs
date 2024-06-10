using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "ScriptableObjects/GameManagerData")]
public class GameManagerScriptableObject : ScriptableObject
{
    //Tempo de exibição de load de jogo em segundos 
    public float tempoMinimoLoadSegundos = 1f;

    //Tempo de exibição de mensagem de jogo em segundos 
    public float showGameMessageSeconds = 2.3f;

}
