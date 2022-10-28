using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionsProject.Migrations
{
    public partial class AddFullTextSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE collections ADD FULLTEXT(Name,Description)",true);
            migrationBuilder.Sql("ALTER TABLE items ADD FULLTEXT(Name)", true);
            migrationBuilder.Sql("ALTER TABLE tags ADD FULLTEXT(TagName)", true);
            migrationBuilder.Sql("ALTER TABLE comments ADD FULLTEXT(CommentText)", true);
            migrationBuilder.Sql("ALTER TABLE additemfields ADD FULLTEXT(Value)", true);
            migrationBuilder.Sql("ALTER TABLE addcollectionfields ADD FULLTEXT(Name)", true);
        }
    }
}
