using Domain.Dtos;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IInstallmentService
{
    Task<Response<int>> DaleteInstallment(int id);
    Task<Response<AddInstallmentDto>> UpdateInstallment(AddInstallmentDto model);
    Task<Response<string>> AddInstallment(AddInstallmentDto model);
    Task<Response<List<GetInstallmentDto>>> GetInstallment();
}