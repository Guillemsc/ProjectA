using Game.GameContext.Dialogues.Configurations;
using Game.GameContext.Dialogues.Enums;
using GUtils.Optionals;

namespace Game.GameContext.Dialogues.UseCases;

public sealed class GetDialogueSpeakerConfigurationUseCase
{
    readonly GameDialoguesConfiguration _gameDialoguesConfiguration;

    public GetDialogueSpeakerConfigurationUseCase(
        GameDialoguesConfiguration gameDialoguesConfiguration
        )
    {
        _gameDialoguesConfiguration = gameDialoguesConfiguration;
    }

    public Optional<DialogueSpeakerConfiguration> Execute(DialogueSpeaker speaker)
    {
        foreach (DialogueSpeakerConfiguration speakerConfiguration in _gameDialoguesConfiguration.SpeakersConfigurations!)
        {
            if (speakerConfiguration.Speaker == speaker)
            {
                return speakerConfiguration;
            }
        }
        
        return Optional<DialogueSpeakerConfiguration>.None;
    }
}