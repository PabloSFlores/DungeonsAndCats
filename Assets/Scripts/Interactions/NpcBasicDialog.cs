using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBasicDialog : BasicInteraction
{
    public string[] dialog;
    public string name;
    public Sprite image;
    int dialogCounter;
    GameManager gameManager;
    /*RandomPatrol randomPatrol;*/

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        /*randomPatrol = GetComponent<RandomPatrol>();*/
    }

    public override bool Interact(Vector2 playerFacing, Vector2 playerPos)
    {
        bool success = FacingNPC(playerFacing, playerPos, transform.position);

        if (success)
        {
            /*randomPatrol.FacePlayer(playerPos);*/
            NextDialog();
        }
        else EndDialog();

        return success;
    }

    private void NextDialog()
    {
        if (dialogCounter == dialog.Length)
        {
            EndDialog();
        }
        else
        {
            gameManager.ShowTextNpc(dialog[dialogCounter], name, image);
            dialogCounter++;
        }
    }

    private void EndDialog()
    {
        gameManager.HideTextNpc();
        dialogCounter = 0;
    }
}
