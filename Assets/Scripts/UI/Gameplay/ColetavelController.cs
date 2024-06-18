using System;
using UnityEngine;

public class ColetavelController : MonoBehaviour
{
    public ColetavelName nome;

    public AnimationCurve curve;

    private Transform targetTransform;
    private float startTime;
    private Vector3 startPosition;
    private bool coletando = false;
    private bool chegouNoDestino = false;
    private Action terminouDeVoar;

    private void Start()
    {
        if (GameManager.instance.itemEstaNaListaDeColetaveis(nome)) Destroy(gameObject);
    }
    void Update()
    {
        if (coletando) this.voaProAlvo();
        if (chegouNoDestino) this.terminouDeVoar();
    }

    void OnMouseDown()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        Debug.Log(hitInfo.collider.gameObject.name);
        if (hitInfo && gameObject == hitInfo.collider.gameObject)
        {
            coletando = true;
            GameManager.instance.coletarItem(nome, gameObject);
        }
    }

    public void VoarPara(Transform targetPosition, Action terminouDeVoar)
    {
        this.terminouDeVoar = terminouDeVoar;
        this.targetTransform = targetPosition;
        Vector3 newPositionZ = transform.position;
        newPositionZ.z = 100f;
        transform.position = newPositionZ;

        startTime = Time.time;
        startPosition = transform.position;
    }

    private void voaProAlvo()
    {
        // Calculate how far along the curve we should be
        float timeFraction = (Time.time - startTime) / GameManager.instance.tempoDuracaoColetaItemSegundos;

        // Ensure timeFraction stays between 0 and 1
        timeFraction = Mathf.Clamp01(timeFraction);

        // Use the curve to adjust the fraction
        float curveValue = curve.Evaluate(timeFraction);

        // Interpolate between the start position and target position
        transform.position = Vector3.Lerp(startPosition, targetTransform.position, curveValue);

        // Check if the movement is complete
        if (timeFraction == 1.0f)
        {
            coletando = false;
            chegouNoDestino = true;
        }
    }
}
