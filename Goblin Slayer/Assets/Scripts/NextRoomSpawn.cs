using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomSpawn : MonoBehaviour
{
    // Pick a random prefab room and continue the stage

    [SerializeField] private GameObject[] nextRoom;

    void Update()
    {
        if (GlobalConstables.GetGlobalConstables().GetSpawnedRooms() < 3)
        {
            Picker();
        }
    }

    private void Picker()
    {
        int roomNmb = Random.Range(0, nextRoom.Length);
        Instantiate(nextRoom[roomNmb], transform.position, Quaternion.identity);
        GlobalConstables.GetGlobalConstables().IncreaseSpawnedRoomsBy(1);
        Destroy(this.gameObject);
        //gameObject.SetActive(false);
        //transform.position = new Vector3(transform.position.x + 170, 0, 0);
    }//A method that spawns new rooms
}
