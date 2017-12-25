using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	void Start ()
    {
        LevelManager.Instance.LoadNextLevel(null);
	}
}
