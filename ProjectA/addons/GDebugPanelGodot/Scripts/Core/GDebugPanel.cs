
using GDebugPanelGodot.Datas;
using GDebugPanelGodot.DebugActions.Containers;
using GDebugPanelGodot.UseCases;
using Godot;

namespace GDebugPanelGodot.Core
{
    public sealed class GDebugPanel
    {
        static GDebugPanel? _instance;
        static GDebugPanel Instance => _instance ??= new GDebugPanel();

        readonly CacheData _cacheData = new();
        readonly InstancesData _instancesData = new();
        readonly DebugActionsData _debugActionsData = new();
        
        public static void Show(Node parent) => Instance.ShowInternal(parent);
        public static void Hide() => Instance.HideInternal();
        public static bool IsVisible() => Instance.IsVisibleInternal();

        public static DebugActionsSection AddSection(string name) => Instance.AddSectionInternal(name);
        public static void RemoveSection(DebugActionsSection section) => Instance.RemoveSectionInternal(section);

        void ShowInternal(Node parent)
        {
            InstantiateDebugPanelViewUseCase.Execute(_cacheData, _instancesData, parent);
            CreateDebugActionsWidgetsUseCase.Execute(_instancesData, _debugActionsData);
        }
        
        void HideInternal()
        {
            ClearDebugActionsWidgetsUseCase.Execute(_debugActionsData);
            DestroyDebugPanelViewUseCase.Execute(_instancesData);
        }
        
        bool IsVisibleInternal()
        {
            return HasDebugPanelViewInstanceUseCase.Execute(_instancesData);
        }

        DebugActionsSection AddSectionInternal(string name)
        {
            return AddDebugActionsSectionUseCase.Execute(_instancesData, _debugActionsData, name);
        }
        
        void RemoveSectionInternal(DebugActionsSection section)
        {
            RemoveDebugActionsSectionUseCase.Execute(_debugActionsData, section);
        }
    }   
}
