using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "New Level Config", menuName = "Level/New Level Config")]
public class LevelData : ScriptableObject
{
    [SerializeField] public List<Wave> Waves = new List<Wave>();
}

