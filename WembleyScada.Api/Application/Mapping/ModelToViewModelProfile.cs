using AutoMapper;
using WembleyScada.Api.Application.Queries.DeviceReferences;
using WembleyScada.Api.Application.Queries.Devices;
using WembleyScada.Api.Application.Queries.Products;
using WembleyScada.Api.Application.Queries.References;
using WembleyScada.Api.Application.Queries.ShiftReports;
using WembleyScada.Domain.AggregateModels.DeviceAggregate;
using WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;
using WembleyScada.Domain.AggregateModels.ProductAggregate;
using WembleyScada.Domain.AggregateModels.ReferenceAggregate;
using WembleyScada.Domain.AggregateModels.ShiftReportAggregate;

namespace WembleyScada.Api.Application.Mapping;

public class ModelToViewModelProfile : Profile
{
    public ModelToViewModelProfile() 
    {
        CreateMap<Device, DeviceViewModel>();

        CreateMap<Product, ProductViewModel>();

        CreateMap<Reference, ReferenceViewModel>()
            .ForMember(dest => dest.ProductName, dest => dest.MapFrom(src => src.Product.ProductName));

        CreateMap<DeviceReference, DeviceReferenceViewModel>();
        CreateMap<MFC, MFCViewModel>();

        CreateMap<ShiftReport, ShiftReportViewModel>();
    }
}