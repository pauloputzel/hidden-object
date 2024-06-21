using TMPro;
using UnityEngine;

public class TimerCounter : MonoBehaviour
{
    private float timerSegundos;
    private TextMeshProUGUI textmesh;
    void Start()
    {
        timerSegundos = GameManager.instance.contadorTimerSegundos;
        textmesh = GetComponent<TextMeshProUGUI>();
        updateTextValue();
    }

    void Update()
    {
        if (GameManager.instance.jogoPausado) return;

        timerSegundos -= Time.deltaTime;

        if (timerSegundos < 0)
        {
            GameManager.instance.LevelTimeOut();
        } else
        {
            updateTextValue();
            GameManager.instance.LevelTimerUpdate(timerSegundos);
        }
    }

    private void updateTextValue()
    {
        float timeaux = timerSegundos / 60;
        float minutos = Mathf.Floor(timeaux);
        float segundos = ((timeaux - minutos) * 60);
        textmesh.text = $"{minutos:00}:{segundos:00}";
    }
}
