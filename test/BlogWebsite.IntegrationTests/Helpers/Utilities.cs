using System;
using BlogWebsite.Common.Models;

namespace BlogWebsite.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(DatabaseContext db)
        {
            db.Articles.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(DatabaseContext db)
        {
            db.Articles.RemoveRange(db.Articles);
            InitializeDbForTests(db);
        }

        public static List<Article> GetSeedingMessages()
        {
            return new List<Article>()
            {
                new Article(){ ArticleId = 1, Title = "Test 1", Description = "Test 1", ThumbnailId = Guid.Parse("ac06e599-5851-40ae-ad50-7816c0fb4fa9"), ContentId = Guid.Parse("dcd92ca2-e272-47a8-806a-0d0e5aca631c"), Published = true },
                new Article(){ ArticleId = 2, Title = "Test 2", Description = "Test 2", ThumbnailId = Guid.Parse("865264a6-e662-4222-a75b-ddbe2946f76a"), ContentId = Guid.Parse("3351b5f7-5a12-4b5f-a1cb-8516ddc8bade"), Published = false},
            };
        }
    }
}
