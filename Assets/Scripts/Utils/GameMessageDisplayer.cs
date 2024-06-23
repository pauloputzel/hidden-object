using UnityEngine;

public class GameMessageDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameMessageLoadingScriptableObject gameMessageLoadingData;

    [SerializeField]
    private Transform canvasTransform;

    private float timeUltimaMensagem = 0f;
    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        if (GameManager.instance) EscreveMensagemInformativa();
    }

    void Update()
    {
        if (GameManager.instance)
        {
            if (timeUltimaMensagem < Time.time - gameMessageLoadingData.segundosEntreMensagens)
            {
                EscreveMensagemInformativa();
            }
        }
    }

    private void EscreveMensagemInformativa()
    {
        int randMsgPos = Mathf.FloorToInt(Random.Range(0, gameMessageLoadingData.listaMensagens.Count -1));
        Mensagem mensagem = gameMessageLoadingData.listaMensagens[randMsgPos];
        GameManager.instance.showGameMessage(mensagem.texto, gameMessageLoadingData.segundosEntreMensagens, canvasTransform, rect);
        timeUltimaMensagem = Time.time;
    }
}
