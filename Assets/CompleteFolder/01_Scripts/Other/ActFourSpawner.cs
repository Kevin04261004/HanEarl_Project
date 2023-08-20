using UnityEngine;

public class ActFourSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnObjs;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        foreach (var obj in _spawnObjs)
        {
            obj.SetActive(true);
        }
    }
}
