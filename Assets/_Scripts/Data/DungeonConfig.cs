using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGeneratorParameters_",menuName = "PDG/DungeonRandomWalkConfig")]
public class DungeonConfig : ScriptableObject
{
    public int iter = 10, walkLen = 10;
    public bool randomStart = true;

}
