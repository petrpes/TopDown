using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : Vision
{
    protected override bool IsImportant(GameObject gameObject)
    {
        return true;
    }
}

