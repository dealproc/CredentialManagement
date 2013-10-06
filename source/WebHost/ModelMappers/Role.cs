using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHost.ModelMappers
{
    public class Role
        : Profile
    {
        protected override void Configure()
        {
            CreateMap<DataModel.Role, Models.RoleListItemModel.InheritedRole>();

            CreateMap<DataModel.Role, Models.RoleListItemModel>()
                .ForMember(x => x.IsSystem, opt => opt.MapFrom(x => x.HasAllPermissions))
                .ForMember(x => x.IncludedRoles, opt => opt.MapFrom(x => x.AllParentRoles()))
                .ForMember(x => x.Permissions, opt => opt.MapFrom(x => x.Permissions));

            CreateMap<DataModel.Role, Models.RoleEditModel>()
                .ReverseMap()
                .ConstructUsing((ResolutionContext ctx) =>
                {
                    var model = ctx.SourceValue as Models.RoleEditModel;
                    if (model == null)
                    {
                        return null;
                    }

                    var DbContext = DependencyResolver.Current.GetService<DataModel.IDataContext>();
                    return DbContext.Roles.SingleOrDefault(u => u.Id == model.Id) ?? new DataModel.Role();
                })
                .ForMember(x => x.Permissions, opt => opt.Ignore())
                .ForMember(x => x.IncludedRoles, opt => opt.Ignore())
                .ForMember(x => x.HasAllPermissions, opt => opt.UseValue(false));

            base.Configure();
        }
    }
}