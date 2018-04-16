using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected NodeScript currentNode;

    public virtual void StartScript(NodeScript node)
    {
        currentNode = node;
    }
}