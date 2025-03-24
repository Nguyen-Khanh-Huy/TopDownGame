using System.Collections;
using UnityEngine;

public class EnemyFlashingEffect : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _defaultColor;

    [SerializeField] private float _timeFlashing = 0.5f;
    [SerializeField] private float _timeInterval = 0.1f;

    private Coroutine _flashCoroutine;

    public void StartFlash()
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
            ResetColor();
        }
        _flashCoroutine = StartCoroutine(Flashing());
    }

    private IEnumerator Flashing()
    {
        float timeCount = 0f;
        while (timeCount < _timeFlashing)
        {
            if (_enemyCtrl.Hp <= 0)
            {
                ResetColor();
                yield break;
            }

            SetColor(Color.red);
            yield return new WaitForSeconds(_timeInterval);

            SetColor(Color.white);
            yield return new WaitForSeconds(_timeInterval);

            timeCount += _timeInterval * 2;
        }
        ResetColor();
    }

    private void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void ResetColor()
    {
        _renderer.material.color = _defaultColor;
    }

    protected override void LoadComponents()
    {
        if (_enemyCtrl != null && _renderer != null && _defaultColor != null) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrlAbstract>();
        _renderer = transform.parent.GetComponentInChildren<Renderer>();
        _defaultColor = _renderer.sharedMaterial.color;
    }
}
