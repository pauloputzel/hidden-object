using System;
using TMPro;
using UnityEngine;

public class ColetavelController : MonoBehaviour
{
    public ColetavelName nome;
    public bool coletavelDaDengue = false;
    public String mensagemDaDengue;
    public AnimationCurve curve;
    private float minScaleAnim = 0.3f;

    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock block;
    private Transform targetTextTransform;
    private Transform targetDetalhesTransform;
    private DetalhesColetavelPanelController detalhesPanel;
    private float startTime;
    private Vector3 startPosition;
    private Vector3 startScale;
    private bool detalhesAnimExecutando = false;
    private bool coletando = false;
    private bool chegouNoDestino = false;
    private Action terminouDeVoar;

    private void Start()
    {
        if (GameManager.instance.itemEstaNaListaDeColetaveis(nome)) Destroy(gameObject);
        spriteRenderer = GetComponent<SpriteRenderer>();
        block = new MaterialPropertyBlock();
    }
    void Update()
    {
        if (detalhesAnimExecutando) this.voaProAlvo(targetDetalhesTransform);
        if (coletando) this.voaProAlvo(targetTextTransform);
        if (chegouNoDestino) this.terminouDeVoar();
    }

    void OnMouseDown()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        Debug.Log(hitInfo.collider.gameObject.name);
        if (hitInfo && gameObject == hitInfo.collider.gameObject)
        {
            GameManager.instance.coletarItem(nome, gameObject);
        }
    }

    public void ExecutarAnimacao(DetalhesColetavelPanelController detalhesPanel, Transform targetDetalhesTransform, Transform targetTextTransform, Action terminouAction)
    {
        spriteRenderer.GetPropertyBlock(block);
        block.SetColor("_Color", new Color(1024, 847, 0, 1));
        block.SetFloat("_Thick", 3f);
        spriteRenderer.SetPropertyBlock(block);

        if (coletavelDaDengue) this.targetDetalhesTransform = targetDetalhesTransform;
        if (coletavelDaDengue) this.detalhesPanel = detalhesPanel;

        this.terminouDeVoar = terminouAction;
        this.targetTextTransform = targetTextTransform;

        Vector3 newPositionZ = transform.position;
        newPositionZ.z = 100f;
        transform.position = newPositionZ;

        ResetVoarSettings();

        if (coletavelDaDengue)
        {
            detalhesAnimExecutando = true;

        }
        else
        {
            coletando = true;
        }
    }

    private void ResetVoarSettings()
    {
        startTime = Time.time;
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    private void voaProAlvo(Transform target)
    {
        float timeFraction = (Time.time - startTime) / GameManager.instance.tempoDuracaoColetaItemSegundos;
        timeFraction = Mathf.Clamp01(timeFraction);

        float curveValue = curve.Evaluate(timeFraction);

        transform.position = Vector3.Lerp(startPosition, target.position, curveValue);
        if (coletando) transform.localScale = Vector3.Lerp(startScale, startScale * minScaleAnim, curveValue);

        if (timeFraction == 1.0f)
        {
            if (coletando)
            {
                coletando = false;
                chegouNoDestino = true;
                GameManager.instance.jogoPausado = false;
            }

            if (detalhesAnimExecutando && coletavelDaDengue)
            {
                detalhesAnimExecutando = false;
                carregarAnimacaoParaDetalhesPanel();
            }
        }
    }

    private void carregarAnimacaoParaDetalhesPanel()
    {
        detalhesPanel.coletavelImage.sprite = spriteRenderer.sprite;
        detalhesPanel.coletavelImage.SetNativeSize();

        detalhesPanel.coletavelImage.rectTransform.sizeDelta = new Vector2(
            detalhesPanel.coletavelImage.rectTransform.sizeDelta.x * transform.lossyScale.x,
            detalhesPanel.coletavelImage.rectTransform.sizeDelta.y * transform.lossyScale.y);

        detalhesPanel.coletavelNomeText.text = EnumUtils.GetEnumDescription(nome);
        detalhesPanel.detalhesColetavel.text = mensagemDaDengue;
        detalhesPanel.continuarButton.onClick.RemoveAllListeners();
        detalhesPanel.continuarButton.onClick.AddListener(() =>
        {
            ResetVoarSettings();
            coletando = true;
            detalhesPanel.gameObject.SetActive(false);
        });

        detalhesPanel.gameObject.SetActive(true);
    }
}
