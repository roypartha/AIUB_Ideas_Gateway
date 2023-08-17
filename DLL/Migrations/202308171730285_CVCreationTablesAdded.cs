﻿namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CVCreationTablesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicQualifications",
                c => new
                    {
                        QualificationId = c.Int(nullable: false, identity: true),
                        Degree = c.String(),
                        Institution = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        CVId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QualificationId)
                .ForeignKey("dbo.CVs", t => t.CVId, cascadeDelete: true)
                .Index(t => t.CVId);
            
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        CVId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CVId)
                .ForeignKey("dbo.Users", t => t.CVId)
                .Index(t => t.CVId);
            
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        AwardId = c.Int(nullable: false, identity: true),
                        AwardName = c.String(),
                        AwardingInstitution = c.String(),
                        DateReceived = c.DateTime(nullable: false),
                        CVId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AwardId)
                .ForeignKey("dbo.CVs", t => t.CVId, cascadeDelete: true)
                .Index(t => t.CVId);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        ExperienceId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Position = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        CVId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExperienceId)
                .ForeignKey("dbo.CVs", t => t.CVId, cascadeDelete: true)
                .Index(t => t.CVId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                        Proficiency = c.String(),
                        CVId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SkillId)
                .ForeignKey("dbo.CVs", t => t.CVId, cascadeDelete: true)
                .Index(t => t.CVId);
            
            CreateTable(
                "dbo.ThesisPapers",
                c => new
                    {
                        ThesisId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PublicationDate = c.DateTime(nullable: false),
                        CoAuthors = c.String(),
                        CVId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThesisId)
                .ForeignKey("dbo.CVs", t => t.CVId, cascadeDelete: true)
                .Index(t => t.CVId);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        JobApplicationId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        AppliedOn = c.DateTime(nullable: false),
                        ApplicationStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobApplicationId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.JobId);
            
            AddColumn("dbo.Users", "CvId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CVs", "CVId", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "UserId", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.ThesisPapers", "CVId", "dbo.CVs");
            DropForeignKey("dbo.Skills", "CVId", "dbo.CVs");
            DropForeignKey("dbo.Experiences", "CVId", "dbo.CVs");
            DropForeignKey("dbo.Awards", "CVId", "dbo.CVs");
            DropForeignKey("dbo.AcademicQualifications", "CVId", "dbo.CVs");
            DropIndex("dbo.JobApplications", new[] { "JobId" });
            DropIndex("dbo.JobApplications", new[] { "UserId" });
            DropIndex("dbo.ThesisPapers", new[] { "CVId" });
            DropIndex("dbo.Skills", new[] { "CVId" });
            DropIndex("dbo.Experiences", new[] { "CVId" });
            DropIndex("dbo.Awards", new[] { "CVId" });
            DropIndex("dbo.CVs", new[] { "CVId" });
            DropIndex("dbo.AcademicQualifications", new[] { "CVId" });
            DropColumn("dbo.Users", "CvId");
            DropTable("dbo.JobApplications");
            DropTable("dbo.ThesisPapers");
            DropTable("dbo.Skills");
            DropTable("dbo.Experiences");
            DropTable("dbo.Awards");
            DropTable("dbo.CVs");
            DropTable("dbo.AcademicQualifications");
        }
    }
}
