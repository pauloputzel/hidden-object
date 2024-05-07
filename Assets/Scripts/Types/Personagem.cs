public class Personagem
{
    public PersonagemName name;

    public Personagem(PersonagemName personagem)
    {
        name = personagem;
    }

    public string getMapaScene()
    {
        return $"{name}MapaScene";
    }
}
