using AutoMapper;
using WembleyScada.Domain.AggregateModels.DeviceReferenceAggregate;

namespace WembleyScada.Api.Application.Commands.DeviceReferences;

public class UpdateDeviceReferenceCommandHandler : IRequestHandler<UpdateDeviceReferenceCommand, bool>
{
    private readonly IDeviceReferenceRepository _deviceReferenceRepository;
    private readonly IMapper _mapper;

    public UpdateDeviceReferenceCommandHandler(IDeviceReferenceRepository deviceReferenceRepository, IMapper mapper)
    {
        _deviceReferenceRepository = deviceReferenceRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateDeviceReferenceCommand request, CancellationToken cancellationToken)
    {
        var deviceReference = await _deviceReferenceRepository.GetAsync(request.ReferenceId, request.DeviceId)
            ?? throw new ResourceNotFoundException($"The entity of type {nameof(DeviceReference)} with ReferenceId: {request.ReferenceId}, DeviceId: {request.DeviceId} can be found");

        var mfcs = _mapper.Map<List<MFC>>(request.MFCs);
        
        deviceReference.UpdateMFC(mfcs);

        return await _deviceReferenceRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
