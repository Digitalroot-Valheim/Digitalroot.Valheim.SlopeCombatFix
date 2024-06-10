using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn.Utils;
using System;

namespace Digitalroot.Valheim.SlopeCombatAssistance
{
  [BepInPlugin(Guid, Name, Version)]
  [NetworkCompatibility(CompatibilityLevel.VersionCheckOnly, VersionStrictness.Minor)]
  [BepInDependency(Jotunn.Main.ModGuid, "2.10.0")]
  [BepInIncompatibility("hitbox.fix")]
  [BepInIncompatibility("digitalroot.mods.slopecombatfix")]
  public partial class Main : BaseUnityPlugin
  {
    public static Main Instance;
    private Harmony _harmony;

    [UsedImplicitly] public static ConfigEntry<int> NexusId;
    public readonly ConfigEntry<float> Offset;
    public readonly ConfigEntry<float> Height;


    public Main()
    {
      Instance = this;
      NexusId = Config.Bind("General", "NexusID", 1569, new ConfigDescription("Nexus mod ID for updates", null, new ConfigurationManagerAttributes { Browsable = false, ReadOnly = true }));
      Offset = Config.Bind("General", "Offset", 0f, new ConfigDescription("Modify offset", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = true, Order = 1}));
      Height = Config.Bind("General", "Height", 1f, new ConfigDescription("Modify height (0.6 default, 1 mod default)", new AcceptableValueRange<float>(0f, 5f), new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = true, Order = 0}));
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
