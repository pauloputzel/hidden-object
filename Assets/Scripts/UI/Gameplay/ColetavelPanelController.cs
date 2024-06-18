using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColetavelPanelController : MonoBehaviour
{
    public GameObject prefabItemColetavel;

    public GameObject content;

    private Dictionary<ColetavelName, GameObject> itensMostrados = new Dictionary<ColetavelName, GameObject>();

    void Start()
    {
        criarListaDeItens();
    }

    public void criarListaDeItens()
    {
        itensMostrados.Clear();

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ColetavelName coletavelName in GameManager.instance.listaItensColetaveis)
        {
            GameObject createdObject = Instantiate(prefabItemColetavel, content.transform);
            createdObject.GetComponent<TextMeshProUGUI>().text = EnumUtils.GetEnumDescription(coletavelName);
            itensMostrados.Add(coletavelName, createdObject);
        }
    }

    public GameObject encontrarTextDaLista(ColetavelName coletavelName)
    {
        if (itensMostrados.ContainsKey(coletavelName)) return itensMostrados[coletavelName];

        return null;
    }
}
