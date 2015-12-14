using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class World {
    private string WorldName;
    private int seed;
    private Thread WorldThread;
    private bool WorldThreadRunning = false;
    private List<Chunk> _Chunks = new List<Chunk>();

    public World(string WorldName, int seed)
    {
        this.WorldName = WorldName;
        this.seed = seed;
        SetUpWorld();
    }
    private void SetUpWorld()
    {
        Logger.Log("SetUpWorld()");
        WorldThreadRunning = true;
        WorldThread = new Thread(Tick);
        Logger.Log("Tick().Start();");
        WorldThread.Start();
    }
    private void Tick()
    {
        while (WorldThreadRunning)
        {
            try
            {
                if (_Chunks.Count < 1)
                {
                    _Chunks.Add(new Chunk(new Int2nd(0, 0), this));
                }
            }
            catch(System.Exception e)
            {
                Logger.Log("Error: " + e.Message);
                Logger.Log(e.StackTrace);
            }
        }
    }
    public void requestWorldThreadStop()
    {
        WorldThreadRunning = false;
    }

    public Thread GetWorldThread()
    {
        return WorldThread;
    }

    public string GetWorldName() { return WorldName; }

}
