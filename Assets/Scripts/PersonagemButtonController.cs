using UnityEngine;

public class PersonagemButtonController : MonoBehaviour
{
    public PersonagemName personagem;

    public void selectPersonagem()
    {
        GameManager.instance.setPersonagem(personagem);
        GameManager.instance.carregarMapaPersonagem();
    }
}
