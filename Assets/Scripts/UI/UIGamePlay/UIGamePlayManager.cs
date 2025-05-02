using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlayManager : Singleton<UIGamePlayManager>
{
    public List<GameObject> ListPanelPlayerHp = new();

    public ShaderWarningUI ShaderWarningUI;
    public GameObject PanelSkillsDialog;
    [SerializeField] private GameObject PanelPlayerHp;

    public Slider SliderMana;
    public TMP_Text TxtCountEnemyDead;
    public TMP_Text TxtCountPlayTime;

    public bool CheckPlayTime;
    private float _gamePlayTime = 0f;

    public float GamePlayTime { get => _gamePlayTime; }

    private bool _isWarning;
    private void OnEnable() 
    {
        Init();
    }

    private void Update()
    {
        if (CheckPlayTime)
            _gamePlayTime += Time.deltaTime;
        TxtCountPlayTime.text = TimeConvert(_gamePlayTime);

        if (_gamePlayTime >= 300f && !_isWarning)
        {
            _isWarning = true;
            ShaderWarningUI.IsWarning = true;
        }
    }

    protected override void LoadComponents()
    {
        DontDestroy(false);
        if (ListPanelPlayerHp.Count <= 0 && ShaderWarningUI != null && PanelPlayerHp != null && SliderMana != null && TxtCountEnemyDead != null && TxtCountPlayTime != null) return;
        ShaderWarningUI = GameObject.Find("PanelWarning").GetComponentInChildren<ShaderWarningUI>();
        PanelPlayerHp = GameObject.Find("PanelPlayerHp");
        SliderMana = GameObject.Find("PanelMana").GetComponentInChildren<Slider>();
        TxtCountEnemyDead = GameObject.Find("TxtCountEnemyDead").GetComponentInChildren<TMP_Text>();
        TxtCountPlayTime = GameObject.Find("TxtCountPlayTime").GetComponentInChildren<TMP_Text>();

        foreach (Transform child in PanelPlayerHp.transform)
        {
            foreach (Transform grandChild in child)
            {
                ListPanelPlayerHp.Add(grandChild.gameObject);
            }
        }
    }

    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Close(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void Init()
    {
        SliderMana.value = 0;
        CheckPlayTime = true;
        _gamePlayTime = 0f;
        TxtCountEnemyDead.text = PlayerCtrl.Ins.PlayerShoot.CountEnemyDead.ToString();
    }

    public string TimeConvert(double time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    public void DisableImgHpOn()
    {
        if (ListPanelPlayerHp == null || ListPanelPlayerHp.Count == 0) return;
        for (int i = ListPanelPlayerHp.Count - 1; i >= 0; i--)
        {
            GameObject imgHpOn = ListPanelPlayerHp[i];
            if (imgHpOn.activeSelf)
            {
                imgHpOn.SetActive(false);
                break;
            }
        }
    }
}
