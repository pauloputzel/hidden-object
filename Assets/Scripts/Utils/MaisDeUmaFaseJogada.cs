using UnityEngine;

public class MaisDeUmaFaseJogada : MonoBehaviour
{
    private DialogoPersonagemUnicoController dialogoController;
    private PolygonCollider2D colider2D;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        dialogoController = gameObject.GetComponent<DialogoPersonagemUnicoController>();
        colider2D = gameObject.GetComponent<PolygonCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.instance && GameManager.instance.primeiraFaseJogada)
        {
            if (dialogoController != null) dialogoController.enabled = true;
            if (colider2D != null) colider2D.enabled = true;
            if (spriteRenderer != null) spriteRenderer.enabled = true;
        }
    }
}
