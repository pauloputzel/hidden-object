using UnityEngine;
using UnityEngine.UI;

public class MudoController : MonoBehaviour { 

    private Toggle toggle;

    public void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = GameManager.instance.mudo;
    }

    public void setMudo(System.Boolean mudo)
    { 
        GameManager.instance.setMudo(mudo);
    }
}
