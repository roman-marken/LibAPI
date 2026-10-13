using AutoMapper;
using LibraryApi.Dtos;
using LibraryApi.Models;

namespace LibraryApi.Mapping;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateBookRequest, Book>();
    }
}
