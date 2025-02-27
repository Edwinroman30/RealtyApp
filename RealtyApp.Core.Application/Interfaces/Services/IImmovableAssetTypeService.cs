﻿using RealtyApp.Core.Application.ViewModels.ImmovableAssetType;
using RealtyApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Interfaces.Services
{
    public interface IImmovableAssetTypeService : IGenericService<ImmovableAssetTypeSaveViewModel, ImmovableAssetTypeViewModel, ImmovableAssetType>
    {
        //Custome Functionalities
        public Task<List<ImmovableAssetTypeViewModel>> GetAllViewModelWithIncludes();
        Task<List<ImmovableAssetTypeViewModel>> GetAllWithCountTypeImmovableUse();


    }
}
