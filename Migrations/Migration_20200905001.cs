using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(20200905001)]
    public class Migration_20200905001 : Migration
    {
        public override void Down()
        {
            Delete.Table("services");
        }

        public override void Up()
        {
            Create.Table("services")
                  .WithColumn("id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("name").AsString().Unique()
                  .WithColumn("price").AsDecimal();
        }
    }
}