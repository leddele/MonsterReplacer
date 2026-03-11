using HarmonyLib;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace MonsterReplacer.Patches;

[HarmonyPatch(typeof(NCreatureVisuals), "SetUpSkin")]
public static class WrigglerVisualPatch
{
    private const string CustomSpriteName = "MyCustomWrigglerSprite";

    [HarmonyPostfix]
    public static void Postfix(NCreatureVisuals __instance, MonsterModel model)
    {
        
        if (model.Id.Entry == "WRIGGLER")
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

                
                string imgPath = "res://images/monsters/wriggler/wriggler_static.png";
                var tex = GD.Load<Texture2D>(imgPath);

                if (tex != null)
                {
                    sprite.Texture = tex;
                    
                    
                    sprite.Scale = new Vector2(0.5f, 0.5f); 
                    sprite.Position = new Vector2(0, -80);  
                    
                    __instance.AddChild(sprite);
                }
                else
                {
                    GD.PrintErr($"[MonsterReplacer ERROR] 找不到蠕虫贴图: {imgPath}");
                }
            }
        }
    }
}