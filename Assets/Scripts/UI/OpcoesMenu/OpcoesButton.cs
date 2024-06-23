using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpcoesButton : MonoBehaviour
{
    public GameObject opcoesMenuPanel;

    public void AbrirMenuOpcoes()
    {
        GameManager.instance.jogoPausado = true;
        opcoesMenuPanel.SetActive(true);
    }
    public void FecharMenuOpcoes()
    {
        GameManager.instance.jogoPausado = false;
        opcoesMenuPanel.SetActive(false);
    }
}
