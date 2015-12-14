using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Int2nd
{
    public int x;
    public int z;
    public Int2nd(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(x, 0, z);
    }
    public Vector2 ToVector2()
    {
        return new Vector3(x, z);
    }
}

public class MathHelper {


    public static MeshData DrawBlock(Chunk chunk, int x, int y, int z, World world, List<Vector2> UVs)
    {
        MeshData tmp = new MeshData();

        if (y < 1 || chunk.GetBlock(x, y - 1, z).IsTransparent())
        {
            tmp.AddMeshData(new MeshData(
                new List<Vector3>() {
                new Vector3(0,0,0),
                new Vector3(0,0,1),
                new Vector3(1,0,0),
                new Vector3(1,0,1)

                },
                new List<int>() { 0, 2, 1, 3, 1, 2 },
                new List<Vector2>(UVs)
                ));
        }
        if (y > Chunk.ChunkHeight - 2 || chunk.GetBlock(x, y + 1, z).IsTransparent())
        {
            tmp.AddMeshData(new MeshData(
               new List<Vector3>() {
                new Vector3(0,1,0),
                new Vector3(0,1,1),
                new Vector3(1,1,0),
                new Vector3(1,1,1)

                },
               new List<int>() { 0, 1, 2, 3, 2, 1 },
               new List<Vector2>(UVs)
               ));
        }
        if (x > Chunk.ChunkWidth - 2 || chunk.GetBlock(x + 1, y, z).IsTransparent())
        {
            tmp.AddMeshData(new MeshData(
               new List<Vector3>() {
                new Vector3(1,0,1),
                new Vector3(1,1,1),
                new Vector3(1,0,0),
                new Vector3(1,1,0)

                },
               new List<int>() { 0, 2, 1, 3, 1, 2 },
               new List<Vector2>(UVs)
               ));
        }
        if (x < 1 || chunk.GetBlock(x - 1, y, z).IsTransparent())
        {
            tmp.AddMeshData(new MeshData(
               new List<Vector3>() {
                new Vector3(0,0,1),
                new Vector3(0,1,1),
                new Vector3(0,0,0),
                new Vector3(0,1,0)

                },
               new List<int>() { 0, 1, 2, 3, 2, 1 },
               new List<Vector2>(UVs)
               ));
        }
        if (z > Chunk.ChunkWidth - 2 || chunk.GetBlock(x, y, z + 1).IsTransparent())
        {
            tmp.AddMeshData(new MeshData(
               new List<Vector3>() {
                new Vector3(1,0,1),
                new Vector3(1,1,1),
                new Vector3(0,0,1),
                new Vector3(0,1,1)

                },
               new List<int>() { 0, 1, 2, 3, 2, 1 },
               new List<Vector2>(UVs)
               ));
        }
        if (z < 1 || chunk.GetBlock(x, y, z - 1).IsTransparent())
        {
            tmp.AddMeshData(new MeshData(
               new List<Vector3>() {
                new Vector3(1,0,0),
                new Vector3(1,1,0),
                new Vector3(0,0,0),
                new Vector3(0,1,0)

                },
               new List<int>() { 0, 2, 1, 3, 1, 2 },
               new List<Vector2>(UVs)
               ));
        }

        tmp.AddPos(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        return tmp;


    }

}
