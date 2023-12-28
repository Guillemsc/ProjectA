using GUtils.Disposing.Disposables;
using GUtils.Loading.Loadables;
using GUtils.Starting.Startables;

namespace Game.MetaContext.General.Interactors;

public interface IMetaContextInteractor : ILoadable, IStartable, ITaskDisposable
{
    
}