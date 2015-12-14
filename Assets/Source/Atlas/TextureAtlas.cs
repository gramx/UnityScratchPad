using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class AltasCoordinate {
    public Vector4 Location;
    public string Name;
    public Vector2 TextureSize;
    public AltasCoordinate(string Name, Vector4 Location, Vector2 TextureSize) {
        this.Name = Name;
        this.Location = Location;
        this.TextureSize = TextureSize;
    }

    //This may be wrong needs to be checked over 25:00 in vid 5
    public Vector2[] ToUvMap() {
        return new Vector2[] {
            new Vector2((Location.x)/TextureSize.x,(Location.y)/TextureSize.y),
            new Vector2(((Location.x)/TextureSize.x),(Location.y + Location.w)/TextureSize.y),
            new Vector2((Location.x + Location.z)/TextureSize.x,((Location.y)/TextureSize.y)),
            new Vector2(((Location.x + Location.z)/TextureSize.x),((Location.y + Location.w)/TextureSize.y))
        };
    }
}

public class TextureAtlas {
    private static readonly int TextureWidth = 16;
    private static readonly int TextureHeight = 16;
    private static List<AltasCoordinate> _Coords = new List<AltasCoordinate>();
    private static Texture2D Atlas;

    public static void RegisterAtlas() {
        List<string> _Textures = new List<string>();
        string[] temp = Directory.GetFiles(FileManager.BlockTextures);
        foreach(string s in temp) {
            if (s.EndsWith(".png")) {
                _Textures.Add(s);
            }
        }
        //Fix this hard cast
        int WidthHeight = (int)Mathf.Ceil(Mathf.Sqrt(Mathf.Ceil(_Textures.Count)));
        WidthHeight *= TextureWidth;
        Texture2D AtlasTemp = new Texture2D(WidthHeight, WidthHeight, TextureFormat.ARGB32, false);
        int x = 0;
        int y = 0;
        foreach (string loc in _Textures) {
            if( x > (int)Mathf.Ceil(Mathf.Sqrt(Mathf.Ceil(_Textures.Count))) - 1) {
                x = 0;
                y += 1;
            }
            Texture2D texTemp = new Texture2D(0,0, TextureFormat.ARGB32, false);
            texTemp.LoadImage(File.ReadAllBytes(loc));
            if(texTemp.width == TextureWidth && texTemp.height == TextureHeight) {
                AtlasTemp.SetPixels(x * TextureWidth, y * TextureHeight, TextureWidth, TextureHeight, texTemp.GetPixels());
                string[] locat = loc.Split('/');
                string path = locat[locat.Length - 1];
                path = path.Split('.')[0];
                _Coords.Add(new AltasCoordinate(
                    path, 
                    new Vector4(x * TextureWidth, y * TextureHeight, TextureWidth, TextureHeight),
                    new Vector2(AtlasTemp.width, AtlasTemp.height)
                    ));
                x++;
            } else {
                System.Exception notFoundError = new System.Exception(string.Format("Texture geater then supported [{0}].", loc));
                Logger.Log("Error: " + notFoundError.Message);
                Logger.Log(notFoundError.StackTrace);
            }
        }
        //Debugging to see if works
        File.WriteAllBytes("Atlas.png",AtlasTemp.EncodeToPNG());

        Atlas = AtlasTemp;
    }

    public static AltasCoordinate GetCoordinate(string name) {
        if (_Coords.Count < 1) {
            RegisterAtlas();
        }
        foreach(AltasCoordinate cord in _Coords) {
            if (cord.Name.Equals(name)) {
                return cord;
            }
        }
        System.Exception notFoundError = new System.Exception(string.Format("Altas Texture with name [{0}] could not be found.", name));
        Logger.Log("Error: " + notFoundError.Message);
        Logger.Log(notFoundError.StackTrace);
        return _Coords[0];
    }

    public static Texture2D GetAtlas() {
        Texture2D Clone = new Texture2D(0, 0, TextureFormat.ARGB32, false);
        Clone.LoadImage(Atlas.EncodeToPNG());
        Clone.wrapMode = TextureWrapMode.Clamp;
        Clone.filterMode = FilterMode.Point;
        return Clone;
    }
}
