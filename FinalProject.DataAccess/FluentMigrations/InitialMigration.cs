using FluentMigrator;

namespace FinalProject.DataAccess
{
    [Migration(20220810, "V1.0")]
    public class InitialMigration : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            #region Tables
            //Product
            Create.Table("Products")
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("UnitPrice").AsDecimal().NotNullable()
                .WithColumn("ImageUrl").AsString().NotNullable()
                .WithColumn("Description").AsString(500).NotNullable()
                .WithColumn("IsOfferable").AsBoolean().NotNullable()
                .WithColumn("IsSold").AsBoolean().NotNullable()
                .WithColumn("UsageStatus").AsInt32().NotNullable()
                .WithColumn("CreatedDate").AsDate().NotNullable()
                .WithColumn("UpdatedDate").AsDate().Nullable()
                .WithColumn("DeletedDate").AsDate().Nullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("CategoryID").AsInt32().NotNullable()
                .WithColumn("BrandID").AsInt32().Nullable()
                .WithColumn("ColorID").AsInt32().Nullable()
                .WithColumn("AppUserID").AsInt32().NotNullable();

            //Category
            Create.Table("Categories")
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("Description").AsString(200).NotNullable()
                .WithColumn("CreatedDate").AsDate().NotNullable()
                .WithColumn("UpdatedDate").AsDate().Nullable()
                .WithColumn("DeletedDate").AsDate().Nullable()
                .WithColumn("Status").AsInt32().NotNullable();

            //Offer
            Create.Table("Offers")
                 .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("AppUserID").AsInt32().NotNullable()
                .WithColumn("IsApproved").AsBoolean().Nullable()
                .WithColumn("ProductID").AsInt32().NotNullable()
                .WithColumn("CreatedDate").AsDate().NotNullable()
                .WithColumn("UpdatedDate").AsDate().Nullable()
                .WithColumn("DeletedDate").AsDate().Nullable()
                .WithColumn("Status").AsInt32().NotNullable();

            //Color
            Create.Table("Colors")
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(40).NotNullable()
                .WithColumn("CreatedDate").AsDate().NotNullable()
                .WithColumn("UpdatedDate").AsDate().Nullable()
                .WithColumn("DeletedDate").AsDate().Nullable()
                .WithColumn("Status").AsInt32().NotNullable();

            //Brand
            Create.Table("Brands")
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("CreatedDate").AsDate().NotNullable()
                .WithColumn("UpdatedDate").AsDate().Nullable()
                .WithColumn("DeletedDate").AsDate().Nullable()
                .WithColumn("Status").AsInt32().NotNullable();

            //AppUser
            Create.Table("AppUsers")
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserName").AsString(50).NotNullable()
                .WithColumn("Email").AsString(75).Nullable()
                .WithColumn("PasswordHash").AsBinary().Nullable()
                .WithColumn("PasswordSalt").AsBinary().Nullable()
                .WithColumn("IncorrectEntry").AsInt16().Nullable()
                .WithColumn("IsLock").AsBoolean().Nullable()
                .WithColumn("ActivationCode").AsGuid().NotNullable()
                .WithColumn("Active").AsBoolean().NotNullable()
                .WithColumn("FirstName").AsString(50).Nullable()
                .WithColumn("LastName").AsString(50).Nullable()
                .WithColumn("PhoneNumber").AsString(15).Nullable()
                .WithColumn("CreatedDate").AsDate().NotNullable()
                .WithColumn("UpdatedDate").AsDate().Nullable()
                .WithColumn("DeletedDate").AsDate().Nullable()
                .WithColumn("LastActivty").AsDate().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable();

            //Role
            Create.Table("Roles")
                .WithColumn("ID").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("CreatedDate").AsDate().NotNullable()
                .WithColumn("UpdatedDate").AsDate().Nullable()
                .WithColumn("DeletedDate").AsDate().Nullable()
                .WithColumn("Status").AsInt32().NotNullable();

            //AppUserRole
            Create.Table("AppUserRoles")
                .WithColumn("AppUserID").AsInt32().Nullable()
                .WithColumn("RoleID").AsInt32().Nullable()
                .WithColumn("CreatedDate").AsDate().NotNullable()
                .WithColumn("UpdatedDate").AsDate().Nullable()
                .WithColumn("DeletedDate").AsDate().Nullable()
                .WithColumn("Status").AsInt32().NotNullable();


            #endregion

            #region ForeignKey

            //Product - Category Relational
            Create.ForeignKey("FK_Product_Category")
                .FromTable("Products").ForeignColumn("CategoryID")
                .ToTable("Categories").PrimaryColumn("ID");

            //Product - Brand Relational
            Create.ForeignKey("FK_Product_Brand")
                .FromTable("Products").ForeignColumns("BrandID")
                .ToTable("Brands").PrimaryColumn("ID");

            //Product - Color Relational
            Create.ForeignKey("FK_Product_Color")
                .FromTable("Products").ForeignColumn("ColorID")
                .ToTable("Colors").PrimaryColumn("ID");

            //Offer - Product Relational 
            Create.ForeignKey("FK_Offer_Product")
                .FromTable("Offers").ForeignColumn("ProductID")
                .ToTable("Products").PrimaryColumn("ID");

            //Product - AppUser Relational
            Create.ForeignKey("FK_Product_AppUser")
                .FromTable("Products").ForeignColumn("AppUserID")
                .ToTable("AppUsers").PrimaryColumn("ID");

            //Offer - AppUser Relational
            Create.ForeignKey("Fk_Offer_AppUser")
                .FromTable("Offers").ForeignColumn("AppUserID")
                .ToTable("AppUsers").PrimaryColumn("ID");

            //Role - AppUserRole Relational
            Create.ForeignKey("Role_AppUser_CompositKey")
            .FromTable("AppUserRoles").ForeignColumn("RoleID")
                .ToTable("Roles").PrimaryColumn("ID");

            //AppUser - AppUserRole Relational
            Create.ForeignKey("AppUser_AppUser_CompositKey")
            .FromTable("AppUserRoles").ForeignColumn("AppUserID")
            .ToTable("AppUsers").PrimaryColumn("ID");

            #endregion
        }
    }
}
