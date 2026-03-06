using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class NavMeshPathVisualizer : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    [SerializeField] GameObject pathCamera;

    private NavMeshPath path;
    private LineRenderer lineRenderer;

    private bool hasUsedPath = false;   // Only once per level

    void Awake()
    {
        path = new NavMeshPath();
        lineRenderer = GetComponent<LineRenderer>();

        // Make sure path is hidden at start
        lineRenderer.positionCount = 0;
        pathCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !hasUsedPath)
        {
            hasUsedPath = true;   // lock it immediately
            ShowPath();
        }
    }

    void ShowPath()
    {
        pathCamera.SetActive(true);

        if (NavMesh.CalculatePath(
            startPoint.position,
            endPoint.position,
            NavMesh.AllAreas,
            path))
        {
            DrawPath();
        }

        StartCoroutine(HidePathAfterTime());
    }

    void DrawPath()
    {
        if (path.corners.Length < 2)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }

    IEnumerator HidePathAfterTime()
    {
        yield return new WaitForSecondsRealtime(10f);

        // Hide line
        lineRenderer.positionCount = 0;

        // Hide camera
        pathCamera.SetActive(false);
    }
}