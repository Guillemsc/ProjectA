using GUtils.Disposing.Disposables;
using GUtils.Loading.Loadables;
using GUtils.Starting.Startables;

namespace Game.GameContext.General.Interactors;

public interface IGameContextInteractor : ILoadable, IStartable, ITaskDisposable
{
    
}