using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHost.ModelMappers
{
    public class Group
        : Profile
    {
        protected override void Configure()
        {
            CreateMap<DataModel.AccountRole, Models.GroupListItemModel.AccountAssignmentListItemModel>()
                .ForMember(x => x.AccountId, opt => opt.MapFrom(x => x.Account.Id))
                .ForMember(x => x.AccountName, opt => opt.MapFrom(x => x.Account.CommonName))
                .ForMember(x => x.RoleId, opt => opt.MapFrom(x => x.Role.Id))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(x => x.Role.Name));

            CreateMap<DataModel.Group, Models.GroupListItemModel>()
                .ForMember(x => x.AccountAssignments, opt => opt.MapFrom(x => x.AccountRoles))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<DataModel.Group, Models.GroupEditModel>()
                .ReverseMap()
                .ConstructUsing((ResolutionContext ctx) =>
                {
                    var model = ctx.SourceValue as Models.GroupEditModel;
                    if (model == null)
                    {
                        return null;
                    }

                    var DbContext = DependencyResolver.Current.GetService<DataModel.IDataContext>();
                    return DbContext.Groups.SingleOrDefault(u => u.Id == model.Id) ?? new DataModel.Group();
                })
                .ForMember(x => x.AccountRoles, opt => opt.Ignore())
                .ForMember(x => x.AutoAssign, opt => opt.UseValue(false))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Parents, opt => opt.Ignore())
                .ForMember(x => x.Users, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    var DbContext = DependencyResolver.Current.GetService<DataModel.IDataContext>();

                    var parentGroups = DbContext.Groups.All().Where(x => src.ParentGroups.Contains(x.Id));
                    dest.AddParents(parentGroups);
                });

            base.Configure();
        }
    }
}