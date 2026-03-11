using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace MonsterReplacer; 

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    internal const string ModId = "MonsterReplacer"; 
    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        harmony.PatchAll();
        Logger.Info("资源包怪物贴图替换器已启动！");
    }
}