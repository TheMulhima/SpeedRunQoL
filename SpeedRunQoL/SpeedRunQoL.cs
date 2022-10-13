using System;
using System.Linq;
using Modding;
using SpeedRunQoL.Functionality;

namespace SpeedRunQoL
{
    //debug doesnt do any of the dll loading so the mod needs to inherit from "Mod" so the modding api loads it
    public class SpeedRunQoL: Mod
    {
        public override string GetVersion() => "v0.56"; // shoutouts
        internal static SpeedRunQoL instance { get; private set; }
        public override void Initialize()
        {
            if (instance == null) instance = this;
            //checks whether debug is loaded. probably could also use
            //"ModHooks.GetMod("DebugMod") is Mod" or
            //"ModHooks.LoadedModsWithVersions.ContainsKey("DebugMod")
            if (AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetName().Name).Contains("DebugMod"))
            {
                AddStuffToDebug();
            };

            ModHooks.HeroUpdateHook += VisualStateViewer.CheckForFlashConditions;

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (s, _) => LoadScreenBlankerControl.DisableRandomBlankers(s);
        }

        //making this a different function is the way that the mod doesnt fail to load when debug isnt downloaded.
        //not so relevant here but doesnt hurt to include it
        private void AddStuffToDebug()
        {
            //The function that needs to be called to add keybinds
            //the function exists in DebugMod.cs if you wanna see what it does
            // the parameter is takes the the Typeof(ClassMade)
            DebugMod.DebugMod.AddToKeyBindList(typeof(KeyBinds));
        }
    }
}