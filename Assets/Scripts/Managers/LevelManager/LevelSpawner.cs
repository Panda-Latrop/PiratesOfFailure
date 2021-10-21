using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour {

    public LevelBase LB;
    public Transform levelConteiner;
    List<LevelSegment> segments = new List<LevelSegment>();
    List<LevelSegmentSpawn> segmentsSpawn = new List<LevelSegmentSpawn>();
    public void Start()
    {
        CreatingLevel(GameSettingManager.Instance.SLP.currentLoadLevel);
    }

    public void CreatingLevel(int _levelId)
    {
        int levelId = _levelId;
        List<LevelProperty> preSegments = LevelList.Instance.levels[levelId].Segments;
        if (preSegments[0].spawn)
        {
            segments.Add(Instantiate(LB.levelType[preSegments[0].levelTypeId].spawnSegments[preSegments[0].segmentId], Vector2.zero, Quaternion.identity) as LevelSegment);
            segmentsSpawn.Add(segments[0] as LevelSegmentSpawn);
        }
        else
        {
            segments.Add(Instantiate(LB.levelType[preSegments[0].levelTypeId].trapSegments[preSegments[0].segmentId], Vector2.zero, Quaternion.identity) as LevelSegment);
        }
        segments[0].gameObject.transform.position = Vector2.zero;
        segments[0].idInArr = 0;
        segments[0].gameObject.transform.parent = levelConteiner.transform;

        for (int i = 1; i < preSegments.Count; i++)
        {
            Vector2 tmpPos = new Vector2(segments[i - 1].right.position.x, segments[i - 1].right.position.y);// + preSegments[i].posNearNeighbor * 3.2f);
            if (preSegments[i].spawn)
            {
                segments.Add(Instantiate(LB.levelType[preSegments[i].levelTypeId].spawnSegments[preSegments[i].segmentId], Vector2.zero, Quaternion.identity) as LevelSegment);
                segmentsSpawn.Add(segments[i] as LevelSegmentSpawn);
            }
            else
            {
                segments.Add(Instantiate(LB.levelType[preSegments[i].levelTypeId].trapSegments[preSegments[i].segmentId], Vector2.zero, Quaternion.identity) as LevelSegment);
            }
            segments[i].gameObject.transform.position = tmpPos;
            segments[i].idInArr = i;
            segments[i].gameObject.transform.parent = levelConteiner.transform;
        }
    }
    public void UnitSegmentLocationCheck(int id)
    {
        if (id - 1 >= 0)
            segments[id - 1].unitNear = true;
        if (id + 1 < segments.Count)
            segments[id + 1].unitNear = true;
    }
    public void SegmentSelfCheck(int id)
    {
        if (id - 1 >= 0 && id + 1 < segments.Count)
        {
            if (!segments[id - 1].unitHere && !segments[id + 1].unitHere)
            {
                segments[id].unitNear = false;
            }
        }
        else
        {
            if ((id == segments.Count - 1) && !segments[id - 1].unitHere)
            {
                segments[id].unitNear = false;
            }
            if ((id == 0) && !segments[id + 1].unitHere)
            {
                segments[id].unitNear = false;
            }
        }
    }
    public void UdateSpawnPoint(LevelSegmentSpawn lss)
    {
        for (int i = 0; i < segmentsSpawn.Count; i++)
        {
            if (segmentsSpawn[i] != lss)
            {
                segmentsSpawn[i].activeSpawnPoint = false;
            }
            else
            {
                groupManager.Instance.spawnPoint = lss.spawnPoint.position;
                groupManager.Instance.timeToSpawn = lss.timeToSpawn;
                if (!groupManager.Instance.coroutineStart)
                {
                    StartCoroutine(groupManager.Instance.WaitTimeToSpawn());
                }
            }
        }
    }
}
