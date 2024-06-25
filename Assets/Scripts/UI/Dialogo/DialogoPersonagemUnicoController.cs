using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class DialogoPersonagemUnicoController : MonoBehaviour
{
    [SerializeField]
    private DialogoPersonagemUnicoScriptableObject dialogoData;

    [SerializeField]
    private ButtonClickedEvent acoesFinalDialogo = new ButtonClickedEvent();

    [SerializeField]
    private Image personagemImage;

    [SerializeField]
    private GameObject dialogoPanel;

    [SerializeField]
    private TextMeshProUGUI dialogoText;

    [SerializeField]
    private Button continuarButton;

    [SerializeField]
    private DialogWriter dialogWriter;

    private bool escritaIniciada = false;

    private int dialogoAtual = 0;

    void Start()
    {
        dialogoPanel.SetActive(true);
        personagemImage.gameObject.SetActive(true);
        personagemImage.sprite = dialogoData.personagemSprite;
        continuarButton.onClick.RemoveAllListeners();
        continuarButton.onClick.AddListener(() =>
        {
            if (escritaIniciada && !dialogWriter.escreveuTudo)
            {
                dialogWriter.ResumeDialog();
                escritaIniciada = false;
            } else
            {
                CarregarProximoDialogo();
            }
        });

        CarregarProximoDialogo();
    }

    private void CarregarProximoDialogo()
    {
        if (dialogoAtual > dialogoData.listaDialogo.Count - 1)
        {
            FinalizarDialogo();
        }
        else
        {
            Dialogo dialogo = dialogoData.listaDialogo[dialogoAtual];
            dialogoAtual++;
            escritaIniciada = true;
            dialogWriter.StartDialogWriter(dialogo.texto);
        }
    }

    private void FinalizarDialogo()
    {
        acoesFinalDialogo.Invoke();
        Destroy(gameObject);
    }
}
