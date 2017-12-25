using UnityEngine;

public static class CameraPositionCalculator
{
    public static Vector2 CalculatePosition(Vector2 cameraSize, Rect sceneRectUnits, Vector3 targetPosition)
    {
        if (sceneRectUnits.Equals(Rect.zero))
        {
            return Vector3.zero;
        }

        Vector2 cameraHalfSize = cameraSize / 2f;
        Vector2 sceneHalfSize = sceneRectUnits.size / 2f;

        if (targetPosition.x > sceneRectUnits.xMax)
        {
            targetPosition.x = sceneRectUnits.xMax - 0.0001f;
        }
        else if (targetPosition.x < sceneRectUnits.xMin)
        {
            targetPosition.x = sceneRectUnits.xMin + 0.0001f;
        }

        if (targetPosition.y > sceneRectUnits.yMax)
        {
            targetPosition.y = sceneRectUnits.yMax - 0.0001f;
        }
        else if (targetPosition.y < sceneRectUnits.yMin)
        {
            targetPosition.y = sceneRectUnits.yMin + 0.0001f;
        }

        float positionX = Mathf.Min(cameraHalfSize.x, sceneHalfSize.x);
        float positionY = Mathf.Min(cameraHalfSize.y, sceneHalfSize.y);
        float sizeX = Mathf.Max(sceneRectUnits.size.x - cameraSize.x, 0);
        float sizeY = Mathf.Max(sceneRectUnits.size.y - cameraSize.y, 0);

        Rect cameraMovingRectX = new Rect(positionX, 0, sizeX, sceneRectUnits.size.y);
        Rect cameraMovingRectY = new Rect(0, positionY, sceneRectUnits.size.x, sizeY);

        cameraMovingRectX.position += sceneRectUnits.position;
        cameraMovingRectY.position += sceneRectUnits.position;

        Vector2 cameraPosition = Vector3.zero;

        if (cameraMovingRectX.Contains(targetPosition))
        {
            cameraPosition.x = targetPosition.x;
        }
        else
        {
            cameraPosition.x =
                targetPosition.x < cameraMovingRectX.min.x ?
                cameraMovingRectX.min.x :
                cameraMovingRectX.max.x;
        }

        if (cameraMovingRectY.Contains(targetPosition))
        {
            cameraPosition.y = targetPosition.y;
        }
        else
        {
            cameraPosition.y =
                targetPosition.y < cameraMovingRectY.min.y ?
                cameraMovingRectY.min.y :
                cameraMovingRectY.max.y;
        }

        return cameraPosition;
    }
}

