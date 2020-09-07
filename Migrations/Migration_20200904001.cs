using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(20200904001, "Create result history for check")]
    public class Migration_20200904001 : Migration
    {
        public override void Down()
        {
            Delete.Table("result_history");
        }

        public override void Up()
        {
            Create.Table("result_history")
                  .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                  .WithColumn("subscription_id").AsInt64()
                  .WithColumn("access").AsInt32()
                  .WithColumn("message").AsString()
                  .WithColumn("date").AsDateTime();
        }
    }
}