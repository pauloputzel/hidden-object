using UnityEngine;

public class ComboProgressBar : MonoBehaviour
{
    public float widthZerado;
    public float widthCheio;

    private SpriteRenderer sprRenderer;
    private float difWidth = 0f;

    public void Start()
    {
        difWidth = widthCheio - widthZerado;
        sprRenderer = GetComponent<SpriteRenderer>();
        SetEmpty();
    }

    public void UpdateValue(float value)
    {
        float newWidth = (difWidth * value) / 100f;
        sprRenderer.size = new Vector2(widthZerado + newWidth, sprRenderer.size.y);
    }

    public void SetEmpty()
    {
        sprRenderer.size = new Vector2(widthZerado, sprRenderer.size.y);
    }
}
