using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer_controller : MonoBehaviour
{
    private float timer_segundos;
    private TextMeshProUGUI textmesh;
    void Start()
    {
        timer_segundos = GameManager.instance.contador_timer;
        textmesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer_segundos -= Time.deltaTime;

        if (timer_segundos < 0)
        {
            GameManager.instance.LevelTimeOut();
        }
        else
        {
            float timeaux = timer_segundos / 60;
            float minutos = Mathf.Floor(timeaux);
            float segundos = ((timeaux - minutos) * 60);
            textmesh.text = $"{minutos:00}:{segundos:00}";
            GameManager.instance.LevelTimerUpdate(timer_segundos);
        }

    }
}
