using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyBehaviour))]

public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        // full white circle
        EnemyBehaviour fov = (EnemyBehaviour)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

        // yellow wedge
        Vector3 viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.viewAngle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.viewAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.viewRadius);

        // green line to player
        if (fov.playerDetection)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.Player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}