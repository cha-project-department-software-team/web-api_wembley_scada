namespace WembleyScada.Api.Application.Queries.References;

public class ReferenceWithLotsQuery : IRequest<ReferenceWithLotViewModel>
{
    public int ReferenceId { get; set; }
}
