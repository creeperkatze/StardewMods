using Pathoschild.Stardew.FastAnimations.Framework;
using StardewValley;
using StardewValley.Tools;

namespace Pathoschild.Stardew.FastAnimations.Handlers;

/// <summary>Handles the tool swinging animation.</summary>
internal sealed class ToolSwingHandler : BaseAnimationHandler
{
    /*********
    ** Public methods
    *********/
    /// <inheritdoc />
    public ToolSwingHandler(float multiplier)
        : base(multiplier) { }

    /// <inheritdoc />
    public override bool TryApply(int playerAnimationId)
    {
        Farmer player = Game1.player;
        Tool tool = player.CurrentTool;

        return
            player.UsingTool
            && !player.canStrafeForToolUse()
            && tool is not null
            && (
                (tool as MeleeWeapon)?.isScythe() is true
                || tool is not (FishingRod or MeleeWeapon or Slingshot)
            )
            && this.SpeedUpPlayer(until: () => !player.UsingTool);
    }
}
