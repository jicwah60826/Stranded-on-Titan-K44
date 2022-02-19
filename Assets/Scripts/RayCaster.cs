 using UnityEngine;
 
 public class Raycaster : MonoBehaviour {
     void Update() {
         RaycastHit hit;
         if (Physics.Raycast(transform.position, -Vector3.up, out hit))
             hit.transform.SendMessage ("HitByRay");
         
     }
 }