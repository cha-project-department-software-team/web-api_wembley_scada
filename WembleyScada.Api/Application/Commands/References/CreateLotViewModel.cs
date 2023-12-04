using System.Runtime.Serialization;

namespace WembleyScada.Api.Application.Commands.References;

[DataContract]
public class CreateLotViewModel
{
    [DataMember]
    public string LotId { get; set; }
    [DataMember]
    public int LotSize { get; set; }

    public CreateLotViewModel(string lotId, int lotSize)
    {
        LotId = lotId;
        LotSize = lotSize;
    }
}
