using Domain.Dtos;
using Domain.Response;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstallmentController : Controller
{
        private readonly IInstallmentService _installmentService;
        public InstallmentController(IInstallmentService installmentService)
        {
            _installmentService = installmentService;
        }

        [HttpGet]
        public async Task<Response<List<GetInstallmentDto>>> GetInstallments()
        {
            return await _installmentService.GetInstallment();
        }

        [HttpPost]
        public async Task<Response<AddInstallmentDto>> AddInstallment(AddInstallmentDto installmentDto)
        {
            return await _installmentService.AddInstallment(installmentDto);
        }

        [HttpPut]
        public async Task<Response<AddInstallmentDto>> UpdateInstallment(AddInstallmentDto installmentDto)
        {
            return await _installmentService.UpdateInstallment(installmentDto);
        }

        [HttpDelete]
        public async Task<Response<bool>> DeleteInstallment(int id)
        {
            return await _installmentService.DaleteInstallment(id);
        }
}
