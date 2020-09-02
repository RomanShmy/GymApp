using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(20200902, "IntialMigrate")]
    public class Migration_20200902 : Migration
    {
        public override void Down()
        {
            Delete.Table("Transactions");
            Delete.Table("Account");
        }

        public override void Up()
        {
            Create.Table("Account").InSchema("gym")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Amount").AsDecimal();
                
            Create.Table("Transactions").InSchema("gym")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("Amount").AsDecimal()
                .WithColumn("Descriptions").AsString().NotNullable()
                .WithColumn("Date").AsDateTime()
                .WithColumn("AccountId").AsInt64().ForeignKey("Account", "Id");
        }
    }
}