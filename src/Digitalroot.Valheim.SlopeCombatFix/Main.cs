using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace Digitalroot.Valheim.SlopeCombatFix
{
  [BepInPlugin(Guid, Name, Version)]
  [BepInIncompatibility("hitbox.fix")]
  public class Main : BaseUnityPlugin
  {
    public const string Version = "2.0.0";
    public const string Name = "Digitalroot SlopeCombatFix";
    public const string Guid = "digitalroot.mods.slopecombatfix";
    public const string Namespace = "Digitalroot.Valheim" + nameof(SlopeCombatFix);
    public static Main Instance;
    private Harmony _harmony;

    public ConfigEntry<float> Offset;
    public ConfigEntry<float> Height;


    public Main()
    {
      Instance = this;
      Offset = Config.Bind<float>("Modify offset", "offset", 0f, "Offset");
      Height = Config.Bind<float>("Modify height (0.6 default, 1 mod default)", "height", 1f, "Height");
    }

    private void Awake()
    {
      try
      {
        _harmony = Harmony.CreateAndPatchAll(typeof(Main).Assembly, Guid);
      }
      catch (Exception e)
      {
        Logger.LogError(e);
      }
    }

    private void OnDestroy()
    {
      try
      {
        _harmony?.UnpatchSelf();
      }
      catch (Exception e)
      {
        Logger.LogError(e);
      }
    }
  }
}
