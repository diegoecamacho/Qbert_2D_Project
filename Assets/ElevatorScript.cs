using UnityEngine;

public class ElevatorScript : NodeScript
{
    [SerializeField] private Transform endLocation;

    [SerializeField] private NodeScript endCube;
    public NodeScript deadZone;

    [SerializeField] private QbertScript Qbert;

    // Update is called once per frame
    private void Update()
    {
        if (selected)
        {
            Qbert.transform.parent = transform;
            transform.position = Vector3.MoveTowards(transform.position, endLocation.position, 0.02f);

            if (transform.position == endLocation.position)
            {
                selected = false;
                Qbert.CurrentCube = endCube;
                Qbert.CurrentCube.SelectCube();
                Qbert.onElevator = false;
            }
        }
    }
}