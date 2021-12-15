using HarmonyLib;

namespace Digitalroot.Valheim.SlopeCombatFix
{
  public static class Patch
  {
    [HarmonyPatch(typeof(Attack), nameof(Attack.DoMeleeAttack))]
    public class PatchCharacterSetHealth
    {
      private static void Prefix(ref Attack __instance, ref float ___m_maxYAngle, ref float ___m_attackOffset, ref float ___m_attackHeight)
      {
        if (__instance.m_character is not Player || (Player)__instance.m_character != Player.m_localPlayer) return;
				___m_maxYAngle = 180f;
        ___m_attackOffset = Main.Instance.Offset.Value;
        ___m_attackHeight = Main.Instance.Height.Value;
      }
    }
  }
}
