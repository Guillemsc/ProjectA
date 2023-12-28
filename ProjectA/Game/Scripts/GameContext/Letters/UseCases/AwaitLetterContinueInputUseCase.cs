using System.Threading;
using System.Threading.Tasks;
using Godot;
using TaskExtensions = GUtilsGodot.Extensions.TaskExtensions;

namespace Game.GameContext.Letters.UseCases;

public sealed class AwaitLetterContinueInputUseCase
{
    public async Task Execute(CancellationToken cancellationToken)
    {
        bool cancel = false;
        
        while (!cancellationToken.IsCancellationRequested && !cancel)
        {
            cancel = Input.IsActionJustPressed("ui_accept");

            await TaskExtensions.GodotYield();
        }
    }
}