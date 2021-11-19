using MyEvernote.BusinessLayer.Abstract;
using MyEvernote.Entities;
using System.Linq;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager : MockManagerBase<Category>
    {
        public override int Delete(Category category)
        {
            NoteManager noteManager = new NoteManager();
            LikedManager likedManager = new LikedManager();
            CommentManager commentManager = new CommentManager();

            // Kategori ile ilişkili notların silinmesi gerekiyor.
            foreach (Note note in category.Notes.ToList())
            {

                // Note ile ilişikili like'ların silinmesi.
                foreach (Liked like in note.Likes.ToList())
                {
                    likedManager.Delete(like);
                }

                // Note ile ilişkili comment'lerin silinmesi
                foreach (Comment comment in note.Comments.ToList())
                {
                    commentManager.Delete(comment);
                }

                noteManager.Delete(note);
            }

            return base.Delete(category);
        }
    }
}
