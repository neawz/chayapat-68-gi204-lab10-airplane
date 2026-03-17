using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -8);
    public float smoothSpeed = 5f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
            );

        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            smoothSpeed * Time.deltaTime
            );
    }
}
