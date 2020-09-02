using System;
using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(20200902, "IntialMigrate")]
    public class Migration_20200902 : Migration
    {
        public override void Down()
        {
            Delete.Table("transactions");
            Delete.Table("account");
        }

        public override void Up()
        {
            Create.Table("account")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("amount").AsDecimal();
                
            Create.Table("transactions")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("amount").AsDecimal()
                .WithColumn("descriptions").AsString().NotNullable()
                .WithColumn("date").AsDateTime()
                .WithColumn("accountId").AsInt64().ForeignKey("account", "id");

            Insert.IntoTable("account").Row(new { amount = 0 });
            Insert.IntoTable("transactions").Row(new {amount = 23, descriptions = "replenishment", date = DateTime.Now, accountId = 1});
        }
    }
}