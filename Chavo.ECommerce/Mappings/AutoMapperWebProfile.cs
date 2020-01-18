namespace Chavo.ECommerce.Mappings
{
    using Common;
    using Models;
    using System;

    public class AutoMapperWebProfile : AutoMapper.Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Register, RegisterViewModel>();

            //Reverese mapping
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<RegisterViewModel, Register>();
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