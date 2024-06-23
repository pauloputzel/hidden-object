using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CenarioMenu : MonoBehaviour
{
    public TextMeshProUGUI mapaNomeText;
    public TextMeshProUGUI scoreText;
    public Button continuarButton;

    private Button fecharButton;
    private float timeToFechar = 0.2f;
    private bool abrindo = false;
    private float abrindoTime = 0f;

    private void Start()
    {
        fecharButton = GetComponent<Button>();
        fecharButton.enabled = false;
        abrindoTime = Time.time;
        abrindo = true;
    }

    private void Update()
    {
        if (abrindo && abrindoTime + timeToFechar < Time.time)
        {
            fecharButton.enabled = true;
        }
    }

    public void SetActive(bool active)
    {
        GameManager.instance.jogoPausado = active;
        abrindoTime = Time.time;
        abrindo = active;
        if (active)
        {
            gameObject.SetActive(true);
            if (fecharButton) fecharButton.enabled = false;
        }
        else
        {
            fecharButton.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
