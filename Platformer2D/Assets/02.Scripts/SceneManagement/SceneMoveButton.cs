using UnityEngine;
public class SceneMoveButton : MonoBehaviour
{
    public void OnClick(string sceneName)
    {
        SceneMover.MoveTo(sceneName);
    }
}