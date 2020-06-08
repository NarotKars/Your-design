using AutoMapper;
using DataAccess.Models;
using WEB_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<APICategory,Category>();
            CreateMap<Category,APICategory>();
            CreateMap<APIAddress, Address>();
            CreateMap<Address, APIAddress>();
            CreateMap<APIFeedbackModel, FeedbackModel>();
            CreateMap<FeedbackModel, APIFeedbackModel>();
            CreateMap<APICustomersPersonalInformation, PersonalInformation>();
            CreateMap<PersonalInformation, APICustomersPersonalInformation>();
            CreateMap<APIProduct, Product>();
            CreateMap<Product, APIProduct>();
            CreateMap<APIPostOrder, Order>();
            CreateMap<Order, APIPostOrder>();
            CreateMap<APIPostOrder, Product>();
            CreateMap<APIOrderDetails, OrderDetail>();
            CreateMap<OrderDetail, APIOrderDetails>();
            CreateMap<APIPostOrder, OrderDetail>();
            CreateMap<APICompaniesPersonalInformation, PersonalInformation>();
            CreateMap<PersonalInformation, APICompaniesPersonalInformation>();
            CreateMap<APIUpdateCompanyPersonalInformation, PersonalInformation>();
            CreateMap<PersonalInformation, APIUpdateCompanyPersonalInformation>();
            CreateMap<APIUpdateCustomerPersonalInformation, PersonalInformation>();
            CreateMap<PersonalInformation, APIUpdateCustomerPersonalInformation>();
            CreateMap<APIProductDetails, Product>();
            CreateMap<Product, APIProductDetails>();
            CreateMap<APIUpdateStatusOfProductInOrder, OrderDetail>();
            CreateMap<OrderDetail, APIUpdateStatusOfProductInOrder>();
            CreateMap<APIOrder, Order>();
            CreateMap<Order, APIOrder>();
            CreateMap<APIUpdateOrderStatus, Order>();
            CreateMap<Order, APIUpdateOrderStatus>();
            CreateMap<OrderDetailsCompany, ApiOrderDetailsCompany>();
            CreateMap<ApiOrderDetailsCompany, OrderDetailsCompany>();
            CreateMap<APIUser, User>();
            CreateMap<User, APIUser>();
        }
    }
}
