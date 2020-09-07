using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(202009050004)]
    public class Migration_202009050004 : Migration
    {
        public override void Down()
        {
            Delete.Column("service_id").FromTable("result_history");
        }

        public override void Up()
        {
            Alter.Table("result_history").AddColumn("service_id").AsInt64().Nullable();
        }
    }
}