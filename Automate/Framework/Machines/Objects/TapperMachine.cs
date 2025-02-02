using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.TerrainFeatures;
using SObject = StardewValley.Object;

namespace Pathoschild.Stardew.Automate.Framework.Machines.Objects;

/// <summary>A tapper that accepts input and provides output.</summary>
/// <remarks>Derived from <see cref="SObject.checkForAction"/> (search for 'Tapper').</remarks>
internal class TapperMachine : GenericObjectMachine<SObject>
{
    /*********
    ** Fields
    *********/
    /// <summary>The tree being tapped.</summary>
    private readonly Tree Tree;


    /*********
    ** Public methods
    *********/
    /// <summary>Construct an instance.</summary>
    /// <param name="machine">The underlying machine.</param>
    /// <param name="location">The location to search.</param>
    /// <param name="tile">The tile covered by the machine.</param>
    /// <param name="tree">The tree being tapped.</param>
    public TapperMachine(SObject machine, GameLocation location, Vector2 tile, Tree tree)
        : base(machine, location, tile)
    {
        this.Tree = tree;
    }

    /// <inheritdoc />
    public override ITrackedStack? GetOutput()
    {
        return this.GetTracked(this.Machine.heldObject.Value, onEmpty: this.Reset);
    }

    /// <inheritdoc />
    public override bool SetInput(IStorage input)
    {
        return false; // no input
    }


    /*********
    ** Private methods
    *********/
    /// <summary>Reset the machine so it's ready to accept a new input.</summary>
    /// <param name="item">The output item that was taken.</param>
    private void Reset(Item item)
    {
        this.Machine.heldObject.Value = null;
        this.Machine.readyForHarvest.Value = false;
        this.Tree.UpdateTapperProduct(tapper: this.Machine, previousOutput: item as SObject);
    }
}
