using System;

namespace MongoTestApp.Entities
{
    public class Comment
    {
        public DateTime TimePosted { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }
    }
}
