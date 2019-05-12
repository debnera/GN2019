
using UnityEngine;

public enum EnemyType
{
    Wolf,
    Cloud,
    Typhoon,
    Boulder,
    Hamster,
}

internal class CharacterDef
{
    public CharacterDef(string prefab, string audio, EnemyType enemy, Color color)
    {
        prefabName = prefab;
        audioName = audio;
        counteredEnemy = enemy;
        instrumentColor = color;
    }

    public string prefabName;
    public string audioName;
    public EnemyType counteredEnemy;
    public Color instrumentColor;
}


