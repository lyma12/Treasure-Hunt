using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private TilemapPrefab tilemapPrefab;
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;

    public void Start()
    {
            tilemapPrefab.dictinary = new Dictionary<Vector2Int, List<Vector2Int>>();
            tilemapPrefab.center = new List<Vector2Int>();
            tilemapVisualizer.Clear();
            RunProceduralGeneration();
    }
    protected override void RunProceduralGeneration()
    {
        tilemapPrefab.dictinary = new Dictionary<Vector2Int, List<Vector2Int>>();
        tilemapPrefab.center = new List<Vector2Int>();
        tilemapVisualizer.Clear();
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        

        CreateCorridors(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        foreach (var i in deadEnds) potentialRoomPositions.Add(i);
        tilemapPrefab.center = potentialRoomPositions.OrderBy(i => i.x).ToList();
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        tilemapPrefab.floorPosition = floorPositions.ToList();
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        tilemapPrefab.CreatePrefab();
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                tilemapPrefab.dictinary[position] = room.ToList();
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
                
            }
            if (neighboursCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        foreach (var i in deadEnds)
        {
            if(!tilemapPrefab.dictinary.ContainsKey(i))
            tilemapPrefab.dictinary.Add(i, new List<Vector2Int>());
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        // ClearRoomData();
        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            tilemapPrefab.dictinary[roomPosition] = roomFloor.ToList();
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        foreach(var i in potentialRoomPositions)
        {
            tilemapPrefab.dictinary.Add(i, new List<Vector2Int>());
        }
    }
    
    

}
