using UnityEngine;

public class ResetButtonController : MonoBehaviour
{
    public void Reset()
    {
        GameManager.instance.resetGame();
    }
}
