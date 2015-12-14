using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;

public class WorldManager : MonoBehaviour {

    private static WorldManager _Instance;
    private World _World;
    private List<Delegate> _Rundels = new List<Delegate>();

    void Start () {
        string WorldName = "TestWorldName";
        int WorldSeed = WorldName.GetHashCode();
        Logger.Log("World [" + WorldName + "] with seed [" + WorldSeed.ToString() + "] Stating Up.");
        FileManager.SetUpFileManager(WorldName);
        _Instance = this;
        _World = new World(WorldName, WorldSeed);
        Logger.Log("World [" + WorldName + "] with seed [" + WorldSeed.ToString() + "] Created.");
        Block.Stone.GetBlockID();
        BlockRegistry.PrintAllBlocks();
    }

    public World GetWorld() { return _World; }

    public void RegisterDelegate(Delegate d)
    {
        _Rundels.Add(d);
    }
	
	void Update()
    {
        RunAllDelegates();
        Logger.WriteLog();
    }

    void RunAllDelegates()
    {
        List<Delegate> RundelCopy = null;
        lock (_Rundels)
        {
            RundelCopy = new List<Delegate>(_Rundels);
        }
        foreach(Delegate d in RundelCopy)
        {
            if(d != null)
            {
                try
                {
                    d.DynamicInvoke();
                }
                catch(System.Exception e)
                {
                    Logger.Log("Error: " + e.Message);
                    Logger.Log(e.StackTrace);
                    Debug.Log("Error: " + e.Message);
                    Debug.Log(e.StackTrace);
                }
                _Rundels.Remove(d);
            }
        }
    }

    void OnApplicationQuit()
    {
        Logger.Log("Application Closing");
        Logger.WriteLog();
        _World.requestWorldThreadStop();
        while (_World.GetWorldThread().IsAlive) ;
        Logger.Log("Application Succesfully Closed");
        Logger.WriteLog();
    }
    public static WorldManager GetInstance() { return _Instance; }
}
