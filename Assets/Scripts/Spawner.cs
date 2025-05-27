using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] levelPieces;
    public GameObject currentPiece;
    public GameObject nextPiece;
    public void SpawnNextPiece()
    {
        currentPiece = nextPiece;
        Piece pieceScript = currentPiece.GetComponent<Piece>();
        pieceScript.enabled = true;
        pieceScript.ChangeBlocksTransparency(1.0f);
        StartCoroutine(PrepareNextPiece());
    }

    private void Start()
    {
        nextPiece = Instantiate(levelPieces[Random.Range(0, levelPieces.Length)], this.transform.position, Quaternion.identity);
        SpawnNextPiece();
    }

    IEnumerator PrepareNextPiece()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        int i = Random.Range(0, levelPieces.Length);
        nextPiece = Instantiate(levelPieces[i], this.transform.position, Quaternion.identity);
        Piece pieceScript = nextPiece.GetComponent<Piece>();
        pieceScript.enabled = false;
        pieceScript.ChangeBlocksTransparency(0.2f);
    }
}
