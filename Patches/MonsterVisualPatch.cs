using HarmonyLib;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using System.Collections.Generic;

namespace MonsterReplacer.Patches;

[HarmonyPatch(typeof(NCreatureVisuals), "SetUpSkin")]
public static class MonsterVisualPatch
{

    private const string CustomSpriteName = "MyCustomReplacerSprite";

    [HarmonyPostfix]
    public static void Postfix(NCreatureVisuals __instance, MonsterModel model)
    {
        // 获取怪物 ID
        string id = model.Id.Entry;

    
        string imgPath = $"res://images/monsters/replacements/{id}.png";

      
        if (ResourceLoader.Exists(imgPath))
        {
            //隐藏原有的所有节点
            foreach (Node child in __instance.GetChildren())
            {
           
                if (child is CanvasItem ci && child.Name != CustomSpriteName)
                {
                    ci.Visible = false;
                }
            }

            // 加载并挂载贴图
            if (__instance.GetNodeOrNull<Sprite2D>(CustomSpriteName) == null)
            {
                Sprite2D customSprite = new Sprite2D();
                customSprite.Name = CustomSpriteName;

                // 使用 GD.Load 加载 PCK 内部已导入的资源
                var tex = GD.Load<Texture2D>(imgPath);

                if (tex != null)
                {
                    customSprite.Texture = tex;
                    
                    // 根据怪物 ID 进行差异化调整
                    if (id == "PHROG_PARASITE")
                    {
                        customSprite.Scale = new Vector2(0.5f, 0.5f);
                        customSprite.Position = new Vector2(0, -80);
                    }
                    else
                    {
                        // 默认通用缩放
                        customSprite.Scale = new Vector2(3.0f, 3.0f);
                        customSprite.Position = new Vector2(0, -100);
                    }

                    __instance.AddChild(customSprite);
                }
            }
        }
    }
}