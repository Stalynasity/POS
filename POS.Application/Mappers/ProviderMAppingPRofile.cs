﻿using AutoMapper;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class ProviderMAppingPRofile: Profile
    {
        public ProviderMAppingPRofile()
        {
            CreateMap<Provider, ProviderResponseDto>()
                .ForMember(x => x.ProviderId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateProvider, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ForMember(x => x.DocumentType, x=> x.MapFrom(Y => Y.DocumentType.Abbreviation))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Provider>, BaseEntityResponse<ProviderResponseDto>>()
                .ReverseMap();

            CreateMap<ProviderRequestDto, Provider>();
        }
    }
}
