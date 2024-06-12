using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class DialogWriter : MonoBehaviour
{
    private float writeLetterSeconds = 0.06f;
    private AudioSource m_AudioSource;
    private TextMeshProUGUI m_TextMeshProUGUI;
    private string fullText;
    private int charCount;
    private float timer;

    private Dictionary<string, string> replaces = new Dictionary<string, string>
    {   //Variáveis do texto devem ser escritas no Text com chaves ex {JOGADOR_NOME}
        {"JOGADOR_NOME", GameManager.instance.nomePersonagem},
        {"SCORE_TOTAL", GameManager.instance.scoreTotal},
    };

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        fullText = StringReplacer.Replace(m_TextMeshProUGUI.text, replaces);
        StartDialogWriter();
    }

    void OnEnable()
    {
       StartDialogWriter();
    }

    void Update()
    {
        if (charCount < fullText.Length && (timer < (Time.time)))
        {
            m_TextMeshProUGUI.text += fullText[charCount++];
            timer = Time.time + writeLetterSeconds;
        }
    }

    private void StartDialogWriter()
    {
        m_AudioSource.Play();
        m_TextMeshProUGUI.text = "";
        timer = Time.time + writeLetterSeconds;
        charCount = 0;
    }
}
