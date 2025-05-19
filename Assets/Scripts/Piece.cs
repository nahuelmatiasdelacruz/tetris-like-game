using UnityEngine;

public class Piece : MonoBehaviour
{
    
    bool IsValidPiecePosition()
    {
        foreach(Transform block in this.transform)
        {
            Vector2 position = GridHelper.RoundVector(block.position);
            if (!GridHelper.IsInsideBorders(position))
            {
                return false;
            }
            Transform possibleObject = GridHelper.grid[(int)position.x, (int)position.y];
            if (possibleObject != null && possibleObject.parent != this.transform)
            {
                return false;
            }
        }
        return true;
    }
}
