using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatsSystem;

public class StatsDependencies
{
    public void CalculateSkills(StatsContainer stats)
    {
        stats.SKL_Attack.BaseValue = 5;
        Debug.Log(stats.SKL_Attack.BaseValue);
    }
}
