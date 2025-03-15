using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSkillsCtrl : PISMonoBehaviour
{
    [SerializeField] private List<PlayerSkillAbstract> _listAllPbSkills = new();

    protected override void LoadComponents()
    {
        //if (_listSkillPrefabs.Count > 0) return;
        //_listSkillPrefabs.Clear();
        //PlayerSkillAbstract[] skillPrefabs = Resources.LoadAll<PlayerSkillAbstract>("Enemies");
        //foreach (PlayerSkillAbstract enemyPrefab in enemyPrefabs)
        //{
        //    if (enemyPrefab != null)
        //    {
        //        _listSkillPrefabs.Add(enemyPrefab);
        //    }
        //}

        if (_listAllPbSkills.Count == transform.childCount) return;
        foreach (Transform child in transform)
        {
            _listAllPbSkills.Add(child.GetComponent<PlayerSkillAbstract>());
        }
    }

    public PlayerSkillAbstract GetRandomSkill()
    {
        List<PlayerSkillAbstract> filteredSkills = _listAllPbSkills.Where(skill => skill.LevelSkill > 0 && skill.LevelSkill < 3).ToList();
        if (filteredSkills.Count == 0)
            return null;

        int rand = Random.Range(0, filteredSkills.Count);
        return filteredSkills[rand];
    }
}
