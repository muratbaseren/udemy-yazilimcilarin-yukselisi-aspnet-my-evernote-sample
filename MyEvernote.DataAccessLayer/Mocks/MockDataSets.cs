using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyEvernote.DataAccessLayer.Mocks
{
    public static class MockDataSets
    {
        public static List<Category> Categories { get; set; }
        public static List<EvernoteUser> EvernoteUsers { get; set; }
        public static List<Note> Notes { get; set; }
        public static List<Comment> Comments { get; set; }
        public static List<Liked> Likes { get; set; }

        public static void Reset()
        {
            Categories = new List<Category>();
            EvernoteUsers = new List<EvernoteUser>();
            Notes = new List<Note>();
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }

        public static void Seed()
        {
            // Adding admin user..
            EvernoteUser admin = new EvernoteUser()
            {
                Id = 1,
                Name = "Murat",
                Surname = "Başeren",
                Email = "kadirmuratbaseren@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "muratbaseren",
                ProfileImageFilename = "user_boy.png",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "muratbaseren"
            };

            // Adding standart user..
            EvernoteUser standartUser = new EvernoteUser()
            {
                Id = 2,
                Name = "Kadir",
                Surname = "Başeren",
                Email = "muratbaseren@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "kadirbaseren",
                Password = "654321",
                ProfileImageFilename = "user_boy.png",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "muratbaseren"
            };

            MockDataSets.EvernoteUsers.Add(admin);
            MockDataSets.EvernoteUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Id = MockDataSets.EvernoteUsers.Max(x => x.Id) + 1,
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ProfileImageFilename = "user_boy.png",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    Username = $"user{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}"
                };

                MockDataSets.EvernoteUsers.Add(user);
            }

            // User list for using..
            List<EvernoteUser> userlist = MockDataSets.EvernoteUsers.ToList();

            // Adding fake categories..
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Id = i + 1,
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUsername = "muratbaseren"
                };

                MockDataSets.Categories.Add(cat);

                // Adding fake notes..
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    EvernoteUser owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                    string title = FakeData.TextData.GetSentence();
                    if (title.Length > 50)
                    {
                        title = title.Substring(0, 50);
                    }

                    int noteId = MockDataSets.Notes.Count > 0 ? MockDataSets.Notes.Max(x => x.Id) + 1 : 1;

                    Note note = new Note()
                    {
                        Id = noteId,
                        CategoryId = cat.Id,
                        Category = cat,
                        Title = title,
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername = owner.Username,
                    };

                    MockDataSets.Notes.Add(note);
                    cat.Notes.Add(note);

                    // Adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        EvernoteUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                        int commentId = MockDataSets.Comments.Count > 0 ? MockDataSets.Comments.Max(x => x.Id) + 1 : 1;

                        Comment comment = new Comment()
                        {
                            Id = commentId,
                            Note = note,
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username
                        };

                        MockDataSets.Comments.Add(comment);
                        note.Comments.Add(comment);
                    }

                    // Adding fake likes..

                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        int likeId = MockDataSets.Likes.Count > 0 ? MockDataSets.Likes.Max(x => x.Id) + 1 : 1;

                        Liked liked = new Liked()
                        {
                            Id = likeId,
                            LikedUser = userlist[m],
                            Note = note
                        };

                        MockDataSets.Likes.Add(liked);
                        note.Likes.Add(liked);
                    }

                }

            }
        }
    }
}
