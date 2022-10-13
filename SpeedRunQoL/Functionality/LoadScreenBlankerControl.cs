using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpeedRunQoL.Functionality
{
    internal static class LoadScreenBlankerControl
    {
        public static bool hidingLoadScreens;

        public static void DisableRandomBlankers(Scene s)
        {
            if (!hidingLoadScreens || GameManager.instance == null)
            {
                return;
            }
            
            var blankers = s.GetRootGameObjects().SelectMany(go => go.GetComponentsInChildren<PlayMakerFSM>())
                .Where(fsm => fsm.FsmName == "Blanker Control");
            foreach (var blankerFsm in blankers)
            {
                blankerFsm.enabled = false;
                blankerFsm.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}