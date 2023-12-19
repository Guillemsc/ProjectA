using GUtils.Optionals;
using GUtilsGodot.Cameras.Behaviours;
using GUtilsGodot.Cameras.PositionProcessors;

namespace Game.GameContext.Cameras.Datas;

public sealed class CameraBehavioursData
{
    public Optional<BoundsConfinementCamera2dBehaviour> BoundsConfinement;
    public Optional<BoundsPosition2dProcessor> BoundsProcessor;
}