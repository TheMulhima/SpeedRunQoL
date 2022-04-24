using System.Collections;
using System.Linq;
using DebugMod;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpeedRunQoL.Functionality
{
    public static class ForceUumuuExtraAttack
    {
        
        private static bool fsmToggle = false;
        public static void ToggleExtraAttack()
        {
            if (!fsmToggle)
            {
                //this is called cuz if your already in uumuu fight and you want extra attack then it will happen
                SetUumuuExtraAttack(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                
                UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SetUumuuExtraAttack;
                Console.AddLine("Uumuu forced extra attack ON");
                fsmToggle = true;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SetUumuuExtraAttack;
                Console.AddLine("Uumuu forced extra attack OFF");
                fsmToggle = false;
            }
        }
       
        private static string[] UumuuScenes = new[] {"Fungus3_archive_02"};

        private static void SetUumuuExtraAttack(Scene sceneFrom, Scene sceneTo) => SetUumuuExtraAttack(sceneTo.name);
        private static void SetUumuuExtraAttack(string NextSceneName)
        {
            if (UumuuScenes.Contains(NextSceneName))
            {
                GameManager.instance.StartCoroutine(UumuuExtraAttackCoro(NextSceneName));
            }
        }

        private static IEnumerator UumuuExtraAttackCoro(string activeScene)
        {
            Console.AddLine("Uumuu set extra attack coro launched");
            yield return new WaitUntil(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == activeScene);

            // Find Uumuu, their FSM, the specific State, and the Action that dictates the possibility of extra attacks
            GameObject uumuu = GameObject.Find("Mega Jellyfish");
            PlayMakerFSM fsm = uumuu.LocateMyFSM("Mega Jellyfish");
            FsmState fsmState = fsm.FsmStates.First(t => t.Name == "Idle");
            WaitRandom waitRandom = (WaitRandom) fsmState.Actions.OfType<WaitRandom>().First();
            waitRandom.timeMax.Value = 1.6f;
            yield break;
        }
    }
}