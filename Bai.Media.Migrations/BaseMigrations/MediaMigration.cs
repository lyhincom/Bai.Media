using Bai.General.Migrations.BaseMigrations;

namespace Bai.Media.Migrations.BaseMigrations
{
    public abstract class MediaMigration : BaseMigration
    {
        protected override string DefaultPath => @".\..\Bai.Media.Migrations\MigrationScripts";

        public MediaMigration(bool updateSchema = true, bool updateData = true, bool enableRollback = true, bool enableDebug = false)
            : base(updateSchema, updateData, enableRollback, enableDebug)
        {
        }
    }
}
