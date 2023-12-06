using AutoMapper;
using WembleyScada.Api.Application.Commands.DeviceReferences;
using WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;

namespace WembleyScada.Api.Application.Mapping;

public class ViewModelToModelProfile : Profile
{
    public ViewModelToModelProfile() 
    {
        CreateMap<UpdateMFCViewModel, MFC>();
    }
}
