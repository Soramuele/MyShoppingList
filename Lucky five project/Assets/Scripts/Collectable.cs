using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectableSpawner spawner;
    
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (spawner.counter < 3)
        {
            spawner.counter++;
            spawner.SpawnNewObject();
        }
    }
}
