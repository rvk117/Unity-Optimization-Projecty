using UnityEngine;

public class AgentController : MonoBehaviour
{
    private static int nextId = 0;
    private int id;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float turnSpeed = 360f;
    [SerializeField] private Vector2 arenaSize = new Vector2(50f, 50f);

    private Vector3 targetPosition;

    private void OnEnable()
    {
        id = nextId++;
        PickNewTarget();
    }

    private void Update()
{
    int throttleFactor = 1;

    if (CrowdManager.GlobalMode == CrowdMode.Optimized)
    {
        throttleFactor = 4; // same “every 4th frame” logic

        if ((Time.frameCount + id) % throttleFactor != 0)
            return;
    }

    float effectiveDeltaTime = Time.deltaTime * throttleFactor;

    MoveTowardsTarget(effectiveDeltaTime);
    CheckDistanceToTarget();
    KeepWithinBounds();
}


private void MoveTowardsTarget(float deltaTime)
{
    Vector3 direction = (targetPosition - transform.position);
    direction.y = 0f;

    if (direction.sqrMagnitude < 0.001f)
        return;

    direction.Normalize();
    Quaternion targetRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        targetRotation,
        turnSpeed * deltaTime
    );

    transform.position += direction * (moveSpeed * deltaTime);
}


    private void CheckDistanceToTarget()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < 1f)
        {
            PickNewTarget();
        }
    }

    private void PickNewTarget()
    {
        float x = Random.Range(-arenaSize.x * 0.5f, arenaSize.x * 0.5f);
        float z = Random.Range(-arenaSize.y * 0.5f, arenaSize.y * 0.5f);
        targetPosition = new Vector3(x, 0f, z);
    }

    private void KeepWithinBounds()
    {
        Vector3 pos = transform.position;
        float halfX = arenaSize.x * 0.5f;
        float halfZ = arenaSize.y * 0.5f;

        pos.x = Mathf.Clamp(pos.x, -halfX, halfX);
        pos.z = Mathf.Clamp(pos.z, -halfZ, halfZ);

        transform.position = pos;
    }
}
