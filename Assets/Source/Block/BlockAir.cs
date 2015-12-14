using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BlockAir : Block
{
    public BlockAir(string name, bool IsTrans) :base (name, IsTrans)
    {

    }
    public override MeshData Draw(Chunk chunk, int x, int y, int z, World world)
    {
        return new MeshData();
    }
}