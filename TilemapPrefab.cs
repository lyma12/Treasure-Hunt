using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class TilemapPrefab : MonoBehaviour
{
    [SerializeField]
    private GameObject finish;
    [SerializeField]
    private List<Monster> Monsters;
    [SerializeField]
    private Monster Boss;
    private GameObject player;
    [SerializeField]
    private Treasure treasurePrefab;
    [SerializeField]
    private Key key;
    [SerializeField]
    private List<GameObject> outsideObject = new List<GameObject>();
    private Queue<string> keyCode = new Queue<string>();
    public List<Vector2Int> floorPosition = new List<Vector2Int>();
    private List<Vector2Int> floorTilemap = new List<Vector2Int>();
    public Dictionary<Vector2Int, List<Vector2Int>> dictinary = new Dictionary<Vector2Int, List<Vector2Int>>();
    public List<Vector2Int> center = new List<Vector2Int>();
    private Dictionary<Vector2Int, int> diffirentRooms = new Dictionary<Vector2Int, int>();

    public void CreatePrefab()
    {

        keyCode = new Queue<string>();
        ClearGameObject();
        floorTilemap.Clear();
        if(outsideObject.Count > 0)
        CreateOutSideObject();
        foreach(var i in floorPosition)
        {
            bool choose = true;
            foreach(var j in Direction2D.eightDirectionsList)
            {
                Vector2Int tmp = i + j;
                if (!floorPosition.Contains(tmp))
                {
                    choose = false;
                    break;
                }
            }
            if (choose) floorTilemap.Add(i);
        }
        FindRoom();
    }
    
    public void FindRoom()
    {
        diffirentRooms.Clear();
        diffirentRooms.Add(center[0], 0);
        center = center.OrderBy(x => Vector2Int.Distance(x, center[0])).ToList();
        diffirentRooms.Add(center[center.Count - 1], 3);
        for(int i = 1; i < center.Count -1; i++)
        {
            int level = UnityEngine.Random.Range(1, 3);
            diffirentRooms.Add(center[i], level);
        }
        foreach(var i in diffirentRooms)
        {
            if (i.Value == 0)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3Int(i.Key.x, i.Key.y, 0); 
            }
            else if (i.Value == 1) FindTreasurePosition(i.Key, 1, 5);
            else if (i.Value == 2) FindTreasurePosition(i.Key, 2, 12);
            else if (i.Value == 3) LevelBoss(i.Key);
        }
        while(keyCode.Count > 0 && floorPosition.Count > 0)
        {
            floorPosition = floorPosition.OrderBy(x => Guid.NewGuid()).ToList();
            Key k = Instantiate(key, new Vector3Int(floorPosition[0].x, floorPosition[0].y, 0), Quaternion.identity);
            k.setCodeKey(keyCode.Peek());
            keyCode.Dequeue();
            floorPosition.RemoveAt(0);
        }
    }
    private void FindTreasurePosition(Vector2Int center, int treasure, int monsters)
    {
        List<Vector2Int> positionPrefab = new List<Vector2Int>();
        HashSet<Vector2Int> positionMonster = new HashSet<Vector2Int>();
        List<Vector2Int> positionFloor = dictinary[center].OrderBy(x => Guid.NewGuid()).ToList();
        foreach(var i in positionFloor)
        {
            if (floorTilemap.Contains(i))
            {
                positionPrefab.Add(i);
                floorTilemap.Remove(i);
                floorPosition.Remove(i);
            }
        }
        positionPrefab = positionPrefab.OrderBy(x => Guid.NewGuid()).ToList();
        int positionTreasure = 0;
        
        while(positionTreasure < treasure && positionTreasure < positionPrefab.Count)
        {
            Vector2Int tmp = positionPrefab[positionTreasure];
            Treasure treasureObject =  Instantiate(treasurePrefab, new Vector3Int(tmp.x, tmp.y, 0), Quaternion.identity);
            floorPosition.Remove(tmp);
            floorTilemap.Remove(tmp);
            treasureObject.setCodeKey(center.ToString() + positionTreasure.ToString());
            keyCode.Enqueue(center.ToString() + positionTreasure.ToString());
            do
            {
                HashSet<Vector2Int> po = ProceduralGenerationAlgorithms.SimpleRandomWalk(positionPrefab[positionTreasure], 2 * monsters);
                for (int i = 0; i < po.Count; i++)
                {
                    if (positionFloor.Contains(po.ElementAt(i))) positionMonster.Add(po.ElementAt(i));
                }
            } while (positionMonster.Count < monsters * 2);
            positionTreasure++;
        }
      
        do
        {
            List<Vector2Int> setPosition = positionMonster.OrderBy(x => Guid.NewGuid()).ToList();
            if (setPosition.Count > 0 && monsters > 0)
            {
                int monsterType = UnityEngine.Random.Range(0, Monsters.Count);
                Instantiate(Monsters[monsterType], new Vector3Int(setPosition[0].x, setPosition[0].y, 0), Quaternion.identity);
                floorPosition.Remove(setPosition[0]);
                floorTilemap.Remove(setPosition[0]);
                setPosition.RemoveAt(0);
                monsters--;
            }
            else break;
        } while (true);
        
    }
    private void LevelBoss(Vector2Int position)
    {
        Instantiate(Boss, new Vector3Int(position.x, position.y, 0), Quaternion.identity);
        Instantiate(finish, new Vector3Int(position.x + 2, position.y + 2, 0), Quaternion.identity);
        List<Vector2Int> mons = dictinary[position].OrderBy(x => Guid.NewGuid()).ToList();
        for(int i = 0; i< 4; i++)
        {
            int monsterType = UnityEngine.Random.Range(0, Monsters.Count);
            Instantiate(Monsters[monsterType], new Vector3Int(mons[0].x, mons[0].y, 0), Quaternion.identity);
            mons.RemoveAt(0);
        }
    }
    private void ClearGameObject()
    {
            var gameDestroyList = GameObject.FindGameObjectsWithTag("Monster");
            foreach (var monster in gameDestroyList) DestroyImmediate(monster);
            gameDestroyList = GameObject.FindGameObjectsWithTag("Key");
            foreach (var key in gameDestroyList) DestroyImmediate(key);
            gameDestroyList = GameObject.FindGameObjectsWithTag("Treasure");
            foreach (var treasure in gameDestroyList) DestroyImmediate(treasure);
            gameDestroyList = GameObject.FindGameObjectsWithTag("Icon");
            foreach (var treasure in gameDestroyList) DestroyImmediate(treasure);
       
    }
    private void CreateOutSideObject()
    {
        int house = UnityEngine.Random.Range(5, 10);
        List<Vector2Int> outSide = FindDirectionObjectOutSide().OrderBy(i => Guid.NewGuid()).ToList();
        foreach(var position in outSide)
        {
            int types = UnityEngine.Random.Range(0, outsideObject.Count);
            GameObject House = outsideObject[types];
            if (House.GetComponent<House>() != null && house > 0)
            {
                Instantiate(outsideObject[types], new Vector3Int(position.x, position.y, 0), Quaternion.identity);
                house--;
            }
            else if(House.GetComponent<House>() == null)
            {
                Instantiate(outsideObject[types], new Vector3Int(position.x, position.y, 0), Quaternion.identity);
            }
        }
    }
    private HashSet<Vector2Int> FindDirectionObjectOutSide()
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPosition)
        {
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPosition.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition);
            }
        }
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
        int cum = UnityEngine.Random.Range(10, 20);
        List<Vector2Int> choose = wallPositions.OrderBy(x => Guid.NewGuid()).ToList();
        if(cum < choose.Count)
        {
            for (int i = cum; i < choose.Count; i++) choose.RemoveAt(i);
        }
        foreach(var p in choose)
        {
            HashSet<Vector2Int> tmp = ProceduralGenerationAlgorithms.SimpleRandomWalk(p, 3);
            foreach(var j in tmp)
            {
                if (!floorPosition.Contains(j)) positions.Add(j);
            }
        }
        return positions;
    }
}
