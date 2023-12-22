using System.Threading;
using System.Threading.Tasks;
using Game.GameContext.Players.Datas;
using Game.GameContext.Players.Views;
using TaskExtensions = GUtilsGodot.Extensions.TaskExtensions;

namespace Game.GameContext.Cinematics.UseCases;

public sealed class AwaitUntilPlayerIsOnTheGroundUseCase
{
    readonly PlayerViewData _playerViewData;

    public AwaitUntilPlayerIsOnTheGroundUseCase(
        PlayerViewData playerViewData
        )
    {
        _playerViewData = playerViewData;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        bool hasPlayer = _playerViewData.PlayerView.TryGet(out PlayerView playerView);
        
        if(!hasPlayer)
        {
            return;
        }
        
        while (!cancellationToken.IsCancellationRequested && playerView.AnimationPlayer!.OnAir)
        {
            await TaskExtensions.GodotYield();
        }
    }
}