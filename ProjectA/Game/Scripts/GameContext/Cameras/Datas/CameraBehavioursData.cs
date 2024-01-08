using GUtils.Optionals;
using GUtilsGodot.Cameras.Behaviours;
using GUtilsGodot.Cameras.PositionProcessors;

namespace Game.GameContext.Cameras.Datas;

public sealed class CameraBehavioursData
{
    public Optional<ShakeCamera2dBehaviour> ShakeBehaviour;
    public Optional<BoundsConfinementCamera2dBehaviour> BoundsConfinementBehaviour;
    public Optional<BoundsPosition2dProcessor> BoundsProcessor;
}