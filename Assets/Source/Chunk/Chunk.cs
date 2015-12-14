using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using System.Text.RegularExpressions;

public class Chunk {
    /// <summary>
    /// Chunk size is how many voxels fit across within one square meter.
    /// Please use base 2 numbers like 16, 32 and 64 for fast execution.
    /// Note that textures do not work well based on the size.
    /// </summary>
    public static Int16 ChunkSize = 16;
    public static int ChunkWidth = 16;
    public static int ChunkHeight = 16;
    private Int2nd Location;
    private World world;
    private bool HasGenerated = false;
    private bool HasDrawn = false;
    private Block[,,] _Blocks = new Block[ChunkWidth, ChunkHeight, ChunkWidth];
    private Transform ChunkTransform;

    public delegate void Rundel(); 

    public Chunk(Int2nd Location, World world)
    {
        this.Location = Location;
        this.world = world;
        Thread t = new Thread(Generate);
        //t.Priority = System.Threading.ThreadPriority.Highest;
        t.Start();
    }

    public void Generate()
    {
        try
        {
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int y = 0; y < ChunkHeight; y++)
                {
                    for (int z = 0; z < ChunkWidth; z++)
                    {
                        System.Random random = new System.Random(Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value));
                        switch  (random.Next(10)) {
                            case 1:
                                _Blocks[x, y, z] = Block.OtherBlock;
                                break;
                            case 2:
                                _Blocks[x, y, z] = Block.DarkOutline;
                                break;
                            case 3:
                                _Blocks[x, y, z] = Block.LightOutline;
                                break;
                            case 4:
                                _Blocks[x, y, z] = Block.Grass;
                                break;
                            case 5:
                                _Blocks[x, y, z] = Block.Ice;
                                break;
                            case 6:
                                _Blocks[x, y, z] = Block.Dirt;
                                break;
                            case 7:
                                _Blocks[x, y, z] = Block.Stone;
                                break;
                            default:
                                _Blocks[x, y, z] = Block.Air;
                                break;
                        }
                        //if (random.Next(10) > 5)
                        //{
                        //    _Blocks[x, y, z] = Block.OtherBlock;
                        //}
                        //else
                        //{
                        //    _Blocks[x, y, z] = Block.Stone;
                        //}
                    }
                }
            }
            Draw();
        }
        catch (System.Exception e)
        {
            Logger.Log("Error: " + e.Message);
            Logger.Log(e.StackTrace);
            Debug.Log("Error: " + e.Message);
            Debug.Log(e.StackTrace);
        }
        HasGenerated = true;
    }

    public Block GetBlock(int x, int y, int z)
    {
        return _Blocks[x, y, z];
    }

    private MeshData _Chuckmesh;

    public void Draw()
    {
        try
        {
            _Chuckmesh = new MeshData();
            HasDrawn = true;
            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int y = 0; y < ChunkHeight; y++)
                {
                    for (int z = 0; z < ChunkWidth; z++)
                    {
                        _Chuckmesh.AddMeshData(_Blocks[x, y, z].Draw(this, x, y, z, world));
                    }
                }
            }
            WorldManager.GetInstance().RegisterDelegate(new Rundel(_Draw));
        }
        catch (System.Exception e)
        {
            Logger.Log("Error: " + e.Message);
            Logger.Log(e.StackTrace);
            Debug.Log("Error: " + e.Message);
            Debug.Log(e.StackTrace);
        }
    }

    //Unity's Draw
    private void _Draw()
    {
        if (ChunkTransform == null)
        {
            ChunkTransform = Transform.Instantiate(Resources.Load<Transform>("Chunk"), Location.ToVector3(), Quaternion.identity) as Transform;
            ChunkTransform.name = "Chunk:" + Location.ToVector3().ToString();
            ChunkTransform.transform.GetComponent<Renderer>().material.mainTexture = TextureAtlas.GetAtlas();
        }
        Mesh mesh = _Chuckmesh.ToMesh(ChunkSize);
        ChunkTransform.transform.GetComponent<MeshCollider>().sharedMesh = mesh;
        ChunkTransform.transform.GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
