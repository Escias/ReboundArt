using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    List<Vector3> linePoints;
    float timer;
    public float timerdelay;

    GameObject newLine;
    LineRenderer drawLine;
    public float lineWidth;
    public float maxLineLength = 10f;
    public MouseRaycast m_mouseRaycast;

    public GameManager manager;
    public BallMove ballMove;
    public PlayerSkill playerSkill;

    public PhysicMaterial spherePhysicMaterial;
    public float bounceMultiplier = 8f;

    void Start()
    {
        linePoints = new List<Vector3>();
        timer = timerdelay;
    }

    void Update()
    {
        if (manager.GetGameStart())
        {
            if (Input.GetMouseButtonDown(0) && CanStartNewLine())
            {
                newLine = new GameObject("Line");
                drawLine = newLine.AddComponent<LineRenderer>();
                drawLine.material = new Material(Shader.Find("Sprites/Default"));

                Color lineColor = Color.black;
                
                if (playerSkill != null && playerSkill.IsSkillRedActive())
                {
                    lineColor = Color.red;
                    AddLineColliderJumpToLine();
                }
                
                if (playerSkill != null && playerSkill.IsSkillGreenActive())
                {
                    lineColor = Color.green;
                    AddLineColliderStickyToLine();
                }

                drawLine.startColor = lineColor;
                drawLine.endColor = lineColor;
                drawLine.startWidth = lineWidth;
                drawLine.endWidth = lineWidth;

                if (playerSkill != null && playerSkill.IsSkillRedActive())
                {
                    playerSkill.MarkLineDrawn();
                }
                if (playerSkill != null && playerSkill.IsSkillGreenActive())
                {
                    playerSkill.MarkLineDrawn();
                }
            }

            if (Input.GetMouseButton(0) && newLine != null)
            {
                timer -= Time.deltaTime;
                if (timer <= 0 && GetCurrentLineLength() < maxLineLength)
                {
                    Vector3 point = m_mouseRaycast.GetHitPosition();
                    linePoints.Add(point);
                    drawLine.positionCount = linePoints.Count;
                    drawLine.SetPositions(linePoints.ToArray());

                    timer = timerdelay;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (linePoints.Count > 1)
                {
                    AddMeshColliderToLine();
                }

                linePoints.Clear();
                if (!manager.GetBallStart())
                {
                    manager.SetBallStart(true);
                    ballMove.StartMove();
                }
            }
        }
    }

    float GetCurrentLineLength()
    {
        float totalLength = 0f;
        for (int i = 0; i < linePoints.Count - 1; i++)
        {
            totalLength += Vector3.Distance(linePoints[i], linePoints[i + 1]);
        }
        return totalLength;
    }

    bool CanStartNewLine()
    {
        return playerSkill == null || !playerSkill.IsSkillRedActive() || playerSkill.CanDrawLine();
    }

    void AddMeshColliderToLine()
    {
        try
        {
            Mesh lineMesh = new Mesh();
            GenerateMesh(lineMesh);
            MeshCollider meshCollider = newLine.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = lineMesh;
        }
        catch
        {
        }
    }

    void AddLineColliderJumpToLine()
    {
        LineCollider lineColliderScript = newLine.AddComponent<LineCollider>();
        lineColliderScript.bounceMultiplier = bounceMultiplier;
        lineColliderScript.isSkillRed = true;
    }
    
    void AddLineColliderStickyToLine()
    {
        LineCollider lineColliderScript = newLine.AddComponent<LineCollider>();
        lineColliderScript.bounceMultiplier = -bounceMultiplier;
        lineColliderScript.isSkillRed = false;
    }

    void GenerateMesh(Mesh mesh)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < linePoints.Count - 1; i++)
        {
            Vector3 start = linePoints[i];
            Vector3 end = linePoints[i + 1];

            Vector3 direction = (end - start).normalized;
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward) * lineWidth / 2;

            vertices.Add(start + perpendicular);
            vertices.Add(start - perpendicular);
            vertices.Add(end + perpendicular);
            vertices.Add(end - perpendicular);

            int baseIndex = i * 4;
            triangles.Add(baseIndex);
            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 2);

            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 3);
            triangles.Add(baseIndex + 2);
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
    }
}
