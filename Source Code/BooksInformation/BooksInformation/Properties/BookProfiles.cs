using System;
using AutoMapper;

namespace BooksInformation.Properties
{
    public class BookProfiles : Profile
    {
        public BookProfiles()
        {
            CreateMap<Models.BookCreation, Models.Book>()
                .ForMember(dest => dest.ID, src => src.MapFrom( g => Guid.NewGuid().ToString()));
        }
    }
}

