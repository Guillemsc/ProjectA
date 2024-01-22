
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
        
        /// <summary>
        /// Shows the debug actions UI with the specified parent node.
        /// </summary>
        /// <param name="parent">The parent node to attach the debug UI to.</param>
        public static void Show(Node parent) => Instance.ShowInternal(parent);
        
        /// <summary>
        /// Hides the debug actions UI.
        /// </summary>
        public static void Hide() => Instance.HideInternal();
        
        /// <summary>
        /// Checks if the debug actions UI is currently visible.
        /// </summary>
        /// <returns>True if the UI is visible, false otherwise.</returns>
        public static bool IsVisible() => Instance.IsVisibleInternal();

        /// <summary>
        /// Adds a collapsible section to the debug actions UI.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="collapsed">Whether the section should be initially collapsed.</param>
        /// <param name="priority">The priority of the section.</param>
        /// <returns>The added section.</returns>
        public static IDebugActionsSection AddSection(string name, bool collapsed = false, int priority = 0) 
            => Instance.AddSectionInternal(name, collapsed, priority);
        
        /// <summary>
        /// Adds a collapsible section to the debug actions UI with a specified priority.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="priority">The priority of the section.</param>
        /// <returns>The added section.</returns>
        public static IDebugActionsSection AddSection(string name, int priority) 
            => AddSection(name, false, priority);
        
        /// <summary>
        /// Adds a non-collapsible section to the debug actions UI.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="priority">The priority of the section.</param>
        /// <returns>The added non-collapsible section.</returns>
        public static IDebugActionsSection AddNonCollapsableSection(string name, int priority = 0) 
            => Instance.AddNonCollapsableSectionInternal(name, priority);
        
        /// <summary>
        /// Adds a collapsible section to the debug actions UI with specified options object.
        /// Using reflection, debug actions are going to be created for the options object.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="optionsObject">Options associated with the section.</param>
        /// <param name="collapsed">Whether the section should be initially collapsed.</param>
        /// <param name="priority">The priority of the section.</param>
        /// <returns>The added section.</returns>
        public static IDebugActionsSection AddSection(string name, object optionsObject, bool collapsed = false, int priority = 0) 
            => Instance.AddSectionInternal(name, optionsObject, collapsed, priority);
        
        /// <summary>
        /// Adds a collapsible section to the debug actions UI with specified options and priority.
        /// Using reflection, debug actions are going to be created for the options object.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="optionsObject">Options associated with the section.</param>
        /// <param name="priority">The priority of the section.</param>
        /// <returns>The added section.</returns>
        public static IDebugActionsSection AddSection(string name, object optionsObject, int priority) 
            => AddSection(name, optionsObject, false, priority);
        
        /// <summary>
        /// Adds a non-collapsible section to the debug actions UI with specified options.
        /// Using reflection, debug actions are going to be created for the options object.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="optionsObject">Options associated with the section.</param>
        /// <param name="priority">The priority of the section.</param>
        /// <returns>The added non-collapsible section.</returns>
        public static IDebugActionsSection AddNonCollapsableSection(string name, object optionsObject, int priority = 0) 
            => Instance.AddNonCollapsableSectionInternal(name, optionsObject, priority);
        
        /// <summary>
        /// Removes a section from the debug actions UI.
        /// </summary>
        /// <param name="section">The section to be removed.</param>
        public static void RemoveSection(IDebugActionsSection section) 
            => Instance.RemoveSectionInternal(section);
        
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

        IDebugActionsSection AddSectionInternal(string name, bool collapsed, int priority)
        {
            return AddDebugActionsSectionUseCase.Execute(
                _instancesData,
                _debugActionsData,
                name,
                true,
                collapsed,
                priority
            );
        }
        
        IDebugActionsSection AddNonCollapsableSectionInternal(string name, int priority)
        {
            return AddDebugActionsSectionUseCase.Execute(
                _instancesData,
                _debugActionsData,
                name,
                false,
                false,
                priority
            );
        }
        
        IDebugActionsSection AddSectionInternal(string name, object optionsObject, bool collapsed, int priority)
        {
            DebugActionsSection section = AddDebugActionsSectionUseCase.Execute(
                _instancesData,
                _debugActionsData,
                name,
                true,
                collapsed,
                priority
            );
            
            PopulateDebugActionsSectionFromOptionsObjectUseCase.Execute(section, optionsObject);
            
            return section;
        }
        
        IDebugActionsSection AddNonCollapsableSectionInternal(string name, object optionsObject, int priority)
        {
            DebugActionsSection section = AddDebugActionsSectionUseCase.Execute(
                _instancesData,
                _debugActionsData,
                name,
                false,
                false,
                priority
            );
            
            PopulateDebugActionsSectionFromOptionsObjectUseCase.Execute(section, optionsObject);
            
            return section;
        }
        
        void RemoveSectionInternal(IDebugActionsSection section)
        {
            RemoveDebugActionsSectionUseCase.Execute(_debugActionsData, section);
        }
    }   
}
