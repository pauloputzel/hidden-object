using TMPro;
using UnityEngine;

public class GameMessageController : MonoBehaviour
{
    public float displayingSeconds = 0.3f;

    private TextMeshProUGUI textMeshPro;

    private float displayTimer;

    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        GameManager.instance.setGameMessage(this);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (displayTimer > 0f && displayTimer < Time.time) 
        {
            displayTimer = 0f;
            gameObject.SetActive(false);
            textMeshPro.text = "";
        }
    }

    public void showMessage(string message)
    {
        textMeshPro.text = message;
        displayTimer = Time.time + displayingSeconds;
        gameObject.SetActive(true);
    }
}
