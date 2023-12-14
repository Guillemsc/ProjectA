using Game.GameContext.General.Datas;
using Game.GameContext.VisualEffects.Configurations;
using Game.GameContext.VisualEffects.Datas;
using Game.GameContext.VisualEffects.UseCases;
using GUtils.Di.Builder;
using GUtils.Tasks.Runners;

namespace Game.GameContext.VisualEffects.Installers;

public static class GameVisualEffectsInstaller
{
    public static void InstallGameVisualEffects(this IDiContainerBuilder builder)
    {
        builder.Bind<VisualEffectsPrefabsData>().FromNew();

        builder.Bind<RegisterVisualEffectsPrefabsUseCase>()
            .FromFunction(c => new RegisterVisualEffectsPrefabsUseCase(
                c.Resolve<GameVisualEffectsConfiguration>(),
                c.Resolve<VisualEffectsPrefabsData>()
            ))
            .WhenInit(o => o.Execute)
            .NonLazy();

        builder.Bind<PlayOneShotVisualEffectUseCase>()
            .FromFunction(c => new PlayOneShotVisualEffectUseCase(
                c.Resolve<GameGeneralViewData>(),
                c.Resolve<VisualEffectsPrefabsData>(),
                c.Resolve<IAsyncTaskRunner>()
            ));
    }
}