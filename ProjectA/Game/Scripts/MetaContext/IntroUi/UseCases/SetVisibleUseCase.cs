using System.Threading;
using System.Threading.Tasks;

namespace Game.MetaContext.IntroUi.UseCases;

public sealed class SetVisibleUseCase
{
    readonly ShowUseCase _showUseCase;
    readonly HideUseCase _hideUseCase;

    public SetVisibleUseCase(
        ShowUseCase showUseCase, 
        HideUseCase hideUseCase
        )
    {
        _showUseCase = showUseCase;
        _hideUseCase = hideUseCase;
    }

    public Task Execute(bool visible, bool instantly, CancellationToken cancellationToken)
    {
        if (!visible)
        {
            return _hideUseCase.Execute(instantly, cancellationToken);
        }

        return _showUseCase.Execute(instantly, cancellationToken);
    }
}