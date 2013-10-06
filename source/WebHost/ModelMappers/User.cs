using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHost.ModelMappers
{
    public class User
        : Profile
    {
        protected override void Configure()
        {
            CreateMap<DataModel.Group, Models.UserListItemModel.GroupListModel>();


            CreateMap<DataModel.AccountRole, Models.UserListItemModel.AccountRoleListModel>()
                .ForMember(x => x.AccountName, opt => opt.MapFrom(x => x.Account.CommonName))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(x => x.Role.Name));

            CreateMap<DataModel.User, Models.UserListItemModel>()
                .ForMember(x => x.Groups, opt => opt.ResolveUsing<ValueResolvers.UserGroupsResolver>()
                    .FromMember(x => x));

            CreateMap<DataModel.User, Models.UserEditModel>()
                .ReverseMap()
                .ConstructUsing((ResolutionContext ctx) =>
                {
                    var model = ctx.SourceValue as Models.UserEditModel;
                    if (model == null)
                    {
                        return null;
                    }

                    var DbContext = DependencyResolver.Current.GetService<DataModel.IDataContext>();
                    return DbContext.Users.SingleOrDefault(u => u.Id == model.Id) ?? new DataModel.User();
                });

            base.Configure();
        }
    }
}