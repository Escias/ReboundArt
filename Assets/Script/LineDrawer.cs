using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    List<Vector3> linePoints;
    float timer;
    public float timerdelay;

    GameObject newLine;
    LineRenderer drawLine;
    public float lineWidth;
    public MouseRaycast m_mouseRaycast;
    public float maxLineLength = 10f; // Longueur maximale de la ligne
    private bool canDraw = true; // Permet de contrôler si le dessin est autorisé
    public GameManager manager;
    public BallMove ballMove;
    // Start is called before the first frame update
    void Start()
    {
        linePoints = new List<Vector3>();
        timer = timerdelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetGameStart())
        {
            canDraw = true; // Réinitialiser l'état de dessin
            newLine = new GameObject("Line");
            drawLine = newLine.AddComponent<LineRenderer>();
            drawLine.material = new Material(Shader.Find("Sprites/Default"));

            drawLine.startColor = Color.black;
            drawLine.endColor = Color.black;
            drawLine.startWidth = lineWidth;
            drawLine.endWidth = lineWidth;
        }

        if (Input.GetMouseButton(0) && canDraw)
        {
            Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), m_mouseRaycast.GetHitPosition(), Color.red);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Vector3 newPoint = m_mouseRaycast.GetHitPosition();

                // Vérifier si l'ajout du nouveau point ne dépasse pas la longueur maximale
                if (linePoints.Count > 0)
                {
                    float newSegmentLength = Vector3.Distance(linePoints[linePoints.Count - 1], newPoint);
                    if (GetTotalLineLength() + newSegmentLength > maxLineLength)
                    {
                        canDraw = false; // Arrêter le dessin
                        return;
                    }
                }

                // Ajouter le nouveau point
                linePoints.Add(newPoint);
                drawLine.positionCount = linePoints.Count;
                drawLine.SetPositions(linePoints.ToArray());

                timer = timerdelay;
            }

            if (Input.GetMouseButton(0))
            {
                Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), m_mouseRaycast.GetHitPosition(), Color.red);
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    linePoints.Add(m_mouseRaycast.GetHitPosition());
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

    float GetTotalLineLength()
    {
        float totalLength = 0f;

        // Calculer la longueur totale des segments de la ligne
        for (int i = 0; i < linePoints.Count - 1; i++)
        {
            totalLength += Vector3.Distance(linePoints[i], linePoints[i + 1]);
        }

        return totalLength;
    }

    void AddMeshColliderToLine()
    {
        // Créer un nouveau mesh pour la ligne
        Mesh lineMesh = new Mesh();
        GenerateMesh(lineMesh);

        // Ajouter un composant MeshCollider et lui attribuer le mesh
        MeshCollider meshCollider = newLine.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = lineMesh;
    }

    void GenerateMesh(Mesh mesh)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // Créer les sommets et triangles pour la ligne
        for (int i = 0; i < linePoints.Count - 1; i++)
        {
            Vector3 start = linePoints[i];
            Vector3 end = linePoints[i + 1];

            Vector3 direction = (end - start).normalized;
            Vector3 perpendicular = Vector3.Cross(direction, Vector3.forward) * lineWidth / 2;

            // Ajouter les sommets
            vertices.Add(start + perpendicular);
            vertices.Add(start - perpendicular);
            vertices.Add(end + perpendicular);
            vertices.Add(end - perpendicular);

            // Ajouter les triangles
            int baseIndex = i * 4;
            triangles.Add(baseIndex);
            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 2);

            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 3);
            triangles.Add(baseIndex + 2);
        }

        // Appliquer les sommets et triangles au mesh
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
    }
}
