using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<ColetavelName> listaColetaveis;

    public List<ColetavelName> listaColetados = new List<ColetavelName>();

    public void Start()
    {
        GameManager.instance.setLevelManager(this);
    }


    public List<ColetavelName> getLevelProximosColetaveisList()
    {
        //retorna os primeiros {GameManager.instance.maximoColetavel} itens da lista
        return listaColetaveis.Take(GameManager.instance.maximoColetavel).ToList();
    }

    public void coletarItem(ColetavelName coletavelNome, GameObject coletavel)
    {

        //separando os X primeiros itens da lista de itens para respeitar regra do jogo
        List<ColetavelName> primeirosMaxItens = getLevelProximosColetaveisList();

        //encontrando se o item esta entre a lista dos primeiros, Find busca o item para cada x (Coletavel) se o nome é o mesmo
        ColetavelName coletavelEncontrado = primeirosMaxItens.Find(x => x == coletavelNome);

        //Se Find não encontrar o item coletavelEncontrado será "Nenhum"
        if (coletavelEncontrado == ColetavelName.Nenhum)
        {
            GameManager.instance.showGameMessage($"Esse item não está na lista de primeiros {GameManager.instance.maximoColetavel} itens coletaveis");

        }
        else
        {
            GameManager.instance.showGameMessage($"Parabéns {EnumUtils.GetEnumDescription(coletavelEncontrado)} encontrado!");
            listaColetaveis.Remove(coletavelEncontrado);
            listaColetados.Add(coletavelEncontrado);
            Destroy(coletavel);
        }

        if (listaColetaveis.Count == 0)
        {
            GameManager.instance.carregarScene("GameOverScene");
        }
    }
}
