using HarmonyLib;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace MonsterReplacer.Patches;

[HarmonyPatch(typeof(NCreatureVisuals), "SetUpSkin")]
public static class PhrogVisualPatch
{

    private const string CustomSpriteName = "MyCustomPhrogSprite";

    [HarmonyPostfix]
    public static void Postfix(NCreatureVisuals __instance, MonsterModel model)
    {
    
        if (model.Id.Entry == "PHROG_PARASITE")
        {
         
            foreach (Node child in __instance.GetChildren())
            {
         
                if (child is CanvasItem ci && child.Name != CustomSpriteName)
                {
                    ci.Visible = false;
                }
            }

          
            if (__instance.GetNodeOrNull<Sprite2D>(CustomSpriteName) == null)
            {
                Sprite2D sprite = new Sprite2D();
                sprite.Name = CustomSpriteName;

 
                string imgPath = "res://images/monsters/phrog/phrog_static.png";
                var tex = GD.Load<Texture2D>(imgPath);

                if (tex != null)
                {
                    sprite.Texture = tex;
                    
     
                    sprite.Scale = new Vector2(0.3f, 0.3f); 
                    sprite.Position = new Vector2(0, -180);
                    
                    __instance.AddChild(sprite);
          
                }
                else
                {
                   
                    GD.PrintErr($"[MonsterReplacer ERROR] 在 PCK 中找不到异蛙贴图: {imgPath}");
                }
            }
        }
    }
}