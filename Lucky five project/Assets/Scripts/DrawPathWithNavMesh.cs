using UnityEngine;
using UnityEngine.InputSystem;

public class DrawPathWithNavMesh : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public Transform target1;  // Reference to the target object to collect
    public Transform target2;
    public Transform target3;
    private LineRenderer lineRenderer;

    int counter = 0;
    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();

        // Set the number of line segments (start and end points)
        lineRenderer.positionCount = 2;
        
        lineRenderer.startWidth = .5f;
        lineRenderer.endWidth = .5f;
    }

    void Update()
    {
        switch (counter)
        {
            case 0:
            {
                // Set the start point of the line to the player's position
                lineRenderer.SetPosition(0, player.position);

                // Set the end point of the line to the target's position
                lineRenderer.SetPosition(1, target1.position);
                break;
            }
            case 1:
            {
                // Set the start point of the line to the player's position
                lineRenderer.SetPosition(0, player.position);

                // Set the end point of the line to the target's position
                lineRenderer.SetPosition(1, target2.position);
                break;
            }
            case 2:
            {
                // Set the start point of the line to the player's position
                lineRenderer.SetPosition(0, player.position);

                // Set the end point of the line to the target's position
                lineRenderer.SetPosition(1, target3.position);
                break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Finish"))
            Debug.Log("obj found");
        Destroy(gameObject);
        counter++;
    }
}
