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
        displayedColetavelItens = GameManager.instance.getLevelProximosColetaveisList();
        criarItens();
    }

    // Update is called once per frame
    void Update()
    {
        displayedColetavelItens = GameManager.instance.getLevelProximosColetaveisList();

        if (totalItensColetaveis != displayedColetavelItens.Count)
        {
            criarItens();
        }
    }

    private void criarItens()
    {
        totalItensColetaveis = displayedColetavelItens.Count;

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ColetavelName coletavelName in displayedColetavelItens)
        {
            GameObject createdObject = Instantiate(prefabItemColetavel, content.transform);
            createdObject.GetComponent<TextMeshProUGUI>().text = EnumUtils.GetEnumDescription(coletavelName);
        }
    }
}
