using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColetavelPanelController : MonoBehaviour
{
    public GameObject prefabItemColetavel;

    public GameObject content;

    private List<ColetavelName> displayedColetavelItens;

    private int totalItensColetaveis;

    // Start is called before the first frame update
    void Start()
    {
        totalItensColetaveis = GameManager.instance.listaColetaveis.Count;
        displayedColetavelItens = GameManager.instance.getListaColevateis();
        criarItens();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalItensColetaveis != GameManager.instance.listaColetaveis.Count)
        {
            criarItens();
        }
    }

    void criarItens()
    {
        totalItensColetaveis = GameManager.instance.listaColetaveis.Count;
        displayedColetavelItens = GameManager.instance.getListaColevateis();

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ColetavelName coletavelName in displayedColetavelItens)
        {
            GameObject createdObject = Instantiate(prefabItemColetavel, content.transform);
            createdObject.GetComponent<TextMeshProUGUI>().text = GameManager.GetEnumDescription(coletavelName);
        }

    }
}
