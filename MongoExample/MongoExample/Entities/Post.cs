using System;
using System.Collections.Generic;

namespace MongoTestApp.Entities
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int CharCount { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}