using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private RoomFirstDungeonGenerator dungeon;

    public void changeRoom()
    {
        dungeon.change();
    }
}
