﻿using AutoMapper;
using MediatR;
using RealtyApp.Core.Application.Dtos.EntitiesDTOs.ImmovableAsset;
using RealtyApp.Core.Application.Interfaces.Services;
using RealtyApp.Core.Application.ViewModels.ImmovableAsset;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Features.ImmovableAsset.Queries.GetAllImmovableAssetById
{ 
    //<summary>
    // Permite obtener una propiedad mediante su id.
    //</summary>

    public class GetImmovableAssetByIdQuery : IRequest<ImmovableAssetDTO>
    {
        [SwaggerParameter(Description = "Introducir el id de la propiedad a obtener")]
        public int Id { get; set; }
    }

    public class GetImmovableAssetByIdQuerytHandler : IRequestHandler<GetImmovableAssetByIdQuery,ImmovableAssetDTO>
    {
        private readonly IImmovableAssetService _immovableAssetService;
        private readonly IMapper _mapper;
        public GetImmovableAssetByIdQuerytHandler(IMapper mapper
            ,IImmovableAssetService immovableAssetService)
        {
            _immovableAssetService = immovableAssetService;
            _mapper = mapper;
        }

        public async Task<ImmovableAssetDTO> Handle(GetImmovableAssetByIdQuery request, CancellationToken cancellationToken)
        {
            var immovableAssets = await GetIncludeDetailsById(request.Id);
            if (immovableAssets != null)
            {
                return immovableAssets;
            }
            return null;
        }
        private async Task<ImmovableAssetDTO> GetIncludeDetailsById(int id)
        {
            DetailsViewModelApi immovableAssetDetails = await _immovableAssetService.GetIncludeDetailsById(id);           
            ImmovableAssetDTO immovables = _mapper.Map<ImmovableAssetDTO>(immovableAssetDetails);           
            if(immovableAssetDetails != null)
            {
                return immovables;
            }
            else
            {
                return null;
            }            
        }


    }


}
