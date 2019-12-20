namespace Chavo.Web.Mappings
{
    using Data.Entity;
    using Models;
    using System;

    public class AutoMapperWebProfile : AutoMapper.Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.BirthDateString, opt => opt.MapFrom(src => src.BirthDate != null ? src.BirthDate.Value.ToString("dd/MM/yyyy") : string.Empty));

            CreateMap<Functionary, FunctionaryViewModel>()
              .ForMember(dest => dest.BirthDateString, opt => opt.MapFrom(src => src.BirthDate != null ? src.BirthDate.Value.ToString("dd/MM/yyyy") : string.Empty));

            CreateMap<Category, CategoryViewModel>();
            CreateMap<SubCategory, SubCategoryViewModel>();
            CreateMap<Product, ProductViewModel>();


            //Reverese mapping
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<FunctionaryViewModel, Functionary>();
            CreateMap<CategoryViewModel, Category>();
            CreateMap<SubCategoryViewModel, SubCategory>();
            CreateMap<ProductViewModel, Product>();

        }
        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutoMapperWebProfile>();
            });
        }

    }
    public static class ExtensionMethod
    {
        public static string Encrypt(this Int32 num)
        {
            return "Technotips:" + num;
        }
    }
}