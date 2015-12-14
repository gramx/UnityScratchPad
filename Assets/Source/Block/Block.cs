using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Block {
    public static readonly Block Air = new BlockAir("Air", true);
    public static readonly Block Stone = new Block("Stone", "Stone");
    public static readonly Block Dirt = new Block("Dirt", "Dirt");
    public static readonly Block Grass = new Block("Grass", "Grass");
    public static readonly Block Ice = new Block("Ice", "Ice");
    public static readonly Block OtherBlock = new Block("OtherBlock", "OtherBlock");
    public static readonly Block DarkOutline = new Block("DarkOutline", "DarkOutline");
    public static readonly Block LightOutline = new Block("LightOutline", "LightOutline");
    public static readonly List<Vector2> BasicUv = new List<Vector2>() {
        new Vector2(0,0), new Vector2(0,1), new Vector2(1,0), new Vector2(1,1)
    };

    private string BlockName;
    private int BlockID;
    private bool _IsTransparent = false;
    private Vector2[] UvMap;

    public Block(string BlockName) {
        this.BlockName = BlockName;
        BlockID = BlockRegistry.RegisterBlock(this);
    }

    public Block(string BlockName, bool IsTrans) : this(BlockName) {
        _IsTransparent = IsTrans;
    }

    public Block(string BlockName, string TextureName) : this(BlockName) {
        UvMap = TextureAtlas.GetCoordinate(TextureName).ToUvMap();
    }

    public string GetBlockName()
    {
        return BlockName;
    }

    public int GetBlockID()
    {
        return BlockID;
    }

    public bool IsTransparent() {
        return _IsTransparent;
    }

    public virtual MeshData Draw(Chunk chunk, int x, int y, int z, World world) {
        //return MathHelper.DrawBlock(chunk, x, y, z, world, new List<Vector2>(BasicUv)); //should this be a new list???

        //Could have an if null check around this
        return MathHelper.DrawBlock(chunk, x, y, z, world, new List<Vector2>(UvMap));
    }
}
