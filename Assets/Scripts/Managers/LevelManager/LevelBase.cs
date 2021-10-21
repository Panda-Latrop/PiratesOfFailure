using UnityEngine;

public class LevelBase : MonoBehaviour {

    [System.Serializable]
    public struct LevelStucture
    {
        public string nameOfTypeLevel;
        public LevelSegment[] trapSegments;
        public LevelSegmentSpawn[] spawnSegments;
    }
    public LevelStucture[] levelType ;
}
