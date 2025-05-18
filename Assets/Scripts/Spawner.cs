using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject[] levelPieces;
    

    public void SpawnNextPiece()
    {
        int i = Random.Range(0, levelPieces.Length);
        Instantiate(levelPieces[i], this.transform.position, Quaternion.identity);
    }

    private void Start()
    {
        SpawnNextPiece();
    }

}
