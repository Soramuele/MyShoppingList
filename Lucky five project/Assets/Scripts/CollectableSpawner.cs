using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private Collectable prefab;
    [SerializeField] private Transform player;
    [SerializeField] private LineRenderer linePath;
    [SerializeField] private float PathHeighOffset = 1.25f;
    [SerializeField] private float PathUpdateSpeed = 0.175f;

    private Collectable activeInstance;
    private NavMeshTriangulation triangulation;
    private Coroutine DrawPathCorutine;

    public int counter = 0;
    [SerializeField] private Transform[] products;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        triangulation = NavMesh.CalculateTriangulation();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewObject();
    }

    public void SpawnNewObject()
    {
        activeInstance = Instantiate(prefab, products[counter].position + Vector3.up, Quaternion.Euler(90,0,0));
        activeInstance.spawner = this;

        if (DrawPathCorutine != null)
            StopCoroutine(DrawPathCorutine);

        DrawPathCorutine = StartCoroutine(DrawPathToCollectable());
    }

    private IEnumerator DrawPathToCollectable()
    {
        WaitForSeconds wait = new WaitForSeconds(PathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();

        while (true)
        {
            if (NavMesh.CalculatePath(player.position, products[counter].position, NavMesh.AllAreas, path))
            {
                linePath.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                    linePath.SetPosition(i, path.corners[i] + Vector3.up * PathHeighOffset);
            }

            yield return wait;
        }
    }
}
