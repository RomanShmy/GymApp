using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(20200904, "Add column for subscription")]
    public class Migration_20200904 : Migration
    {
        public override void Down()
        {
            Delete.Column("coverage").FromTable("subscription");
        }

        public override void Up()
        {
            Alter.Table("subscription").AddColumn("coverage").AsInt32().Nullable();
        }
    }
}