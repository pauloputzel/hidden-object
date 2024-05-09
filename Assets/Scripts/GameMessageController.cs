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
            hidePanel();
        }
    }

    public void showMessage(string message)
    {
        displayTimer = Time.time + displayingSeconds;
        textMeshPro.text = message;
        gameObject.SetActive(true);
    }

    private void hidePanel()
    {
        displayTimer = 0f;
        textMeshPro.text = "";
        gameObject.SetActive(false);
    }
}
