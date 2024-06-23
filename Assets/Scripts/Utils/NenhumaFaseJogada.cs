using UnityEngine;

public class NenhumaFaseJogada : MonoBehaviour
{
    private DialogoPersonagemUnicoController dialogoController;
    private LocalController localController;

    private void Start()
    {
        dialogoController = gameObject.GetComponent<DialogoPersonagemUnicoController>();
        localController = gameObject.GetComponent<LocalController>();
    }

    void Update()
    {
        if (GameManager.instance && !GameManager.instance.primeiraFaseJogada)
        {
            if (dialogoController != null) dialogoController.enabled = true;
            if (localController != null) localController.enabled = true;
        }
    }
}
