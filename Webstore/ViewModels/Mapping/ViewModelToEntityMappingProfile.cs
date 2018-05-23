using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webstore.Data;

namespace Webstore.ViewModels.Mapping
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<RegistrationViewModel, ApplicationUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
