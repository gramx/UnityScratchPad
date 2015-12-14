using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockRegistry
{
    private static List<Block> _Blocks = new List<Block>();

    public static int RegisterBlock(Block b)
    {
        _Blocks.Add(b);
        return _Blocks.Count - 1;
    }

    public static void PrintAllBlocks()
    {
        Logger.Log("Registerd Blocks");
        foreach (Block b in _Blocks)
        {
            Logger.Log(string.Format("ID:{0},Name:{1}", b.GetBlockID(), b.GetBlockName()));
        }
        Logger.Log(" ");
    }
}
