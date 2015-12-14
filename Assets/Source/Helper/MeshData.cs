using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MeshData
{
    private List<Vector3> Vertices = new List<Vector3>();
    private List<int> Tris = new List<int>();
    private List<Vector2> Uvs = new List<Vector2>();

    public MeshData()
    {

    }

    public MeshData(List<Vector3> Vertices, List<int> Tris, List<Vector2> Uvs)
    {
        this.Vertices = Vertices;
        this.Tris = Tris;
        this.Uvs = new List<Vector2>();
        this.Uvs.AddRange(Uvs);
        //this.Uvs = Uvs;
    }

    public void AddPos(Vector3 v)
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vertices[i] += v;
        }
    }

    public void AddMeshData(MeshData toad)
    {
        if(toad.Vertices.Count < 1)
        {
            return;
        }
        if (Vertices.Count < 1)
        {
            Vertices = toad.Vertices;
            Tris = toad.Tris;
            Uvs = toad.Uvs;
            return;
        }
        int count = Vertices.Count;
        Vertices.AddRange(toad.Vertices);
        foreach (int i in toad.Tris)
        {
            Tris.Add(i + count);
        }
        Uvs.AddRange(toad.Uvs);
    }

    /// <summary>
    /// Srinks a Vector down to unitiy crappy float sizes
    /// </summary>
    /// <param name="shrinkBy">The amout to shink by, please use a base 2 number for speed (1, 2, 4, 8, 16, 32, 64, 128)</param>
    /// <returns></returns>
    private Vector3[] GetMiniVertices(Int16 shrinkBy)
    {
        return Vertices.ConvertAll<Vector3>(big => new Vector3(big.x / shrinkBy, big.y / shrinkBy, big.z / shrinkBy)).ToArray();
    }

    /// <summary>
    /// Make Vectors into a Unity mesh object
    /// </summary>
    /// <param name="shrinkBy">The amout to shink by, please use a base 2 number for speed (1, 2, 4, 8, 16, 32, 64, 128)</param>
    /// <returns></returns>
    public Mesh ToMesh(Int16 shrinkBy)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = GetMiniVertices(shrinkBy);
        mesh.triangles = Tris.ToArray();
        mesh.uv = Uvs.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.Optimize();
        return mesh;
    }
}
