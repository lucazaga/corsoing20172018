using System;
using System.Linq;
using MongoDB.Driver;
using MongoTestApp.Entities;
using System.Collections.Generic;

namespace MongoTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a default mongo object.  This handles our connections to the database.
            //By default, this will connect to localhost, port 27017 which we already have running from earlier.
            MongoClient client = new MongoClient("mongodb://127.0.0.1");

            //Get the blog database.  If it doesn't exist, that's ok because MongoDB will create it 
            //for us when we first use it. Awesome!!!
            var db = client.GetDatabase("posts");

            //Get the Post collection.  By default, we'll use the name of the class as the collection name. Again,
            //if it doesn't exist, MongoDB will create it when we first use it.
            var collection = db.GetCollection<Post>("posts");

            var totalNumberOfPosts = collection.Count(x => true);
            Console.WriteLine("Before delete : {0}", totalNumberOfPosts);

            //this deletes everything out of the collection so we can run this over and over again.
            collection.DeleteMany(p => true);

            totalNumberOfPosts = collection.Count(x => true);
            Console.WriteLine("After delete : {0}", totalNumberOfPosts);

            //Create posts to enter into the database.
            Post post = new Post
            {
                Id = Guid.NewGuid(),
                Body = "prova",
                CharCount = 122,
                Title = "non saprei",
                Comments = new List<Comment>()
                {
                    new Comment{
                        TimePosted =new DateTime(2016,09,01),
                        Email ="luca.zagarella@intellisync.it",
                        Body="bad post",
                    },
                    new Comment{
                        TimePosted =new DateTime(2017,02,01),
                        Email ="luca.zagarella@intellisync.it",
                        Body="bla bla post",
                    },
                    new Comment() {
                        TimePosted = new DateTime(2010,1,1),
                        Email = "bob_mcbob@gmail.com",
                        Body = "This article is too short!",
                    }
                },
            };

            collection.InsertOne(post);

            Post post2 = new Post
            {
                Id = Guid.NewGuid(),
                Body = "nel mezzo del cammin",
                CharCount = 8765,
                Title = "prova2",
            };

            collection.InsertOne(post2);

            //count all the Posts
            totalNumberOfPosts = collection.Count(x => true);

            Console.WriteLine("After inserts : {0}", totalNumberOfPosts);


            //count only the Posts that have 1 comment
            var numberOfPostsWith1Comment = collection.Count(p => p.Comments.Count == 1);
            Console.WriteLine("numberOfPostsWith1Comment: {0}", numberOfPostsWith1Comment);

            //count only the Posts that have 1 comment
            var numberOfPostsWith2Comments = collection.Count(p => p.Comments.Count == 2);
            Console.WriteLine("numberOfPostsWith2Comments: {0}", numberOfPostsWith2Comments);


            var postsThatLucaCommentedOn = collection.AsQueryable().Where(p => p.Comments.Any(c => c.Email.StartsWith("luca"))).Select(x => x.Title);
            foreach (var item in postsThatLucaCommentedOn)
            {
                Console.WriteLine(item);
            }


            //find the titles and comments of the posts that have comments after January First 2017.
            var postsWithJanuary1st = collection.AsQueryable()
                                                    .Where(p => p.Comments.Any(c => c.TimePosted > new DateTime(2017, 1, 1)))
                                                    .Select(x => new { x.Title, x.Comments });



            //find posts with less than 40 characters
            var postsWithLessThan10Words = from p in collection.AsQueryable()
                                           where p.CharCount < 40
                                           select p;


            //get the total character count for all posts...
            var linqSum = (int)collection.AsQueryable().Sum(p => p.CharCount);

            //Now imagine about doing this by hand...
            var stats = from p in collection.AsQueryable()
                        where p.Comments.Any(c => c.Email.StartsWith("luca"))
                        group p by p.CharCount < 40 into g
                        select new
                        {
                            LessThan40 = g.Key,
                            Sum = g.Sum(x => x.CharCount),
                            Count = g.Count(),
                            Average = g.Average(x => x.CharCount),
                            Min = g.Min(x => x.CharCount),
                            Max = g.Max(x => x.CharCount)
                        };

            Console.WriteLine("---------------");

            foreach (var item in collection.AsQueryable().Select(x => x.CharCount))
            {
                Console.WriteLine(item);
            }

            //find the MasterID with 1130 and replace it with 1120
            var result = collection.FindOneAndUpdate<Post>(p => p.CharCount > 200, Builders<Post>.Update.Set(e => e.CharCount, 100));

            Console.WriteLine("---------------");

            foreach (var item in collection.AsQueryable().Select(x => x.CharCount))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------------");

            var postToUpdate = collection.Find(x => x.CharCount == 100).First().CharCount = 120;
            foreach (var item in collection.AsQueryable().Select(x => x.CharCount))
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
