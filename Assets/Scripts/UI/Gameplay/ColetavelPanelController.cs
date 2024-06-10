using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColetavelPanelController : MonoBehaviour
{
    public GameObject prefabItemColetavel;

    public GameObject content;

    void Start()
    {
        criarListaDeItens();
    }

    public void criarListaDeItens()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ColetavelName coletavelName in GameManager.instance.listaItensColetaveis)
        {
            GameObject createdObject = Instantiate(prefabItemColetavel, content.transform);
            createdObject.GetComponent<TextMeshProUGUI>().text = EnumUtils.GetEnumDescription(coletavelName);
        }
    }
}
