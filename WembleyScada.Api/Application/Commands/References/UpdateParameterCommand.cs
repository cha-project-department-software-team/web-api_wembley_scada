namespace WembleyScada.Api.Application.Commands.References;

public class UpdateParameterCommand : IRequest<bool>
{
    public string RefName { get; set; }

    public UpdateParameterCommand(string refName)
    {
        RefName = refName;
    }
}
