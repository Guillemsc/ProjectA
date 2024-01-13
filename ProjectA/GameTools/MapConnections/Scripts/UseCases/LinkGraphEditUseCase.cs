using Godot;
using GUtilsGodot.Extensions;

namespace GameTools.MapConnections.Scripts.UseCases;

public sealed class LinkGraphEditUseCase
{
    readonly GraphEdit _graphEdit;
    readonly WhenGraphEditConnectionRequestUseCase _whenGraphEditConnectionRequestUseCase;
    readonly WhenGraphEditDisconnectionRequestUseCase _whenGraphEditDisconnectionRequestUseCase;
    readonly WhenGraphEditNodeEndMoveUseCase _whenGraphEditNodeEndMoveUseCase;

    public LinkGraphEditUseCase(
        GraphEdit graphEdit, 
        WhenGraphEditConnectionRequestUseCase whenGraphEditConnectionRequestUseCase, 
        WhenGraphEditDisconnectionRequestUseCase whenGraphEditDisconnectionRequestUseCase, 
        WhenGraphEditNodeEndMoveUseCase whenGraphEditNodeEndMoveUseCase
        )
    {
        _graphEdit = graphEdit;
        _whenGraphEditConnectionRequestUseCase = whenGraphEditConnectionRequestUseCase;
        _whenGraphEditDisconnectionRequestUseCase = whenGraphEditDisconnectionRequestUseCase;
        _whenGraphEditNodeEndMoveUseCase = whenGraphEditNodeEndMoveUseCase;
    }

    public void Execute()
    {
        _graphEdit.ConnectConnectionRequest(_whenGraphEditConnectionRequestUseCase.Execute);   
        _graphEdit.ConnectDisconnectionRequest(_whenGraphEditDisconnectionRequestUseCase.Execute);
        _graphEdit.ConnectEndNodeMove(_whenGraphEditNodeEndMoveUseCase.Execute);
    }
}