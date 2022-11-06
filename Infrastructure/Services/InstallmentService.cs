using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;
public class InstallmentServices : IInstallmentService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public InstallmentServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetInstallmentDto>>> GetInstallment()
    {

        var installment = await (from ins in _context.Installments
                                 join pro in _context.Products
                                 on ins.ProductId equals pro.Id
                                 select new GetInstallmentDto()
                                 {
                                     EndInstallment = ins.EndInstallment,
                                     Id = ins.Id,
                                     Percentage = ins.Percentage,
                                     ProductId = pro.Id,
                                     ProductName = pro.ProductName,
                                     Orders = (from cu in _context.Orders
                                                          where ins.Id == cu.CustomerId
                                                          select _mapper.Map<Order>(cu)
                                                     ).ToList()

                                 }).ToListAsync();
        return new Response<List<GetInstallmentDto>>(installment);

    }

public async Task<Response<string>> AddInstallment(AddInstallmentDto model)
{
    try
    {
        var mapped = _mapper.Map<Installment>(model);
        await _context.Installments.AddAsync(mapped);
        await _context.SaveChangesAsync();
        return new Response<string>(_mapper.Map<string>("Installment Added Successfully"));
    }
    catch (Exception ex)
    {
        return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
    }
}
public async Task<Response<AddInstallmentDto>> UpdateInstallment(AddInstallmentDto model)
{
    try
    {
        var record = await _context.Installments.FindAsync(model.Id);
        if (record == null) return new Response<AddInstallmentDto>(System.Net.HttpStatusCode.NotFound, "No record found");
        await _context.SaveChangesAsync();
        return new Response<AddInstallmentDto>(model);
    }
    catch (System.Exception ex)
    {
        return new Response<AddInstallmentDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
    }
}
public async Task<Response<int>> DaleteInstallment(int id)
{
    try
    {
        var record = await _context.Installments.FindAsync(id);
        if (record == null)
            return new Response<int>(System.Net.HttpStatusCode.NotFound, "No record found");
        _context.Installments.Remove(record);
        await _context.SaveChangesAsync();
        return new Response<int>(200);
    }
    catch (System.Exception ex)
    {
        return new Response<int>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
    }
}
}