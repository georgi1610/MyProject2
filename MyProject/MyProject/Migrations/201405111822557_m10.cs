namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transports", "TransportCompanyId_TransportCompanyId", "dbo.TransportCompanies");
            DropIndex("dbo.Transports", new[] { "TransportCompanyId_TransportCompanyId" });
            AddColumn("dbo.Transports", "TransportCompId", c => c.Int(nullable: false));
            AddColumn("dbo.Transports", "TransportComp_TransportCompanyId", c => c.Int());
            CreateIndex("dbo.Transports", "TransportComp_TransportCompanyId");
            AddForeignKey("dbo.Transports", "TransportComp_TransportCompanyId", "dbo.TransportCompanies", "TransportCompanyId");
            DropColumn("dbo.Transports", "TransportCompanyId_TransportCompanyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transports", "TransportCompanyId_TransportCompanyId", c => c.Int());
            DropForeignKey("dbo.Transports", "TransportComp_TransportCompanyId", "dbo.TransportCompanies");
            DropIndex("dbo.Transports", new[] { "TransportComp_TransportCompanyId" });
            DropColumn("dbo.Transports", "TransportComp_TransportCompanyId");
            DropColumn("dbo.Transports", "TransportCompId");
            CreateIndex("dbo.Transports", "TransportCompanyId_TransportCompanyId");
            AddForeignKey("dbo.Transports", "TransportCompanyId_TransportCompanyId", "dbo.TransportCompanies", "TransportCompanyId");
        }
    }
}
