using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.SQLite.DataMaps
{
    public class AccountMap
        : ClassMap<Account>
    {
        public AccountMap()
        {
            DiscriminateSubClassesOnColumn("type");

            Id(x => x.Id);

            Map(x => x.AccountNumber);
            Map(x => x.CommonName);

            Component(x => x.BillTo, a =>
            {
                a.Access.Property();
                a.Map(x => x.City).Column("BillCity");
                a.Map(x => x.Country).Column("BillCountry");
                a.Map(x => x.StateIntlOther).Column("BillStateIntlOther");
                a.Map(x => x.Street).Column("BillStreet");
                a.Map(x => x.ZipPostal).Column("BillZipPostal");
            });

            Component(x => x.ShipTo, a =>
            {
                a.Access.Property();
                a.Map(x => x.City).Column("ShipCity");
                a.Map(x => x.Country).Column("ShipCountry");
                a.Map(x => x.StateIntlOther).Column("ShipStateIntlOther");
                a.Map(x => x.Street).Column("ShipStreet");
                a.Map(x => x.ZipPostal).Column("ShipZipPostal");
            });
        }
    }

    public class MasterAccountMap
        : SubclassMap<MasterAccount>
    {
        public MasterAccountMap()
        {
            DiscriminatorValue("Master");

            Map(x => x.BillingAccountNumber);
            Map(x => x.InvoiceReseller);
   
            HasMany(x => x.SubAccounts);
            
            References(x => x.Reseller);
        }
    }

    public class ChildAccountMap
        : SubclassMap<ChildAccount>
    {
        public ChildAccountMap()
        {
            DiscriminatorValue("Child");

            References(x => x.MasterAccount);
        }
    }
}
