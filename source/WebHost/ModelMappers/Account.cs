using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebHost.ModelMappers
{
    public class Account
        : Profile
    {
        protected override void Configure()
        {
            CreateMap<DataModel.ChildAccount, Models.MasterAccountListItemModel.ChildAccountListItemModel>()
                .ForMember(x => x.AccountNumber, opt => opt.MapFrom(y => y.AccountNumber))
                .ForMember(x => x.CommonName, opt => opt.MapFrom(y => y.CommonName))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));

            CreateMap<DataModel.MasterAccount, Models.MasterAccountListItemModel>()
                .ForMember(x => x.AccountNumber, opt => opt.MapFrom(y => y.AccountNumber))
                .ForMember(x => x.CommonName, opt => opt.MapFrom(y => y.CommonName))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.BillingAccountNumber, opt => opt.MapFrom(x => x.BillingAccountNumber))
                .ForMember(x => x.InvoiceReseller, opt => opt.MapFrom(x => x.InvoiceReseller))
                .ForMember(x => x.ResellerDBAName, opt => opt.MapFrom(x => x.Reseller.DoingBusinessAsName))
                .ForMember(x => x.ResellerId, opt => opt.MapFrom(x => x.Reseller.Id))
                .ForMember(x => x.ResellerLegalName, opt => opt.MapFrom(x => x.Reseller.LegalName));

            CreateMap<DataModel.MasterAccount, Models.MasterAccountEditModel>()
                .ForMember(x => x.ResellerId, opt => opt.MapFrom(x => x.Reseller.Id))
                .ForMember(x => x.UseBillToForShipTo, opt => opt.MapFrom(x => x.BillTo.Equals(x.ShipTo)))
                .ReverseMap()
                .ConstructUsing((ResolutionContext ctx) =>
                {
                    var model = ctx.SourceValue as Models.MasterAccountEditModel;
                    if (model == null)
                    {
                        return null;
                    }

                    var DbContext = DependencyResolver.Current.GetService<DataModel.IDataContext>();
                    return DbContext.Accounts.OfType<DataModel.MasterAccount>()
                        .SingleOrDefault(u => u.Id == model.Id) ?? new DataModel.MasterAccount();
                })
                .ForMember(x => x.Reseller, opt => opt
                    .ResolveUsing<ValueResolvers.ResellerResolver>()
                    .FromMember(x => x.ResellerId)
                )
                .ForMember(x => x.SubAccounts, opt => opt.UseDestinationValue());

            CreateMap<DataModel.ChildAccount, Models.ChildAccountEditModel>()
                .ForMember(x => x.MasterAccountId, opt => opt.MapFrom(x => x.MasterAccount.Id))
                .ForMember(x => x.UseBillToForShipTo, opt => opt.MapFrom(x => x.BillTo.Equals(x.ShipTo)))
                .ReverseMap()
                .ConstructUsing((ResolutionContext ctx) =>
                {
                    var model = ctx.SourceValue as Models.ChildAccountEditModel;
                    if (model == null)
                    {
                        return null;
                    }

                    var DbContext = DependencyResolver.Current.GetService<DataModel.IDataContext>();
                    return DbContext.Accounts.OfType<DataModel.ChildAccount>()
                        .SingleOrDefault(u => u.Id == model.Id) ?? new DataModel.ChildAccount();
                })
                .ForMember(x => x.MasterAccount, opt => opt
                    .ResolveUsing<ValueResolvers.MasterAccountResolver>()
                    .FromMember(x => x.MasterAccountId)
                );

            base.Configure();
        }
    }
}