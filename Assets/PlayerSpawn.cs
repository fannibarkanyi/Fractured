using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawnPoint; // drag SpawnPoint here

    void Start()
    {
        if (spawnPoint != null)
            transform.position = spawnPoint.position;
    }
}
