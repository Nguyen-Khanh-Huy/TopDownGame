using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlayManager : Singleton<UIGamePlayManager>
{
    public List<GameObject> ListPanelPlayerHp = new();

    public GameObject PanelSkillsDialog;
    [SerializeField] private GameObject PanelPlayerHp;

    public PlayerController PlayerCtrl;
    public Slider Slider;
    public TMP_Text TxtCountEnemyDead;
    public TMP_Text TxtCountPlayTime;

    public bool CheckPlayTime;
    private float _gamePlayTime = 0f;

    public float GamePlayTime { get => _gamePlayTime; }

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        if (CheckPlayTime)
        {
            _gamePlayTime += Time.deltaTime;
        }
        TxtCountPlayTime.text = TimeConvert(_gamePlayTime);
    }

    protected override void LoadComponents()
    {
        DontDestroy(false);
        if (ListPanelPlayerHp.Count <= 0 && PanelPlayerHp != null && PlayerCtrl != null && Slider != null && TxtCountEnemyDead != null && TxtCountPlayTime != null) return;
        PanelPlayerHp = GameObject.Find("PanelPlayerHp");
        PlayerCtrl = FindObjectOfType<PlayerController>();
        Slider = GameObject.Find("PanelMana").GetComponentInChildren<Slider>();
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
        Slider.value = 0;
        TxtCountEnemyDead.text = PlayerCtrl.PlayerShoot.CountEnemyDead.ToString();
        CheckPlayTime = true;
        _gamePlayTime = 0f;
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
