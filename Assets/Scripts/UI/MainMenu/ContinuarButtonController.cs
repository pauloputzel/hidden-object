using UnityEngine;
using UnityEngine.UI;

public class ContinuarButtonController : MonoBehaviour
{
    Button m_button;

    void Start()
    {
        m_button = GetComponent<Button>();

    }

    void Update()
    {
        if (GameManager.instance.jogoIniciado)
        {
            m_button.interactable = true;
        }
        else
        {
            m_button.interactable = false;
        }
    }
}
