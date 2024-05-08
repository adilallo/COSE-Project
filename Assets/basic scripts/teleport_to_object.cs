using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport_to_object : MonoBehaviour

{

public Transform player, destination;
public GameObject playerg;
 
 void OnTriggerEnter(Collider other){
  if(other.CompareTag("Player")){
   playerg.SetActive(false);
   player.position = destination.position;
   playerg.SetActive(true);
  }
 }
}