using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       GetComponent<TextMeshProUGUI>().text = GameManager.instance.GetLevelScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
