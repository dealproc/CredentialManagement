using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHost.ModelMappers
{
    public class Address
        : Profile
    {
        protected override void Configure()
        {
            CreateMap<DataModel.Address, Models.Address>()
                .ReverseMap();

            base.Configure();
        }
    }
}