using TMPro;
using UnityEngine;

public class GameMessageController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    private float displayTimer;

    void Update()
    {
        if (displayTimer > 0f && displayTimer < Time.time) 
        {
            Destroy(gameObject);
        }
    }

    public void showMessage(string message)
    {
        textMeshPro.text = message;
        displayTimer = Time.time + GameManager.instance.showGameMessageSeconds;
    }

    public void showMessage(string message, float tempoSeg)
    {
        textMeshPro.text = message;
        displayTimer = Time.time + tempoSeg;
    }
}
