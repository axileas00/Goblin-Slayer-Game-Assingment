using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoomSpawnReseter : MonoBehaviour
{
    [SerializeField] GameObject parent, spawner;

    int waitTime = 500;// the wait time for the async
    public async void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("AreaTrigger"))
        {
            //se he wont be able to go back before the door closes behond him
            GetComponent<move>().able = false;

            // set parent
            parent = collision.gameObject.transform.parent.gameObject;

            //Add rooms to a list so you can delete them later
            GlobalConstables.Rooms.Enqueue(parent);

            // enemy spawn 
            collision.gameObject.GetComponent<EnemySpawn>().enable = true;

            // next room spawn and animation happening
            spawner = null;
            
            for(int i = 0; i < parent.transform.childCount - 1; i++)
            {
                if (parent.transform.GetChild(i).name.Equals("NextRoomSpawner"))// Checks if the current room has a NextRoomSpawner gameobject
                {
                    spawner = parent.transform.GetChild(i).gameObject;
                    Debug.Log("spawner " + spawner);
                }
                else if(parent.transform.GetChild(i).name.Equals("BackDoor") || parent.transform.GetChild(i).name.Equals("FrontDoor"))// resets the animation booleans
                {
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("SpawnedAndDestroyied", false);
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("Spawned", true);
                }
            }
            Debug.Log("parent " + parent);
            
            if (spawner != null && spawner.transform.parent.gameObject == parent)//if the nextroomspawner gameobject exists and has same parent as the room the next trhee rooms are being spawned
            {
                GlobalConstables.spawnedRooms = 0;
            }
            Debug.Log(GlobalConstables.spawnedRooms);

            await Task.Delay(waitTime);
            GetComponent<move>().able = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("AreaTrigger"))
        {
            // enemy spawn 
            collision.gameObject.GetComponent<EnemySpawn>().enable = false;

            //set parent
            parent = collision.gameObject.transform.parent.gameObject;

            for (int i = 0; i < parent.transform.childCount - 1; i++)
            {
                if (parent.transform.GetChild(i).name.Equals("BackDoor") && GlobalConstables.enemies.Count == 0 && GlobalConstables.totalEnemies == 0)//sets the animation booleans
                {
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("SpawnedAndDestroyied", true);
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("Spawned", false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("AreaTrigger"))
        {
            //set parent
            parent = collision.gameObject.transform.parent.gameObject;

            //destroy rooms
            if(GlobalConstables.Rooms.Count >= 6)
            {
                GameObject room;
                for(int i = 0; i < 3; i++)
                {
                    room = GlobalConstables.Rooms.Peek();
                    GlobalConstables.Rooms.Dequeue();
                    Destroy(room);
                }
            }

            for (int i = 0; i < parent.transform.childCount - 1; i++)
            {
                if (parent.transform.GetChild(i).name.Equals("FrontDoor")) //sets the animation booleans
                {
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("SpawnedAndDestroyied", false);
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("Spawned", true);
                }
            }

            GlobalConstables.Highscore += 1;

            Destroy(collision.gameObject);
        }
    }
}