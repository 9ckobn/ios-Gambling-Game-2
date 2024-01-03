using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class lvlSettings : ScriptableObject
{
    public lvlButton levelSelectorPrefab;

    public List<Lvl> lvls;
}

[Serializable]
public class Lvl
{
    public Sprite sprite;

}