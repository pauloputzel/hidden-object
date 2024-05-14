using UnityEngine;
using UnityEngine.UI;

public class MudoController : MonoBehaviour { 

    private Toggle toggle;

    public void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = GameManager.instance.muted;
    }

    public void setMudo(System.Boolean muted)
    {
        GameManager.instance.muted = muted;
    }
}
