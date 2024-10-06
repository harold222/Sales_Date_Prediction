using AutoMapper;
using Backend.Application.Functions.Vm;
using Backend.Domain;

namespace Backend.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employees, EmployeeVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.empid))
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.lastname))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.firstname))
            .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.title))
            .ForMember(dest => dest.Cortesia, opt => opt.MapFrom(src => src.titleofcourtesy))
            .ForMember(dest => dest.Nacimiento, opt => opt.MapFrom(src => src.birthdate))
            .ForMember(dest => dest.Contratacion, opt => opt.MapFrom(src => src.hiredate))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.address))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.city))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.region))
            .ForMember(dest => dest.CodigoPotal, opt => opt.MapFrom(src => src.postalcode))
            .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.country))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.phone))
            .ForMember(dest => dest.Mgrid, opt => opt.MapFrom(src => src.mgrid))
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.FullName));

        CreateMap<Employees, ListEmployeesVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.empid))
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.FullName));

        CreateMap<Orders, OrdersVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.orderid))
            .ForMember(dest => dest.FechaRequerida, opt => opt.MapFrom(src => src.requireddate))
            .ForMember(dest => dest.FechaCompra, opt => opt.MapFrom(src => src.shippeddate))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.shipname))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.shipaddress))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.shipcity))
            .ForMember(dest => dest.IdEmpleado, opt => opt.MapFrom(src => src.empid))
            .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.custid))
            .ForMember(dest => dest.FechaOrden, opt => opt.MapFrom(src => src.orderdate))
            .ForMember(dest => dest.IdCompra, opt => opt.MapFrom(src => src.shipperid))
            .ForMember(dest => dest.Transporte, opt => opt.MapFrom(src => src.freight))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.shipregion))
            .ForMember(dest => dest.CodigoPostal, opt => opt.MapFrom(src => src.shippostalcode))
            .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.shipcountry));


        CreateMap<Orders, OrdersByClientVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.orderid))
            .ForMember(dest => dest.FechaRequerida, opt => opt.MapFrom(src => src.requireddate))
            .ForMember(dest => dest.FechaCompra, opt => opt.MapFrom(src => src.shippeddate))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.shipname))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.shipaddress))
            .ForMember(dest => dest.Ciudad, opt => opt.MapFrom(src => src.shipcity));

        CreateMap<Shippers, ShippersVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.shipperid))
            .ForMember(dest => dest.NombreEmpresa, opt => opt.MapFrom(src => src.companyname));

        CreateMap<Products, ProductsVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.productid))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.productname));

        CreateMap<NextOrder, NextOrderVM>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.custid))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.UltimaCompra, opt => opt.MapFrom(src => src.LastOrderDate))
            .ForMember(dest => dest.Prediccion, opt => opt.MapFrom(src => src.NextPredictedOrder));
    }
}
