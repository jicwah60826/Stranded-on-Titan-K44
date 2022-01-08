using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Object[] textures;


private void Awake() {
    textures = Resources.LoadAll("Textures", typeof(Texture2D));
}
    // Start is called before the first frame update
    void Start()
    {
        // Hide / Lock cursor movement in game window
        Cursor.lockState = CursorLockMode.Locked;
    }
}
