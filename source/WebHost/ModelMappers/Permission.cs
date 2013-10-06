using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHost.ModelMappers
{
    public class Permission
        : Profile
    {
        protected override void Configure()
        {
            CreateMap<DataModel.Permission, Models.RoleListItemModel.PermissionListItem>();

            CreateMap<DataModel.Permission, Models.PermissionListItemModel>();

            base.Configure();
        }
    }
}