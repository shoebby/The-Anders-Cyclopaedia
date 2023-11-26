using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SerializedMonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private string previousScene;
    [SerializeField] private Vector3 playerSpawnPos;
    [SerializeField] private Transform defaultSpawn;

    [SerializeField]
    private Dictionary<string, Transform> spawnPointsDic = new Dictionary<string, Transform>();

    private void Awake() => previousScene = Helpers.GetPreviousScene();

    private void Start()
    {
        if (previousScene != null && spawnPointsDic.TryGetValue(previousScene, out Transform playerSpawn))
        {
            playerSpawnPos = playerSpawn.position;
        }
        else
        {
            playerSpawnPos = defaultSpawn.position;
        }

        playerTransform.position = playerSpawnPos;
    }
}