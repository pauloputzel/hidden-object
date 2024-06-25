using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComoJogarButton : MonoBehaviour
{
    public GameObject ComoJogarMenuPanel;

    public void AbrirComoJogarMenuPanel()
    {
        GameManager.instance.jogoPausado = true;
        ComoJogarMenuPanel.SetActive(true);
    }
    public void FecharComoJogarMenuPanel()
    {
        GameManager.instance.jogoPausado = false;
        ComoJogarMenuPanel.SetActive(false);
    }
}
