
public enum EnemyType
{
    Wolf,
    Cloud,
    Typhoon,
    Boulder,
}

internal class CharacterDef
{
    public CharacterDef(string prefab, string audio, EnemyType enemy)
    {
        prefabName = prefab;
        audioName = audio;
        counteredEnemy = enemy;
    }

    public string prefabName;
    public string audioName;
    public EnemyType counteredEnemy;
}


