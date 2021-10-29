using UnityEngine;

// The SceneReaction is used to change between scenes. Though there is a delay
// while the scene fades out, this is done with the SceneController class and so
// this is just a Reaction not a DelayedReaction
public class SceneReaction : Reaction 
{
    public string sceneName;
    public string startingPointInLoadedScene;
    public SaveData playerSaveData;

    private SceneController sceneController;

    protected override void SpecificInit()
    {
        sceneController = FindObjectOfType<SceneController>();
    }


    protected override void ImmediateReaction()
    {
        //Save the StartingPosition's name to the data asset.
        playerSaveData.Save(PlayerMovement.startingPositionKey, startingPointInLoadedScene);

        //Start the scene loading process.
        sceneController.FadeAndLoadScene(this);
    }
}