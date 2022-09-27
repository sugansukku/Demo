using AutoMapper;
using BooksDemo.Data.DataAccess;

namespace BooksDemo.Models.Mapping
{
    public  class BookMapper: Profile
    {
       public BookMapper()
        {
            CreateMap<BookEntity, Book>();
            CreateMap<BookRequest, BookEntity>()
                .ForMember(dest => dest.Id, option => option.Ignore());
            CreateMap<Book, BookEntity>();
        }
    }
}
