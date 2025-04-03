﻿using AutoMapper;
using Project_00.Models;

namespace Project_00.Mappings
{
    public class ProjectMapping : Profile 
    {
        public ProjectMapping()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
