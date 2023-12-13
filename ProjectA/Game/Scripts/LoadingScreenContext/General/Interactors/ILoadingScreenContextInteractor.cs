using GUtils.Disposing.Disposables;
using GUtils.Loading.Loadables;
using GUtils.Starting.Startables;

namespace Game.LoadingScreenContext.General.Interactors;

public interface ILoadingScreenContextInteractor : ILoadable, IStartable, ITaskDisposable
{
    
}