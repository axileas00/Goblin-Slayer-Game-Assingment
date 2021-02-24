using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawnReseter : MonoBehaviour
{
    [SerializeField] GameObject parent, spawner;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("AreaTrigger"))
        {
            parent = collision.gameObject.transform.parent.gameObject;// set parent

            GlobalConstables.GetGlobalConstables().GetRooms().Enqueue(parent);//Add rooms to a list so you can delete them later

            collision.gameObject.GetComponent<EnemySpawn>().enable = true;// enemy spawn 

            spawner = null;// next room spawn and animation happening

            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Debug.Log(parent.transform.GetChild(i).name);
                if (parent.transform.GetChild(i).name.Equals("NextRoomSpawner"))
                {
                    spawner = parent.transform.GetChild(i).gameObject;
                    Debug.Log("spawner " + spawner);
                }// Checks if the current room has a NextRoomSpawner gameobject
                else if(parent.transform.GetChild(i).name.Equals("BackDoor") || parent.transform.GetChild(i).name.Equals("FrontDoor"))
                {
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("SpawnedAndDestroyied", false);
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("Spawned", true);
                }// resets the animation booleans
            }

            if (GlobalConstables.GetGlobalConstables().GetHighscore() % 3 == 0)
            {
                Debug.Log(GlobalConstables.GetGlobalConstables().GetHighscore() % 3);
                GetComponent<PlayerCombat>().dmg += 5;
                collision.GetComponent<EnemySpawn>().hpSet += 10;
                collision.GetComponent<EnemySpawn>().dmgTaken += 5;
            }


            if (spawner != null && spawner.transform.parent.gameObject == parent)
            {
                GlobalConstables.GetGlobalConstables().SetSpawnedRoomsTo(0);
            }//if the nextroomspawner gameobject exists and has same parent as the room the next three rooms are being spawned
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("AreaTrigger"))
        {
            collision.gameObject.GetComponent<EnemySpawn>().enable = false;// enemy spawn 

            parent = collision.gameObject.transform.parent.gameObject;//set parent

            for (int i = 0; i < parent.transform.childCount; i++)
            {
                if (parent.transform.GetChild(i).name.Equals("BackDoor") && GlobalConstables.GetGlobalConstables().GetEnemies().Count == 0 && GlobalConstables.GetGlobalConstables().GetTotalEnemies() <= 0)
                {
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("SpawnedAndDestroyied", true);
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("Spawned", false);
                }
                else if (parent.transform.GetChild(i).name.Equals("HpPotion") && GlobalConstables.GetGlobalConstables().GetEnemies().Count == 0 && GlobalConstables.GetGlobalConstables().GetTotalEnemies() <= 0)
                {
                    parent.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                    parent.transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
                }
            }//sets the animation booleans
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("AreaTrigger"))
        {
            parent = collision.gameObject.transform.parent.gameObject;//set parent

            if (GlobalConstables.GetGlobalConstables().GetRooms().Count >= 6)
            {
                GameObject room;
                for(int i = 0; i < 3; i++)
                {
                    room = GlobalConstables.GetGlobalConstables().GetRooms().Peek();
                    GlobalConstables.GetGlobalConstables().GetRooms().Dequeue();
                    Destroy(room);
                }
            }//destroy rooms

            for (int i = 0; i < parent.transform.childCount; i++)
            {
                if (parent.transform.GetChild(i).name.Equals("FrontDoor")) 
                {
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("SpawnedAndDestroyied", false);
                    parent.transform.GetChild(i).GetComponent<Animator>().SetBool("Spawned", true);
                }
            }//sets the animation booleans

            GlobalConstables.GetGlobalConstables().IncreaseHighscoreBy(1);

            Destroy(collision.gameObject);
        }
    }
}