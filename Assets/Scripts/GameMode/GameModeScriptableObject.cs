using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeScriptableObject", menuName = "Scriptable Objects/GameModeScriptableObject")]
public class GameModeScriptableObject : ScriptableObject
{
    [SerializeField] private string _gameModeName;
    [SerializeField] private string[] _mapSceneListNames;
    [SerializeField] private GameManager _gameManager;

#if UNITY_EDITOR
    [SerializeField] private SceneAsset[] _mapSceneList;

    private void OnValidate()
    {
        this._mapSceneListNames = new string[this._mapSceneList.Length];
        int i = 0;

        foreach (SceneAsset scene in this._mapSceneList) 
        {
            this._mapSceneListNames.SetValue(scene.name, i);
            i++;
        }
    }
#endif

    public GameManager GameManager()
    {
        return this._gameManager;
    }

    public string Name()
    {
        return this._gameModeName;
    }

    public string[] Maps()
    {
        return this._mapSceneListNames;
    }
}
