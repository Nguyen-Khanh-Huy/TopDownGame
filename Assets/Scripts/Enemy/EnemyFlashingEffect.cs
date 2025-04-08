using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFlashingEffect : PISMonoBehaviour
{
    [SerializeField] private EnemyCtrlAbstract _enemyCtrl;
    [SerializeField] private Renderer[] _renderer;
    [SerializeField] private Material[] _defaultMaterial;
    [SerializeField] private Material _freezeMaterial;

    [SerializeField] private MaterialPropertyBlock _propertyBlock;
    [SerializeField] private float _timeFlashing = 0.5f;
    [SerializeField] private float _timeInterval = 0.1f;

    private Coroutine _flashCoroutine;
    private Coroutine _freezeCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartFreeze();
        }
    }

    public void StartFlash()
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
            ResetColor();
        }
        _flashCoroutine = StartCoroutine(Flashing());
    }

    public void StartFreeze()
    {
        if (_freezeCoroutine != null)
        {
            StopCoroutine(_freezeCoroutine);
            ResetFreeze();
        }
        _freezeCoroutine = StartCoroutine(FreezeCoroutine());
    }

    //-----------------------------------------------------------------------------------

    private IEnumerator FreezeCoroutine()
    {
        SetFreeze();
        float timeRemaining = _enemyCtrl.PlayerCtrl.PlayerSkillsCtrl.PlayerSkillFreeze.TimeFreeze;
        while (timeRemaining > 0f)
        {
            if (_enemyCtrl.Hp <= 0)
            {
                ResetFreeze();
                yield break;
            }
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        ResetFreeze();
    }

    private void SetFreeze()
    {
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].sharedMaterial = _freezeMaterial;
        }
    }

    private void ResetFreeze()
    {
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].sharedMaterial = _defaultMaterial[i];
        }
    }

    //-----------------------------------------------------------------------------------

    private IEnumerator Flashing()
    {
        float timeCount = 0f;
        bool toggle = true;

        while (timeCount < _timeFlashing)
        {
            if (_enemyCtrl.Hp <= 0)
            {
                ResetColor();
                yield break;
            }
            SetColor(toggle ? Color.red : Color.white);
            toggle = !toggle;

            yield return new WaitForSeconds(_timeInterval);
            timeCount += _timeInterval;
        }
        ResetColor();
    }

    private void ApplyColor(Color color)
    {
        _propertyBlock ??= new MaterialPropertyBlock();

        foreach (var renderer in _renderer)
        {
            if (renderer == null) continue;

            renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor("_Color", color);
            renderer.SetPropertyBlock(_propertyBlock);
        }
    }

    private void SetColor(Color color) => ApplyColor(color);

    private void ResetColor() => ApplyColor(Color.white);

    //-----------------------------------------------------------------------------------

    protected override void LoadComponents()
    {
        if (_enemyCtrl != null && _renderer.Length > 0 && _defaultMaterial.Length > 0 && _freezeMaterial != null) return;
        _enemyCtrl = GetComponentInParent<EnemyCtrlAbstract>();
        //_renderer = transform.parent.GetComponentInChildren<Renderer>();
        //_defaultColor = _renderer.sharedMaterial.color;

        _renderer = transform.parent.Find("Model").GetComponentsInChildren<Renderer>();

        _defaultMaterial = new Material[_renderer.Length];
        for (int i = 0; i < _renderer.Length; i++)
        {
            _defaultMaterial[i] = _renderer[i].sharedMaterial;
        }

        _freezeMaterial = Resources.Load<Material>("Shader/FreezeMaterial");
    }
}
